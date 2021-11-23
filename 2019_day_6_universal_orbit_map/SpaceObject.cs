using System;
using System.Collections.Generic;

namespace _2019_day_6_universal_orbit_map
{
    public class SpaceObject
    {
        public string Name;
        public SpaceObject OrbitParent;
        int _totalOrbits = -1;

        public SpaceObject(string name, SpaceObject orbitParent = null)
        {
            Name = name;
            OrbitParent = orbitParent;
            _totalOrbits = -1;
        }


        public int TotalOrbits
        {
            get
            {
                if (_totalOrbits < 0)
                {
                    _totalOrbits = 0;
                    if (OrbitParent is not null) { _totalOrbits += 1 + OrbitParent.TotalOrbits; }
                }
                return _totalOrbits;
            }
        }

        public IEnumerable<SpaceObject> GetRouteFromCOM()
        {
            var output = new List<SpaceObject>(TotalOrbits);
            var current = OrbitParent;

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
