using IoDit.WebAPI.DTO.Farm;

namespace IoDit.WebAPI.DTO.Field;


public class CreateFieldRequestDTO
{
    public FarmDTO Farm { get; set; }
    public string FieldName { get; set; }
    public List<List<double>> Coordinates { get; set; }
}