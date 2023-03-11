namespace CarReviewApp.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Rating { get; set; }

        public Reviewer Reviewer { get; set; }
        public Car Car { get; set; }
    }
}
