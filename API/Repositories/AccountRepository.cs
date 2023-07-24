using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class AccountRepository : GeneralRepository<Account>, IAccountRepository
    {
        public AccountRepository(MyDbContext context) : base(context)
        {

        }

        public Account? GetEmail(string email)
        {
            return _context.Set<Account>().SingleOrDefault(e => e.Email == email);
        }
    }


}
