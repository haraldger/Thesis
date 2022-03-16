using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    private IList<GameObject> _selectedUnits;
 
    void Awake()
    {
        Instance = this;
        _selectedUnits = new List<GameObject>();
        DataHandler.LoadGameData();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) DeselectAll();
    }

    public void Select(GameObject unit)
    {
        if (_selectedUnits.Contains(unit)) return;
        _selectedUnits.Add(unit);
        unit.GetComponentInChildren<UnitController>().Select();
    }

    public void Deselect(GameObject unit)
    {
        if (!_selectedUnits.Contains(unit)) return;
        _selectedUnits.Remove(unit);
        unit.GetComponentInChildren<UnitController>().Deselect();
    }

    public void DeselectAll()
    {
        for (int i = _selectedUnits.Count - 1; i >= 0; i--)
        {
            var unit = _selectedUnits[i];
            if (unit != null) Deselect(unit);
            else _selectedUnits.RemoveAt(i);
        }
    }
}
