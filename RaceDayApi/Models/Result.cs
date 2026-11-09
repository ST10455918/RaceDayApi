namespace RaceDayApi.Models
{
    public class Result
    {
        public int Id { get; set; }
        public int EnrolmentId { get; set; }
        public Enrolment? Enrolment { get; set; }
        public string Position { get; set; } = "";
        public TimeSpan FinishTime { get; set; }
    }
}