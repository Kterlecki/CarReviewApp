using CarReviewApp.Models;

namespace CarReviewApp.Dto
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<CarCategory> CarCategories { get; set; }

    }
}
