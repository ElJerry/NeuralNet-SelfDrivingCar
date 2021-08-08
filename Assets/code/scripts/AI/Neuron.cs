using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuron
{
    public float bias;
    public float weight;
    public int inputs, outputs;
    public float input;
    public float output;

    public List<Neuron> inputList;
    public List<Neuron> outputList;
    public string neuronId { get; }

    private Printer printer;

    public Neuron(float bias, float weight, string neuronId)
    {
        this.bias = bias;
        this.weight = weight;
        this.neuronId = neuronId;

        inputList = new List<Neuron>();
        outputList = new List<Neuron>();
    }

    public Neuron(string neuronId) : this(0, 0, neuronId) { }

    public void LinkNeurons(Neuron other)
    {
        this.outputList.Add(other);
        other.inputList.Add(this);
        //Printer.print("Linked " + neuronId +" with " + other.neuronId);
    }

    public float Evaluate()
    {
        // gather inputs from inputList
        float localInput = input;
        foreach (Neuron n in inputList)
        {
            var test = this.neuronId;
            localInput += (n.output);
        }
        //Printer.print(neuronId + " Local input: " + localInput);

        output = ((localInput) * weight) + bias;
        output = (float)(Math.Tanh((double)output));
        //Printer.print(neuronId + " output: " + output);

        // clean input at the end
        //Printer.print(neuronId + " output: " + output);        
        return output;
    }

    public void PrintNeuronInfo()
    {
        string message = neuronId +"=== bias:" + bias + " weight:" + weight;
        //Printer.print(message);
    }

    public void SetValues(float bias, float weight)
    {
        this.bias = bias;
        this.weight = weight;
    }
}
