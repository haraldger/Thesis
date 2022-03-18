using System;
using System.IO;
using UnityEngine;

public class GameUnit
{

	// ================================= DATA FIELDS
	public GameUnitData Data { get; private set; }


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
    }

	public void InstantiatePrefab(Vector3 worldPosition)
    {
        if (Instance != null)
        {
			Destroy();
        }

		Instance = GameObject.Instantiate(LoadPrefab($"{Data.code}"), worldPosition, Quaternion.identity) as GameObject;
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

	public UnitController GetUnitController()
    {
		if (Instance == null) return null;

		return Instance.GetComponentInChildren<UnitController>();
    }

	/// <summary>
	/// Utility method to dynamically load resource by searching all subfolders.
    /// 
	/// Credit: 'Emad'
	/// Source: https://stackoverflow.com/a/57094972/7302006
	/// License: https://creativecommons.org/licenses/by-sa/4.0/
    ///
    /// Method has been modified.
	/// </summary>
	public  static UnityEngine.Object LoadPrefab(string resourceName)
	{
		string prefabsPath = Application.dataPath + "/Resources/Prefabs";
		string[] directories = Directory.GetDirectories(prefabsPath, "*", SearchOption.AllDirectories);
		foreach (var item in directories)
		{
			string itemPath = item.Substring(prefabsPath.Length + 1);
			string resourcePath = itemPath + "/" + resourceName;
			UnityEngine.Object result = Resources.Load($"Prefabs/{resourcePath}");
			if (result != null)
            {
				return result;
			}
		}
		return null;
	}

}

