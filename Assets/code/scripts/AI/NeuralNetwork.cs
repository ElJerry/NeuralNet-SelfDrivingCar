using Assets.code.src;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork
{
    public int inputs;
    public int outputs;
    public int hiddenLayers;
    public int nodesInHiddenLayers;

    private List<Neuron> inputList;
    private List<Neuron> outputList;
    private List<Neuron> allNeurons;

    private Neuron[,] layerNeurons;

    public NeuralNetwork(int inputs, int outputs, int hiddenLayers, int nodesInHiddenLayers)
    {

        this.inputs = inputs;
        this.outputs = outputs;
        this.hiddenLayers = hiddenLayers;
        this.nodesInHiddenLayers = nodesInHiddenLayers;

        inputList = new List<Neuron>();
        outputList = new List<Neuron>();
        allNeurons = new List<Neuron>();

        layerNeurons = new Neuron[hiddenLayers, nodesInHiddenLayers];

        // Create input neurons
        for (int i = 0; i < inputs; i++)
        {
            //Neuron neuron = GameObject.Instantiate();
            var neuron = new Neuron( "input" + i);
            inputList.Add(neuron);
            allNeurons.Add(neuron);
            neuron.PrintNeuronInfo();
        }        

        // Fill hidden layers
        for (int i = 0; i < hiddenLayers; i++)
        {
            for (int j = 0; j < nodesInHiddenLayers; j++)
            {
                var neuron = new Neuron("hidden" + i + "-" + j);
                neuron.PrintNeuronInfo();
                layerNeurons[i, j] = neuron;
                allNeurons.Add(neuron);
            }
        }

        // Create output neurons
        for (int i = 0; i < inputs; i++)
        {
            //Neuron neuron = GameObject.Instantiate();
            var neuron = new Neuron("output" + i);
            outputList.Add(neuron);
            allNeurons.Add(neuron);
            neuron.PrintNeuronInfo();
        }

        // Link all nodes
        //Printer.print("========== LINKING IN PROCESS =========");
        //Printer.print("========== LINKING INPUTS =========");
        // Link input nodes with first hidden layer
        foreach (Neuron n in inputList)
        {
            for (int k = 0; k < nodesInHiddenLayers; k++)
            {
                n.LinkNeurons(layerNeurons[0, k]);
            }
        }


        // link hidden layers
        //Printer.print("========== LINKING HIDDEN LAYERS =========");
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

        //Printer.print("========== LINKING OUTPUT LAYER =========");
        // link last hidden layer with outputs
        for (int j = 0; j < nodesInHiddenLayers; j++)
        {
            // k => node in following layer
            foreach (Neuron n in outputList)
            {                
                layerNeurons[hiddenLayers - 1, j].LinkNeurons(n);
            }
        }
    }

    public void ConfigureNetwork(float[] genes)
    {
        int aux = 0;
        for (int i = 0; i < allNeurons.Count; i++)
        {
            allNeurons[i].bias = genes[aux++];
            allNeurons[i].weight = genes[aux++];
        }
    }

    public void SetInputs(List<float> inputs)
    {
        for (int i = 0; i < inputs.Count; i++)
        {
            inputList[i].input = inputs[i];
        }
    }

    public List<float> GetOutputs()
    {
        List<float> outputs = new List<float>();
        foreach (Neuron n in outputList)
        {
            outputs.Add(n.output);

            if (n.outputs != 0)
            {
                // stop here
                int a;
                a = 1;
            }
        }

        return outputs;
    }


    public void EvaluateState(List<float> inputs)
    {
        // update inputs
        SetInputs(inputs);

        foreach (Neuron n in allNeurons)
        {
            n.Evaluate();
        }
    }

    private void Update()
    {
        
    }

    private void Print(Object msg)
    {

    }
}
