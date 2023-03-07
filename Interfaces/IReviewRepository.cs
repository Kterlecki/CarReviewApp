using CarReviewApp.Models;

namespace CarReviewApp.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        Review GetReview(int id);
        ICollection<Review> GetReviewsOfACar(int carId);
        bool ReviewExists(int id);
        public bool CreateReview(Review review);
        public bool Save();
    }
}
