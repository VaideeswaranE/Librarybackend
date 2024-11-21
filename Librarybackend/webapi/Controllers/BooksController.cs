using AdminWebAPI.Data;
using AdminWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly AdminWebAPI.Data.DbContext _context;

        public BooksController(AdminWebAPI.Data.DbContext context)
        {
            _context = context;
        }

// GET: api/Books
[HttpGet]
public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
{
    return await _context.Books
                         .Include(b => b.Borrower)  // Include Borrower
                         .Include(b => b.Category)  // Include Category
                         .ToListAsync();
}

// GET: api/Books/5
[HttpGet("{id}")]
public async Task<ActionResult<Book>> GetBook(int id)
{
    var book = await _context.Books
                             .Include(b => b.Borrower)  // Include Borrower
                             .Include(b => b.Category)  // Include Category
                             .FirstOrDefaultAsync(b => b.BookId == id);

    if (book == null)
    {
        return NotFound();
    }

    return book;
}

        // POST: api/Books
        [HttpPost]
public async Task<IActionResult> PostBook([FromBody] Book book)
{
    if (book == null)
    {
        return BadRequest("Book object cannot be null.");
    }

    // If CategoryId is provided, verify it exists.
    if (book.CategoryId.HasValue)
    {
        var category = await _context.Categories.FindAsync(book.CategoryId);
        if (category == null)
        {
            return BadRequest($"Category with ID {book.CategoryId} does not exist.");
        }
        book.Category = category;
    }

    // If BorrowerId is provided, verify it exists.
    if (book.BorrowerId.HasValue)
    {
        var borrower = await _context.Borrowers.FindAsync(book.BorrowerId);
        if (borrower == null)
        {
            return BadRequest($"Borrower with ID {book.BorrowerId} does not exist.");
        }
        book.Borrower = borrower;
    }

    try
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetBook", new { id = book.BookId }, book);
    }
    catch (DbUpdateException ex)
    {
        return StatusCode(500, $"Internal server error: {ex.Message}");
    }
}
        // PUT: api/Books/5
[HttpPut("{id}")]
public async Task<IActionResult> PutBook(int id, Book book)
{
    if (id != book.BookId)
    {
        return BadRequest();
    }

    // If borrowerId is null, disassociate the book from the borrower
    if (!book.BorrowerId.HasValue)
    {
        //book.Borrower = null;  // Ensure the navigation property is set to null
    }
    else
    {
        // Check if the borrower exists in the database
        var existingBorrower = await _context.Borrowers.FindAsync(book.BorrowerId);
        if (existingBorrower == null)
        {
            return BadRequest("The specified borrower does not exist.");
        }

        // Attach the existing borrower to prevent adding a new one
        _context.Entry(existingBorrower).State = EntityState.Unchanged;
        book.Borrower = existingBorrower;
    }

    _context.Entry(book).State = EntityState.Modified;

    try
    {
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!_context.Books.Any(e => e.BookId == id))
        {
            return NotFound();
        }
        else
        {
            throw;
        }
    }

    return NoContent();
}
        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Books/search
        [HttpGet("search")]
        public IActionResult SearchBooks(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return BadRequest("Search query cannot be empty.");
            }

            var books = _context.Books
                                .Where(b => b.Title.Contains(query) 
                                        || b.Author.Contains(query) 
                                        || (b.Category != null && b.Category.CategoryName.Contains(query)))
                                .Include(b => b.Category)  // Ensure Category is included in the result
                                .Include(b => b.Borrower)  // Ensure Borrower is included in the result
                                .ToList();

            return Ok(books);
        }
    }
}