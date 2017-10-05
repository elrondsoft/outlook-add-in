namespace Helios.Api.Domain.Dtos.Api
{
    public class ScheduleRefreshTokensResponceDto
    {
        public int MicrosoftTokensUpdated { get; set; }
        public int HeliosTokenUpdated { get; set; }

        public ScheduleRefreshTokensResponceDto()
        {
            MicrosoftTokensUpdated = 0;
            HeliosTokenUpdated = 0;
        }
    }
}
