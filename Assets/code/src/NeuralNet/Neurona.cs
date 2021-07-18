using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.code.src.NeuralNet
{
    class Neurona
    {
        List<Neurona> linksTo = new List<Neurona>();
        List<Neurona> linksFrom = new List<Neurona>();

        public float peso;
        public float input;
        public float output;

        public void LinkNeurona(Neurona otra)
        {
            linksTo.Add(otra);
            otra.linksFrom.Add(this);
        }

        public float evaluar()
        {
            float sum = 0;

            foreach(Neurona n in linksFrom)
            {
                sum += n.output;
            }

            output = (input * peso) + (sum*peso);
            output = (float)Math.Tanh(output);

            return output;
        }
    }
}
