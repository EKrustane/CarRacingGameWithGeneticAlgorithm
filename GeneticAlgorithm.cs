using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CarRacingGameWithGeneticAlgorithm
{

    class GeneticAlgorithm
    {
        //Chromosome chromosome = new Chromosome();
        public ArrayList generation = new ArrayList();
        public ArrayList genW = new ArrayList();
        private List<int> genF = new List<int>();
        public ArrayList nextGeneration = new ArrayList();
        public List<int> fitness = new List<int>();
        public List<int> numbers = new List<int>();
        public List<int> fitnessForSelection = new List<int>();
        public ArrayList weightsForSelection = new ArrayList();
        private ArrayList weights1 = new ArrayList();
        private Random rand = new Random();
        private double selection = 0;
        //private int iterationNumber;
        public double[] w = new double[13];

        public GeneticAlgorithm()
        {
            InitializeGeneticAlgorithm();
        }

        public void InitializeGeneticAlgorithm()
        {
            //Selection();
            //Crossover();
        }



        public double RandomNumber()
        {
            return Math.Round(rand.NextDouble(), 3);
        }

        public void IntervalsForSelection()
        {
            for(int i = 0; i < 12; i++)
            {
                selection = RandomNumber();
                if (selection >= 0 && selection <= 0.167)
                {
                    numbers.Add(0);
                    fitnessForSelection.Add(fitness[0]);
                    for(int j = 0; j < 13; j++)
                    {
                        weights1.Add(generation[j]);   
                    }
                    weightsForSelection.AddRange(weights1);
                    //MessageBox.Show(weightsForSelection[5].ToString());
                    weights1.Clear();
                }
                else if (selection > 0.167 && selection <= 0.333)
                {
                    numbers.Add(1);
                    fitnessForSelection.Add(fitness[1]);
                    for (int k = 13; k < 26; k++)
                    {
                        weights1.Add(generation[k]);
                    }
                    weightsForSelection.AddRange(weights1);
                    weights1.Clear();
                }
                else if (selection > 0.333 && selection <= 0.5)
                {
                    numbers.Add(2);
                    fitnessForSelection.Add(fitness[2]);
                    for (int l = 26; l < 39; l++)
                    {
                        weights1.Add(generation[l]);
                    }
                    weightsForSelection.AddRange(weights1);
                    weights1.Clear();
                }
                else if (selection > 0.5 && selection <= 0.667)
                {
                    numbers.Add(3);
                    fitnessForSelection.Add(fitness[3]);
                    for (int m = 39; m < 52; m++)
                    {
                        weights1.Add(generation[m]);
                    }
                    weightsForSelection.AddRange(weights1);
                    weights1.Clear();
                }
                else if (selection > 0.667 && selection <= 0.833)
                {
                    numbers.Add(4);
                    fitnessForSelection.Add(fitness[4]);
                    for (int n = 52; n < 65; n++)
                    {
                        weights1.Add(generation[n]);
                    }
                    weightsForSelection.AddRange(weights1);
                    weights1.Clear();
                }
                else if (selection > 0.833 && selection <= 1)
                {
                    numbers.Add(5);
                    fitnessForSelection.Add(fitness[5]);
                    for (int o = 65; o < 78; o++)
                    {
                        weights1.Add(generation[o]);
                    }
                    weightsForSelection.AddRange(weights1);
                    weights1.Clear();
                }
            }

            Selection();
        }

        public void Selection()
        {

            for (int j = 0; j < 12; j = j + 2)
            {
                if (fitnessForSelection[j] >= fitnessForSelection[j + 1])
                {
                    for (int l = j * 13; l < j * 13 + 13; l++)
                    {
                        weights1.Add(weightsForSelection[l]);
                    }
                    genW.AddRange(weights1);
                    genF.Add(fitnessForSelection[j]);
                    weights1.Clear();
                    //MessageBox.Show(genW[5].ToString());
                }
                else
                {
                    for (int l = (j + 1) * 13; l < (j + 1) * 13 + 13; l++)
                    {
                        weights1.Add(weightsForSelection[l]);
                    }
                    genW.AddRange(weights1);
                    genF.Add(fitnessForSelection[j + 1]);
                    weights1.Clear();
                }
            }
        }

        public int CrossoverLocation()
        {
            double r = RandomNumber();
            int number = 0;
            if (r >= 0 && r <= 0.167)
            {
                number = 0;
            }
            else if (r > 0.167 && r <= 0.333)
            {
                number = 1;
            }
            else if (r > 0.333 && r <= 0.5)
            {
                number = 2;
            }
            else if (r > 0.5 && r <= 0.667)
            {
                number = 3;
            }
            else if (r > 0.667 && r <= 0.833)
            {
                number = 4;
            }
            else if (r > 0.833 && r <= 1)
            {
                number = 5;
            }
            return number;
        }

        public void Crossover()
        {
            object gen;
            int j = CrossoverLocation();
            int k = CrossoverLocation() + 6;
            for(int i = j; i <= k; i++)
            {
                
                gen = genW[i];
                genW[i] = genW[i + 13];
                genW[i + 13] = gen;
            }

            j = CrossoverLocation();
            k = CrossoverLocation() + 6;
            for (int l = j + 26; l <= k + 26; l++)
            {
                gen = genW[l];
                genW[l] = genW[l + 13];
                genW[l + 13] = gen;
            }
            
            j = CrossoverLocation();
            k = CrossoverLocation() + 6;
            for (int m = j + 52; m <= k + 52; m++)
            {
                gen = genW[m];
                genW[m] = genW[m + 13];
                genW[m + 13] = gen;
            }
            
        }


        public void AllToList()
        {
            //for (int i = 0; i < 72; i++)
            //{
                generation.AddRange(genW);
            //}
        }

        public void Mutation()
        {
            for(int i = 0; i < 12; i++)
            {
                double k = RandomNumber();
                if(k <= 0.3)
                {
                    int loc = CrossoverLocation();
                    generation[i * 13 + loc] = RandomNumber();
                }
            }
        }

        public void SelectionToNextGeneration()
        {
            for (int i = 0; i < 6; i++)
            {
                selection = RandomNumber();
                if (selection >= 0 && selection <= 0.083)
                {
                    for (int j = 0; j < 13; j++)
                    {
                        weights1.Add(generation[j]);
                    }
                    nextGeneration.AddRange(weights1);
                    weights1.Clear();
                }
                else if (selection > 0.083 && selection <= 0.167)
                {
                    for (int j = 13; j < 26; j++)
                    {
                        weights1.Add(generation[j]);
                    }
                    nextGeneration.AddRange(weights1);
                    weights1.Clear();
                }
                else if (selection > 0.167 && selection <= 0.25)
                {
                    for (int j = 26; j < 39; j++)
                    {
                        weights1.Add(generation[j]);
                    }
                    nextGeneration.AddRange(weights1);
                    weights1.Clear();
                }
                else if (selection > 0.25 && selection <= 0.333)
                {
                    for (int j = 39; j < 52; j++)
                    {
                        weights1.Add(generation[j]);
                    }
                    nextGeneration.AddRange(weights1);
                    weights1.Clear();
                }
                else if (selection > 0.333 && selection <= 0.417)
                {
                    for (int j = 52; j < 65; j++)
                    {
                        weights1.Add(generation[j]);
                    }
                    nextGeneration.AddRange(weights1);
                    weights1.Clear();
                }
                else if (selection > 0.417 && selection <= 0.5)
                {
                    for (int j = 65; j < 78; j++)
                    {
                        weights1.Add(generation[j]);
                    }
                    nextGeneration.AddRange(weights1);
                    weights1.Clear();
                }
                if (selection >= 0.5 && selection <= 0.583)
                {
                    for (int j = 78; j < 91; j++)
                    {
                        weights1.Add(generation[j]);
                    }
                    nextGeneration.AddRange(weights1);
                    weights1.Clear();
                }
                else if (selection > 0.583 && selection <= 0.667)
                {
                    for (int j = 91; j < 104; j++)
                    {
                        weights1.Add(generation[j]);
                    }
                    nextGeneration.AddRange(weights1);
                    weights1.Clear();
                }
                else if (selection > 0.667 && selection <= 0.75)
                {
                    for (int j = 104; j < 117; j++)
                    {
                        weights1.Add(generation[j]);
                    }
                    nextGeneration.AddRange(weights1);
                    weights1.Clear();
                }
                else if (selection > 0.75 && selection <= 0.833)
                {
                    for (int j = 117; j < 130; j++)
                    {
                        weights1.Add(generation[j]);
                    }
                    nextGeneration.AddRange(weights1);
                    weights1.Clear();
                }
                else if (selection > 0.833 && selection <= 0.917)
                {
                    for (int j = 130; j < 143; j++)
                    {
                        weights1.Add(generation[j]);
                    }
                    nextGeneration.AddRange(weights1);
                    weights1.Clear();
                }
                else if (selection > 0.917 && selection <= 1)
                {
                    for (int j = 143; j < 156; j++)
                    {
                        weights1.Add(generation[j]);
                    }
                    nextGeneration.AddRange(weights1);
                    weights1.Clear();
                }
            }
        }

        public string PrintWeightsAfterCrossover(int i)
        {
            string weightsString = "";
            for (int j = i * 13; j < (i * 13 + 13); j++)
            {
                weightsString += genW[j].ToString() + "  ";
            }
            return weightsString;
        }

        public string PrintWeightsAfterMutation(int i)
        {
            string weightsString = "";
            for (int j = i * 13; j < (i * 13 + 13); j++)
            {
                weightsString += generation[j].ToString() + "  ";
            }
            return weightsString;
        }

        public string PrintNextGenerationWeights(int i)
        {
            string weightsString = "";
            for (int j = i * 13; j < (i * 13 + 13); j++)
            {
                weightsString += nextGeneration[j].ToString() + "  ";
            }
            return weightsString;
        }

        public string PrintWeightsForSelection(int i)
        {
            string weightsString = "";
            for (int j = i * 13; j < (i * 13 + 13); j++)
            {
                weightsString += weightsForSelection[j].ToString() + "  ";
            }
            return weightsString;
        }

        public string PrintFitnessForSelection(int i)
        {
            string fitnessString = fitnessForSelection[i].ToString();
            return fitnessString;
        }

        public void NextIteration()
        {
            generation.Clear();
            genF.Clear();
            genW.Clear();
            fitness.Clear();
            numbers.Clear();
            fitnessForSelection.Clear();
            weightsForSelection.Clear();
        }
    }
}
