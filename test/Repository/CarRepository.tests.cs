using System;
using System.Linq;
using Xunit;
using FluentAssertions;
using FakeItEasy;
using CarReviewApp.Data;
using CarReviewApp.Repository;
using Microsoft.EntityFrameworkCore;
using CarReviewApp.Models;
using System.Collections.Generic;
using Moq;
using CarReviewApp.Interfaces;

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
                            new Car { Id = 1, Make = "Audi", Model = "A7", YearBuilt = 2015,
                            Reviews = new List<Review>{
                                new Review{ Id = 1,
                                            Title = "example",
                                            Description = "try example",
                                            Rating = 1,
                                            Reviewer = new Reviewer(),
                                            Car = new Car()},
                                new Review{ Id = 2,
                                            Title = "example",
                                            Description = "try example",
                                            Rating = 1,
                                            Reviewer = new Reviewer(),
                                            Car = new Car()}}, CarOwners = new List<CarOwner>(), CarCategories = new List<CarCategory>()},
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
    [Fact]
    public void GetCarRating_ShouldReturRating_WhenCarExist()
    {
        // Arrange
        var carId = 1;

        // Act
        var result = _repository.GetCarRating(carId);

        // Assert
        Assert.IsType<decimal>(result);
    }
    [Fact]
    public void GetCarRating_ShouldReturZero_WhenNoReviewsAreFound()
    {
        // Arrange
        var carId = 5;

        // Act
        var result = _repository.GetCarRating(carId);

        // Assert
        Assert.IsType<decimal>(result);
        result.Should().Be(0);
    }
    [Fact]
    public void GetCars_ShouldReturnCarList_WhenCalled()
    {
        // Act
        var result = _repository.GetCars();

        // Assert
        Assert.IsType<List<Car>>(result);
        var resultCount = result.Count;
        resultCount.Should().Be(3);
    }
    [Fact]
    public void CreateCar_ShouldCreateNewCar_WhenCalled()
    {
        // Arrange
        var ownerId = 1;
        var categoryId = 1;
        var car = new Car
        {
            Id = 55,
            Make = "Honda",
            Model = "Civic",
            YearBuilt = 2020,
            Reviews = new List<Review>(),
            CarOwners = new List<CarOwner>(),
            CarCategories = new List<CarCategory>()
        };
        var mockContex = new Mock<IDataContextWrapper>();
        mockContex.Setup(x => x.CreateDataContext()).Returns(new DataContext(new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("testDb").Options));
        var repository = new CarRepository(mockContex.Object.CreateDataContext());
        // Act
        var result = _repository.CreateCar(ownerId, categoryId, car);
        // Assert
        Assert.True(result);
    }

    
}
