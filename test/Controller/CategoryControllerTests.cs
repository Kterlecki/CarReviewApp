using AutoMapper;
using CarReviewApp.Controllers;
using CarReviewApp.Data;
using CarReviewApp.Dto;
using CarReviewApp.Interfaces;
using CarReviewApp.Models;
using CarReviewApp.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CarReviewApp.tests.Controller;

public class CategoryControllerTests
{
   [Fact]
   public void CategoryController_CreateCategory_ReturnsOk()
   {
        //  Arrange
        var categoryDto = new CategoryDto(){Id = 1, Name = "Sport"};
        var categoryRepository = new Mock<ICategoryRepository>();
        var mapper = new Mock<IMapper>();
        var categoryController = new CategoryController(categoryRepository.Object, mapper.Object);
        categoryRepository.Setup(c => c.CreateCategory(It.IsAny<Category>())).Returns(true);
        // Act
        var result = categoryController.CreateCategory(categoryDto);
        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
   }
}