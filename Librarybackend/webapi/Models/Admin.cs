namespace AdminWebAPI.Models
{
    public class Admin
    {
        public int AdminId { get; set; }
        public required string Adminname { get; set; }
        public required string Password { get; set; }
    }
}
