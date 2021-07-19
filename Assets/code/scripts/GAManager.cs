using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.code.src;
using Assets.code.src.NeuralNet;
using System;

public class GAManager : MonoBehaviour
{
    GeneticAlgorithm ga;
    NeuralNet []nNet;
    CarController[] carController;
    
    //public Laser sFront, sRight, sLeft;
    public int individuosPorGeneracion = 50;

    public GameObject carObject;

    private int currentIndividual = 0;
    private int currentGeneration = 1;

    // only for live debugging
    public float currentGas;
    public float currentSteer;

    

    // Start is called before the first frame update
    void Start()
    {
        ga = new GeneticAlgorithm(individuosPorGeneracion, 6);
        print(ga.getIndividualsChart());
        InitializeCars();
    }

    private void InitializeCars()
    {
        carController = new CarController[individuosPorGeneracion];
        nNet = new NeuralNet[individuosPorGeneracion];
        for (int i = 0; i < individuosPorGeneracion; i++)
        {
            nNet[i] = new NeuralNet();
            nNet[i].configurar(ga.GetIndividuo(i).genes);
            GameObject car = spawnCar();
            carController[i] = car.GetComponent<CarController>();
        }
    }

    private GameObject spawnCar()
    {
        GameObject car = GameObject.Instantiate(carObject, transform.position, Quaternion.identity);
        return car;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInputs();
        HandleNeuralNetLogic();

        //foreach (var controller in carController)
        for (int i=0; i<carController.Length; i++)
        {
            var controller = carController[i];
            if (controller.crashed)
            {
                ga.AsignarFitness(i, controller.distanceTraveled);
                controller.gameObject.SetActive(false);
                //simulateNext();
            }
        }
    }

    private void HandleInputs()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            simulateNext();
            InitializeCars();
        }
    }

    private void simulateNext()
    {   
        //print("Nueva generacion " + currentGeneration);
        ga.Cruzar();
        print(ga.getIndividualsChart());
        
        // delete all cars
        foreach (var car in carController)
        {
            Destroy(car.sphereRb.gameObject);
            Destroy(car.gameObject);
        }

        //nNet.configurar(ga.GetIndividuo(currentIndividual).genes);
        //carController.ResetCar();
    }

    private void HandleNeuralNetLogic()
    {

        for (int i = 0; i<individuosPorGeneracion; i++)
        {
            if (!carController[i].gameObject.active)
            {
                continue;
            }

            float front, left, right;
            carController[i].sFront.GetHitInfo(out front);
            carController[i].sRight.GetHitInfo(out right);
            carController[i].sLeft.GetHitInfo(out left);
            nNet[i].Evaluar(front, right, left);
            //print("Evaluando: " + front + " " + right + " " + left);
            float gas, steerIzq, steerDer;
            nNet[i].getResults(out gas, out steerIzq, out steerDer);
            currentSteer = steerDer - steerIzq;
            carController[i].SendInputs(gas, currentSteer);
            currentGas = gas;
            //print("Neuron outputs[" + i + "] " + gas + " izq: " + steerIzq + " der: " + steerDer);
            //print("Sending inputs[" +i+ "] " + currentGas + " " + currentSteer);
        }
    }
}
