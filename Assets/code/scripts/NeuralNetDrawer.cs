using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetDrawer : MonoBehaviour
{
    public float xOffset;
    public float yOffset;

    NeuralNetwork neuralNetwork;

    bool neuronsCreated = false;

    List<GameObject> inputNodes;
    List<GameObject> hiddenNodes;
    List<GameObject> outputNodes;
    List<LineRenderer> lines;
    Dictionary<string, GameObject> nodeMapping;

    // Start is called before the first frame update
    void Start()
    {
        inputNodes = new List<GameObject>();
        hiddenNodes = new List<GameObject>();
        outputNodes = new List<GameObject>();
        nodeMapping = new Dictionary<string, GameObject>();

        int linesNeeded = (neuralNetwork.inputList.Count * neuralNetwork.nodesInHiddenLayers) 
                        + (neuralNetwork.hiddenLayers * neuralNetwork.nodesInHiddenLayers) 
                        + (neuralNetwork.nodesInHiddenLayers * neuralNetwork.outputList.Count);

        lines = new List<LineRenderer>();
        for (int i=0; i<linesNeeded; i++)
        {
            var lineObject = new GameObject("line" + i);
            lineObject.transform.parent = gameObject.transform;
            var line = lineObject.AddComponent<LineRenderer>();
            line.material = new Material(Shader.Find("Sprites/Default"));
            line.widthMultiplier = 0.05f;
            lines.Add(line);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (neuralNetwork != null)
        {
            DrawNeuralNet();
        }
    }

    void DrawNeuralNet()
    {
        List<Neuron> inputs = neuralNetwork.inputList;
        List<Neuron> outputs = neuralNetwork.outputList;
        Neuron[,] layerNeurons = neuralNetwork.layerNeurons;


        if (!neuronsCreated)
        {
            CreateNodes(inputs, outputs);
        }

        // link nodes
        int lineN = 0;
        foreach (Neuron n in neuralNetwork.allNeurons)
        {
            var origin = nodeMapping[n.neuronId];
            foreach (Neuron nLink in n.outputList)
            {
                var destiny = nodeMapping[nLink.neuronId];
                Color color;
                if (n.output > .3f)
                    color = Color.black;
                else
                    color = Color.white;

                //color = new Color(n.output, n.output, n.output);

                //Debug.DrawRay(nodeMapping[n.neuronId].transform.position, destiny.transform.localPosition - origin.transform.localPosition, color);
                lines[lineN].startColor = lines[lineN].endColor = color;

                lines[lineN].SetPosition(0, nodeMapping[n.neuronId].transform.position);
                lines[lineN].SetPosition(1, destiny.transform.position);
                lineN++;
            }
            // write output value
            TextMesh text = origin.transform.GetComponentInChildren<TextMesh>();
            text.text = n.output.ToString();

            // print value

        }

    }

    private void CreateNodes(List<Neuron> inputs, List<Neuron> outputs)
    {
        for (int i = 0; i < inputs.Count; i++)
        {
            var go = CreateSphere(new Vector3(0, i * yOffset, 0), "Input" + i);
            nodeMapping[inputs[i].neuronId] = go;
            inputNodes.Add(go);
        }

        for (int i = 0; i < neuralNetwork.hiddenLayers; i++)
        {
            for (int j = 0; j < neuralNetwork.nodesInHiddenLayers; j++)
            {
                var go = CreateSphere(new Vector3((1 + i) * xOffset, j * yOffset, 0), "Hidden" + i + "," + j);
                nodeMapping[neuralNetwork.layerNeurons[i,j].neuronId] = go;
                hiddenNodes.Add(go);
            }
        }


        for (int i = 0; i < outputs.Count; i++)
        {
            var go = CreateSphere(new Vector3((neuralNetwork.hiddenLayers + 1) * xOffset, i * yOffset, 0), "Output" + i);
            nodeMapping[outputs[i].neuronId] = go;
            outputNodes.Add(go);
        }

        neuronsCreated = true;

        //downscale
        //gameObject.transform.localScale = new Vector3(.2f, .2f, .2f);
    }


    GameObject CreateSphere(Vector3 position, string name)
    {
        var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        go.transform.parent = gameObject.transform;
        go.transform.localPosition = position;
        go.name = name;

        go.GetComponent<SphereCollider>().enabled = false;

        var text = new GameObject("Text");
        text.transform.parent = go.transform;
        text.transform.localPosition = new Vector3(0, 0, -.49f);       

        var textComponent = text.AddComponent<TextMesh>();
        textComponent.text = "Testing!";
        textComponent.anchor = TextAnchor.MiddleCenter;
        textComponent.characterSize = .2f;
        textComponent.color = Color.black;
        return go;
    }

    public void SetNeuralNetwork(NeuralNetwork neuralNetwork)
    {
        this.neuralNetwork = neuralNetwork;
    }
}
