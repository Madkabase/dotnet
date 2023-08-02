using IoDit.WebAPI.DTO.Farm;
using IoDit.WebAPI.DTO.Threshold;

namespace IoDit.WebAPI.DTO.Field;


public class CreateFieldRequestDTO
{
    public long FarmId { get; set; }
    public string FieldName { get; set; }
    public List<List<double>> Coordinates { get; set; }
    public ThresholdDto Threshold { get; set; }
}