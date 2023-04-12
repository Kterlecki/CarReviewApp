// See https://aka.ms/new-console-template for more information
using CarReviewApp.client.Client;
using CarReviewApp.client.Interfaces;
using CarReviewApp.client.Service;
using CarReviewApp.Dto;

//var getCarsEndPoint = "api/Car";
var getCarEndPoint = "api/Car/1";
var getCarRatingEndPoint = "api/Car/1/rating";
var createCarEndPoint = "api/Car?ownerId=1&catId=3";
var carDto = new CarDto{Make = "Lambo", Model = "Hur", YearBuilt = 2020 };

var httpClient = new HttpClient();
var carAppClient = new CarAppClient(httpClient);
var carService = new CarService(carAppClient);

// await carService.GetCars(endPoint);
// await carService.GetCar(getCarEndPoint);
// await carService.GetCarRating(getCarRatingEndPoint);
await carService.CreateCar(createCarEndPoint, carDto );