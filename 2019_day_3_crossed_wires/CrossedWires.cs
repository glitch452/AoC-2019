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

        public (string direction, int magnitude)[] Instructions1 { get; private set; }
        public (string direction, int magnitude)[] Instructions2 { get; private set; }

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

        static List<LineSegment> convertInstructionsToSegments((string direction, int magnitude)[] instructions)
        {
            var result = new List<LineSegment>(instructions.Length);
            var current = new Point(0, 0);

            foreach (var (direction, magnitude) in instructions)
            {
                Point next = direction switch
                {
                    "D" => new Point(current.X, current.Y - magnitude),
                    "U" => new Point(current.X, current.Y + magnitude),
                    "L" => new Point(current.X - magnitude, current.Y),
                    "R" => new Point(current.X + magnitude, current.Y),
                    _ => throw new ArgumentException(INVALID_INPUT_MSG),
                };
                result.Add(new LineSegment(current, next));
                current = next;
            }

            return result;
        }

        static (string direction, int magnitude)[] parseInput(string wire)
        {
            if (wire is null || wire.Length < 1)
            {
                throw new ArgumentException(INVALID_INPUT_MSG);
            }

            var instructions = wire.Split(',');

            var result = instructions.Select((ins, i) => {

                var match = DIR_PATTERN.Match(ins);
                if (!match.Success)
                {
                    throw new ArgumentException(INVALID_INPUT_MSG);
                }

                var direction = match.Groups[1].ToString();
                var magnitude = int.Parse(match.Groups[2].ToString());
                return (direction, magnitude);
            });

            return result.ToArray();
        }

    }
}
