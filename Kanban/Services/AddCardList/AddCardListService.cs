using System.Net.Http.Json;

namespace Kanban.Services.AddCardList
{
    public class AddCardListService
    {
        private readonly KanbanApi kanbanApi;

        public AddCardListService(KanbanApi kanbanApi)
        {
            this.kanbanApi = kanbanApi;
        }

        public async Task AddCardList(AddCardListRequest addCardListRequest)
        {
            await kanbanApi.AddCardList(addCardListRequest);
        }
    }
}
