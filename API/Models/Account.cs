using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_accounts")]
public class Account
{
    [Column("password", TypeName = "nvarchar(255)")]
    public string Password { get; set; }
}
