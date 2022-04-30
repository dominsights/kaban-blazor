namespace Kanban.Pages.Board.Model
{
    public record Board(string Title, List<CardList> CardLists, List<Guid> ListIds);
}
