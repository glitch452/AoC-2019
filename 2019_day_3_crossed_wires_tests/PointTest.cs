using System;
using Xunit;
using _2019_day_3_crossed_wires;

namespace _2019_day_3_crossed_wires_tests
{
    public class PointTest
    {

        [Fact]
        public void ItCorrectlySetsTheCoordinates()
        {
            var point = new Point(5, 12);
            Assert.Equal(5, point.X);
            Assert.Equal(12, point.Y);
        }

        [Fact]
        public void ItShouldCompareForEqualityCorrectly()
        {
            var point1 = new Point(5, 12);
            var point2 = new Point(5, 12);
            var point3 = new Point(6, 12);
            var point4 = new Point(5, 15);

            Assert.True(point1 == point2);
            Assert.True(point1 != point3);
            Assert.True(point1 != point4);

            Assert.False(point1.Equals(new object()));
            Assert.True(point1.Equals(point2));
            Assert.False(point1.Equals(point3));
            Assert.False(point1.Equals(point4));
        }

        [Fact]
        public void ItComputesTheManhattanDistanceCorrectly()
        {
            var point1 = new Point(5, 12);
            Assert.Equal(17, point1.ManhattanDistance);

            var point2 = new Point(-5, 12);
            Assert.Equal(17, point2.ManhattanDistance);

            var point3 = new Point(5, -12);
            Assert.Equal(17, point3.ManhattanDistance);

            var point4 = new Point(-5, -12);
            Assert.Equal(17, point4.ManhattanDistance);
        }

    }
}
