using ASP_Meeting_8.Data.Entities;
using ASP_Meeting_8.Models.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASP_Meeting_8.Models.ViewModels.CatViewModels
{
    public class IndexCatViewModel
    {
        public IEnumerable<CatDTO> Cats { get; set; } = default!;

        public SelectList BreedSL { get; set; } = default!;

        public int BreedId { get; set; }

        public string? Search { get; set; }
    }
}
