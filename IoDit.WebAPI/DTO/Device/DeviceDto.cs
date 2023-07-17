namespace IoDit.WebAPI.DTO.Device
{
    public class DeviceDto
    {
        public string Id { get; set; }
        public List<DeviceDataDTO> Data { get; set; } = new List<DeviceDataDTO>();
        public string Name { get; set; } = "";

        public static DeviceDto FromEntity(Persistence.Entities.Device device)
        {
            return new DeviceDto
            {
                Id = device.DevEUI,
                Name = device.Name,
                Data = device.DeviceData.Select(d => DeviceDataDTO.FromEntity(d)).ToList()
            };
        }
    }
}