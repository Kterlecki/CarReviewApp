
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
    private readonly CategoryRepository _repository;

    public CountryRepositoryTests()
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
                        new Owner { Id = 1, Name = "Audi"}
                    );
                    databaseContext.SaveChanges();
            }
            return databaseContext;
        }
    
}