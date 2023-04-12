using System.Net.Http.Headers;

using CarReviewApp.client.Client;
using CarReviewApp.client.Interfaces;

namespace CarReviewApp.client.Service;

public class CarService
{
    private readonly ICarAppClient _httpClient;
    private const string _contentTypeAccepted = "application/json";
    private readonly Uri _baseAdress = new("https://localhost:7179/");

    public CarService(ICarAppClient httpClient)
    {
        _httpClient = httpClient;
    }
 public async Task GetCars(string endPoint)
    {
        _httpClient.AddBaseAdress(_baseAdress);
        _httpClient.ClearDefaultRequestHeaders();
        _httpClient.AddMediaTypeWithQualityHeaderValue(_contentTypeAccepted);

        string responseBody = await _httpClient.GetCars(endPoint);

        Console.WriteLine(responseBody);
    }
 public async Task GetCar(string endPoint)
    {
        _httpClient.AddBaseAdress(_baseAdress);
        _httpClient.ClearDefaultRequestHeaders();
        _httpClient.AddMediaTypeWithQualityHeaderValue(_contentTypeAccepted);

        string responseBody = await _httpClient.GetCars(endPoint);

        Console.WriteLine(responseBody);
    }
}