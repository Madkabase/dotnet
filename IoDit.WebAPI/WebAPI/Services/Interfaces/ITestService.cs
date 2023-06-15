using IoDit.WebAPI.Persistence.Entities.Company;

namespace IoDit.WebAPI.WebAPI.Services.Interfaces;

public interface ITestService
{
    Task<Company?> ClearAllDataAsyncAndPopulate();
    Task<string> Test();
    Task<string> ClearAllDataAsync();
    Task<string> TestMail();
}