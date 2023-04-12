// See https://aka.ms/new-console-template for more information
using CarReviewApp.client.Client;
using CarReviewApp.client.Interfaces;
using CarReviewApp.client.Service;

//var getCarsEndPoint = "api/Car";
var getCarEndPoint = "api/Car/1";

var httpClient = new HttpClient();
var carAppClient = new CarAppClient(httpClient);
var carService = new CarService(carAppClient);

//await carService.GetCars(endPoint);
await carService.GetCars(getCarEndPoint);