using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRacingGameWithGeneticAlgorithm
{
    /// <summary>
    /// Klase nodrošina spēles hromosomas saglabāšanu (tās svarus un piemērotību)
    /// </summary>
    public class Chromosome
    {
        /// <summary>
        /// Hromosomas elementu definēšana
        /// </summary>
        public int Fitness { get; set; } = 0;
        public List<int> fitness = new List<int>();
        public ArrayList weights = new ArrayList();

        public Chromosome()
        {

        }

        /// <summary>
        /// Pievieno indivīdu piemērotības sarakstā
        /// </summary>
        /// <param name="fit"> Indivīda piemērotība </param>
        public void AddFitnessToList(int fit)
        {
            fitness.Add(fit);
        }

        /// <summary>
        /// Pārveido svaru sarakstu simbolu virknē
        /// </summary>
        /// <param name="num"> Transporta numurs </param>
        /// <returns> Simbolu virkni ar konkrētā transporlīdzekļa numuru un tā svariem </returns>
        public string PrintWeightsForChromosome(int num)
        {
            string weightsString = "  " + num + ".        ";
            for (int j = (num * 16); j < ((num * 16) + 16); j++)
            {
                weightsString += weights[j].ToString() + "       ";
            }
            return weightsString; 
        }

        /// <summary>
        /// Lielākās piemērotības noteikšana
        /// </summary>
        /// <returns> Lielāko piemērotību veselā skaitlī </returns>
        public int MaxFitness()
        {
            int max = 0;
            max = fitness.Max();
            return max;
        }

        /// <summary>
        /// Mazākās piemērotības noteikšana
        /// </summary>
        /// <returns> Mazāko piemērotību veselā skaitlī </returns>
        public int MinFitness()
        {
            int min = 0;
            min = fitness.Min();
            return min;
        }

        /// <summary>
        /// Vidējās aritmetiskās piemērotības noteikšana
        /// </summary>
        /// <returns> Vidējo piemērotību daļskaitlī </returns>
        public double AverageFitness()
        {
            double avg = 0;
            avg = fitness.Average();
            return avg;
        }

        /// <summary>
        /// Notīra visus klases sarakstus
        /// </summary>
        public void NextIteration()
        {
            fitness.Clear();
            weights.Clear();
        }
    }
}
