using Unity.AI.Planner;
using Unity.Collections;
using Unity.Entities;
using Unity.AI.Planner.Traits;
using Generated.AI.Planner.StateRepresentation;
using Generated.AI.Planner.StateRepresentation.BuildFourBuildings;
using Generated.Semantic.Traits.Enums;

namespace Generated.AI.Planner.Plans.BuildFourBuildings
{
    public struct FourBuildingsBuilt
    {
        public bool IsTerminal(StateData stateData)
        {
            var Building1Filter = new NativeArray<ComponentType>(2, Allocator.Temp){[0] = ComponentType.ReadWrite<Buildable>(),[1] = ComponentType.ReadWrite<Location>(),  };
            var Building1ObjectIndices = new NativeList<int>(2, Allocator.Temp);
            stateData.GetTraitBasedObjectIndices(Building1ObjectIndices, Building1Filter);
            var Building2Filter = new NativeArray<ComponentType>(2, Allocator.Temp){[0] = ComponentType.ReadWrite<Buildable>(),[1] = ComponentType.ReadWrite<Location>(),  };
            var Building2ObjectIndices = new NativeList<int>(2, Allocator.Temp);
            stateData.GetTraitBasedObjectIndices(Building2ObjectIndices, Building2Filter);
            var Building3Filter = new NativeArray<ComponentType>(2, Allocator.Temp){[0] = ComponentType.ReadWrite<Buildable>(),[1] = ComponentType.ReadWrite<Location>(),  };
            var Building3ObjectIndices = new NativeList<int>(2, Allocator.Temp);
            stateData.GetTraitBasedObjectIndices(Building3ObjectIndices, Building3Filter);
            var Building4Filter = new NativeArray<ComponentType>(2, Allocator.Temp){[0] = ComponentType.ReadWrite<Buildable>(),[1] = ComponentType.ReadWrite<Location>(),  };
            var Building4ObjectIndices = new NativeList<int>(2, Allocator.Temp);
            stateData.GetTraitBasedObjectIndices(Building4ObjectIndices, Building4Filter);
            var LocationBuffer = stateData.LocationBuffer;
            var BuildableBuffer = stateData.BuildableBuffer;
            for (int i0 = 0; i0 < Building1ObjectIndices.Length; i0++)
            {
                var Building1Index = Building1ObjectIndices[i0];
                var Building1Object = stateData.TraitBasedObjects[Building1Index];
            
                
                
                
                
                
                
                
                if (!(BuildableBuffer[Building1Object.BuildableIndex].Type == BuildingType.Barracks))
                    continue;
                
                
                
            for (int i1 = 0; i1 < Building2ObjectIndices.Length; i1++)
            {
                var Building2Index = Building2ObjectIndices[i1];
                var Building2Object = stateData.TraitBasedObjects[Building2Index];
            
                
                if (!(LocationBuffer[Building1Object.LocationIndex].Position != LocationBuffer[Building2Object.LocationIndex].Position))
                    continue;
                
                
                
                
                
                
                
                if (!(BuildableBuffer[Building2Object.BuildableIndex].Type == BuildingType.Barracks))
                    continue;
                
                
            for (int i2 = 0; i2 < Building3ObjectIndices.Length; i2++)
            {
                var Building3Index = Building3ObjectIndices[i2];
                var Building3Object = stateData.TraitBasedObjects[Building3Index];
            
                
                
                if (!(LocationBuffer[Building1Object.LocationIndex].Position != LocationBuffer[Building3Object.LocationIndex].Position))
                    continue;
                
                
                if (!(LocationBuffer[Building2Object.LocationIndex].Position != LocationBuffer[Building3Object.LocationIndex].Position))
                    continue;
                
                
                
                
                
                if (!(BuildableBuffer[Building3Object.BuildableIndex].Type == BuildingType.Barracks))
                    continue;
                
            for (int i3 = 0; i3 < Building4ObjectIndices.Length; i3++)
            {
                var Building4Index = Building4ObjectIndices[i3];
                var Building4Object = stateData.TraitBasedObjects[Building4Index];
            
                
                
                
                if (!(LocationBuffer[Building1Object.LocationIndex].Position != LocationBuffer[Building4Object.LocationIndex].Position))
                    continue;
                
                
                if (!(LocationBuffer[Building2Object.LocationIndex].Position != LocationBuffer[Building4Object.LocationIndex].Position))
                    continue;
                
                if (!(LocationBuffer[Building3Object.LocationIndex].Position != LocationBuffer[Building4Object.LocationIndex].Position))
                    continue;
                
                
                
                
                if (!(BuildableBuffer[Building4Object.BuildableIndex].Type == BuildingType.Barracks))
                    continue;
                Building1ObjectIndices.Dispose();
                Building1Filter.Dispose();
                Building2ObjectIndices.Dispose();
                Building2Filter.Dispose();
                Building3ObjectIndices.Dispose();
                Building3Filter.Dispose();
                Building4ObjectIndices.Dispose();
                Building4Filter.Dispose();
                return true;
            }
            }
            }
            }
            Building1ObjectIndices.Dispose();
            Building1Filter.Dispose();
            Building2ObjectIndices.Dispose();
            Building2Filter.Dispose();
            Building3ObjectIndices.Dispose();
            Building3Filter.Dispose();
            Building4ObjectIndices.Dispose();
            Building4Filter.Dispose();

            return false;
        }

        public float TerminalReward(StateData stateData)
        {
            var reward = 10f;

            return reward;
        }
    }
}
