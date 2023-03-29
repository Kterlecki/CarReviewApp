using System.Linq;
using Xunit;
using FluentAssertions;
using FakeItEasy;
using CarReviewApp.Data;
using CarReviewApp.Repository;
using Microsoft.EntityFrameworkCore;
using CarReviewApp.Models;
using System.Collections.Generic;

public class CarRepositoryTests
{
    private readonly CarRepository _repository;

    public CarRepositoryTests()
    {
        _repository = new CarRepository(GetDbContext());
    }
    
    private static DataContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "test")
            .Options;

            var databaseContext = new DataContext(options);
            databaseContext.Database.EnsureCreated();
            if (!databaseContext.Cars.Any())
            {
                    databaseContext.Cars.AddRange(
                            new Car { Id = 1, Make = "Audi", Model = "A7", YearBuilt = 2015, Reviews = new List<Review>(), CarOwners = new List<CarOwner>(), CarCategories = new List<CarCategory>()},
                            new Car { Id = 2, Make = "Toyota", Model = "Camry", YearBuilt = 2020 , Reviews = new List<Review>(), CarOwners = new List<CarOwner>(), CarCategories = new List<CarCategory>()},
                            new Car { Id = 3, Make = "Honda", Model = "Civic", YearBuilt = 2019 , Reviews = new List<Review>(), CarOwners = new List<CarOwner>(), CarCategories = new List<CarCategory>()}
                        );
                    databaseContext.SaveChanges();
            }
            return databaseContext;
        }
    [Fact]
    public void CarExists_ShouldReturnTrue_WhenCarExists()
    {
        // Arrange
        var carId = 1;
        // Act
        var result = _repository.CarExists(carId);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void CarExists_ShouldReturnFalse_WhenCarDoesNotExist()
    {
        // Arrange
        int carId = 20;

        // Act
        var result = _repository.CarExists(carId);

        // Assert
        result.Should().BeFalse();
    }
    [Fact]
    public void GetCarById_ShouldReturnCar_WhenCarExist()
    {
        // Arrange
        int carId = 1;

        // Act
        var result = _repository.GetCar(carId);

        // Assert
        result.Should().BeOfType<Car>();
        Assert.Matches(result.Make, "Audi");
    }
    [Fact]
    public void GetCarById_ShouldReturnNull_WhenCarDoesntExist()
    {
        // Arrange
        int carId = 10;

        // Act
        var result = _repository.GetCar(carId);

        // Assert
        result.Should().BeNull();
    }
    [Fact]
    public void GetCarByMake_ShouldReturCar_WhenCarExist()
    {
        // Arrange
        var carMake = "Audi";

        // Act
        var result = _repository.GetCar(carMake);

        // Assert
        result.Should().BeOfType<Car>();
        Assert.Matches(result.Make, "Audi");
    }
    [Fact]
    public void GetCarByMake_ShouldReturNull_WhenCarDoesntExist()
    {
        // Arrange
        var carMake = "Audi X";

        // Act
        var result = _repository.GetCar(carMake);

        // Assert
        result.Should().BeNull();
    }
}
