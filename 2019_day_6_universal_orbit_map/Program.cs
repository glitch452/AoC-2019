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

        Dictionary<string, SpaceObject> IngestMap;
        readonly SpaceObject CenterOfMass;


        public Program(string orbitMapFileName)
        {
            CenterOfMass = CreateSpaceObjectTree(orbitMapFileName);
        }

        public int ComputeTotalOrbits()
        {
            var total = 0;
            foreach (var spaceObject in IngestMap.Values)
            {
                total += spaceObject.ComputeTotalOrbits();
            }
            return total;
        }

        public int ComputeMinimumTransfers(string a = "YOU", string b = "SAN")
        {
            var aExists = IngestMap.TryGetValue(a, out SpaceObject aSpaceObject);
            var bExists = IngestMap.TryGetValue(b, out SpaceObject bSpaceObject);

            if (!aExists)
            {
                throw new ArgumentException($"A SpaceObject with the name '{a}' does not exist.");
            }
            if (!bExists)
            {
                throw new ArgumentException($"A SpaceObject with the name '{b}' does not exist.");
            }

            var aRoute = aSpaceObject.GetRouteFromCOM().GetEnumerator();
            var bRoute = bSpaceObject.GetRouteFromCOM().GetEnumerator();

            while (aRoute.Current == bRoute.Current)
            {
                aRoute.MoveNext();
                bRoute.MoveNext();
            }

            return aSpaceObject.OrbitParent.TotalOrbits + bSpaceObject.OrbitParent.TotalOrbits - (2 * (aRoute.Current.TotalOrbits - 1));
        }

        SpaceObject CreateSpaceObjectTree(string orbitMapFileName)
        {
            IngestMap = new Dictionary<string, SpaceObject>();

            string[] lines = System.IO.File.ReadAllLines(orbitMapFileName);
            foreach (var line in lines)
            {
                var match = ORBIT_MAP_PATTERN.Match(line);
                if (!match.Success) { throw new ArgumentException(INVALID_INPUT_MSG); }

                var aName = match.Groups[1].ToString();
                var bName = match.Groups[2].ToString();

                var aExists = IngestMap.TryGetValue(aName, out SpaceObject aSpaceObj);
                var bExists = IngestMap.TryGetValue(bName, out SpaceObject bSpaceObj);

                if (aExists && bExists && bSpaceObj.OrbitParent == aSpaceObj)
                {
                    throw new ArgumentException($"'{bName}' orbits '{aName}' has already been added to the Orbit Map.");
                }

                if (!aExists)
                {
                    aSpaceObj = new SpaceObject(aName);
                    IngestMap.Add(aName, aSpaceObj);
                    //Console.WriteLine($"Creating {aName}");
                }
                if (!bExists)
                {
                    bSpaceObj = new SpaceObject(bName, aSpaceObj);
                    IngestMap.Add(bName, bSpaceObj);
                    //Console.WriteLine($"Creating {bName}");
                }

                bSpaceObj.OrbitParent = aSpaceObj;
                aSpaceObj.OrbitChildren.Add(bSpaceObj);
                //Console.WriteLine($"Adding {bSpaceObj.Name} to the orbit of {aSpaceObj.Name}");
            }

            var comExists = IngestMap.TryGetValue("COM", out SpaceObject centerOfMass);

            if (!comExists)
            {
                throw new ArgumentException($"The map does not contain a Center of Mass.");
            }

            return centerOfMass;
        }
    }
}
