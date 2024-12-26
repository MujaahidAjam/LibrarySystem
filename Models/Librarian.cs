namespace LibrarySystem.Models
{
    public class Librarian
    {
        public int LibrarianId { get; set; }
        public string LibrarianName { get; set; }
        public ICollection<Book> ManagedBooks { get; set; } // Books the librarian manages
    }
}
