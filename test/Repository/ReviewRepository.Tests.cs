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
                        new Review { Id = 1, Title = "Review 1", Description = "Test Desc", Rating = 3
                        Reviewer = new Reviewer{ Id = 1, FirstName = "John", LastName = "Doe"},
                        Car = new Car { Id = 1, Make = "Audi", Model = "A5", YearBuilt = 2021} }
                            );
                    databaseContext.Reviews.Add(
                        new Owner { Id = 3, Name = "Audi", Surname = "VanB"}
                    );
                    databaseContext.SaveChanges();
            }
            return databaseContext;
        }
}