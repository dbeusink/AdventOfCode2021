using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day5
{
    public class LineSegment
    {
        public Point A { get; }
        public Point B { get; }
        public Direction LineDirection { get; }

        public LineSegment(Point a, Point b)
        {
            A = a;
            B = b;

            if (a.X == b.X)
            {
                LineDirection = Direction.Y;
                if (A.Y > B.Y)
                {
                    A.SwapY(B);
                }
            }
            else if (a.Y == b.Y)
            {
                LineDirection = Direction.X;
                if (A.X > B.X)
                {
                    A.SwapX(B);
                }
            }
            else
            {
                LineDirection = Direction.Diagonal;
            }
        }

        public IEnumerable<Point> GetPoints()
        {
            if (LineDirection == Direction.X)
            {
                int y = A.Y;
                for (int x=A.X; x<=B.X; x++)
                {
                    yield return new Point(x, y);
                }
            }
            else if (LineDirection == Direction.Y)
            {
                int x = A.X;
                for (int y = A.Y; y <= B.Y; y++)
                {
                    yield return new Point(x, y);
                }
            }
            else
            {
                Point diag = A.Clone();
                int step = 0;
                while (!diag.Equals(B))
                {
                    diag.X = A.X > B.X ? A.X - step : A.X + step;
                    diag.Y = A.Y > B.Y ? A.Y - step : A.Y + step;
                    step++;
                    yield return diag;
                }
            }
        }

        public override string ToString()
        {
            return $"From A({A}) to B({B}). Direction: {LineDirection}";
        }
    }
}
