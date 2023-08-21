using Microsoft.EntityFrameworkCore;
using ZContactos.Models;

namespace ZContactos.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options ) : base (options)
        {
                
        }

        public DbSet<Usuario> Usuario { get; set; }
    }
}
