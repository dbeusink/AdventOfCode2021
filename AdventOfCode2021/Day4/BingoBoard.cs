using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Day4
{
    public class BingoBoard
    {
        private readonly int _boardSize = 5;
        private readonly BingoBoardItem[,] _board;
        private readonly BingoBoardItem[,] _transposedBoard;

        public BingoBoard()
        {
            _board = new BingoBoardItem[_boardSize, _boardSize];
            _transposedBoard = new BingoBoardItem[_boardSize, _boardSize];
        }

        public void Parse(IEnumerable<string> input)
        {
            var inputValues = input.ToArray();
            if (inputValues.Length != _boardSize)
            {
                throw new InvalidDataException($"Amount of rows not the same size as the board. Expected '{_boardSize}', got '{inputValues.Length}'");
            }

            for (int i=0; i < _boardSize; i++)
            {
                var rowValues = Regex.Split(inputValues[i], " {1,}").Where(x => x != string.Empty).ToArray();
                if (rowValues.Length != _boardSize)
                {
                    throw new InvalidDataException($"Amount of columns is not the same size as the board. Expected '{_boardSize}', got '{rowValues.Length}'");
                }
                for (int j=0; j< _boardSize; j++)
                {
                    _board[i, j] = new BingoBoardItem(int.Parse(rowValues[j]));
                }
            }
            SetTransposedBoard();
        }

        public void NewNumber(int number)
        {
            for (int i = 0; i < _boardSize; i++)
            {
                for (int j = 0; j < _boardSize; j++)
                {
                    if (_board[i, j].Number == number)
                    {
                        _board[i, j].IsMarked = true;
                    }
                }
            }

        }

        public bool HasBingo()
        {
            for (int i = 0; i < _boardSize; i++)
            {
                bool fullRow = true;
                bool fullRowTransposed = true;
                for (int j = 0; j < _boardSize; j++)
                {
                    fullRow &= _board[i, j].IsMarked;
                    fullRowTransposed &= _transposedBoard[i, j].IsMarked;
                }

                if (fullRow || fullRowTransposed)
                {
                    return true;
                }
            }

            return false;
        }

        public int GetUnmarkedSum()
        {
            int sum = 0;
            for (int i = 0; i < _boardSize; i++)
            {
                for (int j = 0; j < _boardSize; j++)
                {
                    if (!_board[i, j].IsMarked)
                    {
                        sum += _board[i, j].Number;
                    }
                }
            }
            return sum;
        }

        private void SetTransposedBoard()
        {
            for (int i=0; i<_boardSize; i++)
            {
                for (int j=0; j<_boardSize; j++)
                {
                    _transposedBoard[i, j] = _board[j, i];
                }
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < _boardSize; i++)
            {
                for (int j = 0; j < _boardSize; j++)
                {
                    sb.Append(_board[i, j].IsMarked ? $"[{_board[i, j].Number:00}]" : $"-{_board[i, j].Number:00}-");
                    if (j < (_boardSize-1))
                    {
                        sb.Append(" ");
                    }
                }
                if (i < (_boardSize - 1))
                {
                    sb.AppendLine();
                }
            }
            return sb.ToString();
        }
    }
}

