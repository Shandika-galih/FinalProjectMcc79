using Client.ViewModels.Account;
using Client.ViewModels.Role;

namespace Client.Contract
{
    public interface IRoleRepository : IGeneralRepository<RoleVM, Guid>
    {
    }
}
