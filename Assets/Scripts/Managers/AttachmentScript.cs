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
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var rocketPart = hit.transform.gameObject.GetComponent<RocketPartController>();
                if (rocketPart != null)
                {
                    ApplyAttachment(rocketPart);
                }
            }
        }
    }

    private void ApplyAttachment(RocketPartController rocketPart)
    {
        var thrust = rocketPart.part.thrust;
        var type = rocketPart.part.type;

        if (type == PartType.THRUSTER)
        {
            var attachedPart = 
                attachedRocketParts.Find((p) => rocketPart.part.type.Equals(p.part.type));
            if (attachedPart != null)
            {
                detachPart(attachedPart);
            }
            attachMainThruster(rocketPart, thrust);
        }
        else if (type == PartType.RCS)
        {
            print("rcs");
            attachRCSThruster(rocketPart, thrust);
        }
        RocketPartsDatabase.Instance.inventory.Remove(rocketPart);
        print(rocketPart.part.thrust);
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

    private void attachMainThruster(RocketPartController rocketPart, float thrust)
    {
        var attachmentPoint = rocketController.mainThrusterAttachment;
        var rocketPartTransform = rocketPart.transform;

        rocketController.thrust = thrust;
        rocketPartTransform.position = attachmentPoint.position;
        rocketPartTransform.parent = attachmentPoint.transform;
        attachedRocketParts.Add(rocketPart);
    }

    private void attachRCSThruster(RocketPartController rocketPart, float thrust)
    {
        var attachmentPoint = rocketController.rcsThrusterAttachment;
        var rocketPartTransform = rocketPart.transform;

        rocketController.rcsThrust = thrust;
        rocketPartTransform.position = attachmentPoint.position;
        rocketPartTransform.parent = attachmentPoint.transform;
        attachedRocketParts.Add(rocketPart);
        print(attachedRocketParts.Count);
    }
}