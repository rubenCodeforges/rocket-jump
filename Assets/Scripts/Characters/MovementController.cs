using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementController : MonoBehaviour
{
    public float thrust = 100f;
    public float rcsThrust = 100f;
    public Transform mainThruster;
    public Transform rcsThruster;
    
    private Rigidbody rigidBody;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        mainThruster.parent = rigidBody.transform;
        rcsThruster.parent = rigidBody.transform;
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
            // Thrust
            rigidBody.AddForceAtPosition(mainThruster.up * (thrust * Time.deltaTime), mainThruster.position);
        }

        if (horizontalInput != 0f)
        {
            // rcs
            print(Input.GetAxis("Horizontal"));
            rigidBody.AddForceAtPosition(rcsThruster.right * (horizontalInput * rcsThrust * Time.deltaTime)  , rcsThruster.position);
        }
    }
}
