using API.Models;

namespace API.Contracts
{
    public interface IAccountRepository : IGeneralRepository<Account>
    {
        Account? GetEmail(string email);
        bool IsDuplicateValue(string value);
    }
}
