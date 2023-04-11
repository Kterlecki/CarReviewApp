
using System.Net.Http.Headers;
using CarReviewApp.client.Interfaces;

namespace CarReviewApp.client.Client;

public class CarAppClient : ICarAppClient
{
    private readonly HttpClient _httpClient;
    public CarAppClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public void AddMediaTypeWithQualityHeaderValue(string header)
    {
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(header));
    }

    public async Task GetCars()
    {
        _httpClient.BaseAddress = new Uri("https://localhost:7179/");
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        string responseBody = await _httpClient.GetStringAsync("api/Car");

        Console.WriteLine(responseBody);
    }
}