namespace dutchonboard.Core.DomainServices;

public static class FilterService
{
    public static ICollection<BoardGame> FilterByStringListOfIds(this ICollection<BoardGame> boardGames, ICollection<string> ids)
    {
        var filteredBoardGames = new List<BoardGame>(); 
        foreach (var id in ids)
        {
            if (int.TryParse(id, out var parsedId))
            {
                var boardGame = boardGames.FirstOrDefault(p => p.Id == parsedId);

                if (boardGame != null)
                {
                    filteredBoardGames.Add(boardGame);
                }
            }
        }

        return filteredBoardGames;
    }
}