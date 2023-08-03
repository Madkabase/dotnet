using System.Text.Json;

namespace IoDit.WebAPI.Config.ErrorHandling
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
        public ErrorDetails(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
        public ErrorDetails()
        {
            StatusCode = 0;
            Message = "";
        }
    }
}