using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

    private bool _previewing;
    private Building _previewingBuilding;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_previewing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _previewingBuilding.InstantiatePrefab(GetMousePosition());
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

    public void StartPreviewBuilding(BuildingData buildingData)
    {
        _previewing = true;
        _previewingBuilding = new Building(buildingData);
        _previewingBuilding.InstantiatePrefab(GetMousePosition());
        Material previewMaterial =  Resources.Load($"Materials/BuildingPreview") as Material;
        _previewingBuilding.SetMaterial(previewMaterial);
    }

    public void BuildBuilding(BuildingData buildingData, Vector3 position)
    {
        bool canBuild = true;
        foreach (KeyValuePair<GameResourceData, int> resourceCost in buildingData.Cost)
        {
            if (resourceCost.Key.CanConsumeResource(resourceCost.Value))
            {
                canBuild = false;
                break;
            }
        }

        if (canBuild)
        {
            Building newBuilding = new Building(buildingData);
            newBuilding.InstantiatePrefab(position);
            foreach (KeyValuePair<GameResourceData, int> resourceCost in buildingData.Cost)
                resourceCost.Key.ConsumeResource(resourceCost.Value);
        }
        else
        {
            EndPreview();
        }
            
    }

    void EndPreview()
    {
        _previewing = false;
        _previewingBuilding = null;
    }

    Vector3 GetMousePosition()
    {
        Vector3 totalMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        Vector3 modifiedPosition = new Vector3(totalMousePosition.x, 0, totalMousePosition.z);
        return modifiedPosition;
    }
}
