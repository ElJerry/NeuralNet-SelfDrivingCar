using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.code.src;
using Assets.code.src.NeuralNet;
using System;

public class GAManager : MonoBehaviour
{
    GeneticAlgorithm ga;
    NeuralNetwork []nNet;
    CarController[] carController;
    
    //public Laser sFront, sRight, sLeft;
    public int individuosPorGeneracion = 50;

    public GameObject carObject;
    public NeuralNetDrawer neuralNetDrawer;

    private int currentIndividual = 0;
    private int currentGeneration = 1;

    // only for live debugging
    public float currentGas;
    public float currentSteer;
    public int inputs, outputs, layers, layerNodes;

    

    // Start is called before the first frame update
    void Start()
    {
        int nodos = inputs + outputs + (layers * layerNodes);
        ga = new GeneticAlgorithm(individuosPorGeneracion, nodos * 2);
        print(ga.getIndividualsChart());
        InitializeCars();
    }

    private void InitializeCars()
    {
        carController = new CarController[individuosPorGeneracion];
        nNet = new NeuralNetwork[individuosPorGeneracion];
        for (int i = 0; i < individuosPorGeneracion; i++)
        {
            nNet[i] = new NeuralNetwork(inputs, outputs, layers, layerNodes);
            nNet[i].ConfigureNetwork(ga.GetIndividuo(i).genes);
            neuralNetDrawer.SetNeuralNetwork(nNet[i]);
            GameObject car = spawnCar();
            carController[i] = car.GetComponent<CarController>();
            //car.GetComponentInChildren<NeuralNetDrawer>().SetNeuralNetwork(nNet[i]);
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            Start();
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

            List<float> inputs = new List<float> { front / 30, right / 30, left / 30 };
           
            nNet[i].EvaluateState(inputs);
            //print("Evaluando: " + front + " " + right + " " + left);
            var outputs = nNet[i].GetOutputs();
            float gas = outputs[0];
            //float steerIzq = outputs[1];
            //float steerDer = outputs[2];
            currentSteer = outputs[1]; //steerDer - steerIzq;
            carController[i].SendInputs(gas, currentSteer);
            currentGas = gas;
            //print("Neuron outputs[" + i + "] " + gas + " izq: " + steerIzq + " der: " + steerDer);
            //print("Sending inputs[" +i+ "] " + currentGas + " " + currentSteer);
        }
    }
}
