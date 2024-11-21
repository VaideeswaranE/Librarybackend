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
    public class BorrowHistoriesController : ControllerBase
    {
        private readonly AdminWebAPI.Data.DbContext _context;  // Fully qualify the DbContext

        public BorrowHistoriesController(AdminWebAPI.Data.DbContext context)  // Fully qualify the DbContext
        {
            _context = context;
        }

        // GET: api/BorrowHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BorrowHistory>>> GetBorrowHistories()
        {
            return await _context.BorrowHistory
                                 .Include(bh => bh.Borrower)
                                 .Include(bh => bh.Book)
                                 .ToListAsync();
        }

        // GET: api/BorrowHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BorrowHistory>> GetBorrowHistory(int id)
        {
            var borrowHistory = await _context.BorrowHistory
                                               .Include(bh => bh.Borrower)
                                               .Include(bh => bh.Book)
                                               .FirstOrDefaultAsync(bh => bh.HistoryId == id);

            if (borrowHistory == null)
            {
                return NotFound();
            }

            return borrowHistory;
        }

        /*// POST: api/BorrowHistories
        [HttpPost]
        public async Task<ActionResult<BorrowHistory>> PostBorrowHistory(BorrowHistory borrowHistory)
        {
            _context.BorrowHistory.Add(borrowHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBorrowHistory), new { id = borrowHistory.HistoryId }, borrowHistory);
        }

        // PUT: api/BorrowHistories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBorrowHistory(int id, BorrowHistory borrowHistory)
        {
            if (id != borrowHistory.HistoryId)
            {
                return BadRequest();
            }

            _context.Entry(borrowHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BorrowHistoryExists(id))
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

        // DELETE: api/BorrowHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBorrowHistory(int id)
        {
            var borrowHistory = await _context.BorrowHistory.FindAsync(id);
            if (borrowHistory == null)
            {
                return NotFound();
            }

            _context.BorrowHistory.Remove(borrowHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BorrowHistoryExists(int id)
        {
            return _context.BorrowHistory.Any(e => e.HistoryId == id);
        }*/
    }
}
