
using System.Collections.Generic;
using System.Collections;
using CarReviewApp.Models;
using Xunit;

namespace CarReviewApp.tests.Model;

public class ModelTests
{
    [Fact]
    public void Car_CreateCarInstance_ReturnsCar()
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
        // Assert
        Assert.Equal(1, car.Id);
        Assert.Equal("Audi", car.Make);
        Assert.Equal("A7" ,car.Model);
        Assert.Equal(2015 ,car.YearBuilt);
        Assert.IsType<List<Reviewer>>(car.Reviews);
        Assert.IsType<List<CarOwner>>(car.CarOwners);
        Assert.IsType<List<CarCategory>>(car.CarCategories);
    }
    [Fact]
    public void Review_CreateReviewInstance_ReturnsReview()
    {
        // Arrange
        var review = new Review{
            Id = 1,
            Title = "example",
            Description = "try example",
            Rating = 1,
            Reviewer = new Reviewer(),
            Car = new Car()
        };
        Assert.Equal(1, review.Id);
        Assert.Equal("example", review.Title);
        Assert.Equal("try example" ,review.Description);
        Assert.Equal(1 ,review.Rating);
        Assert.IsType<Reviewer>(review.Reviewer);
    }

    [Fact]
    public void Reviewer_CreateReviewerInstance_ReturnsReviewer()
    {
        // Arrange
        var reviewer = new Reviewer{
                Id = 1,
                FirstName = "Test",
                LastName = "tests",
                Reviews = new List<Review>()
            };
        Assert.Equal(1, reviewer.Id);
        Assert.Equal("Test", reviewer.FirstName);
        Assert.Equal("tests" ,reviewer.LastName);
        Assert.IsType<List<Review>>(reviewer.Reviews);
    }
    [Fact]
    public void CarCategory_CreateCarCategoryInstance_ReturnsCarCategory()
    {
        // Arrange
        var carCategory = new CarCategory{
                CarId = 1,
                CategoryId = 1,
                Car = new Car(),
                Category = new Category()
            };
        Assert.Equal(1, carCategory.CarId);
        Assert.Equal(1, carCategory.CategoryId);
        Assert.IsType<Car>(carCategory.Car);
        Assert.IsType<Category>(carCategory.Category);
    }
}