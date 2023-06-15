namespace IoDit.WebAPI.Utilities.Loriot.Types;

public class LoriotCreateAppRequestDto
{
    public string title { get; set; }
    public int capacity { get; set; }
    public string visibility { get; set; }
    public int mcastdevlimit { get; set; }
}