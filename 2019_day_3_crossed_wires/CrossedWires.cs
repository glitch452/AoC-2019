using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;


namespace _2019_day_3_crossed_wires
{
    public class CrossedWires
    {
        const string INVALID_INPUT_MSG = "Invalid Input. Please provide a comma separated list of directions i.e. U2,D5,R6,L1";
        static readonly Regex DIR_PATTERN = new Regex(@"^([UDLR])([1-9][0-9]*)$");

        public string[] Instructions1 { get; private set; }
        public string[] Instructions2 { get; private set; }

        public List<LineSegment> Segments1 { get; private set; }
        public List<LineSegment> Segments2 { get; private set; }

        public Point Best { get; private set; }


        public CrossedWires(string wire1, string wire2)
        {
            Instructions1 = parseInput(wire1);
            Instructions2 = parseInput(wire2);

            Segments1 = convertInstructionsToSegments(Instructions1);
            Segments2 = convertInstructionsToSegments(Instructions2);
        }

        public int Solve()
        {
            Point best = null;

            foreach (var seg1 in Segments1)
            {
                foreach (var seg2 in Segments2)
                {
                    var intersection = seg1.GetBestIntersection(seg2);
                    if (!(intersection is null) &&
                        intersection.ManhattanDistance > 0 &&
                        (best is null || intersection.ManhattanDistance < best.ManhattanDistance)
                       )
                    {
                        best = intersection;
                    }
                }
            }

            Best = best;
            return best is null ? 0 : best.ManhattanDistance;
        }

        static List<LineSegment> convertInstructionsToSegments(string[] instructions)
        {
            List<LineSegment> result = new List<LineSegment>(instructions.Length);
            Point current = new Point(0, 0);

            foreach (var dir in instructions)
            {
                var group = DIR_PATTERN.Match(dir).Groups;
                var direction = group[1].ToString();
                var magnitude = int.Parse(group[2].ToString());
                Point next;

                switch (direction)
                {
                    case "D": magnitude *= -1; goto case "U";
                    case "U": next = new Point(current.X, current.Y + magnitude); break;
                    case "L": magnitude *= -1; goto case "R";
                    case "R": next = new Point(current.X + magnitude, current.Y); break;
                    default: throw new ArgumentException(INVALID_INPUT_MSG);
                }

                result.Add(new LineSegment(current, next));
                current = next;
            }

            return result;
        }

        static string[] parseInput(string wire)
        {
            if (wire is null || wire.Length < 1)
            {
                throw new ArgumentException(INVALID_INPUT_MSG);
            }

            var result = wire.Split(',');
            foreach (var dir in result)
            {
                if (!DIR_PATTERN.IsMatch(dir))
                {
                    throw new ArgumentException(INVALID_INPUT_MSG);
                }
            }
            return result;
        }

    }
}
