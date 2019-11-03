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
    public GameObject inventoryItemPrefab;
    public GameObject inventoryUIContainer;
    public AttachmentScript attachmentScript;
    
    public List<GameObject> uiInventoryItems = new List<GameObject>();
    
    void Start()
    {
        spawnAllParts();
    }

    private void spawnAllParts()
    {
        var items = RocketPartsDatabase.Instance.rocketParts;

        for (int i = 0; i < items.Count; i++)
        {
            AddToInventory(items[i], CreateInventoryItem(items[i]));
        }
    }

    private void AddToInventory(RocketPart part, GameObject newUIElement)
    {
        RocketPartsDatabase.Instance.uiInventory.Add(part);
        uiInventoryItems.Add(newUIElement);
    }
    
    private void RemoveFromInventory(RocketPart rocketPart)
    {
        var foundIndex = uiInventoryItems
            .FindIndex((uiElement) => uiElement.GetComponent<InventoryItem>().rocketPart == rocketPart);
        if (foundIndex != -1)
        {
            Destroy(uiInventoryItems[foundIndex]);
            uiInventoryItems.RemoveAt(foundIndex);            
        }
    }
    
    private GameObject CreateInventoryItem(RocketPart part)
    {
        var newUIElement = Instantiate(inventoryItemPrefab, Vector3.zero, Quaternion.identity, inventoryUIContainer.transform);
        newUIElement.GetComponent<RectTransform>().localPosition = Vector3.zero;
        newUIElement.GetComponent<Image>().sprite = part.icon;
        newUIElement.GetComponentInChildren<TextMeshProUGUI>()
            .SetText(Math.Round(part.weight * massFactor) + " kg");
        SetupInventoryItem(newUIElement, part);
        return newUIElement;
    }

    private void SetupInventoryItem(GameObject newUIElement, RocketPart part)
    {
        var itemInstance = newUIElement.GetComponent<InventoryItem>();
        itemInstance.rocketPart = part;
        itemInstance.AddEventListener(OnItemClick);
    }
    

    private void OnItemClick(RocketPart part)
    {
        var hasDetached = attachmentScript.HandleAttachment(part);
        if (hasDetached)
        {
            AddToInventory(part, CreateInventoryItem(part));
        }
        else
        {
            RemoveFromInventory(part);
        }
    }
}