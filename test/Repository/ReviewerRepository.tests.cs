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

public class ReviewerRepositoryTests
{
    private readonly DataContext _context;
    private readonly ReviewerRepository _repository;

    public ReviewerRepositoryTests()
    {
        _context = GetDbContext();
        _repository = new ReviewerRepository(_context);
    }
    private static DataContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "test")
            .Options;

            var databaseContext = new DataContext(options);
            databaseContext.Database.EnsureCreated();
            if (!databaseContext.Reviewers.Any())
            {
                    databaseContext.Reviewers.AddRange(
                        new Reviewer
                        {
                            Id = 10,
                            FirstName = "Reviewer 1",
                            LastName = "Test ",
                            Reviews = new List<Review> { new Review { Id = 10, Title = "John", Description = "Doe", Rating = 2 } },
                         },
                        new Reviewer
                        {
                            Id = 11,
                            FirstName = "Reviewer 1",
                            LastName = "Test ",
                            Reviews = new List<Review>{new Review {Id = 11, Title = "John", Description = "Doe", Rating = 2} },
                         }
                            );
                    databaseContext.Reviews.Add(
                        new Review { Id = 20, Title = "Review 1", Description = "Test Desc", Rating = 3,
                        Reviewer = new Reviewer(),
                        Car = new Car() }

                    );
                    databaseContext.SaveChanges();
            }
            return databaseContext;
        }
    [Fact]
    public void GetReviewer_WhenInvokedWithIdThatExists_ReturnsReviewer()
    {
        // Arrange
        int id = 10;
        // Act
        var result = _repository.GetReviewer(id);
        // Assert
        Assert.IsType<Reviewer>(result);
        Assert.Equal("Reviewer 1", result.FirstName);
    }
    [Fact]
    public void GetReviewers_WhenInvoked_ReturnsReviewerList()
    {
        // Arrange
        // Act
        var result = _repository.GetReviewers();
        // Assert
        Assert.IsType<List<Reviewer>>(result);
    }
    [Fact]
    public void GetReviewsByReviewer_GivenCorrectId_ReturnsReviewList()
    {
        // Arrange
        var id = 10;
        // Act
        var result = _repository.GetReviewsByReviewer(id);
        // Assert
        Assert.IsType<List<Review>>(result);
        Assert.Equal(1, result.Count);
    }
    [Fact]
    public void ReviewerExists_GivenCorrectId_ReturnsTrue()
    {
        // Arrange
        var id = 10;
        // Act
        var result = _repository.ReviewerExists(id);
        // Assert
        Assert.True(result);
    }
    [Fact]
    public void CreateReviewer_GivenCorrectReviewer_ReturnsTrue()
    {
        // Arrange
        var reviewer = new Mock<Reviewer>();
        // Act
        var result = _repository.CreateReviewer(reviewer.Object);
        // Assert
        Assert.True(result);
    }
    [Fact]
    public void UpdateReviewer_GivenCorrectReviewer_ReturnsTrue()
    {
        // Arrange
        var reviewer = new Mock<Reviewer>();
        // Act
        var result = _repository.UpdateReviewer(reviewer.Object);
        // Assert
        Assert.True(result);
    }
    [Fact]
    public void DeleteReviewer_GivenCorrectReviewer_ReturnsTrue()
    {
        // Arrange
        var reviewer = new Mock<Reviewer>();
        // Act
        var create = _repository.CreateReviewer(reviewer.Object);
        var result = _repository.DeleteReviewer(reviewer.Object);
        // Assert
        Assert.True(result);
    }
}