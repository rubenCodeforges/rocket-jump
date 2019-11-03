using System.Collections.Generic;
using Containers;
using Containers.RocketParts;
using Entities;
using UnityEngine;

public class AttachmentScript : MonoBehaviour
{
    public RocketController rocketController;
    private List<RocketPartController> attachedRocketParts = new List<RocketPartController>();

    public void HandleAttachment(RocketPart rocketPart)
    {
        var type = InstantiatePart(rocketPart, out var rocketPartInstance);

        if (type == PartType.THRUSTER)
        {
            toggleDetach(attachedRocketParts.Find((p) => rocketPart.type.Equals(p.part.type)));
            attachMainThruster(rocketPartInstance);
        }
        else if (type == PartType.RCS)
        {
            attachRCSThruster(rocketPartInstance);
        }
        else if (type == PartType.FUEL)
        {
            attachFuelTank(rocketPartInstance);
        }
        else if (type == PartType.FINS)
        {
            attachFins(rocketPartInstance);
        }

        // RocketPartsDatabase.Instance.rocketPartInventory.Remove(rocketPart);
    }

    private static PartType InstantiatePart(RocketPart rocketPart, out RocketPartController rocketPartInstance)
    {
        var type = rocketPart.type;
        var instance = Instantiate(rocketPart.model);
        instance.AddComponent<RocketPartController>();
        rocketPartInstance = instance.GetComponent<RocketPartController>();
        rocketPartInstance.part = rocketPart;
        return type;
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

    private void applyAttachment(RocketPartController rocketPart, Transform rocketPartTransform,
        Transform attachmentPoint)
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
//        var inventory = RocketPartsDatabase.Instance.rocketPartInventory;
//        var lastInventoryModel = inventory?[inventory.Count - 1].gameObject;
//        var attachedModel = attachedPart.gameObject;
//        var size = attachedModel.GetComponent<Collider>().bounds.size;
//        var lastInventorySize = lastInventoryModel.GetComponent<Collider>().bounds.size;
//
//
//        var position = attachedModel.transform.position;
//        position += new Vector3(size.x + lastInventorySize.x, 0, 0);
//        attachedModel.transform.position = position;
//
//        inventory.Add(attachedPart);
//        attachedRocketParts.Remove(attachedPart);
    }
}