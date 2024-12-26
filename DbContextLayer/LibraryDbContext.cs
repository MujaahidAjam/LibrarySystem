using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Models
{
    public class LibraryDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Checkout> Checkouts { get; set; }
        public DbSet<Librarian> Librarians { get; set; }
        public DbSet<Member> Members { get; set; }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "J.K. Rowling" },
                new Author { Id = 2, Name = "George Orwell" }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Harry Potter", AuthorId = 1, IsAvailable = true },
                new Book { Id = 2, Title = "1984", AuthorId = 2, IsAvailable = true }
            );

            modelBuilder.Entity<Member>().HasData(
                new Member { MemberId = 1, MemberName = "John Doe" },
                new Member { MemberId = 2, MemberName = "Jane Smith" }
            );

            modelBuilder.Entity<Librarian>().HasData(
                new Librarian { LibrarianId = 1, LibrarianName = "Alice" },
                new Librarian { LibrarianId = 2, LibrarianName = "Bob" }
            );

            modelBuilder.Entity<Checkout>().HasData(
                new Checkout { Id = 1, BookId = 1, MemberId = 1, CheckoutDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14) },
                new Checkout { Id = 2, BookId = 2, MemberId = 2, CheckoutDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14) }
            );
            Console.WriteLine("Seed data applied.");
        }
    }
}
