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
            await SetBearerToken();
            return await httpClient.GetFromJsonAsync<Board[]>("http://localhost:8080/board") ?? new Board[0];
        }

        public async Task SaveBoard(CreateBoardRequest board)
        {
            await SetBearerToken();
            await httpClient.PostAsJsonAsync("http://localhost:8080/board", board);
        }

        public async Task<Pages.Board.Model.Board> GetBoard(string boardName)
        {
            await SetBearerToken();
            var board = await httpClient.GetFromJsonAsync<Pages.Board.Model.Board>($"http://localhost:8080/board/{boardName}");
            if(board == null) throw new Exception("Board not returned from service.");
            return board;
        }

        public async Task AddCardList(AddCardListRequest addCardListRequest)
        {
            await SetBearerToken();
            await httpClient.PostAsJsonAsync($"http://localhost:8080/board/{addCardListRequest.board}/cardlist", addCardListRequest);
        }

        public async Task AddCard(string board, AddCardRequest addCard)
        {
            await SetBearerToken();
            await httpClient.PostAsJsonAsync($"http://localhost:8080/board/{board}/cardlist/{addCard.cardlist}", addCard);
        }

        private async Task SetBearerToken()
        {
            string json = await jsRuntime.InvokeAsync<string>("localStorage.getItem", "user");
            if(string.IsNullOrWhiteSpace(json)) return;
            JwtResponse? jwt = JsonSerializer.Deserialize<JwtResponse>(json);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt?.token);
        }
    }
}
