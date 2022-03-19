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

    // Internal variables for logic and performance
    private UnitController _previouslySelected;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Data
        _buildingData = Globals.BUILDING_DATA;
        _resourceData = Globals.RESOURCE_DATA;
        _troopData = Globals.TROOP_DATA;

        // Buildings
        _buildingButtons = new Dictionary<Button, BuildingData>();
        foreach (BuildingData entry in _buildingData)
        {
            GameObject buildingButton = Instantiate(buildingButtonPrefab, buildingMenuPanel, false);
            buildingButton.GetComponentInChildren<Text>().text = entry.code;
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
        _recruitingButtons = new Dictionary<Button, TroopData>();
        foreach (TroopData entry in _troopData)
        {
            GameObject recruitingButton = Instantiate(recruitingButtonPrefab, unitPanel, false);
            recruitingButton.GetComponentInChildren<Text>().text = entry.code;
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
        foreach (KeyValuePair<Button, BuildingData> button in _buildingButtons)
        {
            IList<CostValue> buildingCosts = button.Value.costs;
            foreach (CostValue cost in buildingCosts)
            {
                if (Globals.RESOURCE_DATA[cost.code].CanConsumeResource(cost.value))
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



        // Update unit panel
        UnitController selectedUnit = GameManager.Instance.SelectedUnit;
        if (_previouslySelected != selectedUnit)
        {
            DeactivateUnitButtons();
            _previouslySelected = selectedUnit;
        }

        if (selectedUnit is BuildingController)
        {
            List<TroopData> recruitableTroops = ((BuildingController)selectedUnit).data.recruitingOptions;
            foreach (TroopData troop in recruitableTroops)
            {
                ActivateRecruitingButton(troop.code);
                SetRecruitingButtonInteractable(troop.code, true);

                foreach (CostValue cost in troop.costs)
                {
                    if (!Globals.RESOURCE_DATA[cost.code].CanConsumeResource(cost.value))
                    {
                        SetRecruitingButtonInteractable(troop.code, false);
                    }
                }
            }
        }



        // Input

        // Left mouse selects units
        if (Input.GetMouseButtonDown(0))
        {
            GameObject clickedObject = GetRaycastObject();

            if (clickedObject != null)
            {
                IssueSelection(clickedObject);    
            }
        }

        // Right mouse issues commands (move, attack, etc.)
        if (Input.GetMouseButtonDown(1))
        {
            GameObject targetObject = GetRaycastObject();

            if (targetObject != null)
            {
                switch (targetObject.tag)
                {
                    // Issue command on unit targets
                    case "Unit":
                    case "Building":
                    case "Troop":
                        IssueCommand(GameManager.Instance.SelectedUnit, targetObject);
                        break;

                    // Issue command on world position
                    case "Environment":
                        Vector3 targetPosition = GetRaycastPosition();
                        IssueCommand(GameManager.Instance.SelectedUnit, targetPosition);
                        break;


                    default:
                        break;
                    

                }
            }
        }


        // Escape deselects units
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.Deselect();
        }

    }

    // ------------------------------------------------- Utility methods

    // When a building button is clicked
    void AddBuildingButtonListener(Button b, BuildingData buildingData)
    {
        b.onClick.AddListener( () => BuildingManager.Instance.StartPreviewBuilding(buildingData));
    }

    // When a recruiting button is clicked
    void AddRecruitingButtonListener(Button b, TroopData troopData)
    {
        b.onClick.AddListener( () =>
        {
            BuildingController selectedUnit = (BuildingController)GameManager.Instance.SelectedUnit;
            selectedUnit.RecruitTroop(troopData);
        });
    }

    // Utility method to deactivate all buttons in the unit panel
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
        _recruitingButtons.DefaultIfEmpty(new KeyValuePair<Button, TroopData>(null, null)).FirstOrDefault(x => x.Value.code == unitCode).Key?.gameObject.SetActive(true);
    }

    // Sets interactable on recruiting buttons whose preconditions are met
    private void SetRecruitingButtonInteractable(string unitCode, bool value)
    {
        var button = _recruitingButtons.DefaultIfEmpty(new KeyValuePair<Button, TroopData>(null, null)).FirstOrDefault(x => x.Value.code == unitCode).Key;
        if(button != null)
        {
            button.interactable = value;
        }
    }

    // Raycasts mouse position to find a game object
    private GameObject GetRaycastObject()
    {
        RaycastHit raycastHit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit, 100f))
        {
            if (raycastHit.transform != null)
            {
                GameObject clickedObject = raycastHit.transform.gameObject;
                return clickedObject;
            }
        }
        return null;
    }

    // Raycast mouse position to get a Vector3 position
    // !Returns maxint if raycast is null!
    private Vector3 GetRaycastPosition()
    {
        RaycastHit raycastHit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit, 100f))
        {
            if (raycastHit.point != null)
            {
                return raycastHit.point;
            }
        }
        return new Vector3(int.MaxValue, int.MaxValue);
    }

    // Action logic for left click
    private void IssueSelection(GameObject clickedObject)
    {
        if (clickedObject.tag == "Environment")
        {
            GameManager.Instance.Deselect();
        }
        else if (clickedObject.tag == "Unit" || clickedObject.tag == "Building" || clickedObject.tag == "Troop")
        {
            GameManager.Instance.Select(clickedObject);
        }
    }

    // Action logic for right click on unit
    private void IssueCommand(UnitController unit, GameObject target)
    {
        if (target == null)
        {
            return;
        }

        if (unit is BuildingController building)
        {
            building.RallyPoint = target.transform.position;
        }
        else if (unit is TroopController troop)
        {
            troop.Attack(target.transform);
        }
    }

    // Action logic for right click in world
    private void IssueCommand(UnitController unit, Vector3 position)
    {
        if (position == null)
        {
            return;
        }

        if (unit is BuildingController building)
        {
            building.RallyPoint = position;
        }
        else if (unit is TroopController troop)
        {
            troop.MoveTo(position);
        }
    }


    // Data & Fields
    private BuildingData[] _buildingData;
    private IDictionary<string, GameResourceData> _resourceData;
    private TroopData[] _troopData;
    private IDictionary<Text, GameResourceData> _resourceLabels;
    private IDictionary<Button, BuildingData> _buildingButtons;
    private IDictionary<Button, TroopData> _recruitingButtons;







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
