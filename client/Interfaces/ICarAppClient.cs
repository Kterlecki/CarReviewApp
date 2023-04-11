
namespace CarReviewApp.client.Interfaces;

public interface ICarAppClient
{

    public void AddMediaTypeWithQualityHeaderValue(string header);
    public Task GetCars();
}