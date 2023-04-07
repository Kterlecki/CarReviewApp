

using System;
namespace CarReviewApp.Client.Interfaces;

public interface ICarAppClient
{
    public Task<HttpResponseMessage> GetCars(Uri uri, string parameter);

}