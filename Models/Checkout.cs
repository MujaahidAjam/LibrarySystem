using System.Diagnostics.Metrics;

namespace LibrarySystem.Models
{
    public class Checkout
    {
        public int Id { get; set; } // Primary Key
        public int BookId { get; set; } // Foreign Key to Book
        public int MemberId { get; set; } // Foreign Key to Member
        public DateTime CheckoutDate { get; set; } // Date when the book was checked out
        public DateTime DueDate { get; set; } // Due date for return
        public DateTime? ReturnDate { get; set; } // Date when the book was returned

        // Navigation Properties
        public Book Book { get; set; }
        public Member Member { get; set; }
    }
}
