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
        public const string FieldType = "Type";
        public Generated.Semantic.Traits.Enums.BuildingType Type;

        public void SetField(string fieldName, object value)
        {
            switch (fieldName)
            {
                case nameof(Type):
                    Type = (Generated.Semantic.Traits.Enums.BuildingType)Enum.ToObject(typeof(Generated.Semantic.Traits.Enums.BuildingType), value);
                    break;
                default:
                    throw new ArgumentException($"Field \"{fieldName}\" does not exist on trait Buildable.");
            }
        }

        public object GetField(string fieldName)
        {
            switch (fieldName)
            {
                case nameof(Type):
                    return Type;
                default:
                    throw new ArgumentException($"Field \"{fieldName}\" does not exist on trait Buildable.");
            }
        }

        public bool Equals(Buildable other)
        {
            return Type == other.Type;
        }

        public override string ToString()
        {
            return $"Buildable\n  Type: {Type}";
        }
    }
}
