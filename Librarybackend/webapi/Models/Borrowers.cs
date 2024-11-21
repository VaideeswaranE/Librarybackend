using System;
using System.ComponentModel.DataAnnotations;

namespace AdminWebAPI.Models
{
    public class Borrower
    {
        public int BorrowerId { get; set; }
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }
        public DateTime? DueDate { get; set; } // Nullable DueDate
    }
}
