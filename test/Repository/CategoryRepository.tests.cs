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
using Microsoft.EntityFrameworkCore.ChangeTracking;
using CarReviewApp.Dto;
namespace CarReviewApp.tests.Repository;

public class CategoryRepositoryTests
{
    private readonly DataContext _context;
    private readonly CategoryRepository _repository;

    public CategoryRepositoryTests()
    {
        _context = GetDbContext();
        _repository = new CategoryRepository(_context);
    }
    private static DataContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "test")
            .Options;

            var databaseContext = new DataContext(options);
            databaseContext.Database.EnsureCreated();
            if (!databaseContext.Categories.Any())
            {
                    databaseContext.Categories.AddRange(
                        new Category { Id = 1, Name = "Category 1",
                        CarCategories = new List<CarCategory>{ new CarCategory {
                            CarId = 1, CategoryId = 1
                        }} },
                        new Category { Id = 2, Name = "Category 2", CarCategories = new List<CarCategory>() }
                            );
                    databaseContext.Cars.Add(
                        new Car { Id = 1, Make = "Audi", Model = "A7", YearBuilt = 2015}
                    );
                    databaseContext.SaveChanges();
            }
            return databaseContext;
        }
    [Fact]
    public void CategoryExists_ShouldReturnTrue_WhenCarExists()
    {
        // Arrange
        var id = 1;
        // Act
        var result = _repository.CategoryExists(id);
        // Assert
        result.Should().BeTrue();
    }
    [Fact]
    public void CategoryExists_ShouldReturnFalse_WhenCarDoesntExists()
    {
        // Arrange
        var id = 5;
        // Act
        var result = _repository.CategoryExists(id);
        // Assert
        result.Should().BeFalse();
    }
    [Fact]
    public void GetCarByCategory_ShouldReturnCarList_WhenGivenCorrectCategoryId()
    {
        // Arrange
        var id = 1;
        // Act
        var result = _repository.GetCarByCategory(id);
        // Assert
        Assert.Equal(1, result.Count);
        result.Should().BeOfType<List<Car>>();
    }

    [Fact]
    public void GetCategories_ShouldReturnCategoryList_WhenExecuted()
    {
        // Act
        var result = _repository.GetCategories();
        // Assert
        Assert.Equal(2, result.Count);
        result.Should().BeOfType<List<Category>>();
    }
}