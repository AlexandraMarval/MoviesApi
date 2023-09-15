using Microsoft.EntityFrameworkCore;
using MoviesApi.Entidades;

namespace MoviesApi
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }   
        public DbSet<Movies> Movies { get; set; }
    }
}
