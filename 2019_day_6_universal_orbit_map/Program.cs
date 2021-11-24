using System;

namespace _2019_day_6_universal_orbit_map
{
    class Program
    {
        static void Main(string[] args)
        {
            //var map = new Program("./test_map.txt");
            //var map = new Program("./test_map_2.txt"); // 54, 4
            var map = new OrbitMap("./mercury_map.txt"); // 150150, 352

            Console.WriteLine($"The total orbits is: {map.ComputeTotalOrbits()}");
            Console.WriteLine($"The minimum transfers is: {map.ComputeMinimumTransfers()}");
        }

    }
}
