using Microsoft.EntityFrameworkCore;
 
namespace LizardPirates.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }
        
        // this is the variable we will use to connect to the MySQL table, Lizards
        public DbSet<Lizard> Lizards { get; set; }
    }
}