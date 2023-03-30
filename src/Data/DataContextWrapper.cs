using CarReviewApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarReviewApp.Data;

public class DataContextWrapper : IDataContextWrapper
{
    public DataContext CreateDataContext()
    {
        var options = new DbContextOptions<DataContext>();
        return new DataContext(options);
    }
}
