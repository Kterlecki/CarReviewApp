
using System.Net.Http;
using CarReviewApp.Client.Interfaces;

namespace CarReviewApp.Client.Client;

public class CarAppClient : ICarAppClient
{
    private readonly HttpClient _httpClient;
    public CarAppClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<HttpResponseMessage> GetCars(Uri uri, string parameter)
    {
        throw new NotImplementedException();
    }
}