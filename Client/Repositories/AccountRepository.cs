using Client.Contract;
using Client.Repository;
using Client.ViewModels.Account;
using Client.ViewModels.Employee;

namespace Client.Repositories
{
    public class AccountRepository : GeneralRepository<AccountVM, Guid>, IAccountRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string request;
        public AccountRepository(string request = "accounts/") : base(request)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7114/api/")
            };
            this.request = request;
        }
    }
}
