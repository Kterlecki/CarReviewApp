using CarReviewApp.Models;

namespace CarReviewApp.Interfaces
{
    public interface ICarRepository
    {
        ICollection<Car> GetCars();
        Car GetCar(int id);
        Car GetCar(string name);
        decimal GetCarRating(int id);
        bool CarExists(int id);
        bool CreateCar(int ownerId, int categoryId, Car car);
        bool Save();
    }
}
