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
}