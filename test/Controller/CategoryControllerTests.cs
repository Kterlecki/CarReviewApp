using System.Collections.Generic;
using System.Linq;
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
   [Fact]
   public void CategoryController_GetCategories_ReturnsOk()
   {
        //  Arrange
        var carCategoryOne = new CarCategory(){CarId = 1, CategoryId = 1, Car = new Car(), Category = new Category()};
        var carCategoryTwo = new CarCategory(){CarId = 2, CategoryId = 2, Car = new Car(), Category = new Category()};
        var categoryDtoList = new List<CategoryDto>(){
            new CategoryDto{ Id = 1, Name = "Sport" },
            new CategoryDto{ Id = 1, Name = "Sport" }};
        var categoryList = new List<Category>(){
            new Category{Id = 1, Name = "Sport", CarCategories = new List<CarCategory>{carCategoryOne}},
            new Category{Id = 1, Name = "Sport", CarCategories = new List<CarCategory>{carCategoryTwo}}};
        var categoryRepository = new Mock<ICategoryRepository>();
        var mapper = new Mock<IMapper>();
        var categoryController = new CategoryController(categoryRepository.Object, mapper.Object);
        categoryRepository.Setup(c => c.GetCategories()).Returns(categoryList);
        mapper.Setup(m => m.Map<List<CategoryDto>>(categoryList)).Returns(categoryDtoList);
        // Act
        var result = categoryController.GetCategories() as OkObjectResult;
        // Assert
        var returnedCategories = result.Value as IEnumerable<CategoryDto>;

        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.Equal(categoryDtoList.Count, returnedCategories.Count());
        Assert.Equal(categoryList[0].Name, returnedCategories.ElementAtOrDefault(0).Name);
   }
   [Fact]
   public void CategoryController_GetCategory_ReturnsOk()
   {
        //  Arrange
        var id = 1;
        var carCategory = new CarCategory(){CarId = 1, CategoryId = 1, Car = new Car(), Category = new Category()};
        var categoryDto = new CategoryDto(){Id = 1, Name = "Sport"};
        var category = new Category(){Id = 1, Name = "Sport", CarCategories = new List<CarCategory>{carCategory}};
        var categoryRepository = new Mock<ICategoryRepository>();
        var mapper = new Mock<IMapper>();
        var categoryController = new CategoryController(categoryRepository.Object, mapper.Object);
        categoryRepository.Setup(c => c.GetCategory(It.IsAny<int>())).Returns(category);
        categoryRepository.Setup(c => c.CategoryExists(It.IsAny<int>())).Returns(true);
        mapper.Setup(m => m.Map<CategoryDto>(category)).Returns(categoryDto);
        // Act
        var result = categoryController.GetCategory(id);
        // Assert
        var restwo = result as OkObjectResult;
        Assert.NotNull(result);
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedCategoryDto = Assert.IsType<CategoryDto>(okResult.Value);
        Assert.Equal(categoryDto.Name, returnedCategoryDto.Name);
   }

}