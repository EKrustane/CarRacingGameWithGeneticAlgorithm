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
        public List<int> fitness = new List<int>();
        public ArrayList weights = new ArrayList();

        public Chromosome()
        {
            InitializeChromosome();
        }

        private void InitializeChromosome()
        {
            
        }

        public void AddFitnessToList(int fit)
        {
            fitness.Add(fit);
        }

        public string PrintWeightsForChromosome(int i)
        {
            string weightsString = i + ".                      ";
            for (int j = (i * 12); j < ((i * 12) + 12); j++)
            {
                weightsString += weights[j].ToString() + "              ";
            }
            return weightsString; 
        }

        public int MaxFitness()
        {
            int max = 0;
            max = fitness.Max();
            return max;
        }

        public int MinFitness()
        {
            int min = 0;
            min = fitness.Min();
            return min;
        }
        public double AverageFitness()
        {
            double avg = 0;
            avg = fitness.Average();
            return avg;
        }

        public void NextIteration()
        {
            fitness.Clear();
            weights.Clear();
        }
    }
}
