namespace Kanban.Services.GetAllBoards
{
    public class GetAllBoardsService
    {
        private readonly KanbanApi kanbanApi;

        public GetAllBoardsService(KanbanApi kanbanApi)
        {
            this.kanbanApi = kanbanApi;
        }

        public async Task<Pages.Boards.Model.Board[]> GetAllBoards()
        {
            return await kanbanApi.GetAllBoards();
        }
    }
}
