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
    public class BorrowersController : ControllerBase
    {
        private readonly AdminWebAPI.Data.DbContext _context;

        public BorrowersController(AdminWebAPI.Data.DbContext context)
        {
            _context = context;
        }

        // GET: api/Borrowers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Borrower>>> GetBorrowers()
        {
            return await _context.Borrowers.ToListAsync();
        }

        // GET: api/Borrowers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Borrower>> GetBorrower(int id)
        {
            var borrower = await _context.Borrowers.FindAsync(id);

            if (borrower == null)
            {
                return NotFound();
            }

            return borrower;
        }

        // POST: api/Borrowers
        [HttpPost]
        public async Task<ActionResult<Borrower>> PostBorrower(Borrower borrower)
        {
            if (borrower == null)
            {
                return BadRequest("Borrower data is required.");
            }

            _context.Borrowers.Add(borrower);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBorrower", new { id = borrower.BorrowerId }, borrower);
        }

        // PUT: api/Borrowers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBorrower(int id, Borrower borrower)
        {
            if (id != borrower.BorrowerId)
            {
                return BadRequest("Borrower ID mismatch.");
            }

            if (borrower.DueDate == null)
            {
                Console.WriteLine("Received a null DueDate for borrower.");
            }

            var existingBorrower = await _context.Borrowers.FindAsync(id);
            if (existingBorrower == null)
            {
                return NotFound("Borrower not found.");
            }

            existingBorrower.Name = borrower.Name;
            existingBorrower.PhoneNumber = borrower.PhoneNumber;
            existingBorrower.DueDate = borrower.DueDate;

            _context.Entry(existingBorrower).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Borrowers.Any(e => e.BorrowerId == id))
                {
                    return NotFound("Borrower was not found during update.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Borrowers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBorrower(int id)
        {
            var borrower = await _context.Borrowers.FindAsync(id);
            if (borrower == null)
            {
                return NotFound();
            }

            _context.Borrowers.Remove(borrower);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
