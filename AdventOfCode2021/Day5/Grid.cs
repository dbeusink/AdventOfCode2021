using System.Drawing;
using System.Text;

namespace Day5
{
    public class Grid
    {
        private readonly int _maxX;
        private readonly int _maxY;
        private readonly int[,] _intersections;

        public Grid(int x, int y)
        {
            _maxX = x;
            _maxY = y;
            _intersections = new int[_maxY+1, _maxY+1];
        }

        public void CalculateIntersections(IEnumerable<LineSegment> lineSegments)
        {
            var segments = lineSegments.ToArray();
            foreach (var segment in segments)
            {
                foreach (var point in segment.GetPoints())
                {
                    _intersections[point.X, point.Y]++;
                }
            }
        }

        public int CalculateNumberOfOverlappedIntersections()
        {
            var count = 0;
            for (int x = 0; x <= _maxX; x++)
            {
                for (int y = 0; y <= _maxY; y++)
                {
                    if (_intersections[x, y] >= 2)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int y = 0; y <= _maxY; y++)
            {
                for (int x = 0; x <= _maxX; x++)
                {
                    if (_intersections[x, y] > 0)
                    {
                        sb.Append(_intersections[x, y]);
                    }
                    else
                    {
                        sb.Append('.');
                    }
                }
                if (y < _maxY)
                {
                    sb.AppendLine();
                }
            }
            return sb.ToString();
        }
    }
}
