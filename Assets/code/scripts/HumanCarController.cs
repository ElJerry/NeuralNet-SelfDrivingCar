using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanCarController : MonoBehaviour
{
    private const string AxisHorizontal = "Horizontal";
    private const string AxisVertical = "Vertical";

    public CarController carController;
        
    // Update is called once per frame
    void Update()
    {
        handleInputs();    
    }

    private void handleInputs()
    {
        float gasInput = Input.GetAxis(AxisVertical);
        float steerInput = Input.GetAxis(AxisHorizontal);

        carController.SendInputs(gasInput, steerInput);
    }
}
