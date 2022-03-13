using System.Collections.Generic;
using System.Linq;
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
    public Transform unitPanel;
    public GameObject recruitingButtonPrefab;

    // For development and test purposes
    public bool testMode;


    void Awake()
    {
        Instance = this;

        // Buildings
        _buildingButtons = new Dictionary<Button, GameUnitData>();
        foreach (GameUnitData entry in _buildingData.Values)
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
        }

        // Troops
        _recruitingButtons = new Dictionary<Button, GameUnitData>();
        foreach (GameUnitData entry in _troopData.Values)
        {
            GameObject recruitingButton = Instantiate(recruitingButtonPrefab, unitPanel, false);
            recruitingButton.GetComponentInChildren<Text>().text = entry.Code;
            AddRecruitingButtonListener(recruitingButton.GetComponent<Button>(), entry);
            _recruitingButtons.Add(recruitingButton.GetComponent<Button>(), entry);
            recruitingButton.SetActive(false);
        }


        // DEVELOPER TOOLS
        if (testMode) UITestTools();
    }

    

    // Update is called once per frame
    void Update()
    {
        // Update building buttons
        foreach (KeyValuePair<Button, GameUnitData> button in _buildingButtons)
        {
            IList<CostValue> buildingCosts = button.Value.Costs;
            foreach (CostValue cost in buildingCosts)
            {
                if (Globals.RESOURCE_DATA[cost.Code].CanConsumeResource(cost.Value))
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

    // When a building button is clicked
    void AddBuildingButtonListener(Button b, GameUnitData buildingData)
    {
        b.onClick.AddListener( () => BuildingManager.Instance.StartPreviewBuilding(buildingData));
    }

    void AddRecruitingButtonListener(Button b, GameUnitData troopData)
    {
        return;
    }

    // Unitility method to deactivate all buttons in the unit panel
    private void DeactivateUnitButtons()
    {
        foreach (var entry in _recruitingButtons)
        {
            entry.Key.gameObject.SetActive(false);
        }
    }

    // Utility method to activate a recruiting button
    private void ActivateRecruitingButton(string unitCode)
    {
        _recruitingButtons.DefaultIfEmpty(new KeyValuePair<Button, GameUnitData>(null, null)).FirstOrDefault(x => x.Value.Code == unitCode).Key?.gameObject.SetActive(true);
    }


    // Data & Fields
    private IDictionary<string, GameUnitData> _buildingData = Globals.BUILDING_DATA;
    private IDictionary<string, GameResourceData> _resourceData = Globals.RESOURCE_DATA;
    private IDictionary<string, GameUnitData> _troopData = Globals.TROOP_DATA;
    private IDictionary<Text, GameResourceData> _resourceLabels;
    private IDictionary<Button, GameUnitData> _buildingButtons;
    private IDictionary<Button, GameUnitData> _recruitingButtons;







    // ============================= DEVELOPER TOOLS
    // ============================= FOR TESTING PURPOSES ONLY
    private void UITestTools()
    {
        foreach (var label in _resourceLabels)
        {
            // Custom buttons for adding resources
            GameObject buildingButton = Instantiate(buildingButtonPrefab, resourcePanel, false);
            buildingButton.GetComponentInChildren<Text>().text = "+";
            buildingButton.GetComponent<Button>().onClick.AddListener(() => label.Value.ConsumeResource(-300));
            buildingButton.transform.SetSiblingIndex(label.Key.transform.GetSiblingIndex() + 1);
        }
    }
}
