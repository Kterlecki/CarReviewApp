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
    private readonly DataContext _context;
    private readonly CarRepository _repository;

    public CarRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "test")
            .Options;
        _context = new DataContext(options);
        _repository = new CarRepository(_context);
    }
    [Fact]
    public void CarExists_ShouldReturnTrue_WhenCarExists()
    {
        // Arrange
        var car = new Car{
            Id = 1,
            Make = "Audi",
            Model = "A7",
            YearBuilt = 2015,
            Reviews = new List<Review>(),
            CarOwners = new List<CarOwner>(),
            CarCategories = new List<CarCategory>()
        };
        var carId = 1;
        _context.Add(car);
        _context.SaveChanges();
        // Act
        var result = _repository.CarExists(carId);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void CarExists_ShouldReturnFalse_WhenCarDoesNotExist()
    {
        // Arrange
        int carId = 2;

        // Act
        var result = _repository.CarExists(carId);

        // Assert
        result.Should().BeFalse();
    }

}
