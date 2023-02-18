using System.ComponentModel.DataAnnotations;

namespace ASP_Meeting_8.Data.Entities
{
    public class Cat
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Необхідно вказати  ім'я кота!")]
        [Display(Name ="Cat's Name")]
        public string CatName { get; set; } = default!;

        public string? Description { get; set; }
        [Required]
        public string Gender { get; set; } = default!;
        [Display(Name = "Status of Vacination")]
        public bool IsVacinated { get; set; }

        public byte[]? Image { get; set; }

        public bool IsDeleted { get; set; }

        public Breed? Breed { get; set; }

        public int BreedId { get; set; }
    }

    
}
