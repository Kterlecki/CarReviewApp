using CarReviewApp.Models;

namespace CarReviewApp.Interfaces
{
    public interface IOwnerRepository
    {
        ICollection<Owner> GetOwners();
        Owner GetOwner(int ownerId);
        ICollection<Owner> GetOwnerOfACar(int id);
        ICollection<Car> GetCarByOwner(int id);
        bool OwnerExists(int id);
    }
}
