using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float rayLenght;
    public LayerMask maskToCheck;
    public string laserName;

    private RaycastHit hit;
    private float distance;
    private bool isHitting;

    public bool GetHitInfo(out float distance)
    {
        distance = isHitting ? this.distance : rayLenght;
        return isHitting;
    }

    void Start()
    {
        
    }

    void Update()
    {
        HandleRayHitInfo();

        //logInfo();
    }

    private void HandleRayHitInfo()
    {
        isHitting = Physics.Raycast(transform.position, transform.forward, out hit, rayLenght);
        distance = hit.distance;
    }

    private void logInfo()
    {
        float distance;
        GetHitInfo(out distance);
        print("RaycastHit " + laserName + " distance: " + distance);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * rayLenght);
    }
}
