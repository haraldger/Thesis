using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

    private bool _previewing;
    private GameUnit _previewingBuilding;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_previewing)
        {
            if (Input.GetMouseButtonUp(0))
            {
                BuildingController controller;
                BuildBuilding((BuildingData)_previewingBuilding.Data, GetMousePosition(), out controller);
                EndPreview();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                EndPreview();
            }
            else
            {
                _previewingBuilding.SetWorldPosition(GetMousePosition());
            }
        }
    }

    public void StartPreviewBuilding(BuildingData data)
    {
        _previewing = true;
        _previewingBuilding = new GameUnit(data);
        _previewingBuilding.InstantiatePrefab(GetMousePosition());
        _previewingBuilding.Instance.tag = "Preview";
        GameObject.Destroy(_previewingBuilding.Instance.GetComponentInChildren<NavMeshObstacle>());
        Material previewMaterial =  Resources.Load($"Materials/BuildingPreview") as Material;
        _previewingBuilding.SetMaterial(previewMaterial);
    }


    public bool BuildBuilding(BuildingData buildingData, Vector3 position, out BuildingController newBuilding)
    {
        bool canBuild = true;

        // Check resource constraints
        foreach (CostValue cost in buildingData.costs)
        {
            if (!Globals.RESOURCE_DATA[cost.code].CanConsumeResource(cost.value))
            {
                canBuild = false;
                break;
            }
        }

        if (canBuild)
        {
            GameUnit building = new GameUnit(buildingData);
            building.InstantiatePrefab(position);
            foreach (CostValue cost in building.Data.costs)
                Globals.RESOURCE_DATA[cost.code].ConsumeResource(cost.value);
            var controller = building.Instance.GetComponentInChildren<BuildingController>();
            Globals.EXISTING_UNITS[controller] = building;

            newBuilding = controller;
            return true;
        }
        else
        {
            EndPreview();
            newBuilding = null;
            return false;
        }
    }

    void EndPreview()
    {
        if (_previewingBuilding != null) _previewingBuilding.Destroy();
        _previewing = false;
        _previewingBuilding = null;
    }

    Vector3 GetMousePosition()
    {
        Vector3 totalMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        Vector3 modifiedPosition = new Vector3(totalMousePosition.x, 0, totalMousePosition.z);
        return modifiedPosition;
    }

    //bool CanBuildAtPosition(Vector3 position)
    //{

    //}

    
}
