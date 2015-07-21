using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour 
{
	// Used to store the object pool by comparing prefabs
	Dictionary<GameObject, List<GameObject>> m_ObjectPool;

	// Use this for initialization
	void Start () 
	{
		m_ObjectPool = new Dictionary<GameObject, List<GameObject>>();
	}

	public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
	{
		GameObject result = null;

		if(!m_ObjectPool.ContainsKey(prefab))
		{
			m_ObjectPool.Add (prefab, new List<GameObject>());
		}

		foreach(GameObject pooledObject in m_ObjectPool[prefab])
		{
			if(result == null && !pooledObject.activeSelf)
			{
				pooledObject.SetActive(true);

				result = pooledObject;
			}
		}

		if(result == null)
		{
			result = (GameObject) Instantiate(prefab);

			result.transform.parent = transform;

			m_ObjectPool[prefab].Add (result);
		}

		result.transform.position = position;
		result.transform.rotation = rotation;

		return result;
	}
}
