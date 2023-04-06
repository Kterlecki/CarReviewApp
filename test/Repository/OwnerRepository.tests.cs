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

public class OwnerRepositoryTests
{
    private readonly DataContext _context;
    private readonly OwnerRepository _repository;

    public OwnerRepositoryTests()
    {
        _context = GetDbContext();
        _repository = new OwnerRepository(_context);
    }
    private static DataContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "test")
            .Options;

            var databaseContext = new DataContext(options);
            databaseContext.Database.EnsureCreated();
            if (!databaseContext.Owners.Any())
            {
                    databaseContext.Owners.AddRange(
                        new Owner { Id = 1, Name = "Review 1", Surname = "Test Desc",
                        Country = new Country{ Id = 1, Name = "Ireland", Owners = new List<Owner>()},
                        CarOwners = new List<CarOwner>() }
                            );
                    databaseContext.SaveChanges();
            }
            return databaseContext;
        }
    [Fact]
    public void GetCarByOwner_GivenCorrectId_ReturnCarList()
    {
        // Arrange
        var id = 1;
        // Act
        var result = _repository.GetCarByOwner(id);
        // Assert
        Assert.IsType<List<Car>>(result);
    }
    [Fact]
    public void GetOwner_GivenCorrectId_ReturnOwner()
    {
        // Arrange
        var id = 1;
        // Act
        var result = _repository.GetOwner(id);
        // Assert
        Assert.IsType<Owner>(result);
        Assert.Equal("Review 1", result.Name);
    }
    [Fact]
    public void GetOwnerOfACar_GivenCorrectId_ReturnOwner()
    {
        // Arrange
        var id = 1;
        // Act
        var result = _repository.GetOwnerOfACar(id);
        // Assert
        Assert.IsType<List<Owner>>(result);
    }
    [Fact]
    public void GetOwners_WhenCalled_ReturnOwnerList()
    {
        // Arrange
        // Act
        var result = _repository.GetOwners();
        // Assert
        Assert.IsType<List<Owner>>(result);
    }
    [Fact]
    public void OwnerExists_GivenCorrectId_ReturnTrue()
    {
        // Arrange
        var id = 1;
        // Act
        var result = _repository.OwnerExists(id);
        // Assert
        Assert.True(result);
    }
    [Fact]
    public void CreateOwner_GivenCorrectId_ReturnTrue()
    {
        // Arrange
        var create = new Mock<Owner>();
        // Act
        var result = _repository.CreateOwner(create.Object);
        // Assert
        Assert.True(result);
    }
    [Fact]
    public void UpdateOwner_GivenCorrectId_ReturnTrue()
    {
        // Arrange
        var create = new Mock<Owner>();
        // Act
        var result = _repository.UpdateOwner(create.Object);
        // Assert
        Assert.True(result);
    }
    [Fact]
    public void DeleteOwner_GivenCorrectId_ReturnTrue()
    {
        // Arrange
        var owner = new Mock<Owner>();
        // Act
        var create = _repository.CreateOwner(owner.Object);
        var result = _repository.DeleteOwner(owner.Object);
        // Assert
        Assert.True(result);
    }
}