using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork : MonoBehaviour
{
    public int inputs;
    public int outputs;
    public int hiddenLayers;
    public int nodesInHiddenLayers;

    private List<Neuron> inputList;
    private List<Neuron> outputList;

    private List<Neuron> allNeurons;

    private Neuron[,] layerNeurons;

    private void Start()
    {
        inputList = new List<Neuron>();
        outputList = new List<Neuron>();
        allNeurons = new List<Neuron>();

        layerNeurons = new Neuron[hiddenLayers, nodesInHiddenLayers];

        // Create input neurons
        for (int i = 0; i < inputs; i++)
        {
            //Neuron neuron = GameObject.Instantiate();
            var neuron = new Neuron(1, 1, "input" + i);
            inputList.Add(neuron);
            allNeurons.Add(neuron);
            neuron.PrintNeuronInfo();
        }

        // Create output neurons
        for (int i = 0; i < inputs; i++)
        {
            //Neuron neuron = GameObject.Instantiate();
            var neuron = new Neuron(1, 1, "output" + i);
            outputList.Add(neuron);
            allNeurons.Add(neuron);
            neuron.PrintNeuronInfo();
        }

        // Fill hidden layers
        for (int i = 0; i < hiddenLayers; i++)
        {
            for (int j = 0; j < nodesInHiddenLayers; j++)
            {
                var neuron = new Neuron(1, 1, "hidden" + i + "-" + j);
                neuron.PrintNeuronInfo();
                layerNeurons[i, j] = neuron;
                allNeurons.Add(neuron);
            }
        }

        // Link all nodes
        print("========== LINKING IN PROCESS =========");
        print("========== LINKING INPUTS =========");
        // Link input nodes with first hidden layer
        foreach (Neuron n in inputList)
        {
            for (int k = 0; k < nodesInHiddenLayers; k++)
            {
                n.LinkNeurons(layerNeurons[0, k]);
            }
        }


        // link hidden layers
        print("========== LINKING HIDDEN LAYERS =========");
        // i => layer
        for (int i = 0; i < hiddenLayers-1; i++)
        {
            // j => node in current layer
            for (int j = 0; j < nodesInHiddenLayers; j++)
            {
                // k => node in following layer
                for (int k = 0; k < nodesInHiddenLayers; k++)
                {
                    layerNeurons[i, j].LinkNeurons(layerNeurons[i + 1, k]);
                    //                                          ^^^^^ next layer
                }
            }
        }

        print("========== LINKING OUTPUT LAYER =========");
        // link last hidden layer with outputs
        for (int j = 0; j < nodesInHiddenLayers; j++)
        {
            // k => node in following layer
            foreach (Neuron n in outputList)
            {                
                layerNeurons[hiddenLayers - 1, j].LinkNeurons(n);
            }
        }

        EvaluateState();
    }


    public void EvaluateState()
    {
        foreach (Neuron n in allNeurons)
        {
            n.Evaluate();
        }
    }

    private void Update()
    {
        
    }
}
