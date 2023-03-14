using AutoMapper;
using CarReviewApp.Controllers;
using CarReviewApp.Dto;
using CarReviewApp.Interfaces;
using CarReviewApp.Models;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CarReviewApp.tests.Controller;

public class CarControllerTests
{
    private readonly ICarRepository _carRepository;
    private readonly IReviewRepository _reviewRepository;
    private readonly IMapper _mapper;
    public CarControllerTests()
    {
        _carRepository = A.Fake<ICarRepository>();
        _reviewRepository = A.Fake<IReviewRepository>();
        _mapper = A.Fake<IMapper>();
    }

    [Fact]
    public void CarController_GetCars_ReturnsOk()
    {
        //Arrange
        var cars = A.Fake<ICollection<CarDto>>();
        var carList = A.Fake<List<CarDto>>();
        A.CallTo(() => _mapper.Map<List<CarDto>>(cars)).Returns(carList);
        var controller = new CarController(_carRepository, _reviewRepository, _mapper);

        //Act
        var result = controller.GetCars();

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));
    }

    [Fact]
    public void CarController_CreateCar_ReturnsOk()
    {
        //Arrange
        int ownerId = 1;
        int catId = 2;
        var car = A.Fake<Car>();
        var carCreate = A.Fake<CarDto>();
        var cars = A.Fake<ICollection<CarDto>>();
        var carList = A.Fake<IList<CarDto>>();
        A.CallTo(() => _carRepository.GetCarTrimToUpper(carCreate)).Returns(car);
        A.CallTo(() => _mapper.Map<Car>(carCreate)).Returns(car);
        A.CallTo(() => _carRepository.CreateCar(ownerId, catId, car)).Returns(true);
        var controller = new CarController(_carRepository, _reviewRepository, _mapper);

        //Act
        var result = controller.CreateCar(ownerId, catId, carCreate);
        //Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public void CarController_GetCar_ReturnsOk()
    {
        //Arrange
        int id = 1;
        var car = A.Fake<Car>();
        A.CallTo(() => _carRepository.CarExists(id)).Returns(true);
        A.CallTo(() => _mapper.Map<Car>(_carRepository.GetCar(id))).Returns(car);
        A.CallTo(() => _carRepository.GetCar(id)).Returns(car);
        var controller = new CarController(_carRepository, _reviewRepository, _mapper);

        //Act
        var result = controller.GetCar(id);
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));
    }
    [Fact]
    public void CarController_GetCar_ReturnsNotFound()
    {
        //Arrange
        int id = 1;
        A.CallTo(() => _carRepository.CarExists(id)).Returns(false);
        var controller = new CarController(_carRepository, _reviewRepository, _mapper);

        //Act
        var result = controller.GetCar(id);
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundResult));
    }
    [Fact]
    public void CarController_GetCar_ReturnsModelStateError()
    {
        //Arrange
        var id = 1;
        A.CallTo(() => _carRepository.CarExists(id)).Returns(true);
        var controller = new CarController(_carRepository, _reviewRepository, _mapper);
        controller.ModelState.AddModelError("make", "Make is required");
        //Act
        var result = controller.GetCar(id);
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        // result.Should().BeOfType<BadRequestObjectResult>(); ---- can be written like this
    }

    [Fact]
    public void CarController_GetCarRating_ReturnsOk()
    {
        //Arrange
        int id = 1;
        A.CallTo(() => _carRepository.CarExists(id)).Returns(true);
        A.CallTo(() => _carRepository.GetCarRating(id)).Returns(id);

        var controller = new CarController(_carRepository, _reviewRepository, _mapper);

        //Act
        var result = controller.GetCarRating(id);
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));
    }

    [Fact]
    public void CarController_GetCarRating_ReturnsNotFound()
    {
        //Arrange
        int id = 1;
        A.CallTo(() => _carRepository.CarExists(id)).Returns(false);
        var controller = new CarController(_carRepository, _reviewRepository, _mapper);

        //Act
        var result = controller.GetCarRating(id);
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundResult));
    }
    [Fact]
    public void CarController_GetCarRating_ReturnsModelStateError()
    {
        //Arrange
        var id = 1;
        A.CallTo(() => _carRepository.CarExists(id)).Returns(true);
        var controller = new CarController(_carRepository, _reviewRepository, _mapper);
        controller.ModelState.AddModelError("make", "Make is required");
        //Act
        var result = controller.GetCarRating(id);
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        // result.Should().BeOfType<BadRequestObjectResult>(); ---- can be written like this
    }
}