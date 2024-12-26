Project Spec

## Scenario Overview
This library system is designed to manage two types of users: **Librarians** and **Members**. The system supports various functionalities for both user types, allowing for efficient management of books, checkouts, returns, and overdue tracking. The system includes features like limiting the number of books members can check out, filtering books by author, and marking books as returned.

### User Roles:
- **Librarians**: 
  - Manage books, checkouts, and returns.
  - Can view all books and checkouts.
  - Mark books as returned.
  - View overdue books.
  - Filter books by author.
  
- **Members**:
  - View their own current and past checkouts.
  - See overdue status of borrowed books.
  - Check out books (up to a limit of 5 books at a time).

Both users can see if a book is overdue. Books must be returned within 21 days of checkout.

---

## Requirements

### 1. Data Layer

#### Models:
- **Books**: Contains properties such as title, author, availability status, and the date it was checked out.
- **Members**: Contains properties such as name, membership status, and a list of current checkouts.
- **Checkouts**: Contains properties such as the book ID, member ID, checkout date, return date, and overdue status.
- **Authors**: Contains properties for author name and a list of books they've written.

#### Relationships:
- **Books** are linked to **Authors**.
- **Members** can have many **Checkouts**.
- **Checkouts** are related to both **Books** and **Members**.
- The data schema should be designed to accommodate future additions like reserving books, adding late fees, etc.

---

### 2. Core Services

#### Library Service:
The `LibraryService` class manages the core functionality of the system:

- **Checkout Management**:
  - Ensure that a **Member** can check out a maximum of 5 books at once.
  - Track if books are **overdue** (books must be returned within 21 days).
  
- **Return Management**:
  - **Librarians** can mark books as returned.
  
- **Book Filtering**:
  - Provide the ability to filter books by **author**.

#### Validation:
- Validate that **Members** do not exceed the 5-book checkout limit.
- Ensure that **Books** are available for checkout before allowing a member to check them out.
- Enforce the 21-day return policy for **Books**.

---

### 3. User Roles

#### Librarians:
- Can view **all books** and **checkouts**.
- Can **mark books as returned**.
- Can view **overdue books**.
- Can filter **books by author**.

#### Members:
- Can view their **current** and **past checkouts**.
- Can check the **overdue status** of their books.
- Can **checkout books** (with a limit of 5 books at a time).
- Can see overdue status of borrowed books.

---

### 4. Data Tables

- Use a **table library** (e.g., [DataTables.js](https://datatables.net/)) to display data:
  - **Books**: Include columns such as title, author, availability, and overdue status.
  - **Members**: Include columns such as name, membership status, and the number of books checked out.
  - **Checkouts**: Include columns such as book title, member name, checkout date, return date, and overdue status.
  
- Features:
  - **Search**: Allow searching through books, members, and checkouts.
  - **Sorting**: Sort by book title, member name, and checkout date.
  - **Pagination**: Display results in paginated form for easy navigation.
  - **Overdue Indicators**: Clearly indicate which books are overdue.
  - **Filter by Author**: Allow filtering of books by author name.

---

### 5. Controllers and Views

#### Build Views:

- **Librarians**:
  - A view to **manage books**, including adding, updating, and deleting books.
  - A view to **manage checkouts** and returns.
  - A view to **filter books by author**.
  - A view to **view overdue books**.
  
- **Members**:
  - A view to **view current checkouts** (with details like book title, checkout date, and due date).
  - A view to **view past checkouts**.
  - A view to **see overdue status**.
  - A view to **checkout books** (with a limit of 5 books at a time).

- **Additional Items**:
  - **Admin** views (if required) to manage users and perform administrative tasks.
  - **Authentication and Authorization** to ensure members and librarians have appropriate access to their respective features.