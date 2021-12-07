

var input = await File.ReadAllLinesAsync("input.txt");
Console.WriteLine(GetPowerConsumption(input, input[0].Length));
Console.ReadKey();

static int GetPowerConsumption(IEnumerable<string> input, int nrOfUsedBits)
{
    int[,] commonBitCounter = new int[nrOfUsedBits, 2];
    foreach (var reading in input)
    {
        int val = Convert.ToInt32(reading, 2);
        int start = 1 << nrOfUsedBits - 1;

        for (int i = 0; i < nrOfUsedBits; i++)
        {
            if ((val & start) == start)
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