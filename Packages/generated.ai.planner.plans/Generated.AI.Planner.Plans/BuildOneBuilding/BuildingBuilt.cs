using Unity.AI.Planner;
using Unity.Collections;
using Unity.Entities;
using Unity.AI.Planner.Traits;
using Generated.AI.Planner.StateRepresentation;
using Generated.AI.Planner.StateRepresentation.BuildOneBuilding;
using Generated.Semantic.Traits.Enums;

namespace Generated.AI.Planner.Plans.BuildOneBuilding
{
    public struct BuildingBuilt
    {
        public bool IsTerminal(StateData stateData)
        {
            var BuildingFilter = new NativeArray<ComponentType>(2, Allocator.Temp){[0] = ComponentType.ReadWrite<Buildable>(),[1] = ComponentType.ReadWrite<Location>(),  };
            var BuildingObjectIndices = new NativeList<int>(2, Allocator.Temp);
            stateData.GetTraitBasedObjectIndices(BuildingObjectIndices, BuildingFilter);
            var BuildableBuffer = stateData.BuildableBuffer;
            for (int i0 = 0; i0 < BuildingObjectIndices.Length; i0++)
            {
                var BuildingIndex = BuildingObjectIndices[i0];
                var BuildingObject = stateData.TraitBasedObjects[BuildingIndex];
            
                
                if (!(BuildableBuffer[BuildingObject.BuildableIndex].Type == BuildingType.Barracks))
                    continue;
                BuildingObjectIndices.Dispose();
                BuildingFilter.Dispose();
                return true;
            }
            BuildingObjectIndices.Dispose();
            BuildingFilter.Dispose();

            return false;
        }

        public float TerminalReward(StateData stateData)
        {
            var reward = 10f;

            return reward;
        }
    }
}
