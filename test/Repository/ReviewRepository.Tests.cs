using System.Linq;
using Xunit;
using FluentAssertions;
using CarReviewApp.Data;
using CarReviewApp.Repository;
using Microsoft.EntityFrameworkCore;
using CarReviewApp.Models;
using System.Collections.Generic;
using CarReviewApp.Dto;
using Moq;

namespace CarReviewApp.tests.Repository;

public class ReviewRepositoryTests
{
    private readonly DataContext _context;
    private readonly ReviewRepository _repository;

    public ReviewRepositoryTests()
    {
        _context = GetDbContext();
        _repository = new ReviewRepository(_context);
    }
    private static DataContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "test")
            .Options;

            var databaseContext = new DataContext(options);
            databaseContext.Database.EnsureCreated();
            if (!databaseContext.Reviews.Any())
            {
                    databaseContext.Reviews.AddRange(
                        new Review { Id = 1, Title = "Review 1", Description = "Test Desc", Rating = 3,
                        Reviewer = new Reviewer{ Id = 1, FirstName = "John", LastName = "Doe"},
                        Car = new Car { Id = 1, Make = "Audi", Model = "A5", YearBuilt = 2021} }
                            );
                    databaseContext.Cars.Add(
                        new Car { Id = 2, Make = "Bmw", Model = "A5", YearBuilt = 2021}
                    );
                    databaseContext.SaveChanges();
            }
            return databaseContext;
        }
    [Fact]
    public void GetReviews_WhenInvoked_ReturnsReviewList()
    {
        // Arrange

        // Act
        var result = _repository.GetReviews();
        // Assert
        Assert.IsType<List<Review>>(result);
        Assert.Equal(1, result.Count);
    }
    [Fact]
    public void GetReview_WhenInvoked_ReturnsReviewInstance()
    {
        // Arrange
        int id = 1;
        // Act
        var result = _repository.GetReview(id);
        // Assert
        Assert.IsType<Review>(result);
        Assert.Equal("Review 1", result.Title);
    }
    [Fact]
    public void GetReviewsOfACar_WhenInvoked_ReturnsReviewList()
    {
        // Arrange
        int id = 1;
        // Act
        var result = _repository.GetReviewsOfACar(id);
        // Assert
        Assert.IsType<List<Review>>(result);
        Assert.Equal(1, result.Count);
    }
    [Fact]
    public void ReviewExists_WhenInvoked_ReturnsTrue()
    {
        // Arrange
        int id = 1;
        // Act
        var result = _repository.ReviewExists(id);
        // Assert
        Assert.True(result);
    }
    [Fact]
    public void CreateReview_WhenInvoked_ReturnsTrue()
    {
        // Arrange
        var review = new Review
        {
            Id = 55,
            Title = "TestCreateMethod",
            Description = "Test Creation",
            Rating = 4
        };
        // Act
        var result = _repository.CreateReview(review);
        // Assert
        Assert.True(result);
    }
    [Fact]
    public void UpdateReview_WhenInvoked_ReturnsTrue()
    {
        // Arrange
        var review = new Mock<Review>();
        // Act
        var result = _repository.UpdateReview(review.Object);
        // Assert
        Assert.True(result);
    }
    [Fact]
    public void DeleteReview_WhenInvoked_ReturnsTrue()
    {
        // Arrange
        var review = new Mock<Review>();
        review.Object.Id = 55;
        // Act
        var create = _repository.CreateReview(review.Object);
        var result = _repository.DeleteReview(review.Object);
        // Assert
        Assert.True(result);
    }
    [Fact]
    public void GetCarTrimToUpper_WhenInvoked_ReturnsTrue()
    {
        // Arrange
        var review = new Mock<ReviewDto>();
        review.Object.Title = "Review 1";
        // Act
        var result = _repository.GetCarTrimToUpper(review.Object);
        // Assert
        Assert.Equal("Review 1", result.Title);
    }
}