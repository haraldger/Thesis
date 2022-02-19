using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Semantic.Traits;
using Unity.Entities;
using UnityEngine;

namespace Generated.Semantic.Traits
{
    [ExecuteAlways]
    [DisallowMultipleComponent]
    [AddComponentMenu("Semantic/Traits/Buildable (Trait)")]
    [RequireComponent(typeof(SemanticObject))]
    public partial class Buildable : MonoBehaviour, ITrait
    {
        public Generated.Semantic.Traits.Enums.BuildingType BuildingType
        {
            get
            {
                if (m_EntityManager != default && m_EntityManager.HasComponent<BuildableData>(m_Entity))
                {
                    m_p0 = m_EntityManager.GetComponentData<BuildableData>(m_Entity).BuildingType;
                }

                return m_p0;
            }
            set
            {
                BuildableData data = default;
                var dataActive = m_EntityManager != default && m_EntityManager.HasComponent<BuildableData>(m_Entity);
                if (dataActive)
                    data = m_EntityManager.GetComponentData<BuildableData>(m_Entity);
                data.BuildingType = m_p0 = value;
                if (dataActive)
                    m_EntityManager.SetComponentData(m_Entity, data);
            }
        }
        public BuildableData Data
        {
            get => m_EntityManager != default && m_EntityManager.HasComponent<BuildableData>(m_Entity) ?
                m_EntityManager.GetComponentData<BuildableData>(m_Entity) : GetData();
            set
            {
                if (m_EntityManager != default && m_EntityManager.HasComponent<BuildableData>(m_Entity))
                    m_EntityManager.SetComponentData(m_Entity, value);
            }
        }

        #pragma warning disable 649
        [SerializeField]
        [InspectorName("BuildingType")]
        Generated.Semantic.Traits.Enums.BuildingType m_p0 = (Generated.Semantic.Traits.Enums.BuildingType)0;
        #pragma warning restore 649

        EntityManager m_EntityManager;
        World m_World;
        Entity m_Entity;

        BuildableData GetData()
        {
            BuildableData data = default;
            data.BuildingType = m_p0;

            return data;
        }

        
        void OnEnable()
        {
            // Handle the case where this trait is added after conversion
            var semanticObject = GetComponent<SemanticObject>();
            if (semanticObject && !semanticObject.Entity.Equals(default))
                Convert(semanticObject.Entity, semanticObject.EntityManager, null);
        }

        public void Convert(Entity entity, EntityManager destinationManager, GameObjectConversionSystem _)
        {
            m_Entity = entity;
            m_EntityManager = destinationManager;
            m_World = destinationManager.World;

            if (!destinationManager.HasComponent(entity, typeof(BuildableData)))
            {
                destinationManager.AddComponentData(entity, GetData());
            }
        }

        void OnDestroy()
        {
            if (m_World != default && m_World.IsCreated)
            {
                m_EntityManager.RemoveComponent<BuildableData>(m_Entity);
                if (m_EntityManager.GetComponentCount(m_Entity) == 0)
                    m_EntityManager.DestroyEntity(m_Entity);
            }
        }

        void OnValidate()
        {

            // Commit local fields to backing store
            Data = GetData();
        }

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            TraitGizmos.DrawGizmoForTrait(nameof(BuildableData), gameObject,Data);
        }
#endif
    }
}
