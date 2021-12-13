using Day4;

//var input = await File.ReadAllLinesAsync("input_example.txt");
var input = await File.ReadAllLinesAsync("input.txt");
bool loserMode = true;

var bingoBoards = new List<BingoBoard>();
var bingoNumbers = input[0].Split(',').Select(x => int.Parse(x));

foreach (var batch in input.Skip(1).Chunk(6))
{
    var board = new BingoBoard();
    board.Parse(batch.Skip(1));
    bingoBoards.Add(board);
}

var stop = false;
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