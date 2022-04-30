namespace Kanban.Services.AddCard
{
    public class AddCardService
    {
        private readonly KanbanApi kanbanApi;

        public AddCardService(KanbanApi kanbanApi)
        {
            this.kanbanApi = kanbanApi;
        }

        public async Task AddCard(string board, AddCardRequest addCard)
        {
            await kanbanApi.AddCard(board, addCard);
        }
    }
}
