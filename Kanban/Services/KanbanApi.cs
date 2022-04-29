using Kanban.Pages.Boards;
using System.Net.Http.Json;

namespace Kanban.Services
{
    public class KanbanApi
    {
        private readonly HttpClient httpClient;

        public KanbanApi(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Board[]> GetAllBoards()
        {
            return await httpClient.GetFromJsonAsync<Board[]>("http://localhost:8080/board");
        }

        public async Task SaveBoard(CreateBoardRequest board)
        {
            await httpClient.PostAsJsonAsync("http://localhost:8080/board", board);
        }

        public async Task<Pages.Board.Board_> GetBoard(string boardName)
        {
            return await httpClient.GetFromJsonAsync<Pages.Board.Board_>($"http://localhost:8080/board/{boardName}");
        }

        public async Task AddCardList(Pages.Board.AddCardListRequest addCardListRequest)
        {
            await httpClient.PostAsJsonAsync($"http://localhost:8080/board/{addCardListRequest.board}/cardlist", addCardListRequest);
        }
    }
}
