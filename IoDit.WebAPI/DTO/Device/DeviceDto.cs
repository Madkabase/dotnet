using IoDit.WebAPI.BO;
using NetTopologySuite.Geometries;

namespace IoDit.WebAPI.DTO.Device
{
    public class DeviceDto
    {
        public string Id { get; set; }
        public List<DeviceDataDTO> Data { get; set; } = new List<DeviceDataDTO>();
        public string Name { get; set; } = "";
        public Point Location { get; set; }

        internal static DeviceDto FromBo(DeviceBo device)
        {
            return new DeviceDto
            {
                Id = device.DevEUI,
                Name = device.Name,
                Data = device.DeviceData.Select(DeviceDataDTO.FromBo).ToList(),
                Location = device.Location,
            };
        }
    }
}