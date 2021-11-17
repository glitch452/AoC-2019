using System;
namespace _2019_day_3_crossed_wires
{
    public class Point
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int ManhattanDistance { get; private set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
            ManhattanDistance = Math.Abs(X) + Math.Abs(Y);
        }

        public override int GetHashCode() => (X, Y).GetHashCode();
        public override bool Equals(object obj) => Equals(obj as Point);

        public bool Equals(Point p)
        {
            if (p is null) { return false; }
            if (ReferenceEquals(this, p)) { return true; }
            if (GetType() != p.GetType()) { return false; }
            return X == p.X && Y == p.Y;
        }

        public static bool operator ==(Point p1, Point p2) => p1.Equals(p2);
        public static bool operator !=(Point p1, Point p2) => !(p1 == p2);

        public override string ToString() => $"({X},{Y})";
    }
}
