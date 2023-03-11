using AutoMapper;
using CarReviewApp.Controllers;
using CarReviewApp.Data;
using CarReviewApp.Repository;
using Moq;
using Xunit;

namespace CarReviewApp.tests.Controller.tests;

public class CategoryControllerTests
{
    [Fact]
    public void ControllerGetCategories_RunCorrectly_ReturnsCategories()
    {
        var context = new Mock<DataContext>();
        var categoryRepository = new CategoryRepository(context.Object);
        var mapper = new Mock<IMapper>();
        var categoryController = new CategoryController(categoryRepository, mapper.Object);
        
    }
}