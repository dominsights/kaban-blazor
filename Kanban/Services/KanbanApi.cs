using System.Net.Http.Json;
using static Kanban.Pages.Boards;

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
            await httpClient.PostAsJsonAsync<CreateBoardRequest>("http://localhost:8080/board", board);
        }
    }

}
