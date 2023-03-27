// using AutoMapper;
// using CarReviewApp.Controllers;
// using CarReviewApp.Data;
// using CarReviewApp.Dto;
// using CarReviewApp.Interfaces;
// using CarReviewApp.Models;
// using CarReviewApp.Repository;
// using FakeItEasy;
// using FluentAssertions;
// using Microsoft.AspNetCore.Mvc;
// using Moq;
// using System.Collections.Generic;
// using System.Linq;
// using Xunit;

// namespace CarReviewApp.tests.Repository;

// public class CarRepositoryTests
// {
//     private readonly Mock<DataContext> _context;

//     public CarRepositoryTests()
//     {
//         _context = new Mock<DataContext>();
//     }
//     [Fact]
//     public void CarRepository_CarExists_ReturnsTrue()
//     {
//         // Arrange
//         var id = 1;
//         var repository = new CarRepository(_context.Object);
        
//         // Act
//         var result = repository.CarExists(id);
//         // Assert
//         result.Should().BeTrue();
//     }
// }