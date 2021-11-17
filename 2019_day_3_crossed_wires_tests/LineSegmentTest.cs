using System;
using Xunit;
using _2019_day_3_crossed_wires;

namespace _2019_day_3_crossed_wires_tests
{
    public class LineSegmentTest
    {
        static Point point1 = new Point(5, 12);
        static Point point2 = new Point(15, 22);

        static Point point3 = new Point(15, 12);
        static LineSegment horizontal_segment = new LineSegment(point1, point3);

        static Point point4 = new Point(5, 22);
        static LineSegment vertical_segment = new LineSegment(point1, point4);

        [Fact]
        public void ItCorrectlySetsThePoints()
        {
            Assert.Same(point1, horizontal_segment.Pt1);
            Assert.Same(point3, horizontal_segment.Pt2);
        }

        [Fact]
        public void ItShouldThrowAnExceptionForDiagonalLines()
        {
            Assert.Throws<ArgumentException>(() => new LineSegment(point1, point2));
        }

        [Fact]
        public void ItCorrectlyIdentifiesHorizontalLines()
        {
            Assert.True(horizontal_segment.IsHorizontal());
            Assert.False(vertical_segment.IsHorizontal());
        }

        [Fact]
        public void ItCorrectlyIdentifiesVerticalLines()
        {
            Assert.True(vertical_segment.IsVertical());
            Assert.False(horizontal_segment.IsVertical());
        }

        [Fact]
        public void ItShouldCompareForEqualityCorrectly()
        {
            var point1 = new Point(5, 12);
            var point3 = new Point(6, 12);
            var point4 = new Point(6, 15);

            var ls1 = new LineSegment(point1, point3);
            var ls2 = new LineSegment(point1, point3);
            var ls3 = new LineSegment(point3, point1);
            var ls4 = new LineSegment(point3, point4);

            Assert.True(ls1 == ls2);
            Assert.True(ls1 == ls3);
            Assert.True(ls1 != ls4);

            Assert.False(ls1.Equals(new object()));
            Assert.True(ls1.Equals(ls1));
            Assert.True(ls1.Equals(ls2));
            Assert.True(ls1.Equals(ls3));
            Assert.False(ls1.Equals(ls4));
        }

        [Fact]
        public void ItShouldReturnNullWhenThereIsNoIntersection()
        {
            var point1 = new Point(5, 12);
            var point2 = new Point(5, 10);
            var point3 = new Point(6, 8);
            var point4 = new Point(10, 8);

            var ls1 = new LineSegment(point1, point2);
            var ls2 = new LineSegment(point3, point4);
            Assert.Null(ls1.GetBestIntersection(ls2));

            var point5 = new Point(5, 25);
            var point6 = new Point(5, 30);
            var ls3 = new LineSegment(point5, point6);
            Assert.Null(ls1.GetBestIntersection(ls3));

            var point7 = new Point(15, 8);
            var point8 = new Point(20, 8);
            var ls4 = new LineSegment(point7, point8);
            Assert.Null(ls2.GetBestIntersection(ls4));
        }

        [Fact]
        public void ItShouldReturnTheBestIntersectionPoint()
        {
            var point1 = new Point(5, 8);
            var point2 = new Point(5, 3);
            var point3 = new Point(2, 5);
            var point4 = new Point(7, 5);
            var ls1 = new LineSegment(point1, point2);
            var ls2 = new LineSegment(point3, point4);
            var expected = new Point(5, 5);
            Assert.Equal(expected, ls1.GetBestIntersection(ls2));

            var point5 = new Point(5, 12);
            var point6 = new Point(5, 6);
            var ls3 = new LineSegment(point5, point6);
            var expected2 = new Point(5, 6);
            Assert.Equal(expected2, ls1.GetBestIntersection(ls3));

            var point7 = new Point(5, 7);
            var point8 = new Point(5, 4);
            var ls4 = new LineSegment(point7, point8);
            var expected3 = new Point(5, 4);
            Assert.Equal(expected3, ls1.GetBestIntersection(ls4));

            var point9 = new Point(-6, 5);
            var point10 = new Point(4, 5);
            var ls5 = new LineSegment(point9, point10);
            var expected4 = new Point(2, 5);
            Assert.Equal(expected4, ls2.GetBestIntersection(ls5));

            var point11 = new Point(3, 5);
            var point12 = new Point(6, 5);
            var ls6 = new LineSegment(point11, point12);
            var expected5 = new Point(3, 5);
            Assert.Equal(expected5, ls2.GetBestIntersection(ls6));

            var point13 = new Point(-12, 5);
            var point14 = new Point(6, 5);
            var point15 = new Point(-4, 5);
            var point16 = new Point(8, 5);
            var ls7 = new LineSegment(point13, point14);
            var ls8 = new LineSegment(point15, point16);
            var expected6 = new Point(0, 5);
            Assert.Equal(expected6, ls7.GetBestIntersection(ls8));
        }

    }
}
