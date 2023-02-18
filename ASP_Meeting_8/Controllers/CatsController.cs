using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASP_Meeting_8.Data.Entities;
using ASP_Meeting_8.Models.ViewModels.CatViewModels;
using ASP_Meeting_8.Models.DTO;
using AutoMapper;

namespace ASP_Meeting_8.Controllers
{
    public class CatsController : Controller
    {
        private readonly CatContext _context;
        private readonly IMapper mapper;
        private readonly ILogger _logger;

        public CatsController(CatContext context, 
            ILoggerFactory loggerFactory,
            IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
            _logger = loggerFactory.CreateLogger<CatsController>();
        }

        // GET: Cats
        public async Task<IActionResult> Index(int breedId, string? search)
        {
            
            IQueryable<Cat> cats = _context.Cats
                .Include(c => c.Breed)
                .Where(t=>t.IsDeleted == false);
            if (breedId > 0)
                cats = cats.Where(t => t.BreedId == breedId);
            if(search is not null)
                cats = cats.Where(t=>t.CatName.Contains(search));
            IQueryable<Breed> breeds = _context.Breeds;
            SelectList breedSL = new SelectList(await breeds.ToListAsync(),
                dataValueField: nameof(Breed.Id),
                dataTextField: nameof(Breed.BreedName),
                selectedValue: breedId);
            IEnumerable<CatDTO> tempCats = mapper.Map<IEnumerable<CatDTO>>(await cats.ToListAsync());
          
            IndexCatViewModel vM = new IndexCatViewModel
            {
                Cats = tempCats,
                BreedSL = breedSL,
                BreedId = breedId,
                Search = search
            };
            return View(vM);
        }

        public async Task<IActionResult> Search(int breedId)
        {
            IQueryable<Cat> cats = _context.Cats
                .Include(c => c.Breed)
                .Where(t => t.IsDeleted == false);
            if (breedId > 0)
                cats = cats.Where(t => t.BreedId == breedId);
            IQueryable<Breed> breeds = _context.Breeds;
            SelectList breedSL = new SelectList(
                await breeds.ToArrayAsync(),
                dataValueField: nameof(Breed.Id),
                dataTextField: nameof(Breed.BreedName),
                selectedValue: breedId);
            IEnumerable<CatDTO> tempCats = mapper.Map<IEnumerable<CatDTO>>(await cats.ToListAsync());

            IndexCatViewModel vM = new IndexCatViewModel
            {
                Cats = tempCats,
                BreedSL = breedSL,
                BreedId = breedId
            };
            return View(vM);
        }

        //[Route("List")]
        //[Route("/")]
        //[Route("List/{breedName}")]
        public async Task<IActionResult> List(string breedName)
        {
            IQueryable<Cat> cats = _context.Cats
                .Include(c => c.Breed)
                .Where(t => t.IsDeleted == false);
            if (breedName is not null)
                cats = cats.Where(t => t.Breed!.BreedName == breedName);
            IQueryable<Breed> breeds = _context.Breeds;
            var cat = breeds.FirstOrDefault(t => t.BreedName == breedName);
            int breedId = 0;
            if(cat != null)
            {
                breedId = cat!.Id;
            }
            SelectList breedSL = new SelectList(
                await breeds.ToArrayAsync(),
                dataValueField: nameof(Breed.Id),
                dataTextField: nameof(Breed.BreedName),
                selectedValue: breedId);
            IEnumerable<CatDTO> tempCats = mapper.Map<IEnumerable<CatDTO>>(await cats.ToListAsync());

            IndexCatViewModel vM = new IndexCatViewModel
            {
                Cats = tempCats,
                BreedSL = breedSL,
                BreedId = breedId
            };
            return View(vM);
        }

        // GET: Cats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cats == null)
            {
                return NotFound();
            }

            var cat = await _context.Cats
                .Include(c => c.Breed)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cat == null)
            {
                return NotFound();
            }
            DetailsCatViewModel vM = new DetailsCatViewModel
            {
                Cat = mapper.Map<CatDTO>(cat)
            };
            return View(vM);
        }

        // GET: Cats/Create
        public IActionResult Create()
        {
            CreateCatViewModel vM = new CreateCatViewModel
            {
                BreedSL = new SelectList(_context.Breeds, "Id", "BreedName")
            };
            return View(vM);
        }

        // POST: Cats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCatViewModel vM)
        {
            if (!ModelState.IsValid)
            {
                
                SelectList breedSL = new SelectList(await _context.Breeds.ToListAsync(),
                    nameof(Breed.Id),
                    nameof(Breed.BreedName),
                    vM.Cat.BreedId);
                vM.BreedSL = breedSL;
                foreach (var error in ModelState.Values.SelectMany(t=>t.Errors))
                {
                    _logger.LogError(error.ErrorMessage);
                }
                return View(vM);
            }
            
            using(BinaryReader br = new BinaryReader(vM.Image.OpenReadStream()))
            {
                vM.Cat.Image = br.ReadBytes((int)vM.Image.Length);
            }
            Cat createdCat = mapper.Map<Cat>(vM.Cat);
            _context.Add(createdCat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

        // GET: Cats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            int idForDemonstration =  Convert.ToInt32(RouteData.Values["id"]!.ToString());
            if (id == null || _context.Cats == null)
            {
                return NotFound();
            }

            var cat = await _context.Cats.FindAsync(id);
            if (cat == null)
            {
                return NotFound();
            }
            if(cat.IsDeleted)
                return Unauthorized();
            IQueryable<Breed> breeds = _context.Breeds;
            SelectList breedSL = new SelectList(await breeds.ToListAsync(),
                nameof(Breed.Id),
                nameof(Breed.BreedName),
                selectedValue: cat.BreedId);
            EditCatViewModel vM = new EditCatViewModel
            {
                BreedSL = breedSL,
                Cat = mapper.Map<CatDTO>(cat)
            };
            return View(vM);
        }

        // POST: Cats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditCatViewModel vM)
        {
            if (id != vM.Cat.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(t => t.Errors))
                    _logger.LogError(error.ErrorMessage);
                IQueryable<Breed> breeds = _context.Breeds;
                SelectList breedSL = new SelectList(await breeds.ToListAsync(),
                    nameof(Breed.Id),
                    nameof(Breed.BreedName),
                    selectedValue: vM.Cat.BreedId);
                vM.BreedSL = breedSL;
                return View(vM);
            }
            try
            {
                if(vM.Image is not null)
                {
                    using (BinaryReader br = new BinaryReader(vM.Image.OpenReadStream()))
                    {
                        vM.Cat.Image = br.ReadBytes((int)vM.Image.Length);
                    }
                }
                Cat editedCat = mapper.Map<Cat>(vM.Cat);
                _context.Update(editedCat);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatExists(vM.Cat.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
            
        }

        // GET: Cats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cats == null)
            {
                return NotFound();
            }
            var cat = await _context.Cats
                .Include(c => c.Breed)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cat == null)
            {
                return NotFound();
            }
            DeleteCatViewModel vM = new DeleteCatViewModel
            {
                Cat = mapper.Map<CatDTO>(cat)
            };
            return View(vM);
        }

        // POST: Cats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cats == null)
            {
                return Problem("Entity set 'CatContext.Cats'  is null.");
            }
            var cat = await _context.Cats.FindAsync(id);
            if (cat != null)
            {
                //_context.Cats.Remove(cat);
                cat.IsDeleted = true;
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatExists(int id)
        {
          return _context.Cats.Any(e => e.Id == id);
        }
    }
}