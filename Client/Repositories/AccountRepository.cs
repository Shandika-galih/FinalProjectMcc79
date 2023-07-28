using API.DTOs.Accounts;
using API.Utilities;
using Client.Contract;
using Client.Repository;
using Client.ViewModels.Account;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Client.Repositories;

public class AccountRepository : GeneralRepository<LoginVM, string>, IAccountRepository
{
    private readonly string request;
    private readonly HttpClient httpClient;
    public AccountRepository(string request = "accounts/") : base(request)
    {
        this.request = request;
        httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7114/api/")
        };
        this.request = request;
    }
   /* public async Task<ResponseHandler<string>> Login(LoginVM loginVM)
    {
        ResponseHandler<string> entityVM = null;
        StringContent content = new StringContent(JsonConvert.SerializeObject(loginVM), Encoding.UTF8, "application/json");
        using (var response = await _httpClient.PostAsync(_request + "login", content))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseHandler<string>>(apiResponse);
        }
        return entityVM;
    }*/
    public async Task<ResponseHandler<string>> Login(LoginVM loginVM)
    {
        ResponseHandler<string> responseHandler = null;
        StringContent content = new StringContent(JsonConvert.SerializeObject(loginVM), Encoding.UTF8, "application/json");
        using (var response = await httpClient.PostAsync(request + "login", content))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            responseHandler = JsonConvert.DeserializeObject<ResponseHandler<string>>(apiResponse);
        }
        return responseHandler;
    }

}