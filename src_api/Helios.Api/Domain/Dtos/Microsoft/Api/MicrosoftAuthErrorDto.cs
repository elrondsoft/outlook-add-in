namespace Helios.Api.Domain.Dtos.Microsoft.Api
{
    public class MicrosoftAuthErrorDto
    {
        public ErrorDetails Error { get; set; }
    }

    public class ErrorDetails
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
