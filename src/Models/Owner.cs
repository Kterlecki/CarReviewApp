namespace CarReviewApp.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;

        public Country? Country { get; set; }
        public ICollection<CarOwner>? CarOwners { get; set; }
    }
}
