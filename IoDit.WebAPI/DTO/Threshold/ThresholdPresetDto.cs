using IoDit.WebAPI.DTO.Farm;

namespace IoDit.WebAPI.DTO.Threshold
{
    public class ThresholdPresetDto : ThresholdDto
    {
        public string Name { get; set; }
        public FarmDto Farm { get; set; }


    }
}