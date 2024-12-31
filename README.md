# Library Management System API

 Library Management System built with .NET Core Web API. This system provides comprehensive functionality for managing books, members, loans, and more in a library setting.

## 🚀 Features

- **Book Management**
  - Add new books with automatic author and category creation
  - Track book quantity and available copies
  - View detailed book information including author and category
  - ISBN validation and duplicate checking

- **Member Management**
  - Register new library members
  - Track member activity and loan history
  - Email validation and duplicate prevention
  - Monitor active loans per member

- **Borrow System**
  - Check out books to members
  - Automatic available copies management
  - Due date tracking
  - Late fee calculation
  - Return processing

## 💻 Technology Stack

- **.NET Core 8.0**
- **Entity Framework Core**
- **SQL Server**








## 🔒 Business Rules

1. **Book Management**
   - ISBN must be unique
   - Quantity must be greater than 0
   - Available copies automatically tracked
   - Author and category created if they don't exist

2. **Member Management**
   - Email must be unique
   - Phone number validation
   - Automatic join date assignment
   - Active loans tracking

3. **Loan System**
   - Maximum 5 active loans per member
   - 14-day loan period
   - $1 per day late fee
   - Available copies updated automatically

## 📊 Data Models

### Book
```

public class Book
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public string ISBN { get; set; }
    public DateTime PublishedDate { get; set; }
    public int Quantity { get; set; }
    public int AvailableCopies { get; set; }
    public int AuthorId { get; set; }
    public int CategoryId { get; set; }
}
```

### Member
```

public class Member
{
    public int MemberId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime JoinDate { get; set; }
    public bool IsActive { get; set; }
}
```

## 🔄 Future Enhancements

1. **Authentication & Authorization**
   - JWT authentication
   - Role-based access control
   - Admin dashboard

2. **Advanced Features**
   - Book reservations
   - Email notifications
   - Report generation
   - Book recommendations

3. **Integration Options**
   - Payment gateway for fines
   - SMS notifications
   - Barcode/QR code scanning




## 👥 Authors

* **Mohamed Gomaa** - *Initial work* - [Mohamed Gomaa](https://github.com/Moh-Gomma)
