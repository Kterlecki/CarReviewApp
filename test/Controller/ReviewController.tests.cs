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
    [Fact]
    public void ReviewController_CreateReviewValidationOfCreateReviewSetToFalse_ReturnStatusCode500()
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
        A.CallTo(() => _reviewRepository.CreateReview(review)).Returns(false);
        var controller = new ReviewController(_reviewerRepository,_carRepository, _reviewRepository, _mapper);
        // Act
        var result = controller.CreateReview(reviewId, carId, reviewCreate);
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ObjectResult>();
        result.As<ObjectResult>().StatusCode.Should().Be(500);
    }
    [Fact]
    public void ReviewController_CreateReviewModelStateIsInvalid_ReturnBadRequest()
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
        var controller = new ReviewController(_reviewerRepository,_carRepository, _reviewRepository, _mapper);
        controller.ModelState.AddModelError("", "Error");
        // Act
        var result = controller.CreateReview(reviewId, carId, reviewCreate);
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<BadRequestObjectResult>();
    }
    [Fact]
    public void ReviewController_CreateReviewValidationReviewSetToNotNull_ReturnStatusCode422()
    {
        var reviewId = 1;
        var carId = 1;
        var reviewList = A.Fake<List<ReviewDto>>();
        var review = A.Fake<Review>();
        var reviewCreate = A.Fake<ReviewDto>();
        var reviewer = A.Fake<Reviewer>();
        var car = A.Fake<Car>();
        // Arrange
        A.CallTo(() => _reviewRepository.GetCarTrimToUpper(reviewCreate)).Returns(review);
        var controller = new ReviewController(_reviewerRepository,_carRepository, _reviewRepository, _mapper);
        // Act
        var result = controller.CreateReview(reviewId, carId, reviewCreate);
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ObjectResult>();
        result.As<ObjectResult>().StatusCode.Should().Be(422);
    }
    [Fact]
    public void ReviewController_CreateReviewValidationReviewCreateSetToNull_ReturnBadRequest()
    {
        var reviewId = 1;
        var carId = 1;
        ReviewDto? reviewCreate = null;
        // Arrange
        var controller = new ReviewController(_reviewerRepository,_carRepository, _reviewRepository, _mapper);
        // Act
        var result = controller.CreateReview(reviewId, carId, reviewCreate!);
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public void ReviewController_UpdateReview_ReturnsNoContent()
    {
        var reviewId = 1;
        var reviewList = A.Fake<List<ReviewDto>>();
        var review = A.Fake<Review>();
        var updateReview = A.Fake<ReviewDto>();
        var reviewer = A.Fake<Reviewer>();
        var car = A.Fake<Car>();
        updateReview.Id = 1;
        // Arrange
        A.CallTo(() => _mapper.Map<Review>(updateReview)).Returns(review);
        A.CallTo(() => _reviewRepository.ReviewExists(reviewId)).Returns(true);
        A.CallTo(() => _reviewRepository.UpdateReview(review)).Returns(true);
        var controller = new ReviewController(_reviewerRepository,_carRepository, _reviewRepository, _mapper);
        // Act
        var result = controller.UpdateReview(reviewId, updateReview);
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NoContentResult>();
    }
    [Fact]
    public void ReviewController_UpdateReviewValidationUpdateReviewSetToFalse_ReturnsStatusCode500()
    {
        var reviewId = 1;
        var reviewList = A.Fake<List<ReviewDto>>();
        var review = A.Fake<Review>();
        var updateReview = A.Fake<ReviewDto>();
        var reviewer = A.Fake<Reviewer>();
        var car = A.Fake<Car>();
        updateReview.Id = 1;
        // Arrange
        A.CallTo(() => _mapper.Map<Review>(updateReview)).Returns(review);
        A.CallTo(() => _reviewRepository.ReviewExists(reviewId)).Returns(true);
        A.CallTo(() => _reviewRepository.UpdateReview(review)).Returns(false);
        var controller = new ReviewController(_reviewerRepository,_carRepository, _reviewRepository, _mapper);
        // Act
        var result = controller.UpdateReview(reviewId, updateReview);
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ObjectResult>();
        result.As<ObjectResult>().StatusCode.Should().Be(500);
    }
    [Fact]
    public void ReviewController_UpdateReviewValidationModelStateIsInvalid_ReturnsBadRequest()
    {
        var reviewId = 1;
        var reviewList = A.Fake<List<ReviewDto>>();
        var review = A.Fake<Review>();
        var updateReview = A.Fake<ReviewDto>();
        var reviewer = A.Fake<Reviewer>();
        var car = A.Fake<Car>();
        updateReview.Id = 1;
        // Arrange
        A.CallTo(() => _reviewRepository.ReviewExists(reviewId)).Returns(true);
        var controller = new ReviewController(_reviewerRepository,_carRepository, _reviewRepository, _mapper);
        controller.ModelState.AddModelError("", "Error");
        // Act
        var result = controller.UpdateReview(reviewId, updateReview);
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<BadRequestResult>();
    }
    [Fact]
    public void ReviewController_UpdateReviewValidationReviewExistsSetToFalse_ReturnsNotFound()
    {
        var reviewId = 1;
        var reviewList = A.Fake<List<ReviewDto>>();
        var review = A.Fake<Review>();
        var updateReview = A.Fake<ReviewDto>();
        var reviewer = A.Fake<Reviewer>();
        var car = A.Fake<Car>();
        updateReview.Id = 1;
        // Arrange
        A.CallTo(() => _reviewRepository.ReviewExists(reviewId)).Returns(false);
        var controller = new ReviewController(_reviewerRepository,_carRepository, _reviewRepository, _mapper);
        // Act
        var result = controller.UpdateReview(reviewId, updateReview);
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NotFoundResult>();
    }
    [Fact]
    public void ReviewController_UpdateReviewValidationReviewIdsNotMatching_ReturnsBadRequest()
    {
        var reviewId = 1;
        var updateReview = A.Fake<ReviewDto>();
        updateReview.Id = 2;
        // Arrange
        var controller = new ReviewController(_reviewerRepository,_carRepository, _reviewRepository, _mapper);
        // Act
        var result = controller.UpdateReview(reviewId, updateReview);
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<BadRequestObjectResult>();
    }
    [Fact]
    public void ReviewController_UpdateReviewValidationUpdateReviewSetToNull_ReturnsBadRequest()
    {
        var reviewId = 1;
        ReviewDto? updateReview = null;
        // Arrange
        var controller = new ReviewController(_reviewerRepository,_carRepository, _reviewRepository, _mapper);
        // Act
        var result = controller.UpdateReview(reviewId, updateReview!);
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<BadRequestObjectResult>();
    }
}