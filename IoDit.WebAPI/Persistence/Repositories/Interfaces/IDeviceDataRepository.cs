using IoDit.WebAPI.BO;
using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.Persistence.Repositories;

public interface IDeviceDataRepository
{
    /// <summary>
    /// Get the data from a device between two dates
    /// </summary>
    /// <param name="deviceBo">the device we want the data</param>
    /// <param name="startDate">the start point for the retrieving</param>
    /// <param name="endDate">the end point for the retrieving</param>
    /// <returns>a list of data</returns>
    public Task<List<DeviceData>> GetDeviceDatasByDevice(DeviceBo deviceBo, DateTime startDate, DateTime endDate);
}