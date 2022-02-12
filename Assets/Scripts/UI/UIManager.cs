using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public Transform buildingMenuPanel;
    public GameObject buildingButtonPrefab;

    private BuildingData[] _buildingData = Globals.BUILDING_DATA;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        for (int i = 0; i < _buildingData.Length; i++)
        {
            GameObject buildingButton = Instantiate(buildingButtonPrefab, buildingMenuPanel, false);
            buildingButton.GetComponentInChildren<Text>().text = _buildingData[i].Code;
            AddBuildingButtonListener(buildingButton.GetComponent<Button>(), _buildingData[i]);

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
