using LibrarySystem.Models;
using LibrarySystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService _libraryService;

        public LibraryController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        /// <summary>
        /// Get books by a specific author.
        /// </summary>
        [HttpGet("books/author/{authorId}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksByAuthor(int authorId)
        {
            if (authorId <= 0) return BadRequest("Invalid author ID.");

            var books = await _libraryService.GetBooksByAuthorAsync(authorId);
            if (books.Any()) return Ok(books);

            return NotFound("No books found for the specified author.");
        }

        /// <summary>
        /// Checkout a book for a member.
        /// </summary>
        [HttpPost("checkout")]
        public async Task<IActionResult> CheckoutBook(int memberId, int bookId)
        {
            if (memberId <= 0 || bookId <= 0) return BadRequest("Invalid member or book ID.");

            var success = await _libraryService.CheckoutBookAsync(memberId, bookId);
            return success ? Ok("Checkout successful.") : BadRequest("Failed to checkout book.");
        }

        /// <summary>
        /// Return a checked-out book.
        /// </summary>
        [HttpPost("return/{checkoutId}")]
        public async Task<IActionResult> ReturnBook(int checkoutId)
        {
            if (checkoutId <= 0) return BadRequest("Invalid checkout ID.");

            var success = await _libraryService.ReturnBookAsync(checkoutId);
            return success ? Ok("Return successful.") : NotFound("Failed to return book.");
        }

        /// <summary>
        /// Get a list of overdue books.
        /// </summary>
        [HttpGet("overdue")]
        public async Task<ActionResult<IEnumerable<Checkout>>> GetOverdueBooks()
        {
            var overdueBooks = await _libraryService.GetOverdueBooksAsync();
            if (overdueBooks.Any()) return Ok(overdueBooks);

            return NotFound("No overdue books found.");
        }
    }
}
