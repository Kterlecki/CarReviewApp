using CarReviewApp.Models;

namespace CarReviewApp.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReview();
        Review GetReview(int id);
        ICollection<Review> GetReviewsOfACar(int carId);
        bool ReviewExists(int id);
    }
}
