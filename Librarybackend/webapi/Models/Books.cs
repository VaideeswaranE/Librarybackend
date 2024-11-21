using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminWebAPI.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [StringLength(255)]
        public required string Title { get; set; }

        [StringLength(255)]
        public required string Author { get; set; }

        [StringLength(50)]
        public required string Status { get; set; }

        public int? BorrowerId { get; set; }

        public int? CategoryId { get; set; }

        // Navigation properties
        public Borrower? Borrower { get; set; }
        public Category? Category { get; set; }
    }
}
