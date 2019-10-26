using System.Collections;
using System.Collections.Generic;
using Containers.RocketParts;
using UnityEngine;

public class ItemsSpawner : MonoBehaviour
{
    public List<GameObject> prefabs;
    
    void Start()
    {
        var items = RocketPartsDatabase.Instance.rocketParts;
        var lastPrefabSize = Vector3.zero;
        
        for (int i = 0; i < items.Count; i++)
        {
            var partPrefab = prefabs.Find((prefab) => prefab.name == items[i].name);
            var instance = Instantiate(partPrefab, transform);
            var size = instance.GetComponent<Collider>().bounds.size;
            
            if (i > 0)
            {
                instance.transform.position += new Vector3(size.x + lastPrefabSize.x,0,0);    
            }

            lastPrefabSize = size;
        }
    }
}
