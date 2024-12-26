using LibrarySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly LibraryDbContext _dbContext;

        public LibraryService(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Book>> GetBooksByAuthorAsync(int authorId)
        {
            return await _dbContext.Books
                .Where(b => b.AuthorId == authorId)
                .Include(b => b.Author)
                .ToListAsync();
        }

        public async Task<bool> CheckoutBookAsync(int memberId, int bookId)
        {
            // Ensure member is eligible for checkout
            var member = await _dbContext.Members
                .Include(m => m.Checkouts)
                .FirstOrDefaultAsync(m => m.MemberId == memberId);

            if (member == null || member.Checkouts.Count(c => c.ReturnDate == null) >= 5)
                return false;

            // Ensure book is available
            var book = await _dbContext.Books.FindAsync(bookId);
            if (book == null || !book.IsAvailable)
                return false;

            // Proceed with checkout
            book.IsAvailable = false;
            var checkout = new Checkout
            {
                MemberId = memberId,
                BookId = bookId,
                CheckoutDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(21)
            };

            _dbContext.Checkouts.Add(checkout);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ReturnBookAsync(int checkoutId)
        {
            var checkout = await _dbContext.Checkouts
                .Include(c => c.Book)
                .FirstOrDefaultAsync(c => c.Id == checkoutId);

            if (checkout == null || checkout.ReturnDate != null)
                return false;

            // Mark the book as returned
            checkout.ReturnDate = DateTime.Now;
            checkout.Book.IsAvailable = true;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Checkout>> GetOverdueBooksAsync()
        {
            return await _dbContext.Checkouts
                .Include(c => c.Book)
                .Include(c => c.Member)
                .Where(c => c.ReturnDate == null && c.DueDate < DateTime.Now)
                .ToListAsync();
        }

        public async Task<bool> MarkBookAsReturnedAsync(int checkoutId)
        {
            var checkout = await _dbContext.Checkouts
                .Include(c => c.Book)
                .FirstOrDefaultAsync(c => c.Id == checkoutId);

            if (checkout == null || checkout.ReturnDate != null)
                return false;

            checkout.ReturnDate = DateTime.Now;
            checkout.Book.IsAvailable = true;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Checkout>> GetOverdueBooksManagedByLibrarianAsync(int librarianId)
        {
            var librarian = await _dbContext.Librarians
                .Include(l => l.ManagedBooks)
                .ThenInclude(b => b.Checkouts)
                .FirstOrDefaultAsync(l => l.LibrarianId == librarianId);

            if (librarian == null) return new List<Checkout>();

            return librarian.ManagedBooks
                .SelectMany(b => b.Checkouts)
                .Where(c => c.ReturnDate == null && c.DueDate < DateTime.Now)
                .ToList();
        }

        public async Task<bool> AssignBookToLibrarianAsync(int librarianId, int bookId)
        {
            var librarian = await _dbContext.Librarians.Include(l => l.ManagedBooks).FirstOrDefaultAsync(l => l.LibrarianId == librarianId);
            var book = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == bookId);

            if (librarian == null || book == null)
                return false;

            librarian.ManagedBooks.Add(book);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
