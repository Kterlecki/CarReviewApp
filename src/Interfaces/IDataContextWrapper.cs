using CarReviewApp.Data;

namespace CarReviewApp.Interfaces;

public interface IDataContextWrapper
{
    public DataContext CreateDataContext();
}