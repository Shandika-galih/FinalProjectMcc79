using Client.ViewModels.Account;
using Client.ViewModels.Employee;

namespace Client.Contract
{
    public interface IAccountRepository : IGeneralRepository<AccountVM, Guid>
    {
    }
}
