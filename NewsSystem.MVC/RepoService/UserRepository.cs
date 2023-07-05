using NewsSystem.DAL;
using NewsSystem.DAL.Dtos;
using System.Text;
using System.Text.Json;

namespace NewsSystem.MVC.RepoService
{
    public class UserRepository
    {
        private readonly IConfiguration _configuration;//to get url
        private readonly HttpClient _httpClient;
        private readonly string _URL;

        public UserRepository(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _URL = _configuration.GetValue<string>("url") ?? throw new InvalidOperationException("url not found.");
            _URL = _URL + "/" + "Users";
        }

        public async Task<TokenDto?> RequestLogin(LoginDto login)
        {
            string requestBody = JsonSerializer.Serialize(login);
            HttpContent httpContent = new StringContent(requestBody, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(_URL + "/Login", httpContent);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadFromJsonAsync<TokenDto>();
                return responseData;
            }
            else
                return null;
        }
    }
}
