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
        private System.Random random;

        public GeneticAlgorithm(int individuos, int tamanoGenes)
        {
            this.individuos = new Individuo[individuos];
            genesEnIndividuo = tamanoGenes;
            random = new System.Random();
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

            // Cruzar los mejores x/5 con randoms
            int newIndividuals = individuos.Length / 5;
            Individuo[] nuevos = new Individuo[newIndividuals];

            for (int i = 0; i < newIndividuals; i++)
            {
                int randomCouple = random.Next(newIndividuals, individuos.Length-1);
                nuevos[i] = individuos[i].Cruzar(individuos[randomCouple]);
            }
            

            // remplazar los ultimos con los nuevos
            int j = 0;
            for (int i = (individuos.Length - newIndividuals); i < individuos.Length; i++)
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

        public Individuo GetIndividuo(int id)
        {
            return individuos[id];
        }
    }
}
