using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.code.src;
using Assets.code.src.NeuralNet;

public class GAManager : MonoBehaviour
{
    GeneticAlgorithm ga;
    NeuralNet nNet;
    
    public Laser sFront, sRight, sLeft;
    public CarController carController;

    // Start is called before the first frame update
    void Start()
    {
        ga = new GeneticAlgorithm(10, 5);
        print(ga.getIndividualsChart());

        nNet = new NeuralNet();
        nNet.configurar(ga.GetIndividuo(0).genes);

    }

    // Update is called once per frame
    void Update()
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
    }
}
