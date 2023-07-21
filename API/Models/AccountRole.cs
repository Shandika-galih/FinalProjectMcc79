using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace API.Models;

[Table("tb_tr_account_roles")]
public class AccountRole
{
    [Column("account_guid")]
    public Guid AccountGuid { get; set; }

    [Column("role_guid")]
    public Guid RoleGuid { get; set; }

    //Cardinality
    public Role? Role { get; set; }
    public Account? Account { get; set; }
}
