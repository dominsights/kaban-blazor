namespace Kanban.Pages.Board
{
    public record Board_(string Title, List<CardList> CardLists, List<Guid> ListIds);
}
