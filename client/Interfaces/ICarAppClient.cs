
using CarReviewApp.Dto;

namespace CarReviewApp.client.Interfaces;

public interface ICarAppClient
{
    public void AddMediaTypeWithQualityHeaderValue(string header);
    public void AddBaseAdress(Uri uri);
    public void ClearDefaultRequestHeaders();
    public Task<string> GetCars(string endPoint);
    public Task<string> GetCar(string endPoint);
    public Task<string> GetCarRating(string endPoint);
    public Task<HttpResponseMessage> CreateCar(string endPoint, HttpContent content);
    public Task<HttpResponseMessage> UpdateCar(string endPoint, HttpContent content);
    public Task<HttpResponseMessage> DeleteCar(string endPoint);
}