using System;
using Xunit;
using _2019_day_3_crossed_wires;
using System.Collections.Generic;

namespace _2019_day_3_crossed_wires_tests
{
    public class CrossedWiresTests
    {
        string CORRECT_INPUT = "U1,D2,L3,R4";


        [Fact]
        public void ItAcceptsGoodInput()
        {
            var cw = new CrossedWires(CORRECT_INPUT, CORRECT_INPUT);
            var expected = new string[] { "U1", "D2", "L3", "R4" };
            Assert.Equal(expected, cw.Instructions1);
            Assert.Equal(expected, cw.Instructions2);
        }

        [Fact]
        public void ItRejectsBadInputForWire1()
        {
            Assert.Throws<ArgumentException>(() => new CrossedWires("", CORRECT_INPUT));
            Assert.Throws<ArgumentException>(() => new CrossedWires(null, CORRECT_INPUT));
            Assert.Throws<ArgumentException>(() => new CrossedWires("abcd", CORRECT_INPUT));
            Assert.Throws<ArgumentException>(() => new CrossedWires("R0,L2", CORRECT_INPUT));
            Assert.Throws<ArgumentException>(() => new CrossedWires("UR1,L2", CORRECT_INPUT));
            Assert.Throws<ArgumentException>(() => new CrossedWires("U1|L2", CORRECT_INPUT));
        }

        [Fact]
        public void ItRejectsBadInputForWire2()
        {
            Assert.Throws<ArgumentException>(() => new CrossedWires(CORRECT_INPUT, ""));
            Assert.Throws<ArgumentException>(() => new CrossedWires(CORRECT_INPUT, null));
            Assert.Throws<ArgumentException>(() => new CrossedWires(CORRECT_INPUT, "abcd"));
            Assert.Throws<ArgumentException>(() => new CrossedWires(CORRECT_INPUT, "R0,L2"));
            Assert.Throws<ArgumentException>(() => new CrossedWires(CORRECT_INPUT, "UR1,L2"));
            Assert.Throws<ArgumentException>(() => new CrossedWires(CORRECT_INPUT, "U1|L2"));
        }

        [Fact]
        public void ItShouldConvertSingleDireectionsToSegmentsCorrectly()
        {
            var origin = new Point(0, 0);

            var horizontal = new CrossedWires("L1", "R1");
            var horizontal_expected_L = new LineSegment(origin, new Point(-1, 0));
            var horizontal_expected_R = new LineSegment(origin, new Point(1, 0));

            var vertical = new CrossedWires("U1", "D1");
            var vertical_expected_U = new LineSegment(origin, new Point(0, 1));
            var vertical_expected_D = new LineSegment(origin, new Point(0, -1));

            Assert.Equal(horizontal_expected_L, horizontal.Segments1[0]);
            Assert.Equal(horizontal_expected_R, horizontal.Segments2[0]);

            Assert.Equal(vertical_expected_U, vertical.Segments1[0]);
            Assert.Equal(vertical_expected_D, vertical.Segments2[0]);
        }

        [Fact]
        public void ItShouldConvertMultipleDireectionsToSegmentsCorrectly()
        {
            var origin = new Point(0, 0);
            var p1 = new Point(-4, 0);
            var p2 = new Point(-4, 6);
            var p3 = new Point(-4, -1);
            var p4 = new Point(5, -1);

            var expected = new List<LineSegment>
            {
                new LineSegment(origin, p1),
                new LineSegment(p1, p2),
                new LineSegment(p2, p3),
                new LineSegment(p3, p4),
            };

            var multi = new CrossedWires("L4,U6,D7,R9", "R1");

            Assert.Equal(expected, multi.Segments1);
        }

        [Fact]
        public void ItShouldProduceAScoreOfZeroWithNoIntersections()
        {
            var cw = new CrossedWires("L4", "U5");
            var score = cw.Solve();
            var expected_score = 0;
            Assert.Equal(expected_score, score);
        }

        [Fact]
        public void ItShouldFindTheBestIntersection()
        {
            var cw = new CrossedWires("L4,U3,R8", "U5");
            var expected_point = new Point(0, 3);
            var expected_score = 3;
            var score = cw.Solve();
            Assert.Equal(expected_score, score);
            Assert.Equal(expected_point, cw.Best);
        }

        [Fact]
        public void ItShouldPassProvidedExample()
        {
            var cw = new CrossedWires("R8,U5,L5,D3", "U7,R6,D4,L4");
            var expected_point = new Point(3, 3);
            var expected_score = 6;
            var score = cw.Solve();
            Assert.Equal(expected_score, score);
            Assert.Equal(expected_point, cw.Best);
        }

        [Fact]
        public void ItShouldPassProvidedTest1()
        {
            var cw = new CrossedWires("R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83");
            var expected_point = new Point(155, 4);
            var expected_score = 159;
            var score = cw.Solve();
            Assert.Equal(expected_score, score);
            Assert.Equal(expected_point, cw.Best);
        }

        [Fact]
        public void ItShouldPassProvidedTest2()
        {
            var cw = new CrossedWires("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7");
            var expected_point = new Point(124, 11);
            var expected_score = 135;
            var score = cw.Solve();
            Assert.Equal(expected_score, score);
            Assert.Equal(expected_point, cw.Best);
        }
    }
}
