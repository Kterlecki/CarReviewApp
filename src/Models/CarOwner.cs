namespace CarReviewApp.Models
{
    public class CarOwner
    {
        public int CarId { get; set; }
        public int OwnerId { get; set; }
        public Car Car { get; set; }
        public Owner Owner { get; set; }
    }
}
