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

    private List<Neuron> inputList;
    private List<Neuron> outputList;
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
        foreach (Neuron n in inputList)
        {
            var test = this.neuronId;
            input += (n.output * weight);
        }

        output = ((input * weight)) * weight;
        output = (float)(Math.Tanh((double)output));
        //Printer.print(neuronId + " output: " + output);
        if (neuronId.Contains("output1"))
        {
            var a = 1;
        }
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
