using Client.Contract;
using Client.Repository;
using Client.ViewModels.Employee;
using Client.ViewModels.Role;

namespace Client.Repositories
{
    public class RoleRepository : GeneralRepository<RoleVM, Guid>, IRoleRepository
    {
        public RoleRepository(string request = "roles/") : base(request)
        {
        }
    }
}
