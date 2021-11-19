using System;

namespace _2019_day_3_crossed_wires
{
    class Program
    {

        static void Main(string[] args)
        {

            string wire1 = null;
            string wire2 = null;

            if (args.Length >= 2)
            {
                wire1 = args[0];
                wire2 = args[1];
            }

            while (true)
            {
                try
                {
                    if (wire1 is null)
                    {
                        Console.Write("Enter Wire 1's path: ");
                        wire1 = Console.ReadLine();
                    }

                    if (wire2 is null)
                    {
                        Console.Write("Enter Wire 2's path: ");
                        wire2 = Console.ReadLine();
                    }

                    var cw = new CrossedWires(wire1, wire2);
                    var distance = cw.ComputeClosestIntersection();
                    var steps = cw.ComputeFewestSteps();
                    Console.WriteLine($"The manhattan distance is: {distance}");
                    Console.WriteLine($"The fewest number of steps is: {steps}");
                    return;
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"An error has occurred: {ex.Message}");
                }
                wire1 = wire2 = null;
            }

        }

    }
}
