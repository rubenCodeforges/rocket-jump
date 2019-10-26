using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MovementController : MonoBehaviour
{
    public float thrust = 100f;
    public float rcsThrust = 100f;
    public Transform currentThruster;
    public Transform currentRcs;
    public Transform mainThrusterAttachment;
    public Transform rcsThrusterAttachment;

    private Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
//        mainThruster.parent = rigidBody.transform;
//        rcsThruster.parent = rigidBody.transform;
    }

    // Update is called once per frame
    void Update()
    {
        OnInput();
    }

    void OnInput()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetButton("Jump"))
        {
            EmitInputEvent(InputDirection.UP);
            rigidBody.AddForceAtPosition(mainThrusterAttachment.up * (thrust * Time.deltaTime),
                mainThrusterAttachment.position);
        }

        if (horizontalInput != 0f)
        {
            EmitInputEvent(horizontalInput > 0f ? InputDirection.RIGHT : InputDirection.LEFT);
            rigidBody.AddForceAtPosition(
                rcsThrusterAttachment.right * (horizontalInput * rcsThrust * Time.deltaTime),
                rcsThrusterAttachment.position
            );
        }
    }

    private void EmitInputEvent(InputDirection direction)
    {
        //ExecuteEvents.Execute<IUserInputSubject>(GetComponent<>(), null, (x, y) => x.OnUserInput(direction));
    }
}