using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace _2019_day_6_universal_orbit_map
{
    class Program
    {
        static void Main(string[] args)
        {
            //var map = new Program("./test_map.txt");
            //var map = new Program("./test_map_2.txt"); // 54, 4
            var map = new Program("./mercury_map.txt"); // 150150, 352

            Console.WriteLine($"The total orbits is: {map.ComputeTotalOrbits()}");
            Console.WriteLine($"The minimum transfers is: {map.ComputeMinimumTransfers()}");
        }

        const string INVALID_INPUT_MSG = "Invalid Input. An orbit command must be in the format: 'name1)name2'";
        static readonly Regex ORBIT_MAP_PATTERN = new(@"^([A-Za-z0-9]+)\)([A-Za-z0-9]+)$");

        readonly Dictionary<string, SpaceObject> SpaceObjectDict;


        public Program(string orbitMapFileName)
        {
            SpaceObjectDict = CreateSpaceObjectTree(orbitMapFileName);
        }

        public int ComputeTotalOrbits()
        {
            var total = 0;
            foreach (var spaceObject in SpaceObjectDict.Values)
            {
                total += spaceObject.TotalOrbits;
            }
            return total;
        }

        public int ComputeMinimumTransfers(string a = "YOU", string b = "SAN")
        {
            var aExists = SpaceObjectDict.TryGetValue(a, out SpaceObject aSpaceObject);
            var bExists = SpaceObjectDict.TryGetValue(b, out SpaceObject bSpaceObject);

            if (!aExists) { throw new ArgumentException($"A SpaceObject with the name '{a}' does not exist."); }
            if (!bExists) { throw new ArgumentException($"A SpaceObject with the name '{b}' does not exist."); }

            var aRoute = aSpaceObject.GetRouteFromCOM().GetEnumerator();
            var bRoute = bSpaceObject.GetRouteFromCOM().GetEnumerator();

            while (aRoute.Current == bRoute.Current) { aRoute.MoveNext(); bRoute.MoveNext(); }
            var nearestCommonAncestor = aRoute.Current.OrbitParent;

            return aSpaceObject.OrbitParent.TotalOrbits + bSpaceObject.OrbitParent.TotalOrbits - (2 * nearestCommonAncestor.TotalOrbits);
        }

        static Dictionary<string, SpaceObject> CreateSpaceObjectTree(string orbitMapFileName)
        {
            var spaceObjectDict = new Dictionary<string, SpaceObject>();

            string[] lines = System.IO.File.ReadAllLines(orbitMapFileName);
            foreach (var line in lines)
            {
                var match = ORBIT_MAP_PATTERN.Match(line);
                if (!match.Success) { throw new ArgumentException(INVALID_INPUT_MSG); }

                var aName = match.Groups[1].ToString();
                var bName = match.Groups[2].ToString();

                var aExists = spaceObjectDict.TryGetValue(aName, out SpaceObject aSpaceObj);
                var bExists = spaceObjectDict.TryGetValue(bName, out SpaceObject bSpaceObj);

                if (aExists && bExists && bSpaceObj.OrbitParent == aSpaceObj)
                {
                    throw new ArgumentException($"'{bName}' orbits '{aName}' has already been added to the Orbit Map.");
                }

                if (!aExists)
                {
                    aSpaceObj = new SpaceObject(aName);
                    spaceObjectDict.Add(aName, aSpaceObj);
                }
                if (!bExists)
                {
                    bSpaceObj = new SpaceObject(bName, aSpaceObj);
                    spaceObjectDict.Add(bName, bSpaceObj);
                }

                bSpaceObj.OrbitParent = aSpaceObj;
            }

            if (!spaceObjectDict.ContainsKey("COM"))
            {
                throw new ArgumentException($"The map does not contain a Center of Mass.");
            }

            return spaceObjectDict;
        }
    }
}
