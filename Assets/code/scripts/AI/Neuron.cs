using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuron
{
    public float bias;
    public float weight;
    public int inputs, outputs;

    private List<Neuron> inputList;
    private List<Neuron> outputList;
    private float input;
    private float output;
    private string neuronId { get; }

    private Printer printer;

    public Neuron(float bias, float weight, string neuronId)
    {
        this.bias = bias;
        this.weight = weight;
        this.neuronId = neuronId;

        inputList = new List<Neuron>();
        outputList = new List<Neuron>();
    }

    public void LinkNeurons(Neuron other)
    {
        this.outputList.Add(other);
        other.inputList.Add(this);
        Printer.print("Linked " + neuronId +" with " + other.neuronId);
    }

    public float Evaluate()
    {
        // gather inputs from inputList
        foreach (Neuron n in inputList)
        {
            input += n.output;
        }

        output = (input * weight) + bias;
        output = (float)(Math.Tanh((double)output));
        Printer.print(neuronId + " output: " + output);
        return output;
    }

    public void PrintNeuronInfo()
    {
        string message = neuronId +"=== bias:" + bias + " weight:" + weight;
        Printer.print(message);
    }

    public void SetValues(float bias, float weight)
    {
        this.bias = bias;
        this.weight = weight;
    }
}
