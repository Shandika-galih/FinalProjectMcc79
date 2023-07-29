using Client.Contract;
using Client.Repository;
using Client.ViewModels.AccountRole;
using Client.ViewModels.Role;

namespace Client.Repositories
{
    public class AccountRoleRepository : GeneralRepository<AccountRoleVM, Guid>, IAccountRoleRepository
    {
        public AccountRoleRepository(string request = "account-roles/") : base(request)
        {
        }
    }
}
