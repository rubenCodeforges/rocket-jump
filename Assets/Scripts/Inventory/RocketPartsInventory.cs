using System.Collections.Generic;
using Containers.RocketParts;
using Entities;
using UnityEngine;

public class RocketPartsInventory : MonoBehaviour
{
    private List<RocketPart> availableParts = new List<RocketPart>();
    
    void Start()
    {
        spawnAllParts();
    }

    public List<RocketPart> getAvailableObject()
    {
        return availableParts;
    }
    
    private void spawnAllParts()
    {
        var items = RocketPartsDatabase.Instance.rocketParts;
        var lastPrefabSize = Vector3.zero;
        availableParts = items;
        
        for (int i = 0; i < items.Count; i++)
        {
            var partPrefab = items[i].model;
            var instance = Instantiate(partPrefab, transform);
            var size = instance.GetComponent<Collider>().bounds.size;

            if (i > 0)
            {
                instance.transform.position += new Vector3(size.x + lastPrefabSize.x, 0, 0);
            }

            instance.AddComponent<RocketPartController>();
            instance.GetComponent<RocketPartController>().part = items[i];
            
            lastPrefabSize = size;
        }
    }
}
