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
        var id = 556;
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
    [Fact]
    public void GetCategory_ShouldReturnCategory_WhenGivenCorrectCategoryId()
    {
        // Arrange
        var id = 1;
        // Act
        var result = _repository.GetCategory(id);
        // Assert
        Assert.Equal("Category 1", result.Name);
    }
    [Fact]
    public void GetCategory_ShouldReturnNull_WhenGivenInCorrectCategoryId()
    {
        // Arrange
        var id = 555;
        // Act
        var result = _repository.GetCategory(id);
        // Assert
            result.Should().BeNull();
    }

    [Fact]
    public void CreateCategory_ShouldReturnTrue_WhenGivenCategory()
    {
        // Arrange
        var category = new Category { Id = 30, Name = "Category 2", CarCategories = new List<CarCategory>() };
        // Act
        var result = _repository.CreateCategory(category);
        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void UpdateCategory_ShouldReturnTrue_WhenGivenCategory()
    {
        // Arrange
        var category = new Category { Id = 5, Name = "Category 2", CarCategories = new List<CarCategory>() };
        // Act
        var result = _repository.CreateCategory(category);
        var resultOfUpdate = _repository.UpdateCategory(category);
        // Assert
        resultOfUpdate.Should().BeTrue();
    }
    [Fact]
    public void DeleteCategory_ShouldReturnTrue_WhenGivenCategory()
    {
        // Arrange
        var category = new Category { Id = 11, Name = "Category 2", CarCategories = new List<CarCategory>() };
        // Act
        var result = _repository.CreateCategory(category);
        var resultOfDelete = _repository.DeleteCategory(category);
        // Assert
        resultOfDelete.Should().BeTrue();
    }
    [Fact]
    public void GetCategoryTrimToUpper_ShouldReturnCategory_WhenGivenCategoryDto()
    {
        // Arrange
        var categoryDto = new CategoryDto { Name = "Category 2"};
        // Act
        var result = _repository.GetCategoryTrimToUpper(categoryDto);
        // Assert
        Assert.Equal("Category 2", result.Name);
        Assert.Equal(2, result.Id);
        result.Should().BeOfType<Category>();
    }
}