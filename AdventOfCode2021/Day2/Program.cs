using Day2;
using System;

Console.WriteLine("Enter submarine movement one-by-one. Stop by typing 'x'.");

var readings = new List<Tuple<SubmarineMovement, int>>();
bool stop = false;
while (!stop)
{
    var input = Console.ReadLine();
    if (string.IsNullOrEmpty(input) || input == "x")
    {
        stop = true;
    }
    else
    {

        readings.Add(ParseReading(input));
    }
}

var navigator = new SubmarineNavigator();
foreach (var reading in readings)
{
    navigator.Move(reading.Item1, reading.Item2);
}

Console.WriteLine($"Product: {navigator.Product}");

Console.WriteLine("Press any key to continue...");
Console.ReadKey();

static Tuple<SubmarineMovement, int> ParseReading(ReadOnlySpan<char> input)
{
    var spaceIndex = input.IndexOf(' ');
    var movementString = input.Slice(0, spaceIndex).ToString();
    var units = input.Slice(spaceIndex + 1);
    var movement = movementString switch
    {
        "forward" => SubmarineMovement.Forward,
        "up" => SubmarineMovement.Up,
        "down" => SubmarineMovement.Down,
        _ => throw new InvalidDataException()
    };

    return new Tuple<SubmarineMovement, int>(movement, int.Parse(units));
}