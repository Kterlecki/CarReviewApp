// See https://aka.ms/new-console-template for more information
using CarReviewApp.client.Client;
using CarReviewApp.client.Interfaces;
using CarReviewApp.client.Service;
using CarReviewApp.Dto;

var endPoint = "api/Car";
var getCarsEndPoint = "api/Car";
var getCarEndPoint = "api/Car/1";
var getCarRatingEndPoint = "api/Car/1/rating";
var createCarEndPoint = "api/Car?ownerId=1&catId=3";
var updateCarEndPoint = "api/Car/3003?catId=3&ownerId=1";
var deleteCarEndPoint = "api/Car/3003";
var carDto = new CarDto{ Make = "Porshce", Model = "911", YearBuilt = 2010 };
var carDtoUpdate = new CarDto{ Id = 3003, Make = "Porsche", Model = "Panamera", YearBuilt = 2023 };

var httpClient = new HttpClient();
var carAppClient = new CarAppClient(httpClient);
var carService = new CarService(carAppClient);

// await carService.GetCars(endPoint);
// await carService.GetCar(getCarEndPoint);
// await carService.GetCarRating(getCarRatingEndPoint);
// await carService.CreateCar(createCarEndPoint, carDto );
// await carService.UpdateCar(updateCarEndPoint, carDtoUpdate );
await carService.DeleteCar( updateCarEndPoint );
