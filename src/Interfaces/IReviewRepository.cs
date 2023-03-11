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
        public bool UpdateReview(Review review);
        public bool DeleteReview(Review review);
        public bool DeleteReviews(List<Review> reviews);
        public bool Save();
    }
}
