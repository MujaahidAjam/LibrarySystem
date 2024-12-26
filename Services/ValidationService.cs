using LibrarySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Services
{
    public class ValidationService
    {
        private readonly LibraryDbContext _dbContext;

        public ValidationService(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CanMemberCheckoutBookAsync(int memberId)
        {
            var member = await _dbContext.Members
                .Include(m => m.Checkouts)
                .FirstOrDefaultAsync(m => m.MemberId == memberId);

            return member != null && member.Checkouts.Count(c => c.ReturnDate == null) < 5;
        }

        public async Task<bool> IsBookAvailableAsync(int bookId)
        {
            var book = await _dbContext.Books.FindAsync(bookId);
            return book != null && book.IsAvailable;
        }
    }
}
