using System;
using System.ComponentModel.DataAnnotations;

namespace AdminWebAPI.Models
{
    public class BorrowHistory
    {
        // This explicitly marks HistoryId as the primary key
        public int HistoryId { get; set; }
        public int BorrowerId { get; set; }
        public int BookId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        // Navigation properties
        public required Borrower Borrower { get; set; }
        public required Book Book { get; set; }
    }
}
