using Kanban.Services.AddCard;
using Kanban.Services.AddCardList;
using Kanban.Services.CreateBoard;
using Kanban.Services.GetAllBoards;
using System.Net.Http.Json;
using Microsoft.JSInterop;
using System.Text.Json;
using System.Net.Http.Headers;

namespace Kanban.Services
{
    public class KanbanApi
    {
        private readonly HttpClient httpClient;
        private readonly IJSRuntime jsRuntime;

        public KanbanApi(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
            this.httpClient = httpClient;
        }

        public async Task<Board[]> GetAllBoards()
        {
            string json = await jsRuntime.InvokeAsync<string>("localStorage.getItem", "jwt");
            JwtResponse jwt = JsonSerializer.Deserialize<JwtResponse>(json);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.token);
            return await httpClient.GetFromJsonAsync<Board[]>("http://localhost:8080/board");

        }

        public async Task SaveBoard(CreateBoardRequest board)
        {
            await httpClient.PostAsJsonAsync("http://localhost:8080/board", board);
        }

        public async Task<Pages.Board.Model.Board> GetBoard(string boardName)
        {
            return await httpClient.GetFromJsonAsync<Pages.Board.Model.Board>($"http://localhost:8080/board/{boardName}");
        }

        public async Task AddCardList(AddCardListRequest addCardListRequest)
        {
            await httpClient.PostAsJsonAsync($"http://localhost:8080/board/{addCardListRequest.board}/cardlist", addCardListRequest);
        }

        public async Task AddCard(string board, AddCardRequest addCard)
        {
            await httpClient.PostAsJsonAsync($"http://localhost:8080/board/{board}/cardlist/{addCard.cardlist}", addCard);
        }

        public async Task Login(string username, string password) {
            UserAccount user = new UserAccount(username, password);
            HttpResponseMessage response = await httpClient.PostAsJsonAsync($"http://localhost:8080/authenticate", user);
            Console.WriteLine(JsonSerializer.Serialize(response));
            if(response.IsSuccessStatusCode) {
                 JwtResponse jwtResponse = await response.Content.ReadFromJsonAsync<JwtResponse>();
                 await jsRuntime.InvokeVoidAsync("localStorage.setItem", "jwt", JsonSerializer.Serialize(jwtResponse));
            }
        }

        record UserAccount(string username, string password);
        record JwtResponse(string token);
    }
}
