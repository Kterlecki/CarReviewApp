
using System.Linq;
using Xunit;
using FluentAssertions;
using CarReviewApp.Data;
using CarReviewApp.Repository;
using Microsoft.EntityFrameworkCore;
using CarReviewApp.Models;
using System.Collections.Generic;
using CarReviewApp.Dto;

namespace CarReviewApp.tests.Repository;

public class CountryRepositoryTests
{
    private readonly DataContext _context;
    private readonly CountryRepository _repository;

    public CountryRepositoryTests()
    {
        _context = GetDbContext();
        _repository = new CountryRepository(_context);
    }
    private static DataContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "test")
            .Options;

            var databaseContext = new DataContext(options);
            databaseContext.Database.EnsureCreated();
            if (!databaseContext.Countries.Any())
            {
                    databaseContext.Countries.AddRange(
                        new Country { Id = 1, Name = "Country 1",
                        Owners = new List<Owner>{ new Owner {
                            Id = 1, Name = "Bob"
                        }} },
                        new Country { Id = 2, Name = "Country 2",
                        Owners = new List<Owner>() }
                            );
                    databaseContext.Owners.Add(
                        new Owner { Id = 3, Name = "Audi", Surname = "VanB"}
                    );
                    databaseContext.SaveChanges();
            }
            return databaseContext;
        }
    [Fact]
    public void CountryExists_GivenCorrectId_ReturnsTrue()
    {
        // Arrange
        var id = 1;
        // Act
        var result = _repository.CountryExists(id);
        // Assert
        result.Should().BeTrue();
    }
    [Fact]
    public void GetCountries_WhenCalled_ReturnsListOfCountries()
    {
        // Arrange
        // Act
        var result = _repository.GetCountries();
        // Assert
        result.Should().BeOfType<List<Country>>();
        Assert.Equal(2, result.Count);
    }
    [Fact]
    public void GetCountry_GivenCorrectId_ReturnsCountry()
    {
        // Arrange
        var id = 1;
        // Act
        var result = _repository.GetCountry(id);
        // Assert
        result.Should().BeOfType<Country>();
        Assert.Equal("Country 1", result.Name);
    }
}