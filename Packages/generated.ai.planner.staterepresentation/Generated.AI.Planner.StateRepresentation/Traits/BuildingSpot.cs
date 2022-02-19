using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.AI.Planner.Traits;
using Generated.Semantic.Traits.Enums;

namespace Generated.AI.Planner.StateRepresentation
{
    [Serializable]
    public struct BuildingSpot : ITrait, IBufferElementData, IEquatable<BuildingSpot>
    {
        public const string FieldOccupied = "Occupied";
        public System.Boolean Occupied;

        public void SetField(string fieldName, object value)
        {
            switch (fieldName)
            {
                case nameof(Occupied):
                    Occupied = (System.Boolean)value;
                    break;
                default:
                    throw new ArgumentException($"Field \"{fieldName}\" does not exist on trait BuildingSpot.");
            }
        }

        public object GetField(string fieldName)
        {
            switch (fieldName)
            {
                case nameof(Occupied):
                    return Occupied;
                default:
                    throw new ArgumentException($"Field \"{fieldName}\" does not exist on trait BuildingSpot.");
            }
        }

        public bool Equals(BuildingSpot other)
        {
            return Occupied == other.Occupied;
        }

        public override string ToString()
        {
            return $"BuildingSpot\n  Occupied: {Occupied}";
        }
    }
}
