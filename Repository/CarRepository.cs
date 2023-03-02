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

        public ICollection<Car> GetCars()
        {
            return _context.Cars.OrderBy(c => c.Id).ToList();
        }
    }
}
