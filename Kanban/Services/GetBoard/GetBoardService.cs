namespace Kanban.Services.GetBoard
{
    public class GetBoardService
    {
        private readonly KanbanApi kanbanApi;

        public GetBoardService(KanbanApi kanbanApi)
        {
            this.kanbanApi = kanbanApi;
        }

        public async Task<Pages.Board.Model.Board> GetBoard(string boardName)
        {
            return await kanbanApi.GetBoard(boardName);
        }
    }
}
