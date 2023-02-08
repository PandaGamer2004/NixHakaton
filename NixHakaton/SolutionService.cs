using NixHakaton.Controllers;

namespace NixHakaton;

enum PositionType
{
    None,
    Defence,
    Win
}

public record Position(int X, int Y);

public class SolutionService
{
    public int GetNextColumnNumber(MoveModel model)
    {
        List<Position> possiblePositions = GetPossiblePositions(model);
        List<Position> defencePositions = new List<Position>();
        foreach (var possiblePosition in possiblePositions)
        {
            var positionType = GetPositionType(possiblePosition, model);
            if (positionType is PositionType.Win)
            {
                return possiblePosition.X;
            }

            if (positionType is PositionType.Defence)
            {
                defencePositions.Add(possiblePosition);
            }
        }

        if (defencePositions.Count == 0)
        {
            return possiblePositions[0].X;
        }

        var defensePosition = defencePositions.First();
        return defensePosition.X;
    }

    private PositionType GetPositionType(Position position, MoveModel model)
    {
        var oppositeSymbol = model.Player == "F" ? "S" : "F"; 
        var isWin = Check(position, model.Player, model.Board);
        if (isWin)
        {
            return PositionType.Win;
        }
        var isDefence = Check(position, oppositeSymbol, model.Board);
        return isDefence ? PositionType.Defence : PositionType.None;
    }


    private bool Check(Position position, string symbol, string[][] items)
    {
        var prevSymbol = items[position.Y][position.X];     
        items[position.Y][position.X] = symbol;
        bool vertical = CheckVertical(items, position.Y);
        bool horizontal = CheckHorizontal(items, position.X);
        bool diagonal = CheckDiagonal(items, position.X, position.Y);
        items[position.Y][position.X] = prevSymbol;
        var possibles = new[] { vertical, horizontal, diagonal };
        return possibles.Any();
    }

    private bool CheckDiagonal(string[][] items, int positionX, int positionY)
    {
        Func<int, bool> IsOnField = (int x) => x is >= 0 and < 9;
        int leftXBorder = positionX - positionY;
        int rightXBorder = positionX + positionY;

        bool MainLoop(Func<int, Position> nextPosition, Func<int, Position> curPosition)
        {
            bool succsess = true;
            for (var i = 0; i < 5; i++)
            {
                var (nextX, nextY) = nextPosition(i);
                var (curX, curY) = curPosition(i);
                if (items[curY][curX] != items[nextX][nextY])
                {
                    return false;
                }
            }

            return true;
        } 
        
        bool isLeftDiagonalEqual = IsOnField(leftXBorder) && IsOnField(leftXBorder + 3) && MainLoop((iteration) =>
            {
                var nextX = leftXBorder + iteration + 1;
                var nextY = iteration + 1;
                return new Position(nextX, nextY);
            }, (iteration) =>
            {
                var curX = leftXBorder + iteration;
                var curY = iteration;
                return new Position(curX, curY);
            });


        bool isRightDiagonalEqual = IsOnField(rightXBorder) && IsOnField(rightXBorder - 3) && MainLoop((iteration) =>
            {
                var nextX = rightXBorder - (iteration + 1);
                var nextY = iteration + 1;
                return new Position(nextX, nextY);
            }, (iteration) =>
            {
                var curX = rightXBorder - iteration;
                var curY = iteration;
                return new Position(curX, curY);
            });
        return (isLeftDiagonalEqual || isRightDiagonalEqual);
    }

    private bool CheckVertical(string[][] items, int y)
    {
        var verticalToCheck = items[y];
        int countOfSame = 1;
        for (var i = 1; i < 9; i++)
        {
            if (verticalToCheck[i - 1] == verticalToCheck[i])
            {
                countOfSame++;
            }
            else
            {
                countOfSame = 1;
            }

            if (countOfSame == 4)
            {
                return true;
            }
        }
        return false;
    }

    private bool CheckHorizontal(string[][] items, int x)
    {
        for (var i = 0; i < 5; i++)
        {
            if (items[i][x] != items[i + 1][x])
            {
                return false;
            }
        }

        return true;
    }


    
    
    List<Position> GetPossiblePositions(MoveModel model)
    {
        List<Position> possiblePositions = new List<Position>(9);
        for (var colNumber = 0; colNumber < 9; colNumber++)
        {
            if (model.Board[5][colNumber] == "_")
            {
                possiblePositions.Add(new Position(colNumber, 3));
                continue;
            }

            for (var rowNumber = 0; rowNumber < 5; rowNumber++)
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