using System.Text;
// See https://aka.ms/new-console-template for more information
using System;
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
carService.ClientHttpHeadersSetUp();

// await carService.GetCars(endPoint);
// await carService.GetCar(getCarEndPoint);
// await carService.GetCarRating(getCarRatingEndPoint);
// await carService.CreateCar(createCarEndPoint, carDto );
// await carService.UpdateCar(updateCarEndPoint, carDtoUpdate );
// await carService.DeleteCar( updateCarEndPoint );
var code = 0;

System.Console.WriteLine("Welcome to the Client App of CarReview");
System.Console.WriteLine("Please Select from one of the following options:");
System.Console.WriteLine("1 - Get Cars");
System.Console.WriteLine("2 - Get Car");
System.Console.WriteLine("3 - Get Car Rating");
System.Console.WriteLine("4 - Create Car");
System.Console.WriteLine("5 - Update Car");
System.Console.WriteLine("6 - Delete Car");
System.Console.WriteLine("7 - Exit");
System.Console.WriteLine();
while (code != 7)
{
    System.Console.WriteLine("Enter option number: ");
    code = Convert.ToInt32(Console.ReadLine());

    switch (code)
    {
        case 1:
            Console.WriteLine("Car List:");
            await carService.GetCars(endPoint);
            break;
        case 2:
            Console.WriteLine();
            Console.WriteLine("Please enter the Car ID");
            var carId = Console.ReadLine();
            if (!ValueNullCheck(carId!))
            {
                Console.WriteLine("No value entered");
                break;
            }
            var formatEndPoint = "/" + carId;
            var apiEndPoint = EndPointBuilder(formatEndPoint);
            await carService.GetCar(apiEndPoint);
            break;
        case 3:
            System.Console.WriteLine("get car");
            break;
        case 4:
            System.Console.WriteLine("get car");
            break;
        case 5:
            System.Console.WriteLine("get car");
            break;
        case 6:
            System.Console.WriteLine("get car");
            break;
        case 7:
            Console.WriteLine("Exit selected");
            break;
        default:
            Console.WriteLine("Option entered not available, Please try again");
            break;
    }
}

string EndPointBuilder(string endPoint)
{
    var apiPath = "api/Car";
    var stringBuilder = new StringBuilder();
    stringBuilder.Append(apiPath).Append(endPoint);
    return stringBuilder.ToString();
}

bool ValueNullCheck(string valuePassedIn)
{
    if (string.IsNullOrEmpty(valuePassedIn))
    {
        return false;
    }
    return true;
}