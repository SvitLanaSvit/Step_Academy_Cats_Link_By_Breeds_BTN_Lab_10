using System.ComponentModel.DataAnnotations;

namespace ASP_Meeting_8.Data.Entities
{
    public class Breed
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Cat's Breed")]
        public string BreedName { get; set; } = default!;

        public ICollection<Cat> Cats { get; set; } = default!;
    }
}
