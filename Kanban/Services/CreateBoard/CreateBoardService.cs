namespace Kanban.Services.CreateBoard
{
    public class CreateBoardService
    {
        private readonly KanbanApi kanbanApi;

        public CreateBoardService(KanbanApi kanbanApi)
        {
            this.kanbanApi = kanbanApi;
        }

        public async Task SaveBoard(CreateBoardRequest board)
        {
            await kanbanApi.SaveBoard(board);
        }
    }
}
