using CarReviewApp.Models;

namespace CarReviewApp.Interfaces
{
    public interface ICarRepository
    {
        ICollection<Car> GetCars();
    }
}
