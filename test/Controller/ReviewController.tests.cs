using AutoMapper;
using CarReviewApp.Controllers;
using CarReviewApp.Dto;
using CarReviewApp.Interfaces;
using CarReviewApp.Models;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

namespace CarReviewApp.tests.Controller;

public class ReviewControllerTests
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IMapper _mapper;
    private readonly IReviewerRepository _reviewerRepository;
    private readonly ICarRepository _carRepository;

    public ReviewControllerTests()
    {
        _reviewRepository = A.Fake<IReviewRepository>();
        _mapper = A.Fake<IMapper>();
        _reviewerRepository = A.Fake<IReviewerRepository>();
        _carRepository = A.Fake<ICarRepository>();
    }

    [Fact]
    public void ReviewController_GetReviews_ReturnsOk()
    {
        var reviewList = A.Fake<List<ReviewDto>>();
        // Arrange
        A.CallTo(() => _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews())).Returns(reviewList);
        var controller = new ReviewController(_reviewerRepository,_carRepository, _reviewRepository, _mapper);
        // Act
        var result = controller.GetReviews();
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
    }
    [Fact]
    public void ReviewController_GetReviewsModelStateIsInvalid_ReturnsBadRequest()
    {
        var reviewList = A.Fake<List<ReviewDto>>();
        // Arrange
        A.CallTo(() => _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews())).Returns(reviewList);
        var controller = new ReviewController(_reviewerRepository,_carRepository, _reviewRepository, _mapper);
        controller.ModelState.AddModelError("", "Error");
        // Act
        var result = controller.GetReviews();
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<BadRequestObjectResult>();
    }
    [Fact]
    public void ReviewController_GetReview_ReturnsOk()
    {
        var reviewId = 1;
        var review = A.Fake<ReviewDto>();
        // Arrange
        A.CallTo(() => _reviewRepository.ReviewExists(reviewId)).Returns(true);
        A.CallTo(() => _mapper.Map<ReviewDto>(_reviewRepository.GetReview(reviewId))).Returns(review);
        var controller = new ReviewController(_reviewerRepository,_carRepository, _reviewRepository, _mapper);
        // Act
        var result = controller.GetReview(reviewId);
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
    }
    [Fact]
    public void ReviewController_GetReviewModelStateIsInvalid_ReturnsBadRequest()
    {
        var reviewId = 1;
        var review = A.Fake<ReviewDto>();
        // Arrange
        A.CallTo(() => _reviewRepository.ReviewExists(reviewId)).Returns(true);
        A.CallTo(() => _mapper.Map<ReviewDto>(_reviewRepository.GetReview(reviewId))).Returns(review);
        var controller = new ReviewController(_reviewerRepository,_carRepository, _reviewRepository, _mapper);
        controller.ModelState.AddModelError("", "Error");
        // Act
        var result = controller.GetReview(reviewId);
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<BadRequestObjectResult>();
    }
    [Fact]
    public void ReviewController_GetReviewForACar_ReturnsOk()
    {
        var reviewId = 1;
        var reviewList = A.Fake<List<ReviewDto>>();
        // Arrange
        A.CallTo(() => _reviewRepository.ReviewExists(reviewId)).Returns(true);
        A.CallTo(() => _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewsOfACar(reviewId))).Returns(reviewList);
        var controller = new ReviewController(_reviewerRepository,_carRepository, _reviewRepository, _mapper);
        // Act
        var result = controller.GetReviewForACar(reviewId);
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
    }
    [Fact]
    public void ReviewController_GetReviewForACarModelStateIsInvalid_ReturnsBadRequest()
    {
        var reviewId = 1;
        var reviewList = A.Fake<List<ReviewDto>>();
        // Arrange
        A.CallTo(() => _reviewRepository.ReviewExists(reviewId)).Returns(true);
        A.CallTo(() => _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewsOfACar(reviewId))).Returns(reviewList);
        var controller = new ReviewController(_reviewerRepository,_carRepository, _reviewRepository, _mapper);
        controller.ModelState.AddModelError("", "Error");
        // Act
        var result = controller.GetReviewForACar(reviewId);
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<BadRequestResult>();
    }
    [Fact]
    public void ReviewController_CreateReview_ReturnsOk()
    {
        var reviewId = 1;
        var reviewerId = 1;
        var carId = 1;
        var reviewList = A.Fake<List<ReviewDto>>();
        var review = A.Fake<Review>();
        var reviewCreate = A.Fake<ReviewDto>();
        var reviewer = A.Fake<Reviewer>();
        var car = A.Fake<Car>();
        // Arrange
        A.CallTo(() => _reviewRepository.GetCarTrimToUpper(reviewCreate)).Returns(null);
        A.CallTo(() => _mapper.Map<Review>(reviewCreate)).Returns(review);
        A.CallTo(() => _carRepository.GetCar(carId)).Returns(car);
        A.CallTo(() => _reviewerRepository.GetReviewer(reviewerId)).Returns(reviewer);
        A.CallTo(() => _reviewRepository.CreateReview(review)).Returns(true);
        var controller = new ReviewController(_reviewerRepository,_carRepository, _reviewRepository, _mapper);
        // Act
        var result = controller.CreateReview(reviewId, carId, reviewCreate);
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
    }
}