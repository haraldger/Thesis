using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    private UnitController _selectedUnit;
 
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
        UnitController controller = unit.GetComponentInChildren<UnitController>();
        if (controller == null) return;
        if (_selectedUnit == controller) return;
        

        _selectedUnit = controller;
        controller.Select();
    }

    public void Deselect()
    {
        if (_selectedUnit == null) return;
        _selectedUnit.Deselect();
        _selectedUnit = null;
    }
}
