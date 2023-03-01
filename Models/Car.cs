namespace CarReviewApp.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int yearBuilt { get; set; }

        public ICollection<Review> Reviews { get; set; }
        public ICollection<CarOwner> CarOwners{ get; set; }
        public ICollection<CarCategory> CarCategories{ get; set; }
    }
}
