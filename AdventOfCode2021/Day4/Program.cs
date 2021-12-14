using Day4;

// -- Settings --
bool loserMode = true; // Part 2: Keep playing till the last board has bingo (return the loser board)
bool useExampleInput = false;

// Input
var input = useExampleInput ?
    await File.ReadAllLinesAsync("input_example.txt") :
    await File.ReadAllLinesAsync("input.txt");

// Create and parse bingo boards
var bingoBoards = new List<BingoBoard>();
foreach (var batch in input.Skip(1).Chunk(6))
{
    var board = new BingoBoard();
    board.Parse(batch.Skip(1));
    bingoBoards.Add(board);
}

// Play
var stop = false;
var bingoNumbers = input[0].Split(',').Select(x => int.Parse(x));
foreach (var number in bingoNumbers)
{
    foreach(var board in bingoBoards.ToArray())
    {
        board.NewNumber(number);
        if (board.HasBingo())
        {
            var finalScore = board.GetUnmarkedSum() * number;
            Console.WriteLine($"The lucky number is {number}! BINGO! Final score: {finalScore}");
            Console.WriteLine(board);
            if (loserMode)
            {
                bingoBoards.Remove(board);
                if (bingoBoards.Count == 0)
                {
                    stop = true;
                    break;
                }
            }
            else
            {
                stop = true;
                break;
            }
        }
    }
    if (stop)
    {
        break;
    }
}

Console.WriteLine();
Console.WriteLine("Other boards:");
foreach (var board in bingoBoards)
{
    Console.WriteLine(board);
    Console.WriteLine();
}