

//var input = await File.ReadAllLinesAsync("input_example.txt");
var input = await File.ReadAllLinesAsync("input.txt");

// Part 1
Console.WriteLine($"Power consumption: {GetPowerConsumption(input, input[0].Length)}");

// Part 2
var oxygenRating = GetAirRating(input, input[0].Length, true);
var co2ScrubberRating = GetAirRating(input, input[0].Length, false);

Console.WriteLine($"Oxygen rating: {oxygenRating}");
Console.WriteLine($"C02 scrubber rating: {co2ScrubberRating}");
Console.WriteLine($"Life support rating: {oxygenRating*co2ScrubberRating}");
Console.ReadKey();

static int GetPowerConsumption(IEnumerable<string> input, int nrOfUsedBits)
{
    var numericInput = input.Select(x => Convert.ToInt32(x, 2)).ToArray();
    var commonBitCounter = GetMostCommonBits(numericInput, nrOfUsedBits);

    int gamma = 0;
    for (int i = 0; i < nrOfUsedBits; i++)
    {
        if (i > 0)
        {
            gamma <<= 1;
        }
        if (commonBitCounter[i, 1] > commonBitCounter[i, 0])
        {
            gamma |= 1;
        }
    }

    int epsilon = ~gamma;
    epsilon <<= (32 - nrOfUsedBits);
    epsilon >>= (32 - nrOfUsedBits);

    Console.WriteLine($"Gamma: {gamma}");
    Console.WriteLine($"Epsilon: {epsilon}");

    return gamma * epsilon;
}

static int GetAirRating(IEnumerable<string> input, int nrOfUsedBits, bool considerMostCommon)
{
    var numericInput = input.Select(x => Convert.ToInt32(x, 2)).ToArray();
    var commonBitCounter = GetMostCommonBits(numericInput, nrOfUsedBits);

    var filteredReadings = numericInput;
    for (int i=0; i<nrOfUsedBits; i++)
    {
        var bitMask = 1 << (nrOfUsedBits - 1 - i);

        if (commonBitCounter[i, 1] >= commonBitCounter[i, 0])
        {
            filteredReadings = considerMostCommon ?
                filteredReadings.Where(x => (x & bitMask) == bitMask).ToArray() :
                filteredReadings.Where(x => (x & bitMask) != bitMask).ToArray();
        }
        else
        {
            filteredReadings = considerMostCommon ?
                filteredReadings.Where(x => (x & bitMask) != bitMask).ToArray() :
                filteredReadings.Where(x => (x & bitMask) == bitMask).ToArray();
        }

        if (filteredReadings.Length == 1)
        {
            return filteredReadings[0];
        }
        else
        {
            commonBitCounter = GetMostCommonBits(filteredReadings, nrOfUsedBits);
        }
    }

    return 0;
}

static int[,] GetMostCommonBits(IEnumerable<int> input, int nrOfUsedBits)
{
    int[,] commonBitCounter = new int[nrOfUsedBits, 2];
    foreach (var reading in input)
    {
        int start = 1 << nrOfUsedBits - 1;

        for (int i = 0; i < nrOfUsedBits; i++)
        {
            if ((reading & start) == start)
            {
                commonBitCounter[i, 1]++;
            }
            else
            {
                commonBitCounter[i, 0]++;
            }
            start >>= 1;
        }
    }

    return commonBitCounter;
}