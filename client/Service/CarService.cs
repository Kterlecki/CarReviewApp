using System.Text;
using System.Net.Http.Headers;

using CarReviewApp.client.Client;
using CarReviewApp.client.Interfaces;
using CarReviewApp.Dto;
using Newtonsoft.Json;

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
 public async Task GetCarRating(string endPoint)
    {
        _httpClient.AddBaseAdress(_baseAdress);
        _httpClient.ClearDefaultRequestHeaders();
        _httpClient.AddMediaTypeWithQualityHeaderValue(_contentTypeAccepted);

        string responseBody = await _httpClient.GetCars(endPoint);

        Console.WriteLine(responseBody);
    }
 public async Task CreateCar(string endPoint, CarDto carDto)
    {
        _httpClient.AddBaseAdress(_baseAdress);
        _httpClient.ClearDefaultRequestHeaders();
        _httpClient.AddMediaTypeWithQualityHeaderValue(_contentTypeAccepted);
        var json = JsonConvert.SerializeObject(carDto);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        var responseBody = await _httpClient.CreateCar(endPoint, content);

        Console.WriteLine(responseBody);
    }

    // public Dictionary<string, string> CreateCarHttpContentCreator( int ownerId, int catId, CarDto carDto)
    // {
    //     var data = new Dictionary<string, string>
    //     {
    //         { "ownerId", ownerId.ToString() },
    //         { "catId", catId.ToString() },
    //         { "Id", carDto.Id.ToString() },
    //         { "Make", carDto.Make },
    //         { "Model", carDto.Model },
    //         { "YearBuilt", carDto.YearBuilt.ToString()}
    //     };
    // }
}