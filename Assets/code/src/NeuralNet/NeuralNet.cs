using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.code.src.NeuralNet
{
    class NeuralNet
    {
        Neurona nFrente, nDerecha, nIzquierda, nGas, nSteerIzq, nSteerDer;
        public NeuralNet()
        {
        }

        public void configurar(float []genes)
        {
            nFrente = new Neurona();
            nDerecha = new Neurona();
            nIzquierda = new Neurona();
            nGas = new Neurona();
            nSteerIzq = new Neurona();
            nSteerDer = new Neurona();

            nFrente.peso = genes[0];
            nDerecha.peso = genes[1];
            nIzquierda.peso = genes[2];
            nGas.peso = genes[3];
            nSteerIzq.peso = genes[4];
            nSteerDer.peso = genes[5];

            nFrente.LinkNeurona(nGas);
            nFrente.LinkNeurona(nSteerIzq);
            nFrente.LinkNeurona(nSteerDer);

            nDerecha.LinkNeurona(nGas);
            //nDerecha.LinkNeurona(nSteerIzq);
            nDerecha.LinkNeurona(nSteerDer);

            nIzquierda.LinkNeurona(nGas);
            nIzquierda.LinkNeurona(nSteerIzq);
            //nIzquierda.LinkNeurona(nSteerDer);
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
            nSteerIzq.evaluar();
            nSteerDer.evaluar();

        }

        public void getResults(out float gas, out float steerIzq, out float steerDer)
        {
            gas = nGas.output;
            steerIzq = nSteerIzq.output;
            steerDer = nSteerDer.output;
        }
    }
}
