using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.AI.Planner;
using Unity.AI.Planner.Traits;
using Unity.Burst;
using Generated.AI.Planner.StateRepresentation;
using Generated.AI.Planner.StateRepresentation.SimpleBuildingProblem;
using Generated.Semantic.Traits.Enums;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace Generated.AI.Planner.Plans.SimpleBuildingProblem
{
    [BurstCompile]
    struct BuildBarracks : IJobParallelForDefer
    {
        public Guid ActionGuid;
        
        const int k_BuildingSpotIndex = 0;
        const int k_MaxArguments = 1;

        public static readonly string[] parameterNames = {
            "BuildingSpot",
        };

        [ReadOnly] NativeArray<StateEntityKey> m_StatesToExpand;
        StateDataContext m_StateDataContext;

        // local allocations
        [NativeDisableContainerSafetyRestriction] NativeArray<ComponentType> BuildingSpotFilter;
        [NativeDisableContainerSafetyRestriction] NativeList<int> BuildingSpotObjectIndices;

        [NativeDisableContainerSafetyRestriction] NativeList<ActionKey> ArgumentPermutations;
        [NativeDisableContainerSafetyRestriction] NativeList<BuildBarracksFixupReference> TransitionInfo;

        bool LocalContainersInitialized => ArgumentPermutations.IsCreated;

        internal BuildBarracks(Guid guid, NativeList<StateEntityKey> statesToExpand, StateDataContext stateDataContext)
        {
            ActionGuid = guid;
            m_StatesToExpand = statesToExpand.AsDeferredJobArray();
            m_StateDataContext = stateDataContext;
            BuildingSpotFilter = default;
            BuildingSpotObjectIndices = default;
            ArgumentPermutations = default;
            TransitionInfo = default;
        }

        void InitializeLocalContainers()
        {
            BuildingSpotFilter = new NativeArray<ComponentType>(2, Allocator.Temp){[0] = ComponentType.ReadWrite<BuildingSpot>(),[1] = ComponentType.ReadWrite<Location>(),  };
            BuildingSpotObjectIndices = new NativeList<int>(2, Allocator.Temp);

            ArgumentPermutations = new NativeList<ActionKey>(4, Allocator.Temp);
            TransitionInfo = new NativeList<BuildBarracksFixupReference>(ArgumentPermutations.Length, Allocator.Temp);
        }

        public static int GetIndexForParameterName(string parameterName)
        {
            
            if (string.Equals(parameterName, "BuildingSpot", StringComparison.OrdinalIgnoreCase))
                 return k_BuildingSpotIndex;

            return -1;
        }

        void GenerateArgumentPermutations(StateData stateData, NativeList<ActionKey> argumentPermutations)
        {
            BuildingSpotObjectIndices.Clear();
            stateData.GetTraitBasedObjectIndices(BuildingSpotObjectIndices, BuildingSpotFilter);
            
            var BuildingSpotBuffer = stateData.BuildingSpotBuffer;
            
            

            for (int i0 = 0; i0 < BuildingSpotObjectIndices.Length; i0++)
            {
                var BuildingSpotIndex = BuildingSpotObjectIndices[i0];
                var BuildingSpotObject = stateData.TraitBasedObjects[BuildingSpotIndex];
                
                if (!(BuildingSpotBuffer[BuildingSpotObject.BuildingSpotIndex].Occupied == false))
                    continue;
                

                var actionKey = new ActionKey(k_MaxArguments) {
                                                        ActionGuid = ActionGuid,
                                                       [k_BuildingSpotIndex] = BuildingSpotIndex,
                                                    };
                argumentPermutations.Add(actionKey);
            
            }
        }

        StateTransitionInfoPair<StateEntityKey, ActionKey, StateTransitionInfo> ApplyEffects(ActionKey action, StateEntityKey originalStateEntityKey)
        {
            var originalState = m_StateDataContext.GetStateData(originalStateEntityKey);
            var originalStateObjectBuffer = originalState.TraitBasedObjects;
            var originalBuildingSpotObject = originalStateObjectBuffer[action[k_BuildingSpotIndex]];

            var newState = m_StateDataContext.CopyStateData(originalState);
            var newBuildableBuffer = newState.BuildableBuffer;
            var newLocationBuffer = newState.LocationBuffer;
            TraitBasedObject newBarracksObject;
            TraitBasedObjectId newBarracksObjectId;

            var BarracksTypes = new NativeArray<ComponentType>(2, Allocator.Temp) {[0] = ComponentType.ReadWrite<Buildable>(), [1] = ComponentType.ReadWrite<Location>(), };
            {
                newState.AddObject(BarracksTypes, out newBarracksObject, out newBarracksObjectId);
            }
            BarracksTypes.Dispose();
            {
                    var @Buildable = newBuildableBuffer[newBarracksObject.BuildableIndex];
                    @Buildable.@BuildingType = BuildingType.Barracks;
                    newBuildableBuffer[newBarracksObject.BuildableIndex] = @Buildable;
            }
            {
                    var @Location = newLocationBuffer[newBarracksObject.LocationIndex];
                    @Location.Transform = newLocationBuffer[originalBuildingSpotObject.LocationIndex].Transform;
                    newLocationBuffer[newBarracksObject.LocationIndex] = @Location;
            }

            

            var reward = Reward(originalState, action, newState);
            var StateTransitionInfo = new StateTransitionInfo { Probability = 1f, TransitionUtilityValue = reward };
            var resultingStateKey = m_StateDataContext.GetStateDataKey(newState);

            return new StateTransitionInfoPair<StateEntityKey, ActionKey, StateTransitionInfo>(originalStateEntityKey, action, resultingStateKey, StateTransitionInfo);
        }

        float Reward(StateData originalState, ActionKey action, StateData newState)
        {
            var reward = 10f;

            return reward;
        }

        public void Execute(int jobIndex)
        {
            if (!LocalContainersInitialized)
                InitializeLocalContainers();

            m_StateDataContext.JobIndex = jobIndex;

            var stateEntityKey = m_StatesToExpand[jobIndex];
            var stateData = m_StateDataContext.GetStateData(stateEntityKey);

            ArgumentPermutations.Clear();
            GenerateArgumentPermutations(stateData, ArgumentPermutations);

            TransitionInfo.Clear();
            TransitionInfo.Capacity = math.max(TransitionInfo.Capacity, ArgumentPermutations.Length);
            for (var i = 0; i < ArgumentPermutations.Length; i++)
            {
                TransitionInfo.Add(new BuildBarracksFixupReference { TransitionInfo = ApplyEffects(ArgumentPermutations[i], stateEntityKey) });
            }

            // fixups
            var stateEntity = stateEntityKey.Entity;
            var fixupBuffer = m_StateDataContext.EntityCommandBuffer.AddBuffer<BuildBarracksFixupReference>(jobIndex, stateEntity);
            fixupBuffer.CopyFrom(TransitionInfo);
        }

        
        public static T GetBuildingSpotTrait<T>(StateData state, ActionKey action) where T : struct, ITrait
        {
            return state.GetTraitOnObjectAtIndex<T>(action[k_BuildingSpotIndex]);
        }
        
    }

    public struct BuildBarracksFixupReference : IBufferElementData
    {
        internal StateTransitionInfoPair<StateEntityKey, ActionKey, StateTransitionInfo> TransitionInfo;
    }
}


