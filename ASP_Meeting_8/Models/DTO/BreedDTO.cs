using ASP_Meeting_8.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ASP_Meeting_8.Models.DTO
{
    public class BreedDTO
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Cat's Breed")]
        public string BreedName { get; set; } = default!;

        public ICollection<CatDTO> Cats { get; set; } = default!;
    }
}
