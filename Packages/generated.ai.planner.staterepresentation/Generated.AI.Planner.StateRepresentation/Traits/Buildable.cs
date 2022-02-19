using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.AI.Planner.Traits;
using Generated.Semantic.Traits.Enums;

namespace Generated.AI.Planner.StateRepresentation
{
    [Serializable]
    public struct Buildable : ITrait, IBufferElementData, IEquatable<Buildable>
    {
        public const string FieldBuildingType = "BuildingType";
        public Generated.Semantic.Traits.Enums.BuildingType BuildingType;

        public void SetField(string fieldName, object value)
        {
            switch (fieldName)
            {
                case nameof(BuildingType):
                    BuildingType = (Generated.Semantic.Traits.Enums.BuildingType)Enum.ToObject(typeof(Generated.Semantic.Traits.Enums.BuildingType), value);
                    break;
                default:
                    throw new ArgumentException($"Field \"{fieldName}\" does not exist on trait Buildable.");
            }
        }

        public object GetField(string fieldName)
        {
            switch (fieldName)
            {
                case nameof(BuildingType):
                    return BuildingType;
                default:
                    throw new ArgumentException($"Field \"{fieldName}\" does not exist on trait Buildable.");
            }
        }

        public bool Equals(Buildable other)
        {
            return BuildingType == other.BuildingType;
        }

        public override string ToString()
        {
            return $"Buildable\n  BuildingType: {BuildingType}";
        }
    }
}
