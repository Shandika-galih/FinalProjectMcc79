using API.Utilities;
using Client.Contract;
using Client.Repository;
using Client.ViewModels.Account;
using Newtonsoft.Json;
using System.Text;

namespace Client.Repositories;

public class AccountRepository : GeneralRepository<LoginVM, string>, IAccountRepository
{
    private readonly HttpClient _httpClient;
    private readonly string _request;
    public AccountRepository(string request = "accounts/") : base(request)
    {
        _request = request;
        _httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7114/api/")
        };
    }

    public async Task<ResponseHandler<string>> Login(LoginVM login)
    {
        ResponseHandler<string> responseHandler = null;
        StringContent content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
        using (var response = await _httpClient.PostAsync(_request + "login", content))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            responseHandler = JsonConvert.DeserializeObject<ResponseHandler<string>>(apiResponse);
        }
        return responseHandler;
    }
}