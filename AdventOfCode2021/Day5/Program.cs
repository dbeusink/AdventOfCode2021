

// -- Settings --
using Day5;
using System.Text.RegularExpressions;

bool useExampleInput = false;

// Input
var input = useExampleInput ?
    await File.ReadAllLinesAsync("input_example.txt") :
    await File.ReadAllLinesAsync("input.txt");

var lineSegments = new List<LineSegment>();
var segmentRegex = new Regex(@"(\d{1,}),(\d{1,}) -> (\d{1,}),(\d{1,})");

int maxX = 0;
int maxY = 0;
foreach (var segmentInput in input)
{
    var match = segmentRegex.Match(segmentInput);
    if (match.Success)
    {
        var aX = int.Parse(match.Groups[1].Value);
        if (aX > maxX)
        {
            maxX = aX;
        }

        var aY = int.Parse(match.Groups[2].Value);
        if (aY > maxY)
        {
            maxY = aY;
        }

        var bX = int.Parse(match.Groups[3].Value);
        if (bX > maxX)
        {
            maxX = bX;
        }

        var bY = int.Parse(match.Groups[4].Value);
        if (bY > maxY)
        {
            maxY = bY;
        }

        var lineSegment = new LineSegment(new Point(aX, aY), new Point(bX, bY));
        lineSegments.Add(lineSegment);
    }
}

// Make grid
var grid = new Grid(maxX, maxY);
grid.CalculateIntersections(lineSegments);
if (useExampleInput)
{
    Console.WriteLine(grid);
}
Console.WriteLine($"Number of overlapped segments: {grid.CalculateNumberOfOverlappedIntersections()}");