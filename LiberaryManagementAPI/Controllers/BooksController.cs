using LiberaryManagementAPI.Data;
using LiberaryManagementAPI.Dtos;
using LiberaryManagementAPI.Models;
using LiberaryManagementAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiberaryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LiberaryDbContext _context;

        public BooksController(LiberaryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookResponseDTO>>> GetBooks()
        {
            var books = await _context.books
                .Include( s=> s.Author)
                .Include(b => b.Category)
                .Select(b => new BookResponseDTO
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    ISBN = b.ISBN,
                    PublishedDate = b.PublishDate,
                    Quantity = b.Quantity,
                    AvailableCopies = b.AvailableCopies,
                    AuthorName = $"{b.Author.FirstName} {b.Author.LastName}",
                    CategoryName = b.Category.Name
                })
                .ToListAsync();

            return Ok(books);
        }

        [HttpPost]
        public async Task<ActionResult<BookResponseDTO>> CreateBook(BookCreateDTO bookDTO)
        {

            var authorNames = bookDTO.AuthorName.Split(' ');
            var firstName = authorNames[0];
            var lastName = string.Join(" ", authorNames.Skip(1));


            var author = await _context.authors
                .FirstOrDefaultAsync(a => a.FirstName == firstName && a.LastName == lastName);

            if (author == null)
            {
                author = new Author
                {
                    FirstName = firstName,
                    LastName = lastName
                };
                _context.authors.Add(author);
            }


            var category = await _context.categories
                .FirstOrDefaultAsync(c => c.Name == bookDTO.CategoryName);

            if (category == null)
            {
                category = new Category
                {
                    Name = bookDTO.CategoryName
                };
                _context.categories.Add(category);
            }


            var book = new Book
            {
                Title = bookDTO.Title,
                ISBN = bookDTO.ISBN,
                PublishDate = bookDTO.PublishedDate,
                Quantity = bookDTO.Quantity,
                AvailableCopies = bookDTO.Quantity,
                Author = author,
                Category = category
            };

            _context.books.Add(book);
            await _context.SaveChangesAsync();


            var response = new BookResponseDTO
            {
                BookId = book.BookId,
                Title = book.Title,
                ISBN = book.ISBN,
                PublishedDate = book.PublishDate,
                Quantity = book.Quantity,
                AvailableCopies = book.AvailableCopies,
                AuthorName = bookDTO.AuthorName,
                CategoryName = bookDTO.CategoryName
            };

            return CreatedAtAction(nameof(GetBook), new { id = book.BookId }, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookResponseDTO>> GetBook(int id)
        {
            var book = await _context.books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.BookId == id);

            if (book == null)
                return NotFound();

            var response = new BookResponseDTO
            {
                BookId = book.BookId,
                Title = book.Title,
                ISBN = book.ISBN,
                PublishedDate = book.PublishDate,
                Quantity = book.Quantity,
                AvailableCopies = book.AvailableCopies,
                AuthorName = $"{book.Author.FirstName} {book.Author.LastName}",
                CategoryName = book.Category.Name
            };

            return Ok(response);
        }
    }
}
