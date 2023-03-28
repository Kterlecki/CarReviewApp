using System.Collections.Generic;
using CarReviewApp.Dto;
using Xunit;

namespace CarReviewApp.tests.Dto;

public class DtoTests
{
    [Fact]
    public void CarDto_CreateCarDtoInstance_ReturnsCarDto()
    {
        // Arrange
        var car = new CarDto{
            Id = 1,
            Make = "Audi",
            Model = "A7",
            YearBuilt = 2015,
        };
        // Assert
        Assert.Equal(1, car.Id);
        Assert.Equal("Audi", car.Make);
        Assert.Equal("A7" ,car.Model);
        Assert.Equal(2015 ,car.YearBuilt);
        Assert.IsType<CarDto>(car);
    }
    [Fact]
    public void ReviewDto_CreateReviewDtoInstance_ReturnsReviewDto()
    {
        // Arrange
        var review = new ReviewDto{
            Id = 1,
            Title = "example",
            Description = "try example",
            Rating = 1
        };
        Assert.Equal(1, review.Id);
        Assert.Equal("example", review.Title);
        Assert.Equal("try example" ,review.Description);
        Assert.Equal(1 ,review.Rating);
        Assert.IsType<ReviewDto>(review);
    }

    [Fact]
    public void ReviewerDto_CreateReviewerDtoInstance_ReturnsReviewerDto()
    {
        // Arrange
        var reviewer = new ReviewerDto{
                Id = 1,
                FirstName = "Test",
                LastName = "tests"
            };
        Assert.Equal(1, reviewer.Id);
        Assert.Equal("Test", reviewer.FirstName);
        Assert.Equal("tests" ,reviewer.LastName);
        Assert.IsType<ReviewerDto>(reviewer);
    }
    [Fact]
    public void CategoryDto_CreateCategoryDtoInstance_ReturnsCategoryDto()
    {
        // Arrange
        var category = new CategoryDto{
                Id = 1,
                Name = "test"
            };
        Assert.Equal(1, category.Id);
        Assert.Equal("test", category.Name);
        Assert.IsType<CategoryDto>(category);
    }
    [Fact]
    public void CountryDto_CreateCountryDtoInstance_ReturnsCountryDto()
    {
        // Arrange
        var country = new CountryDto{
                Id = 1,
                Name = "test"
            };
        Assert.Equal(1, country.Id);
        Assert.Equal("test", country.Name);
        Assert.IsType<CountryDto>(country);
    }
    [Fact]
    public void OwnerDto_CreateOwnerDtoInstance_ReturnsOwnerDto()
    {
        // Arrange
        var owner = new OwnerDto{
                Id = 1,
                Name = "test",
                Surname = "test"
            };
        Assert.Equal(1, owner.Id);
        Assert.Equal("test", owner.Name);
        Assert.Equal("test", owner.Surname);
        Assert.IsType<OwnerDto>(owner);
    }
}