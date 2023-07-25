using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_accounts")]
public class Account : BaseEntity
{
    [Column("email", TypeName = "nvarchar(50)")]
    public string Email { get; set; }

    [Column("password", TypeName = "nvarchar(255)")]
    public string Password { get; set; }

    [Column("otp")]
    public int OTP { get; set; }

    [Column("is_used")]
    public bool IsUsed { get; set; }

    [Column("expired_time")]
    public DateTime ExpiredTime { get; set; }

    //Cardinality
    public ICollection<AccountRole>? AccountRoles { get; set; }

    public Employee? Employee { get; set; }
}
