using CarReviewApp.Data;
using CarReviewApp.Interfaces;
using CarReviewApp.Models;

namespace CarReviewApp.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DataContext _context;

        public OwnerRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Car> GetCarByOwner(int id)
        {
            return _context.CarOwners.Where(c => c.Owner.Id == id).Select(c => c.Car).ToList();
        }

        public Owner GetOwner(int id)
        {
            return _context.Owners.Where(o => o.Id == id).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnerOfACar(int id)
        {
            return _context.CarOwners.Where(c => c.Car.Id == id).Select(c => c.Owner).ToList();
        }

        public ICollection<Owner> GetOwners()
        {
            return _context.Owners.ToList();
        }

        public bool OwnerExists(int id)
        {
            return _context.Owners.Any(o => o.Id == id);
        }
    }
}
