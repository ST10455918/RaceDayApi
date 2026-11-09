namespace RaceDayApi.Models
{
    public class Enrolment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public DateTime EnrolDate { get; set; } = DateTime.UtcNow;
    }
}