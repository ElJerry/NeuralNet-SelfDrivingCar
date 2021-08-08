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
    public float distanceTraveled;
    public bool crashed = false;

    public Laser sFront;
    public Laser sRight;
    public Laser sLeft;

    private float gasInput;
    private float steerInput;
    private bool goingForward;
    private Vector3 lastPosition;
    private Vector3 startingPoint;
    private Quaternion startingAngle;

    void Start()
    {
        sphereRb.transform.parent = null;
        //Physics.IgnoreLayerCollision(9, 10, true);
        //Physics.IgnoreLayerCollision(10, 10, true);
        Physics.IgnoreCollision(sphereRb.GetComponent<Collider>(), carCollider);
        startingPoint = sphereRb.position;
        startingAngle = transform.rotation;
        Physics.IgnoreLayerCollision(9, 9);
        Physics.IgnoreLayerCollision(9, 10);
        Physics.IgnoreLayerCollision(10, 10);        
    }


    void Update()
    {
        //logInfo();
    }

    public void ResetCar()
    {
        crashed = false;
        distanceTraveled = 0;

        sphereRb.position = startingPoint;
        sphereRb.velocity = new Vector3(0f, 0f, 0f);
        sphereRb.angularVelocity = new Vector3(0f, 0f, 0f);
        sphereRb.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        sphereRb.angularDrag = 0.05f;
        transform.rotation = startingAngle;
        distanceTraveled = 0;
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
        }
            transform.Rotate(0, steerInput * steerSpeed, 0, Space.World);
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

        distanceTraveled += Vector3.Distance(transform.position, startingPoint);
        lastPosition = transform.position;        
    }

    private void OnTriggerEnter(Collider other)
    {        
        if(other.gameObject.layer == 9 || other.gameObject.layer == 10)
        {
            return;
        }

        //print("crashed!");
        crashed = true;
    }

}
