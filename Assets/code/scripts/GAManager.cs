using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.code.src;
using Assets.code.src.NeuralNet;
using System;

public class GAManager : MonoBehaviour
{
    GeneticAlgorithm ga;
    NeuralNet nNet;
    
    public Laser sFront, sRight, sLeft;
    public CarController carController;
    public int individuosPorGeneracion = 50;

    private int currentIndividual = 0;
    private int currentGeneration = 1;

    // only for live debugging
    public float currentGas;
    public float currentSteer;

    

    // Start is called before the first frame update
    void Start()
    {
        ga = new GeneticAlgorithm(individuosPorGeneracion, 5);
        print(ga.getIndividualsChart());

        nNet = new NeuralNet();
        nNet.configurar(ga.GetIndividuo(0).genes);
    }

    // Update is called once per frame
    void Update()
    {
        HandleInputs();
        HandleNeuralNetLogic();

        if (carController.crashed)
        {
            ga.AsignarFitness(currentIndividual, carController.distanceTraveled);
            carController.ResetCar();
            simulateNext();
        }
    }

    private void HandleInputs()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // force crash
            carController.crashed = true;
        }
    }

    private void simulateNext()
    {
        currentIndividual++;

        if (currentIndividual >= individuosPorGeneracion )
        {
            // brincar de generacion
            currentGeneration++;
            print("Nueva generacion " + currentGeneration);
            ga.Cruzar();
            print(ga.getIndividualsChart());
            currentIndividual = 0;
        }

        nNet.configurar(ga.GetIndividuo(currentIndividual).genes);
        carController.ResetCar();
    }

    private void HandleNeuralNetLogic()
    {
        float front, left, right;
        sFront.GetHitInfo(out front);
        sRight.GetHitInfo(out right);
        sLeft.GetHitInfo(out left);
        nNet.Evaluar(front, right, left);

        float gas, steer;
        nNet.getResults(out gas, out steer);
        //print("Sending inputs!!" + gas + " "+ steer);
        carController.SendInputs(gas, steer);
        currentGas = gas;
        currentSteer = steer;
    }
}
