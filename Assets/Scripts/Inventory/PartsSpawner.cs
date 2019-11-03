using System.Collections.Generic;
using Containers.RocketParts;
using Entities;
using UnityEngine;

public class PartsSpawner : MonoBehaviour
{
    void Start()
    {
        spawnAllParts();
    }

    private void spawnAllParts()
    {
        var items = RocketPartsDatabase.Instance.rocketParts;

        for (int i = 0; i < items.Count; i++)
        {
            var partPrefab = items[i].model;
            var instance = Instantiate(partPrefab, transform);
            var size = instance.GetComponent<Collider>().bounds.size;

            if (i > 0)
            {
                var lastPrefab =
                    RocketPartsDatabase.Instance.rocketPartInventory[RocketPartsDatabase.Instance.rocketPartInventory.Count - 1];
                var lastSize = lastPrefab.GetComponent<Collider>().bounds.size;
                var position = instance.transform.position;

                position = lastPrefab.transform.position;
                position += new Vector3(size.x + lastSize.x  /2, 0, 0);
                instance.transform.position = position;
            }

            instance.AddComponent<RocketPartController>();
            var rocketPart = instance.GetComponent<RocketPartController>();
            rocketPart.part = items[i];
            RocketPartsDatabase.Instance.rocketPartInventory.Add(rocketPart);
        }
    }
}