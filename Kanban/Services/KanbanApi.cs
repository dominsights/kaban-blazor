using Kanban.Services.AddCard;
using Kanban.Services.AddCardList;
using Kanban.Services.CreateBoard;
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

        public async Task<Pages.Boards.Model.Board[]> GetAllBoards()
        {
            return await httpClient.GetFromJsonAsync<Pages.Boards.Model.Board[]>("http://localhost:8080/board");
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
    }
}
