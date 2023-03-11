using System;
using CarReviewApp.Data;
using Microsoft.EntityFrameworkCore;

namespace CarReviewApp.tests;

public class TestFixture : IDisposable
{
    public DataContext context {get; private set;}
    public TestFixture()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
        .UseInMemoryDatabase(databaseName: "TestDatabase")
        .Options;

        context = new DataContext(options);

        SeedData();
    }

    private void SeedData()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        context.Dispose();
    }
}