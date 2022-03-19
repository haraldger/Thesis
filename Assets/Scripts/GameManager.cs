using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public UnitController SelectedUnit { get; private set; }
 
    void Awake()
    {
        Instance = this;
        DataHandler.LoadGameData();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Select(GameObject unit)
    {
        Deselect();

        UnitController controller = unit.GetComponentInChildren<UnitController>();
        if (controller == null) return;
        if (SelectedUnit == controller) return;

        SelectedUnit = controller;
        controller.Select();
    }

    public void Deselect()
    {
        if (SelectedUnit == null) return;
        SelectedUnit.Deselect();
        SelectedUnit = null;
    }

    public void DestroyUnit(UnitController unit)
    {
        GameUnit unitEntity = Globals.EXISTING_UNITS[unit];
        Globals.EXISTING_UNITS.Remove(unit);
        unitEntity.Destroy();
    }
}
