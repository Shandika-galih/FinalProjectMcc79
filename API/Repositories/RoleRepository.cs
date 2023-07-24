using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class RoleRepository : GeneralRepository<Role>, IRoleRepository
{
    public RoleRepository(MyDbContext context) : base(context) { }

    public Role GetByName(string roleName)
    {
        return _context.Roles.FirstOrDefault(r => r.Name == roleName);
    }
}
