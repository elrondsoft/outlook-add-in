namespace Helios.Api.Domain.Dtos.Api
{
    public class HeliosAuthResponceDto
    {
        public string EntityId { get; set; }
        public string Error { get; set; }

        public HeliosAuthResponceDto()
        {
            EntityId = null;
            Error = null;
        }
    }
}