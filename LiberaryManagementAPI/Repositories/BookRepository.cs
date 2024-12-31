using System.Net;
using LiberaryManagementAPI.Data;
using LiberaryManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LiberaryManagementAPI.Repositories
{
    public class BookRepository :GenericRepository<Book>, IBookRepository
    {
        public BookRepository(LiberaryDbContext context) : base(context) {
            
        }

        public async Task<IEnumerable<Book>> GetAvailableBook()
        {
            return await _context.books.Where(b => b.AvailableCopies > 0)
                .Include(b => b.Author)
                .Include(b => b.Category)
                .ToListAsync();
        }

        public async Task<bool> IsAvailable(int id)
        {
            var book = await _context.books.FindAsync(id);
            return book?.AvailableCopies > 0;
        }

        public async Task UpdateBookQuantity(int BookId, int quantity)
        {
            var book = await _context.books.FindAsync(BookId);
            if(book != null)
            {
                book.Quantity = quantity;
                book.AvailableCopies = quantity - _context.loans.Count( l=>l.BookId == BookId && !l.IsReturned);
                await _context.SaveChangesAsync();

            }
        }

       
    }
}
