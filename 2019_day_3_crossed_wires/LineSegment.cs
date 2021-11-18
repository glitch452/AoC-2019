using System;
namespace _2019_day_3_crossed_wires
{
    public class LineSegment
    {
        public Point Pt1 { get; }
        public Point Pt2 { get; }

        public bool IsHorizontal { get; }
        public bool IsVertical { get; }

        public int MinX { get; }
        public int MaxX { get; }
        public int MinY { get; }
        public int MaxY { get; }

        public LineSegment(Point pt1, Point pt2)
        {
            Pt1 = pt1;
            Pt2 = pt2;

            IsHorizontal = Pt1.Y == Pt2.Y;
            IsVertical = Pt1.X == Pt2.X;

            if (!IsHorizontal && !IsVertical)
            {
                throw new ArgumentException("Line Segment must be horizintal or vertical");
            }

            MinX = Math.Min(Pt1.X, Pt2.X);
            MaxX = Math.Max(Pt1.X, Pt2.X);
            MinY = Math.Min(Pt1.Y, Pt2.Y);
            MaxY = Math.Max(Pt1.Y, Pt2.Y);
    }

        public bool IsOverlapping(LineSegment other)
        {
            return Pt1.Y == other.Pt1.Y && (!(MinX > other.MaxX || MaxX < other.MinX));
        }

        public Point GetBestIntersection(LineSegment other)
        {
            var h = IsHorizontal ? this : other;
            var v = IsVertical ? this : other;
            if (IsHorizontal && other.IsHorizontal)
            {
                var isOverlapping = Pt1.Y == other.Pt1.Y && !(MinX > other.MaxX || MaxX < other.MinX);
                if (isOverlapping)
                {
                    var overlapMin = Math.Max(MinX, other.MinX);
                    var overlapMax = Math.Min(MaxX, other.MaxX);
                    if (overlapMax <= 0) { return new Point(overlapMax, Pt1.Y); }
                    if (overlapMin >= 0) { return new Point(overlapMin, Pt1.Y); }
                    return new Point(0, Pt1.Y);
                }
            }
            else if (IsVertical && other.IsVertical)
            {
                var isOverlapping = Pt1.X == other.Pt1.X && !(MinY > other.MaxY || MaxY < other.MinY);
                if (isOverlapping)
                {
                    var overlapMin = Math.Max(MinY, other.MinY);
                    var overlapMax = Math.Min(MaxY, other.MaxY);
                    if (overlapMax <= 0) { return new Point(Pt1.X, overlapMax); }
                    if (overlapMin >= 0) { return new Point(Pt1.X, overlapMin); }
                    return new Point(Pt1.X, 0);
                }
            }
            else if (!(h.MinX > v.Pt1.X || h.MaxX < v.Pt1.X) && !(v.MinY > h.Pt1.Y || v.MaxY < h.Pt1.Y))
            {
                return new Point(v.Pt1.X, h.Pt1.Y);
            }
            return null;
        }

        public override int GetHashCode() => (Pt1, Pt2).GetHashCode();
        public override bool Equals(object obj) => Equals(obj as LineSegment);

        public bool Equals(LineSegment ls)
        {
            if (ls is null) { return false; }
            if (ReferenceEquals(this, ls)) { return true; }
            if (GetType() != ls.GetType()) { return false; }
            return (Pt1 == ls.Pt1 && Pt2 == ls.Pt2) || (Pt1 == ls.Pt2 && Pt2 == ls.Pt1);
        }

        public static bool operator ==(LineSegment ls1, LineSegment ls2) => ls1.Equals(ls2);
        public static bool operator !=(LineSegment ls1, LineSegment ls2) => !(ls1 == ls2);

        public override string ToString() => $"{Pt1} -> {Pt2}";
    }
}
