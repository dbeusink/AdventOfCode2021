using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day5
{
    public class Point : IEquatable<Point>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void SwapX(Point other)
        {
            var x = this.X;
            this.X = other.X;
            other.X = x;
        }

        public void SwapY(Point other)
        {
            var y = this.Y;
            this.Y = other.Y;
            other.Y = y;

        }

        public Point Clone()
        {
            return new Point(X, Y);
        }

        public override string ToString()
        {
            return $"X: {X}, Y: {Y}";
        }

        public bool Equals(Point other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Point);
        }
    }
}
