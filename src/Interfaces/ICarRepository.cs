using CarReviewApp.Dto;
using CarReviewApp.Models;

namespace CarReviewApp.Interfaces
{
    public interface ICarRepository
    {
        ICollection<Car> GetCars();
        Car GetCar(int id);
        Car GetCar(string make);
        decimal GetCarRating(int id);
        bool CarExists(int id);
        bool CreateCar(int ownerId, int categoryId, Car car);
        bool UpdateCar(int ownerId, int categoryId, Car car);
        bool DeleteCar(Car car);
        Car GetCarTrimToUpper(CarDto carCreate);
        bool Save();
    }
}
