using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public Transform buildingMenuPanel;
    public GameObject buildingButtonPrefab;

    private IDictionary<string, BuildingData> _buildingData = Globals.BUILDING_DATA;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        foreach (BuildingData entry in _buildingData.Values)
        {
            GameObject buildingButton = Instantiate(buildingButtonPrefab, buildingMenuPanel, false);
            buildingButton.GetComponentInChildren<Text>().text = entry.Code;
            AddBuildingButtonListener(buildingButton.GetComponent<Button>(), entry);

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // When a button is clicked
    void AddBuildingButtonListener(Button b, BuildingData buildingData)
    {
        b.onClick.AddListener( () => BuildingManager.Instance.StartPreviewBuilding(buildingData));
    }
}
