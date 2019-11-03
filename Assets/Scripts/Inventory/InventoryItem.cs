using System;
using Entities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Inventory
{
    public class InventoryItem: MonoBehaviour
    {
        public EventTrigger eventTrigger;
        public RocketPart rocketPart;

        public void AddEventListener(Action<RocketPart> callback)
        {
            var entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener(arg0 => callback(rocketPart));
            eventTrigger.triggers.Add(entry);
        }
    }
}