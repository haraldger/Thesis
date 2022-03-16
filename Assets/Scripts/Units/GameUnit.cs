using System;
using UnityEngine;

public class GameUnit
{

	// ================================= DATA FIELDS
	public GameUnitData Data { get; private set; }

	private int _currentHP;
	public int CurrentHP
	{
		get => _currentHP;
		private set
        {
			if(value < 0)
            {
				_currentHP = 0;
            }
			else if(value > Data.hp)
            {
				_currentHP = Data.hp;
            }
        }
	}

    // ================================= UNITY FIELDS
    private GameObject _instance;
	public GameObject Instance
	{
		get => _instance;
		private set
		{
			GameObject.Destroy(_instance);
			_instance = value;
		}
	}


	// ================================= METHODS
	public GameUnit(GameUnitData data)
    {
		Data = data;
		CurrentHP = Data.hp;
    }

	public void InstantiatePrefab(Vector3 worldPosition)
    {
        if (Instance != null)
        {
			Destroy();
        }

		Instance = GameObject.Instantiate(Resources.Load($"Prefabs/Buildings/{Data.code}"), worldPosition, Quaternion.identity) as GameObject;
	}

    public void Destroy()
    {
		GameObject.Destroy(Instance);
    }

    public void SetWorldPosition(Vector3 worldPosition)
    {
		Instance.GetComponent<Transform>().position = worldPosition;
	}

	public Vector3 GetWorldPosition()
    {
		return Instance.GetComponent<Transform>().position;
    }

	public void SetMaterial(Material material)
	{
		MeshRenderer[] childrenMeshRenderers = Instance.GetComponentsInChildren<MeshRenderer>();
		for (int i = 0; i < childrenMeshRenderers.Length; i++)
		{
			childrenMeshRenderers[i].material = material;

		}
	}

	public void ResetMeshRenderer()
    {
		InstantiatePrefab(GetWorldPosition());
    }

	public void Damage(int hitpoints)
    {
		CurrentHP -= hitpoints;
    }

	public void Heal(int hitpoints)
    {
		CurrentHP += hitpoints;
    }
}

