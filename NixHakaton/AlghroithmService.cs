using NixHakaton.Controllers;

namespace NixHakaton;

enum PositionType
{
    None,
    Defence,
    Win
}

public record Position(int X, int Y);

public class AlghroithmService
{
    int GetNextColumnNumber(MoveModel model)
    {
        List<Position> possiblePositions = GetPossiblePositions(model);
        List<Position> defencePositions = new List<Position>();
        possiblePositions.ForEach((possiblePosition) =>
        {
            var positionType = GetPossitionType(possiblePosition, model.Player);
    
        });

    }

    private PositionType GetPositionType(Position position, string gamerSymbol)
    {
        var oppositeString = gamerSymbol == "S"? "F" : "S";
        if()
    }



    private string? GetPosledVerticalItem(string[][] items, Position startPosition)
    {
        var col = startPosition.X;
        var row = startPosition.Y;
        if (row != 0)
        {
            
        }
    }

    List<Position> GetPossiblePositions(MoveModel model)
    {
        List<Position> possiblePositions = new List<Position>(9);
        for (var colNumber = 0; colNumber < 9; colNumber++)
        {
            if (model.Board[3][colNumber] == "_")
            {
                possiblePositions.Add(new Position(colNumber, 3));
                continue;
            }

            for (var rowNumber = 0; rowNumber < 2; rowNumber++)
            {
                if (model.Board[rowNumber][colNumber] == "_" && model.Board[rowNumber + 1][colNumber] != "_")
                {
                    possiblePositions.Add(new Position(colNumber, rowNumber));
                }
            }
        }

        return possiblePositions;
    }

}