using LibrarySystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibrarySystem.Services
{
    public interface ILibraryService
    {
        Task<List<Book>> GetBooksByAuthorAsync(int authorId);
        Task<bool> CheckoutBookAsync(int memberId, int bookId);
        Task<bool> ReturnBookAsync(int checkoutId);
        Task<List<Checkout>> GetOverdueBooksAsync();
        Task<bool> MarkBookAsReturnedAsync(int checkoutId);
        Task<List<Checkout>> GetOverdueBooksManagedByLibrarianAsync(int librarianId);
        Task<bool> AssignBookToLibrarianAsync(int librarianId, int bookId);
    }
}
