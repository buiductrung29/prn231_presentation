using Microsoft.EntityFrameworkCore;
using JWT_Sample.Models;
namespace JWT_Sample.DAO
{
    public class DBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("SampleDB");
        }
        public DbSet<Author>? Authors { get; set; }
        public DbSet<Book>? Books { get; set; }
    }
}