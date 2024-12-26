using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySystem.Migrations
{
    public partial class SeedDataMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LibrarianBook");

            migrationBuilder.CreateTable(
                name: "BookLibrarian",
                columns: table => new
                {
                    ManagedBooksId = table.Column<int>(type: "int", nullable: false),
                    ManagersLibrarianId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookLibrarian", x => new { x.ManagedBooksId, x.ManagersLibrarianId });
                    table.ForeignKey(
                        name: "FK_BookLibrarian_Books_ManagedBooksId",
                        column: x => x.ManagedBooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookLibrarian_Librarians_ManagersLibrarianId",
                        column: x => x.ManagersLibrarianId,
                        principalTable: "Librarians",
                        principalColumn: "LibrarianId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "J.K. Rowling" },
                    { 2, "George Orwell" }
                });

            migrationBuilder.InsertData(
                table: "Librarians",
                columns: new[] { "LibrarianId", "LibrarianName" },
                values: new object[,]
                {
                    { 1, "Alice" },
                    { 2, "Bob" }
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "MemberId", "MemberName" },
                values: new object[,]
                {
                    { 1, "John Doe" },
                    { 2, "Jane Smith" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "IsAvailable", "Title" },
                values: new object[] { 1, 1, true, "Harry Potter" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "IsAvailable", "Title" },
                values: new object[] { 2, 2, true, "1984" });

            migrationBuilder.InsertData(
                table: "Checkouts",
                columns: new[] { "Id", "BookId", "CheckoutDate", "DueDate", "MemberId", "ReturnDate" },
                values: new object[] { 1, 1, new DateTime(2024, 12, 26, 12, 34, 50, 24, DateTimeKind.Local).AddTicks(3911), new DateTime(2025, 1, 9, 12, 34, 50, 24, DateTimeKind.Local).AddTicks(3927), 1, null });

            migrationBuilder.InsertData(
                table: "Checkouts",
                columns: new[] { "Id", "BookId", "CheckoutDate", "DueDate", "MemberId", "ReturnDate" },
                values: new object[] { 2, 2, new DateTime(2024, 12, 26, 12, 34, 50, 24, DateTimeKind.Local).AddTicks(3931), new DateTime(2025, 1, 9, 12, 34, 50, 24, DateTimeKind.Local).AddTicks(3932), 2, null });

            migrationBuilder.CreateIndex(
                name: "IX_BookLibrarian_ManagersLibrarianId",
                table: "BookLibrarian",
                column: "ManagersLibrarianId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookLibrarian");

            migrationBuilder.DeleteData(
                table: "Checkouts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Checkouts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Librarians",
                keyColumn: "LibrarianId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Librarians",
                keyColumn: "LibrarianId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "MemberId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "MemberId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.CreateTable(
                name: "LibrarianBook",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    LibrarianId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibrarianBook", x => new { x.BookId, x.LibrarianId });
                    table.ForeignKey(
                        name: "FK_LibrarianBook_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LibrarianBook_Librarians_LibrarianId",
                        column: x => x.LibrarianId,
                        principalTable: "Librarians",
                        principalColumn: "LibrarianId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LibrarianBook_LibrarianId",
                table: "LibrarianBook",
                column: "LibrarianId");
        }
    }
}
