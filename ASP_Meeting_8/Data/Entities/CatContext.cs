using Microsoft.EntityFrameworkCore;

namespace ASP_Meeting_8.Data.Entities
{
    public class CatContext :DbContext
    {
        public DbSet<Breed> Breeds { get; set; } = default!;
        public DbSet<Cat> Cats  =>Set<Cat>();

        public CatContext(DbContextOptions<CatContext> options) : base(options){}
    }
}
