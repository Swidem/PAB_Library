using Kiosk.WebAPI.Db.Models;
using Kiosk.WebAPI.Models;

namespace Kiosk.WebAPI.Db.Persistance
{
    public class LibraryStore
    {
        private static List<Book> Books { get; set; } = new List<Book>
        {
            new Book { BookId = 1, Title = "1984", Author = "George Orwell", ISBN = "123456789", StatusOfBook = StatusOfBook.Available },
            new Book { BookId = 2, Title = "Pan Tadeusz", Author = "Adam Mickiewicz", ISBN = "987654321", StatusOfBook = StatusOfBook.Available }
        };

        private static List<Client> Clients { get; set; } = new List<Client>
        {
            new Client { ClientId = 1, FirstName = "Jan", LastName = "D", Email = "jan.d@example.com" },
            new Client { ClientId = 2, FirstName = "Joanna", LastName = "S", Email = "joanna.s@example.com" }
        };

        private static List<Employee> Employees { get; set; } = new List<Employee>
        {
            new Employee { EmployeeId = 1, FirstName = "Alicja", LastName = "Nowak", Email = "alicja.nowak@example.com" },
            new Employee { EmployeeId = 2, FirstName = "Bob", LastName = "Brown", Email = "bob.brown@example.com" }
        };

        private static List<Loan> Loans { get; set; } = new List<Loan>
        {
            new Loan { LoanId = 1, ClientId = 1, BookId = 1, EmployeeId = 1, LoanDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14), LoanStatus = LoanStatus.Active }
        };

        public static List<Book> GetAllBooks() => Books;
        public static void AddBook(Book book) => Books.Add(book);
        public static void RemoveBook(int bookId)
        {
            var book = Books.FirstOrDefault(b => b.BookId == bookId);
            if (book != null)
            {
                Books.Remove(book);
            }
        }

        public static List<Client> GetAllClients() => Clients;
        public static void AddClient(Client client) => Clients.Add(client);
        public static void RemoveClient(int clientId)
        {
            var client = Clients.FirstOrDefault(c => c.ClientId == clientId);
            if (client != null)
            {
                Clients.Remove(client);
            }
        }

        public static List<Employee> GetAllEmployees() => Employees;
        public static void AddEmployee(Employee employee) => Employees.Add(employee);
        public static void RemoveEmployee(int employeeId)
        {
            var employee = Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (employee != null)
            {
                Employees.Remove(employee);
            }
        }

        public static List<Loan> GetAllLoans() => Loans;
        public static void AddLoan(Loan loan) => Loans.Add(loan);
        public static void RemoveLoan(int loanId)
        {
            var loan = Loans.FirstOrDefault(l => l.LoanId == loanId);
            if (loan != null)
            {
                Loans.Remove(loan);
            }
        }
    }
}