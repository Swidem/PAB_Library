using Kiosk.WebAPI.Db.Models;
using Kiosk.WebAPI.Models;

namespace Kiosk.WebAPI.Persistance
{
    public class DataSeeder
    {
        private readonly LibraryDbContext _dbContext;

        public DataSeeder(LibraryDbContext context)
        {
            _dbContext = context;
        }

        public void Seed()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();

            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Books.Any())
                {
                    var books = new List<Book>
                    {
                        new Book
                        {
                            BookId = 1,
                            Title = "1984",
                            Author = "George Orwell",
                            ISBN = "123456789",
                            StatusOfBook = StatusOfBook.Available
                        },
                        new Book
                        {
                            BookId = 2,
                            Title = "Pan Tadeusz",
                            Author = "Adam Mickiewicz",
                            ISBN = "987654321",
                            StatusOfBook = StatusOfBook.Available
                        }
                    };
                    _dbContext.Books.AddRange(books);
                }

                if (!_dbContext.Clients.Any())
                {
                    var clients = new List<Client>
                    {
                        new Client
                        {
                            ClientId = 1,
                            FirstName = "Jan",
                            LastName = "D",
                            Email = "john.d@example.com"
                        },
                        new Client
                        {
                            ClientId = 2,
                            FirstName = "Joanna",
                            LastName = "",
                            Email = "jane.s@example.com"
                        }
                    };
                    _dbContext.Clients.AddRange(clients);
                }

                if (!_dbContext.Employees.Any())
                {
                    var employees = new List<Employee>
                    {
                        new Employee
                        {
                            EmployeeId = 1,
                            FirstName = "Alicja",
                            LastName = "Nowak",
                            Email = "alicja.nowak@example.com"
                        },
                        new Employee
                        {
                            EmployeeId = 2,
                            FirstName = "Bob",
                            LastName = "Brown",
                            Email = "bob.brown@example.com"
                        }
                    };
                    _dbContext.Employees.AddRange(employees);
                }

                if (!_dbContext.Loans.Any())
                {
                    var loans = new List<Loan>
                    {
                        new Loan
                        {
                            LoanId = 1,
                            ClientId = 1,
                            BookId = 1,
                            EmployeeId = 1,
                            LoanDate = DateTime.Now.AddDays(-3),
                            DueDate = DateTime.Now.AddDays(14),
                            LoanStatus = LoanStatus.Active
                        }
                    };
                    _dbContext.Loans.AddRange(loans);
                }

                _dbContext.SaveChanges();
            }
        }
    }
}