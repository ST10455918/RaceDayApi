namespace RaceDayApi.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public DateTime Date { get; set; }
        public int VenueId { get; set; }
        public Venue? Venue { get; set; }
        public int CreatedById { get; set; }

        public string? ProfileImageUrl { get; set; }
        public string? BannerImageUrl { get; set; } 


    }
}