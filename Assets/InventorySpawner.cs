using System;
using System.Collections;
using System.Collections.Generic;
using Containers.RocketParts;
using Entities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySpawner : MonoBehaviour
{
    public float massFactor = 10f;
    public GameObject inventoryItem;
    public GameObject inventoryContainer;
    // Start is called before the first frame update
    void Start()
    {
        spawnAllParts();
    }

    private void spawnAllParts()
    {
        var items = RocketPartsDatabase.Instance.rocketParts;

        for (int i = 0; i < items.Count; i++)
        {
            var newUIElement = Instantiate(inventoryItem, Vector3.zero, Quaternion.identity, inventoryContainer.transform);
            newUIElement.GetComponent<RectTransform>().localPosition = Vector3.zero;
            newUIElement.GetComponent<Image>().sprite = items[i].icon;
            newUIElement.GetComponentInChildren<TextMeshProUGUI>().SetText(Math.Round(items[i].weight * massFactor) + " kg");
        }
    }
}
