// See https://aka.ms/new-console-template for more information
using CarReviewApp.client.Client;


var _httpClient = new HttpClient();
var carApp = new CarAppClient(_httpClient);

await carApp.GetCars();