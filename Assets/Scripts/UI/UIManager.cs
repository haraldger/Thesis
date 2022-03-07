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


    void Awake()
    {
        Instance = this;

        // Buildings
        _buildingButtons = new Dictionary<Button, BuildingData>();
        foreach (BuildingData entry in _buildingData.Values)
        {
            GameObject buildingButton = Instantiate(buildingButtonPrefab, buildingMenuPanel, false);
            buildingButton.GetComponentInChildren<Text>().text = entry.Code;
            AddBuildingButtonListener(buildingButton.GetComponent<Button>(), entry);
            _buildingButtons.Add(buildingButton.GetComponent<Button>(), entry);
        }

        // Resources
        _resourceLabels = new Dictionary<Text, GameResourceData>();
        foreach (GameResourceData entry in _resourceData.Values)
        {
            GameObject resourceLabel = Instantiate(resourceLabelPrefab, resourcePanel, false);
            resourceLabel.GetComponent<Text>().text = $"{entry.Code}: {entry.CurrentAmount}/{entry.Cap}";
            _resourceLabels.Add(resourceLabel.GetComponent<Text>(), entry);

            /////////////TEST///////////
            GameObject buildingButton = Instantiate(buildingButtonPrefab, resourcePanel, false);
            buildingButton.GetComponentInChildren<Text>().text = "+";
            buildingButton.GetComponent<Button>().onClick.AddListener(() => entry.ConsumeResource(-300));
            /////////////DELETE/////////
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        // Update building buttons
        foreach (KeyValuePair<Button, BuildingData> button in _buildingButtons)
        {
            IDictionary<GameResourceData, int> buildingCost = button.Value.Cost;
            foreach (KeyValuePair<GameResourceData, int> resourceCost in buildingCost)
            {
                if (resourceCost.Key.CanConsumeResource(resourceCost.Value))
                {
                    button.Key.interactable = true;
                }
                else
                {
                    button.Key.interactable = false;
                    break;
                }
            }
        }

        // Update resource labels
        foreach (KeyValuePair<Text, GameResourceData> entry in _resourceLabels)
        {
            entry.Key.text = $"{entry.Value.Code}: {entry.Value.CurrentAmount}/{entry.Value.Cap}";
        }
    }

    // When a button is clicked
    void AddBuildingButtonListener(Button b, BuildingData buildingData)
    {
        b.onClick.AddListener( () => BuildingManager.Instance.StartPreviewBuilding(buildingData));
    }


    // Data & Fields
    private IDictionary<string, BuildingData> _buildingData = Globals.BUILDING_DATA;
    private IDictionary<string, GameResourceData> _resourceData = Globals.RESOURCE_DATA;
    private IDictionary<Text, GameResourceData> _resourceLabels;
    private IDictionary<Button, BuildingData> _buildingButtons;
}
