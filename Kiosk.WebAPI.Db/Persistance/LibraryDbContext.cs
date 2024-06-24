using Microsoft.EntityFrameworkCore;
using Kiosk.WebAPI.Models;
using Kiosk.WebAPI.Db.Models;

namespace Kiosk.WebAPI.Persistance
{
    public class LibraryDbContext : DbContext
    {

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        { }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
