using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.code.src
{
    public class GeneticAlgorithm
    {
        
        private Individuo[] individuos;
        private int genesEnIndividuo;

        public GeneticAlgorithm(int individuos, int tamanoGenes)
        {
            this.individuos = new Individuo[individuos];
            genesEnIndividuo = tamanoGenes;
            GenerarIniciales();
        }

        void GenerarIniciales()
        {
            for (int i = 0; i < individuos.Length; i++)
            {
                individuos[i] = new Individuo(genesEnIndividuo);
                individuos[i].fitness = i;
            }
        }

        public string getIndividualsChart()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\n============ Individuals ============\n");
            sb.Append("Lenght: " + individuos.Length + "\n");
            foreach(Individuo i in individuos)
            {
                sb.Append(i.getGenes() + "\n");
            }
            return sb.ToString();
        }

        public void Cruzar()
        {
            OrdenarPorFitness();
            Individuo[] nuevos = new Individuo[individuos.Length / 2];
            // agarrar los valores promedio
            for (int i = 0; i<individuos.Length/2; i++)
            {
                nuevos[i] = individuos[i].Cruzar(individuos[i + 1]);
            }

            // remplazar los ultimos con los nuevos
            int j = 0;
            for (int i = (individuos.Length / 2) + 1; i < individuos.Length; i++)
            {
                individuos[i] = nuevos[j++];
            }

            OrdenarPorFitness();
        }

        private void OrdenarPorFitness()
        {
            Array.Sort(individuos);
        }

        public void AsignarFitness(int id, float fitness)
        {
            individuos[id].fitness = fitness;
        }
    }
}
