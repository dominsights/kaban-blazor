using Microsoft.JSInterop;
using System.Text.Json;
using System.Net.Http.Json;

namespace Kanban.Services
{
    public class AuthenticationService {       
        private IJSRuntime jsRuntime;
        private HttpClient httpClient;
        
        public AuthenticationService(IJSRuntime jsRuntime, HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.jsRuntime = jsRuntime;    
        }

        public async Task<bool> IsLoggedIn() {
            string json = await jsRuntime.InvokeAsync<string>("localStorage.getItem", "user");
            if(string.IsNullOrWhiteSpace(json)) {
                return false;
            }
            JwtResponse jwt = JsonSerializer.Deserialize<JwtResponse>(json);
            return jwt != null && !string.IsNullOrWhiteSpace(jwt.token);
        }

        public async Task Login(string username, string password) {
            UserAccount user = new UserAccount(username, password);
            HttpResponseMessage response = await httpClient.PostAsJsonAsync($"http://localhost:8080/authenticate", user);
            Console.WriteLine(JsonSerializer.Serialize(response));
            if(response.IsSuccessStatusCode) {
                 JwtResponse jwtResponse = await response.Content.ReadFromJsonAsync<JwtResponse>();
                 await jsRuntime.InvokeVoidAsync("localStorage.setItem", "user", JsonSerializer.Serialize(jwtResponse));
            }
        }
    }
}