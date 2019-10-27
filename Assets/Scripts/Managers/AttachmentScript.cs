using System.Collections.Generic;
using Containers;
using Containers.RocketParts;
using Entities;
using Enums;
using UnityEngine;

public class AttachmentScript : MonoBehaviour
{
    public RocketController rocketController;
    private RocketPartsInventory rocketPartInventory;
    private List<RocketPartController> attachedRocketParts = new List<RocketPartController>();
    
    void Start()
    {
        rocketPartInventory = FindObjectOfType<RocketPartsInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        OnInventorySelect();
    }

    private void HandleAttachment(RocketPartController rocketPart)
    {
        var type = rocketPart.part.type;

        if (type == PartType.THRUSTER)
        {
            toggleDetach(attachedRocketParts.Find((p) => rocketPart.part.type.Equals(p.part.type)));
            attachMainThruster(rocketPart);
        }
        else if (type == PartType.RCS)
        {
            attachRCSThruster(rocketPart);
        } else if (type == PartType.FUEL)
        {
            attachFuelTank(rocketPart);
        }else if (type == PartType.FINS)
        {
            attachFins(rocketPart);
        }
        RocketPartsDatabase.Instance.inventory.Remove(rocketPart);
    }



    private void attachMainThruster(RocketPartController rocketPart)
    {
        rocketController.thrust = rocketPart.part.thrust;
        applyAttachment(rocketPart, rocketPart.transform, rocketController.mainThrusterAttachment);
    }

    private void attachRCSThruster(RocketPartController rocketPart)
    {
        rocketController.rcsThrust = rocketPart.part.thrust;
        applyAttachment(rocketPart, rocketPart.transform, rocketController.rcsThrusterAttachment);
    }
    
    private void attachFuelTank(RocketPartController rocketPart)
    {
        rocketController.fuel = rocketPart.part.fuel;
        applyAttachment(rocketPart, rocketPart.transform, rocketController.fuelTankAttachment);
    }
    
    private void attachFins(RocketPartController rocketPart)
    {
        rocketController.rigidBody.isKinematic = false;
        applyAttachment(rocketPart, rocketPart.transform, rocketController.finsAttachment);
    }

    private void applyAttachment(RocketPartController rocketPart, Transform rocketPartTransform, Transform attachmentPoint)
    {
        rocketPartTransform.position = attachmentPoint.position;
        rocketPartTransform.parent = attachmentPoint.transform;
        rocketController.GetComponent<Rigidbody>().mass += rocketPart.part.weight;
        attachedRocketParts.Add(rocketPart);
    }
    
    private void toggleDetach(RocketPartController attachedPart)
    {
        if (attachedPart != null)
        {
            detachPart(attachedPart);
        }
    }
    
    /**
    * TODO: performance might drop 
    */
    private void detachPart(RocketPartController attachedPart)
    {
        var inventory= RocketPartsDatabase.Instance.inventory;
        var lastInventoryModel = inventory?[inventory.Count - 1].gameObject;
        var attachedModel = attachedPart.gameObject;
        var size = attachedModel.GetComponent<Collider>().bounds.size;
        var lastInventorySize = lastInventoryModel.GetComponent<Collider>().bounds.size;
        attachedModel.transform.parent = rocketPartInventory.transform;
        
        var position = attachedModel.transform.position;
        position = rocketPartInventory.transform.position;
        position += new Vector3(size.x + lastInventorySize.x, 0, 0);
        attachedModel.transform.position = position;

        inventory.Add(attachedPart);
        attachedRocketParts.Remove(attachedPart);
    }
    
    private void OnInventorySelect()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var rocketPart = hit.transform.gameObject.GetComponent<RocketPartController>();
                if (rocketPart != null)
                {
                    HandleAttachment(rocketPart);
                }
            }
        }
    }
}