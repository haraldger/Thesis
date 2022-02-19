using System;
using System.Collections.Generic;
using Unity.Semantic.Traits;
using Unity.Collections;
using Unity.Entities;

namespace Generated.Semantic.Traits
{
    [Serializable]
    public partial struct BuildableData : ITraitData, IEquatable<BuildableData>
    {
        public Generated.Semantic.Traits.Enums.BuildingType BuildingType;

        public bool Equals(BuildableData other)
        {
            return BuildingType.Equals(other.BuildingType);
        }

        public override string ToString()
        {
            return $"Buildable: {BuildingType}";
        }
    }
}
