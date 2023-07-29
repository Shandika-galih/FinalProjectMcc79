using API.Models;
using Client.ViewModels.AccountRole;

namespace Client.Contract
{
    public interface IAccountRoleRepository : IGeneralRepository<AccountRoleVM, Guid>
    {
    }
}
