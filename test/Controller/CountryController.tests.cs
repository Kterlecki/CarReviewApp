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

public class CountryControllerTests
{
    private readonly ICountryRepository _countryRepository;
    private readonly IMapper _mapper;
    public CountryControllerTests()
    {
        _countryRepository = A.Fake<ICountryRepository>();
        _mapper = A.Fake<IMapper>();
    }

    [Fact]
    public void CountryController_GetCountry_ReturnsOk()
    {
        // Arrange
        var country = A.Fake<List<CountryDto>>();
        A.CallTo(() => _mapper.Map<List<CountryDto>>(_countryRepository.GetCountries())).Returns(country);
        var controller = new CountryController(_countryRepository, _mapper);

        // Act
        var result = controller.GetCountry();
        // Arrange
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
    }
    [Fact]
    public void CountryController_GetCountryModelStateInvalid_ReturnsBadRequest()
    {
        // Arrange
        var country = A.Fake<List<CountryDto>>();
        A.CallTo(() => _mapper.Map<List<CountryDto>>(_countryRepository.GetCountries())).Returns(country);
        var controller = new CountryController(_countryRepository, _mapper);
        controller.ModelState.AddModelError("", "error");

        // Act
        var result = controller.GetCountry();
        // Arrange
        result.Should().NotBeNull();
        result.Should().BeOfType<BadRequestObjectResult>();
    }
    [Fact]
    public void CountryController_GetCountryWithId_ReturnsOk()
    {
        // Arrange
        var country = A.Fake<CountryDto>();
        var countryId = 1;
        A.CallTo(() => _countryRepository.CountryExists(countryId)).Returns(true);
        A.CallTo(() => _mapper.Map<CountryDto>(_countryRepository.GetCountry(countryId))).Returns(country);
        var controller = new CountryController(_countryRepository, _mapper);

        // Act
        var result = controller.GetCountry(countryId);
        // Arrange
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
    }
    [Fact]
    public void CountryController_GetCountryWithIdThatDoesntExist_ReturnsNotFound()
    {
        // Arrange
        var countryId = 1;
        A.CallTo(() => _countryRepository.CountryExists(countryId)).Returns(false);
        var controller = new CountryController(_countryRepository, _mapper);

        // Act
        var result = controller.GetCountry(countryId);
        // Arrange
        result.Should().NotBeNull();
        result.Should().BeOfType<NotFoundResult>();
    }
    [Fact]
    public void CountryController_GetCountryWithIdModelError_ReturnsBadRequest()
    {
        // Arrange
         var country = A.Fake<CountryDto>();
        var countryId = 1;
        A.CallTo(() => _countryRepository.CountryExists(countryId)).Returns(true);
        A.CallTo(() => _mapper.Map<CountryDto>(_countryRepository.GetCountry(countryId))).Returns(country);
        var controller = new CountryController(_countryRepository, _mapper);
        controller.ModelState.AddModelError("", "error");

        // Act
        var result = controller.GetCountry(countryId);
        // Arrange
        result.Should().NotBeNull();
        result.Should().BeOfType<BadRequestObjectResult>();
    }
}
