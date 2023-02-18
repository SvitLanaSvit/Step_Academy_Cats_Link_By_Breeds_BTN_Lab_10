using ASP_Meeting_8.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ASP_Meeting_8.Models.DTO
{
    //DTO - Data Transfer Object
    public class CatDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Необхідно вказати  ім'я кота!")]
        [Display(Name = "Cat's Name")]
        public string CatName { get; set; } = default!;

        public string? Description { get; set; }
        [Required]
        public CatGender Gender { get; set; }
        [Display(Name = "Status of Vacination")]
        public bool IsVacinated { get; set; }

        public byte[]? Image { get; set; }

        public BreedDTO? Breed { get; set; }

        public int BreedId { get; set; }
    }
}
