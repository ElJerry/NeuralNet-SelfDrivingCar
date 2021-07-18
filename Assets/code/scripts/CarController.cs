using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Rigidbody sphereRb;
    public MeshCollider carCollider;

    public float forwardSpeed;
    public float steerSpeed;

    private float gasInput;
    private float steerInput;
    private bool goingForward;
    private Vector3 lastPosition;

    void Start()
    {
        sphereRb.transform.parent = null;
        //Physics.IgnoreLayerCollision(9, 10, true);
        //Physics.IgnoreLayerCollision(10, 10, true);
        Physics.IgnoreCollision(sphereRb.GetComponent<Collider>(), carCollider);
    }


    void Update()
    {
        logInfo();
    }

    private void logInfo()
    {
        print("gasInput: " + gasInput);
        print("steerInput: " + steerInput);
    }

    private void updateVehiclePosition()
    {
        transform.position = sphereRb.transform.position;

        // rotate car
        float diff = (lastPosition - transform.position).magnitude;
        if (diff != 0)
        {
            // revert steering if going backwards
            if (!goingForward)
            {
                steerInput = -steerInput;
            }
            transform.Rotate(0, steerInput * steerSpeed, 0, Space.World);
        }
    }

    public void SendInputs(float gas, float steer)
    {
        gasInput = gas;
        steerInput = steer;
    }    

    void FixedUpdate()
    {
        // check steering effect
        if (gasInput > 0)
            goingForward = true;
        else if (gasInput < 0)
            goingForward = false;

        
        sphereRb.AddForce(transform.forward * forwardSpeed * gasInput, ForceMode.Acceleration);        
        updateVehiclePosition();

        lastPosition = transform.position;
    }
}
