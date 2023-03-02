using CarReviewApp.Data;
using CarReviewApp.Interfaces;
using CarReviewApp.Models;

namespace CarReviewApp.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly DataContext _context;
        public CarRepository(DataContext context)
        {
            _context = context;
        }

        public bool CarExists(int id)
        {
            return _context.Cars.Any(c => c.Id == id);
        }

        public Car GetCar(int id)
        {
            return _context.Cars.Where(c => c.Id == id).FirstOrDefault();
        }

        public Car GetCar(string make)
        {
            return _context.Cars.Where(c => c.Make == make).FirstOrDefault();
        }

        public decimal GetCarRating(int id)
        {
            var review = _context.Reviews.Where(r => r.Car.Id == id);
            if(review.Count() < 1)
            {
                return 0;
            }
            return ((decimal)review.Sum(r => r.Rating)) / review.Count();
        }

        public ICollection<Car> GetCars()
        {
            return _context.Cars.OrderBy(c => c.Id).ToList();
        }
    }
}
