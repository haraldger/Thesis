using System;
using System.Collections.Generic;
using Unity.Semantic.Traits;
using Unity.Collections;
using Unity.Entities;

namespace Generated.Semantic.Traits
{
    [Serializable]
    public partial struct BuildingSpotData : ITraitData, IEquatable<BuildingSpotData>
    {
        public System.Boolean Occupied;

        public bool Equals(BuildingSpotData other)
        {
            return Occupied.Equals(other.Occupied);
        }

        public override string ToString()
        {
            return $"BuildingSpot: {Occupied}";
        }
    }
}
