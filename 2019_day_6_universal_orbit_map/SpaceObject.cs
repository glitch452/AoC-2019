using System;
using System.Collections.Generic;

namespace _2019_day_6_universal_orbit_map
{
    public class SpaceObject
    {

        public string Name;
        public SpaceObject OrbitParent;
        public List<SpaceObject> OrbitChildren { get; }
        public int TotalOrbits { get; private set; }

        public SpaceObject(string name, SpaceObject orbitParent = null, List<SpaceObject> orbitChildren = null)
        {
            Name = name;
            OrbitParent = orbitParent;
            if (orbitChildren is not null)
            {
                OrbitChildren = orbitChildren;
            }
            else
            {
                OrbitChildren = new List<SpaceObject>();
            }
            TotalOrbits = -1;
        }

        public int ComputeTotalOrbits()
        {
            if (TotalOrbits < 0)
            {
                TotalOrbits = 0;
                if (OrbitParent != null)
                {
                    TotalOrbits += 1 + OrbitParent.ComputeTotalOrbits();
                }
            }
            return TotalOrbits;
        }

        public IEnumerable<SpaceObject> GetRouteFromCOM()
        {
            var output = new List<SpaceObject>();
            var current = this.OrbitParent;

            while (current != null)
            {
                output.Add(current);
                current = current.OrbitParent;
            }

            output.Reverse();
            return output;
        }

    }
}
