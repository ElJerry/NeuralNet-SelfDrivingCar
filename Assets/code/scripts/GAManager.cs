using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.code.src;

public class GAManager : MonoBehaviour
{
    GeneticAlgorithm ga;

    // Start is called before the first frame update
    void Start()
    {
        ga = new GeneticAlgorithm(10, 6);
        print(ga.getIndividualsChart());


        ga.Cruzar();
        print("Resultado cruza");
        print(ga.getIndividualsChart());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
