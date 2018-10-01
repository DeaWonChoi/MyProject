using RedTheSettlers.GameSystem;
using RedTheSettlers.Tiles;
using System.Collections.Generic;

namespace RedTheSettlers.Users
{
    public interface IAIStrategy
    {
        BoardTile CalculateTileWeight(BoardTile boardTile, ItemData[] itemData);
    }
}
