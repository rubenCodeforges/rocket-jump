using Enums;
using Events;
using Managers;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    public float thrust = 0f;
    public float rcsThrust = 0f;
    public Transform currentThruster;
    public Transform currentRcs;
    public Transform mainThrusterAttachment;
    public Transform rcsThrusterAttachment;

    private Rigidbody rigidBody;
    private InputEventSubject eventSubject;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        eventSubject = GameManager.Instance.inputEventSubject;
        eventSubject.UserInput += (object source, UserInputEventArgs args) => { print(args.Direction); };

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
            eventSubject.OnUserInput(InputDirection.UP);
            rigidBody.AddForceAtPosition(mainThrusterAttachment.up * (thrust * Time.deltaTime),
                mainThrusterAttachment.position);
        }

        if (horizontalInput != 0f)
        {
            eventSubject.OnUserInput(horizontalInput > 0f ? InputDirection.RIGHT : InputDirection.LEFT);
            rigidBody.AddForceAtPosition(
                rcsThrusterAttachment.right * (horizontalInput * rcsThrust * -1 * Time.deltaTime),
                rcsThrusterAttachment.position
            );
        }
    }
}