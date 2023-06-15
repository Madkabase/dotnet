namespace IoDit.WebAPI.Utilities.Repositories;

using IoDit.WebAPI.Persistence.Entities.Company;

public interface IDeviceRepository
{
    //DEVICES
    Task<CompanyDevice?> GetDeviceByEui(string deviceEui);
    Task<List<CompanyDevice>> GetDevices(long companyUserId);


    //DEVICE DATA
    Task<CompanyDevice?> GetDeviceWithDataByEui(string deviceEUI);

}