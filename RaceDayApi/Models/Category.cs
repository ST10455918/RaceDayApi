namespace RaceDayApi.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int EventId { get; set; }
        public decimal EntryFee { get; set; }
    }
}
