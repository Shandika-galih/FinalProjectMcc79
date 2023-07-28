using Client.Repositories;
using API.Utilities.Handler;
using API.Utilities.Handler;
using Client.ViewModels.Account;
using API.DTOs.Accounts;
using API.Utilities;

namespace Client.Contract
{
    public interface IAccountRepository : IGeneralRepository<LoginVM, string>
    {
        Task<ResponseHandler<string>> Login(LoginVM entity);
    }
}