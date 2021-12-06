Console.WriteLine("Enter sonar sweep readings one-by-one. Stop by typing 'x'.");

var readings = new List<int>();
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
        readings.Add(int.Parse(input));
    }
}

var ltp = CalculateMeasurementsLargerThanPrevious(ReadingsAsSlidingWindows(readings.ToArray(), 3).ToArray());
Console.WriteLine($"Measurement larger than previous measurement: {ltp}");

Console.WriteLine("Press any key to continue...");
Console.ReadKey();


int CalculateMeasurementsLargerThanPrevious(int[] readings)
{
    var ltp = 0;
    for (int i = 0; i < readings.Length; i++)
    {
        Console.Write(readings[i]);

        if (i == 0)
        {
            Console.WriteLine(" (N/A - no previous measurement/sum)");
        }
        else if (readings[i] > readings[i - 1])
        {
            ltp++;
            Console.WriteLine(" (increased)");
        }
        else if (readings[i] < readings[i - 1])
        {
            Console.WriteLine(" (decreased)");
        }
        else
        {
            Console.WriteLine(" (no change)");
        }
    }
    return ltp;
}

IEnumerable<int> ReadingsAsSlidingWindows(int[] readings, int windowSize)
{
    int sum = 0;
    for (int i = 0; i < readings.Length; i++)
    {
        sum += readings[i];

        if (i > windowSize - 1)
        {
            sum -= readings[i - windowSize];
        }

        if (i >= windowSize - 1)
        {
            yield return sum;
        }
    }
}