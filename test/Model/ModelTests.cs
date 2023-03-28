
using System.Collections.Generic;
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
        Assert.IsType<List<Review>>(car.Reviews);
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
    [Fact]
    public void CarOwner_CreateOwnerInstance_ReturnsCarOwner()
    {
        // Arrange
        var carOwner = new CarOwner{
                CarId = 1,
                OwnerId = 1,
                Car = new Car(),
                Owner = new Owner()
            };
        Assert.Equal(1, carOwner.CarId);
        Assert.Equal(1, carOwner.OwnerId);
        Assert.IsType<Car>(carOwner.Car);
        Assert.IsType<Owner>(carOwner.Owner);
    }
    [Fact]
    public void Category_CreateCategoryInstance_ReturnsCategory()
    {
        // Arrange
        var category = new Category{
                Id = 1,
                Name = "test",
                CarCategories = new List<CarCategory>()
            };
        Assert.Equal(1, category.Id);
        Assert.Equal("test", category.Name);
        Assert.IsType<List<CarCategory>>(category.CarCategories);
    }
    [Fact]
    public void Country_CreateCountryInstance_ReturnsCountry()
    {
        // Arrange
        var country = new Country{
                Id = 1,
                Name = "test",
                Owners = new List<Owner>()
            };
        Assert.Equal(1, country.Id);
        Assert.Equal("test", country.Name);
        Assert.IsType<List<Owner>>(country.Owners);
    }
    [Fact]
    public void Owner_CreateOwnerInstance_ReturnsOwner()
    {
        // Arrange
        var owner = new Owner{
                Id = 1,
                Name = "test",
                Surname = "test",
                Country = new Country(),
                CarOwners = new List<CarOwner>()
            };
        Assert.Equal(1, owner.Id);
        Assert.Equal("test", owner.Name);
        Assert.Equal("test", owner.Surname);
        Assert.IsType<Country>(owner.Country);
        Assert.IsType<List<CarOwner>>(owner.CarOwners);
    }
}