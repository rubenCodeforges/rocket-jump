using Containers;
using Entities;
using Enums;
using UnityEngine;

public class AttachmentScript : MonoBehaviour
{
    public RocketController rocketController;

    // Start is called before the first frame update
    void Start()
    {
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
            attachMainThruster(rocketPart, thrust);
        }
        else if (type == PartType.RCS)
        {
            attachRCSThruster(rocketPart, thrust);
        }
        print(rocketPart.part.thrust);
    }

    private void attachMainThruster(RocketPartController rocketPart, float thrust)
    {
        var attachmentPoint = rocketController.mainThrusterAttachment;
        var rocketPartTransform = rocketPart.transform;

        rocketController.thrust = thrust;
        rocketPartTransform.position = attachmentPoint.position;
        rocketPartTransform.parent = attachmentPoint.transform;
    }

    private void attachRCSThruster(RocketPartController rocketPart, float thrust)
    {
        var attachmentPoint = rocketController.rcsThrusterAttachment;
        var rocketPartTransform = rocketPart.transform;

        rocketController.rcsThrust = thrust;
        rocketPartTransform.position = attachmentPoint.position;
        rocketPartTransform.parent = attachmentPoint.transform;
    }
}