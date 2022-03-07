using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building
{
    public BuildingData Data { get; private set; }

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

    public Building(BuildingData data)
	{
		Data = data;
	}

	public void InstantiatePrefab(Vector3 worldPosition)
    {
        if (Instance != null)
        {
			Destroy();
        }
		Instance = GameObject.Instantiate(Resources.Load($"Prefabs/Buildings/{Data.Code}"), worldPosition, Quaternion.identity) as GameObject;
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

}

