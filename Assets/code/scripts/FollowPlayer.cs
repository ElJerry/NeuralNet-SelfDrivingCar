using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public GameObject target;

    public Vector3 cameraOffset;

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position + cameraOffset;
    }
}
