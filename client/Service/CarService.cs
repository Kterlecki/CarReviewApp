

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


}