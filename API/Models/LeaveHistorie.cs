using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_leave_histories")]
public class LeaveHistorie
{
    [Column("tb_m_leave_request_guid")]
    public Guid AccountGuid { get; set; }
}
