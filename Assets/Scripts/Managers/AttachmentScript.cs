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
            rocketController.thrust = thrust;
        }
        else if (type == PartType.RCS)
        {
            rocketController.rcsThrust = thrust;
        }

        print(rocketPart.part.thrust);
    }
}