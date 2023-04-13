using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRacingGameWithGeneticAlgorithm
{
    public class Chromosome
    { 
        public int Fitness { get; set; } = 0;
        public ArrayList weights = new ArrayList();

        public Chromosome()
        {
            InitializeChromosome();
        }

        private void InitializeChromosome()
        {
            
        }

        public string PrintWeightsForChromosome(int i)
        {
            string weightsString = i + ".      ";
            for (int j = i * 12; j < i * 12 + 12; j++)
            {
                weightsString += weights[j].ToString() + "    ";
            }
            return weightsString; 
        }
    }
}
