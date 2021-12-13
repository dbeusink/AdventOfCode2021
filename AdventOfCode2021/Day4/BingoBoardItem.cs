using System;
namespace Day4
{
    public class BingoBoardItem
    {
        public int Number { get; }
        public bool IsMarked { get; set; } = false;

        public BingoBoardItem(int number)
        {
            Number = number;
        }
    }
}

