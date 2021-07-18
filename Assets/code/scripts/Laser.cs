using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float rayLenght;
    public LayerMask maskToCheck;
    public string laserName;
    public float distance;

    private RaycastHit hit;
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
        int wallMask = 1 << 11;
        isHitting = Physics.Raycast(transform.position, transform.forward, out hit, rayLenght, wallMask);

        distance = isHitting ? hit.distance : rayLenght; 
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
