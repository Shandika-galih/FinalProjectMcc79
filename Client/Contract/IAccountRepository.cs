using API.DTOs.Accounts;
using Client.Repositories;
using API.Utilities.Handler;
using API.Utilities.Handler;
using Client.ViewModels.Account;

namespace Client.Contract
{
    public interface IAccountRepository : IGeneralRepository<LoginVM, string>
    {
        Task<API.Utilities.ResponseHandler<AccountRepository>> Register(LoginVM entity);
        Task<API.Utilities.ResponseHandler<string>> Login(LoginVM entity);
    }
}