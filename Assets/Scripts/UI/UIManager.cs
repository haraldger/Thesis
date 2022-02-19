using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } // this

    // UI elements
    public Transform buildingMenuPanel;
    public GameObject buildingButtonPrefab;
    public Transform resourcePanel;
    public GameObject resourceLabelPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        _buildingData = Globals.BUILDING_DATA;
        _buildingManager = BuildingManager.Instance;
        _resources = ResourceManager.Instance;

        // Buildings
        foreach (BuildingData entry in _buildingData.Values)
        {
            GameObject buildingButton = Instantiate(buildingButtonPrefab, buildingMenuPanel, false);
            buildingButton.GetComponentInChildren<Text>().text = entry.Code;
            AddBuildingButtonListener(buildingButton.GetComponent<Button>(), entry);

        }

        // Resources
        GameObject foodLabel = Instantiate(resourceLabelPrefab, resourcePanel, false);
        foodLabel.GetComponentInChildren<Text>().text = "Food: " + _resources.Food + $"/{_resources.ResourceCap}";
    }

    // Update is called once per frame
    void Update()
    {

    }

    // When a button is clicked
    void AddBuildingButtonListener(Button b, BuildingData buildingData)
    {
        b.onClick.AddListener( () => _buildingManager.StartPreviewBuilding(buildingData));
    }



    // Managers and Data
    private IDictionary<string, BuildingData> _buildingData;
    private BuildingManager _buildingManager;
    private ResourceManager _resources;
}
