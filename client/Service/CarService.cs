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

    public void ClientHttpHeadersSetUp()
    {
        _httpClient.AddBaseAdress(_baseAdress);
        _httpClient.ClearDefaultRequestHeaders();
        _httpClient.AddMediaTypeWithQualityHeaderValue(_contentTypeAccepted);
    }

    public async Task GetCars(string endPoint)
        {
            string responseBody = await _httpClient.GetCars(endPoint);
            Console.WriteLine(responseBody);
        }
    public async Task GetCar(string endPoint)
        {
            string responseBody = await _httpClient.GetCars(endPoint);
            Console.WriteLine(responseBody);
        }
    public async Task GetCarRating(string endPoint)
        {
            string responseBody = await _httpClient.GetCars(endPoint);
            Console.WriteLine(responseBody);
        }
    public async Task CreateCar(string endPoint, CarDto carDto)
        {
            var json = JsonConvert.SerializeObject(carDto);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var responseBody = await _httpClient.CreateCar(endPoint, content);
            Console.WriteLine(responseBody);
        }
    public async Task UpdateCar(string endPoint, CarDto carDto)
        {
            var json = JsonConvert.SerializeObject(carDto);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var responseBody = await _httpClient.UpdateCar(endPoint, content);
            Console.WriteLine(responseBody);
        }
    public async Task DeleteCar(string endPoint)
        {
            var responseBody = await _httpClient.DeleteCar(endPoint);
            Console.WriteLine(responseBody);
        }

    public string EndPointBuilder(string endPoint)
    {
        var apiPath = "api/Car";
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(apiPath).Append(endPoint);
        return stringBuilder.ToString();
    }

    public bool ValueNullCheck(string valuePassedIn)
    {
        if (string.IsNullOrEmpty(valuePassedIn))
        {
            return false;
        }
        return true;
    }
    }