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
    public void CarController_GetCars_ReturnsModelStateError()
    {
        //Arrange
        var cars = A.Fake<ICollection<CarDto>>();
        var carList = A.Fake<List<CarDto>>();
        A.CallTo(() => _mapper.Map<List<CarDto>>(cars)).Returns(carList);
        var controller = new CarController(_carRepository, _reviewRepository, _mapper);
        controller.ModelState.AddModelError("List", "List is Error");
        //Act
        var result = controller.GetCars();
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<BadRequestObjectResult>();
        // result.Should().BeOfType(typeof(BadRequestObjectResult));
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
        A.CallTo(() => _carRepository.GetCarTrimToUpper(carCreate)).Returns(null);
        A.CallTo(() => _mapper.Map<Car>(carCreate)).Returns(car);
        A.CallTo(() => _carRepository.CreateCar(ownerId, catId, car)).Returns(true);
        var controller = new CarController(_carRepository, _reviewRepository, _mapper);

        //Act
        var result = controller.CreateCar(ownerId, catId, carCreate);
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
    }
    [Fact]
    public void CarController_CreateCarWithFalse_ReturnsObjectResult()
    {
        //Arrange
        int ownerId = 1;
        int catId = 2;
        var car = A.Fake<Car>();
        var carCreate = A.Fake<CarDto>();
        var cars = A.Fake<ICollection<CarDto>>();
        var carList = A.Fake<IList<CarDto>>();
        A.CallTo(() => _carRepository.GetCarTrimToUpper(carCreate)).Returns(null);
        A.CallTo(() => _mapper.Map<Car>(carCreate)).Returns(car);
        A.CallTo(() => _carRepository.CreateCar(ownerId, catId, car)).Returns(false);
        var controller = new CarController(_carRepository, _reviewRepository, _mapper);

        //Act
        var result = controller.CreateCar(ownerId, catId, carCreate);
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ObjectResult>();
        result.As<ObjectResult>().StatusCode.Should().Be(500);
    }

    [Fact]
    public void CarController_CreateCar_ReturnsModelStateError()
    {
        //Arrange
        int ownerId = 1;
        int catId = 2;
        var car = A.Fake<Car>();
        var carCreate = A.Fake<CarDto>();
        var cars = A.Fake<ICollection<CarDto>>();
        var carList = A.Fake<IList<CarDto>>();
        A.CallTo(() => _carRepository.GetCarTrimToUpper(carCreate)).Returns(null);
        var controller = new CarController(_carRepository, _reviewRepository, _mapper);
        controller.ModelState.AddModelError("List", "List is Error");
        //Act
        var result = controller.CreateCar(ownerId, catId, carCreate);
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public void CarController_CreateCarWithNullCar_ReturnsModelStateError()
    {
        //Arrange
        int ownerId = 1;
        int catId = 2;
        var car = A.Fake<Car>();
        var carCreate = A.Fake<CarDto>();
        A.CallTo(() => _carRepository.GetCarTrimToUpper(carCreate)).Returns(car);
        var controller = new CarController(_carRepository, _reviewRepository, _mapper);

        //Act
        var result = controller.CreateCar(ownerId, catId, carCreate);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ObjectResult>();
        result.As<ObjectResult>().StatusCode.Should().Be(422);
    }
     [Fact]
    public void CarController_CreateCarWithNullCarCreate_ReturnsModelStateError()
    {
        //Arrange
        int ownerId = 1;
        int catId = 1;
        var car = A.Fake<Car>();
        var carCreate = A.Fake<CarDto>();
        carCreate = null;
        var controller = new CarController(_carRepository, _reviewRepository, _mapper);

        //Act
        var result = controller.CreateCar(ownerId, catId, carCreate);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public void CarController_CreateCarWithCarExisting_ReturnsModelStateError()
    {
        //Arrange
        int ownerId = 1;
        int catId = 2;
        var car = A.Fake<Car>();
        var carCreate = A.Fake<CarDto>();
        var cars = A.Fake<ICollection<CarDto>>();
        var carList = A.Fake<IList<CarDto>>();
        A.CallTo(() => _carRepository.GetCarTrimToUpper(carCreate)).Returns(car);
        var controller = new CarController(_carRepository, _reviewRepository, _mapper);
        //Act
        var result = controller.CreateCar(ownerId, catId, carCreate);
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ObjectResult>();
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

    [Fact]
    public void CarController_UpdateCar_ReturnsNoContent()
    {
        //Arrange
        var carId = 1;
        var catId = 1;
        var ownerId = 1;
        var updateCar = A.Fake<CarDto>();
        var carMap = A.Fake<Car>();
        updateCar.Id = 1;
        A.CallTo(() => _carRepository.CarExists(carId)).Returns(true);
        A.CallTo(() => _mapper.Map<Car>(updateCar)).Returns(carMap);
        A.CallTo(() => _carRepository.UpdateCar(ownerId, catId, carMap)).Returns(true);
        var controller = new CarController(_carRepository, _reviewRepository, _mapper);
        //Act
        var result = controller.UpdateCar(carId, catId, ownerId, updateCar);
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NoContentResult>();
    }
    [Fact]
    public void CarController_UpdateCarValueNull_ReturnsBadRequest()
    {
        //Arrange
        var carId = 1;
        var catId = 1;
        var ownerId = 1;
        CarDto updateCar = null;
        var controller = new CarController(_carRepository, _reviewRepository, _mapper);
        //Act
        var result = controller.UpdateCar(carId, catId, ownerId, updateCar);
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<BadRequestObjectResult>();
        // result.Should().BeOfType<BadRequestObjectResult>(); ---- can be written like this
    }
    [Fact]
    public void CarController_UpdateCarCarIdNotMatching_ReturnsBadRequest()
    {
        //Arrange
        var carId = 1;
        var catId = 1;
        var ownerId = 1;
        var updateCar = A.Fake<CarDto>();
        var controller = new CarController(_carRepository, _reviewRepository, _mapper);
        //Act
        var result = controller.UpdateCar(carId, catId, ownerId, updateCar);
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<BadRequestObjectResult>();
        // result.Should().BeOfType<BadRequestObjectResult>(); ---- can be written like this
    }

    [Fact]
    public void CarController_UpdateCarCarExist_ReturnsNotFound()
    {
        //Arrange
        var carId = 1;
        var catId = 1;
        var ownerId = 1;
        var updateCar = A.Fake<CarDto>();
        updateCar.Id = 1;
        A.CallTo(() => _carRepository.CarExists(carId)).Returns(false);
        var controller = new CarController(_carRepository, _reviewRepository, _mapper);
        //Act
        var result = controller.UpdateCar(carId, catId, ownerId, updateCar);
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NotFoundResult>();
        // result.Should().BeOfType<BadRequestObjectResult>(); ---- can be written like this
    }

    [Fact]
    public void CarController_UpdateCarModeStateInvalid_ReturnsBadRequest()
    {
        //Arrange
        var carId = 1;
        var catId = 1;
        var ownerId = 1;
        var updateCar = A.Fake<CarDto>();
        var carMap = A.Fake<Car>();
        updateCar.Id = 1;
        A.CallTo(() => _carRepository.CarExists(carId)).Returns(true);
        var controller = new CarController(_carRepository, _reviewRepository, _mapper);
        controller.ModelState.AddModelError("", "Error");
        //Act
        var result = controller.UpdateCar(carId, catId, ownerId, updateCar);
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<BadRequestResult>();
    }
    [Fact]
    public void CarController_DeleteCar_ReturnsNoContent()
    {
        //Arrange
        var carId = 1;
        var review = A.Fake<ICollection<Review>>();
        var car = A.Fake<Car>();

        A.CallTo(() => _carRepository.CarExists(carId)).Returns(true);
        A.CallTo(() => _reviewRepository.GetReviewsOfACar(carId)).Returns(review);
        A.CallTo(() => _carRepository.GetCar(carId)).Returns(car);
        A.CallTo(() => _reviewRepository.DeleteReviews(review.ToList())).Returns(true);
        A.CallTo(() => _carRepository.DeleteCar(car)).Returns(true);
        var controller = new CarController(_carRepository, _reviewRepository, _mapper);

        //Act
        var result = controller.DeleteCar(carId);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NoContentResult>();
    }
    [Fact]
    public void CarController_DeleteCarDeleteSetToFalse_ReturnsModelError()
    {
        //Arrange
        var carId = 1;
        var review = A.Fake<ICollection<Review>>();
        var car = A.Fake<Car>();

        A.CallTo(() => _carRepository.CarExists(carId)).Returns(true);
        A.CallTo(() => _reviewRepository.GetReviewsOfACar(carId)).Returns(review);
        A.CallTo(() => _carRepository.GetCar(carId)).Returns(car);
        A.CallTo(() => _reviewRepository.DeleteReviews(review.ToList())).Returns(true);
        A.CallTo(() => _carRepository.DeleteCar(car)).Returns(false);
        var controller = new CarController(_carRepository, _reviewRepository, _mapper);
        //Act
        var result = controller.DeleteCar(carId);
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NoContentResult>();
    }
}