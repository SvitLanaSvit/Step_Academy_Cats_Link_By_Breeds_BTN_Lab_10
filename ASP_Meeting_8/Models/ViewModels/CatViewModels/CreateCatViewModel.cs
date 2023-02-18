using ASP_Meeting_8.Models.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASP_Meeting_8.Models.ViewModels.CatViewModels
{
    public class CreateCatViewModel
    {
        public CatDTO Cat { get; set; } = default!;

        public SelectList? BreedSL { get; set; } = default!;

        public IFormFile Image { get; set; } = default!;

        //public SelectList CatGenderSL { get; set; } = default!;
    }
}
