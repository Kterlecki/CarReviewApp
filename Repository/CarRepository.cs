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
            throw new NotImplementedException();
        }

        public Car GetCar(int id)
        {
            return _context.Cars.Where(p => p.Id == id).FirstOrDefault();
        }

        public Car GetCar(string name)
        {
            throw new NotImplementedException();
        }

        public decimal GetCarRating(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Car> GetCars()
        {
            return _context.Cars.OrderBy(c => c.Id).ToList();
        }
    }
}
