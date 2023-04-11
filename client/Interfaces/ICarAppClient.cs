
namespace CarReviewApp.client.Interfaces;

public interface ICarAppClient
{
    public void AddMediaTypeWithQualityHeaderValue(string header);
    public void AddBaseAdress(Uri uri);
    public void ClearDefaultRequestHeaders();
    public Task<string> GetCars(string endPoint);
}