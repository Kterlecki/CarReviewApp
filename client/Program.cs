using System.Text;
using CarReviewApp.client.Client;
using CarReviewApp.client.Service;
using CarReviewApp.Dto;

var httpClient = new HttpClient();
var carAppClient = new CarAppClient(httpClient);
var carService = new CarService(carAppClient);
carService.ClientHttpHeadersSetUp();

var code = 0;

System.Console.WriteLine("Welcome to the Client App of CarReview");
System.Console.WriteLine("Please Select from one of the following options:");
System.Console.WriteLine("1 - Get All Cars");
System.Console.WriteLine("2 - Get A Car");
System.Console.WriteLine("3 - Get Car Rating");
System.Console.WriteLine("4 - Create Car");
System.Console.WriteLine("5 - Update Car");
System.Console.WriteLine("6 - Delete Car");
System.Console.WriteLine("7 - Exit");
System.Console.WriteLine();
while (code != 7)
{
    System.Console.Write("Enter option number: ");
    code = Convert.ToInt32(Console.ReadLine());
    var carId = "";
    var formatEndPoint = "";
    var apiEndPoint = "";
    var id = 0;

    switch (code)
    {
        case 1:
            Console.WriteLine("Car List:");
            apiEndPoint = EndPointBuilder("");
            await carService.GetCars(apiEndPoint);
            break;
        case 2:
            Console.WriteLine();
            Console.WriteLine("Please enter the Car ID");
            carId = Console.ReadLine();
            if (!ValueNullCheck(carId!))
            {
                Console.WriteLine("No value entered");
                break;
            }
            formatEndPoint = "/" + carId;
            apiEndPoint = EndPointBuilder(formatEndPoint);
            await carService.GetCar(apiEndPoint);
            break;
        case 3:
            Console.WriteLine();
            Console.WriteLine("Please enter the Car ID");
            carId = Console.ReadLine();
            if (!ValueNullCheck(carId!))
            {
                Console.WriteLine("No value entered");
                break;
            }
            formatEndPoint = "/" + carId + "/rating";
            apiEndPoint = EndPointBuilder(formatEndPoint);
            await carService.GetCarRating(apiEndPoint);
            break;
        case 4:
            Console.WriteLine();
            Console.Write("Please enter the Car Make: ");
            var carMake = Console.ReadLine();
            Console.Write("Please enter the Car Model: ");
            var carModel = Console.ReadLine();
            Console.Write("Please enter the Car Year Built: ");
            var carYearBuilt = Convert.ToInt32(Console.ReadLine());
            var carDto = new CarDto{ Make = carMake, Model = carModel, YearBuilt = carYearBuilt };

            formatEndPoint = "?ownerId=1&catId=3";

            apiEndPoint = EndPointBuilder(formatEndPoint);
            await carService.CreateCar(apiEndPoint, carDto);
            break;
        case 5:
            Console.WriteLine();
            Console.Write("Enter Id of car to update: ");
            id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Please enter the Car Make to: ");
            var carMakeUpdate = Console.ReadLine();
            Console.Write("Please enter the Car Model: ");
            var carModelUpdate = Console.ReadLine();
            Console.Write("Please enter the Car Year Built: ");
            var carYearBuiltUpdate = Convert.ToInt32(Console.ReadLine());
            var carDtoUpdate = new CarDto {Id = id, Make = carMakeUpdate, Model = carModelUpdate, YearBuilt = carYearBuiltUpdate };

            formatEndPoint = $"/{id}?catId=1&ownerId=1";

            apiEndPoint = EndPointBuilder(formatEndPoint);
            await carService.UpdateCar(apiEndPoint, carDtoUpdate);
            break;
        case 6:
            Console.WriteLine();
            Console.Write("Enter Id of car to Delete: ");
            id = Convert.ToInt32(Console.ReadLine());

            formatEndPoint = $"/{id}";

            apiEndPoint = EndPointBuilder(formatEndPoint);
            await carService.DeleteCar(apiEndPoint);
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