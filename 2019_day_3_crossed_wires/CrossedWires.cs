using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;


namespace _2019_day_3_crossed_wires
{
    public class CrossedWires
    {
        const string INVALID_INPUT_MSG = "Invalid Input. Please provide a comma separated list of directions i.e. U2,D5,R6,L1";
        static readonly Regex DIR_PATTERN = new(@"^([UDLR])([1-9][0-9]*)$");

        public (string direction, int magnitude)[] Instructions1 { get; }
        public (string direction, int magnitude)[] Instructions2 { get; }

        public List<LineSegment> Segments1 { get; }
        public List<LineSegment> Segments2 { get; }

        public Point ClosestIntersection { get; private set; }
        public Point FewestStepsIntersection { get; private set; }


        public CrossedWires(string wire1, string wire2)
        {
            Instructions1 = ParseInput(wire1);
            Instructions2 = ParseInput(wire2);

            Segments1 = ConvertInstructionsToSegments(Instructions1);
            Segments2 = ConvertInstructionsToSegments(Instructions2);
        }

        public int ComputeIntersectionWithFewestSteps()
        {

            return 0;
        }

        public int ComputeClosestIntersection()
        {
            ClosestIntersection = null;

            foreach (var seg1 in Segments1)
            {
                foreach (var seg2 in Segments2)
                {
                    var intersection = seg1.GetIntersectionWithBestManDist(seg2);
                    if (intersection is not null &&
                        intersection.ManhattanDistance > 0 &&
                        (ClosestIntersection is null || intersection.ManhattanDistance < ClosestIntersection.ManhattanDistance)
                       )
                    {
                        ClosestIntersection = intersection;
                    }
                }
            }

            return ClosestIntersection is null ? -1 : ClosestIntersection.ManhattanDistance;
        }

        public int ComputeFewestSteps()
        {
            FewestStepsIntersection = null;
            var best_steps = int.MaxValue;

            var wire1_length = 0;
            foreach (var seg1 in Segments1)
            {
                var wire2_length = 0;
                foreach (var seg2 in Segments2)
                {
                    var intersection = seg1.GetIntersectionWithFewestSteps(seg2);
                    if (intersection is not null)
                    {
                        var wire1_seg_steps = new LineSegment(seg1.P1, intersection).Length;
                        var wire2_seg_steps = new LineSegment(seg2.P1, intersection).Length;
                        var current_steps = wire1_length + wire2_length + wire1_seg_steps + wire2_seg_steps;

                        if (current_steps > 0 && current_steps < best_steps)
                        {
                            FewestStepsIntersection = intersection;
                            best_steps = current_steps;
                        }

                    }
                    wire2_length += seg2.Length;
                }
                wire1_length += seg1.Length;
            }

            return FewestStepsIntersection is null ? -1 : best_steps;
        }

        static List<LineSegment> ConvertInstructionsToSegments((string direction, int magnitude)[] instructions)
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

        static (string direction, int magnitude)[] ParseInput(string wire)
        {
            if (wire is null || wire.Length < 1) { throw new ArgumentException(INVALID_INPUT_MSG); }

            var result = wire.Split(',').Select((instruction, i) =>
            {
                var match = DIR_PATTERN.Match(instruction);
                if (!match.Success) { throw new ArgumentException(INVALID_INPUT_MSG); }

                var direction = match.Groups[1].ToString();
                var magnitude = int.Parse(match.Groups[2].ToString());
                return (direction, magnitude);
            });

            return result.ToArray();
        }

    }
}
