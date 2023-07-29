using Client.ViewModels.Employee;
using API.DTOs.Accounts;
using API.Models;
using API.Utilities;
using Client.Contract;
using Client.Repository;
using Client.ViewModels.Account;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Client.Repositories;

public class AccountRepository : GeneralRepository<Account, string>, IAccountRepository
{
    private readonly string request;
    private readonly HttpClient httpClient;

    public AccountRepository(string request = "account/") : base(request)
    {
        this.request = request;
        httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7114/api/")
        };
        this.request = request;
    }

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

    public async Task<ResponseHandler<string>> ForgotPassword(ForgotPasswordVM forgotPasswordVM)
    {
        ResponseHandler<string> responseHandler = null;
        StringContent content = new StringContent(JsonConvert.SerializeObject(forgotPasswordVM), Encoding.UTF8, "application/json");
        using (var response = await httpClient.PostAsync(request + "ForgotPassword", content))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            responseHandler = JsonConvert.DeserializeObject<ResponseHandler<string>>(apiResponse);
        }
        return responseHandler;
    }

    public async Task<ResponseHandler<string>> ChangePassword(ChangePasswordVM changePasswordVM)
    {
        ResponseHandler<string> responseHandler = null;
        StringContent content = new StringContent(JsonConvert.SerializeObject(changePasswordVM), Encoding.UTF8, "application/json");
        using (var response = await httpClient.PutAsync(request + "ChangePassword", content))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            responseHandler = JsonConvert.DeserializeObject<ResponseHandler<string>>(apiResponse);
        }
        return responseHandler;
    }
}

