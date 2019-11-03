using System;
using System.Collections.Generic;
using Containers.RocketParts;
using Entities;
using Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySpawner : MonoBehaviour
{
    public float massFactor = 10f;
    public GameObject inventoryItem;
    public GameObject inventoryContainer;
    public GameObject player;
    public AttachmentScript attachmentScript;
    
    void Start()
    {
        spawnAllParts();
    }

    public void OnItemSelect(GameObject reference)
    {
        print(reference.GetComponent<RocketPartController>().part.name);
    }

    private void spawnAllParts()
    {
        var items = RocketPartsDatabase.Instance.rocketParts;

        for (int i = 0; i < items.Count; i++)
        {
            var newUIElement = CreateInventoryItem(items, i);
            InitInventoryItem(newUIElement, items, i);
            RocketPartsDatabase.Instance.uiInventory.Add(items[i]);
        }
    }


    private GameObject CreateInventoryItem(List<RocketPart> items, int i)
    {
        var newUIElement = Instantiate(inventoryItem, Vector3.zero, Quaternion.identity, inventoryContainer.transform);
        newUIElement.GetComponent<RectTransform>().localPosition = Vector3.zero;
        newUIElement.GetComponent<Image>().sprite = items[i].icon;
        newUIElement.GetComponentInChildren<TextMeshProUGUI>()
            .SetText(Math.Round(items[i].weight * massFactor) + " kg");
        return newUIElement;
    }

    private void InitInventoryItem(GameObject newUIElement, List<RocketPart> items, int i)
    {
        var itemInstance = newUIElement.GetComponent<InventoryItem>();
        itemInstance.rocketPart = items[i];
        itemInstance.AddEventListener(OnItemClick);
    }

    private void OnItemClick(RocketPart part)
    {
        attachmentScript.HandleAttachment(part);
    }
}