using API.Utilities.Enums;

namespace API.DTOs.Manager
{
    public class UpdateStatusRequestDto
    {
        public Guid Guid { get; set; }
        public StatusEnum Status { get; set; }
    }
}
