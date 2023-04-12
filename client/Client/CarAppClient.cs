
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
    public void AddBaseAdress(Uri uri)
    {
        _httpClient.BaseAddress = uri;
    }

    public void ClearDefaultRequestHeaders()
    {
        _httpClient.DefaultRequestHeaders.Accept.Clear();
    }
    public async Task<string> GetCars(string endPoint)
    {
        return await _httpClient.GetStringAsync(endPoint);
    }
    public async Task<string> GetCar(string endPoint)
    {
        return await _httpClient.GetStringAsync(endPoint);
    }
}