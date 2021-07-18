using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.code.src.NeuralNet
{
    class NeuralNet
    {
        Neurona nFrente, nDerecha, nIzquierda, nGas, nSteer;
        public NeuralNet()
        {
        }

        public void configurar(float []genes)
        {
            nFrente = new Neurona();
            nDerecha = new Neurona();
            nIzquierda = new Neurona();
            nGas = new Neurona();
            nSteer = new Neurona();

            nFrente.peso = genes[0];
            nDerecha.peso = genes[1];
            nIzquierda.peso = genes[2];
            nGas.peso = genes[3];
            nSteer.peso = genes[4];

            nFrente.LinkNeurona(nGas);
            nFrente.LinkNeurona(nSteer);

            nDerecha.LinkNeurona(nGas);
            nDerecha.LinkNeurona(nSteer);

            nIzquierda.LinkNeurona(nGas);
            nIzquierda.LinkNeurona(nSteer);
        }

        internal void Evaluar(float front, float right, float left)
        {
            nFrente.input = front;
            nDerecha.input = right;
            nIzquierda.input = left;

            nFrente.evaluar();
            nDerecha.evaluar();
            nIzquierda.evaluar();
            nGas.evaluar();
            nSteer.evaluar();

        }

        public void getResults(out float gas, out float steer)
        {
            gas = nGas.output;
            steer = nSteer.output;
        }
    }
}
