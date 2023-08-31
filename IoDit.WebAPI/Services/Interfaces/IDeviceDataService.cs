using IoDit.WebAPI.BO;

namespace IoDit.WebAPI.Services;

public interface IDeviceDataService
{
    /// <summary>
    /// Get the data from a device between two dates
    /// </summary>
    /// <param name="deviceBo"></param>
    /// <param name="startDate">the start date to fetch the device data</param>
    /// <param name="endDate"> the end date to fetch the device data</param>
    /// <returns>the data of the device from start date to end date</returns>
    Task<List<DeviceDataBo>> GetDeviceDatasByDevice(DeviceBo deviceBo, DateTime startDate, DateTime endDate);
}