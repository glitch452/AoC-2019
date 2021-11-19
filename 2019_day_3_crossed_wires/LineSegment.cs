using System;
namespace _2019_day_3_crossed_wires
{
    public class LineSegment
    {
        public Point P1 { get; }
        public Point P2 { get; }

        public bool IsHorizontal { get; }
        public bool IsVertical { get; }

        public int MinX { get; }
        public int MaxX { get; }
        public int MinY { get; }
        public int MaxY { get; }

        public int Length { get; }

        public LineSegment(Point p1, Point p2)
        {
            P1 = p1;
            P2 = p2;

            IsHorizontal = P1.Y == P2.Y;
            IsVertical = P1.X == P2.X;

            if (!IsHorizontal && !IsVertical)
            {
                throw new ArgumentException("Line Segment must be horizintal or vertical");
            }

            MinX = Math.Min(P1.X, P2.X);
            MaxX = Math.Max(P1.X, P2.X);
            MinY = Math.Min(P1.Y, P2.Y);
            MaxY = Math.Max(P1.Y, P2.Y);
            Length = IsHorizontal ? MaxX - MinX : MaxY - MinY;
        }

        public bool IsOverlapping(LineSegment other)
        {
            return P1.Y == other.P1.Y && (!(MinX > other.MaxX || MaxX < other.MinX));
        }

        public Point GetIntersectionWithBestManDist(LineSegment other)
        {
            var h = IsHorizontal ? this : other;
            var v = IsVertical ? this : other;

            // Tests if the LineSegments interesect in any way before testing if they overlap
            if (!(h.MinX > v.MaxX || h.MaxX < v.MinX) && !(v.MinY > h.MaxY || v.MaxY < h.MinY))
            {
                var x = v.P1.X;
                var y = h.P1.Y;

                // Both Are Horizontal and overlapping
                if (v.IsHorizontal)
                {
                    var overlapMin = Math.Max(MinX, other.MinX);
                    var overlapMax = Math.Min(MaxX, other.MaxX);
                    if (overlapMax <= 0) { return new Point(overlapMax, y); }
                    if (overlapMin >= 0) { return new Point(overlapMin, y); }
                    return new Point(0, y);
                }

                // Both are Vertical and overlapping
                if (h.IsVertical)
                {
                    var overlapMin = Math.Max(MinY, other.MinY);
                    var overlapMax = Math.Min(MaxY, other.MaxY);
                    if (overlapMax <= 0) { return new Point(x, overlapMax); }
                    if (overlapMin >= 0) { return new Point(x, overlapMin); }
                    return new Point(x, 0);
                }

                return new Point(x, y);
            }
            
            return null;
        }

        public override int GetHashCode() => (P1, P2).GetHashCode();
        public override bool Equals(object obj) => Equals(obj as LineSegment);

        public bool Equals(LineSegment ls)
        {
            if (ls is null) { return false; }
            if (ReferenceEquals(this, ls)) { return true; }
            if (GetType() != ls.GetType()) { return false; }
            return (P1 == ls.P1 && P2 == ls.P2) || (P1 == ls.P2 && P2 == ls.P1);
        }

        public static bool operator ==(LineSegment ls1, LineSegment ls2) => ls1.Equals(ls2);
        public static bool operator !=(LineSegment ls1, LineSegment ls2) => !(ls1 == ls2);

        public override string ToString() => $"{P1} -> {P2}";
    }
}
