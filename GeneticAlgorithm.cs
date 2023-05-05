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
    /// <summary>
    /// Spēles klase, kura nodrošina ģenētiskā algoritma funkcionalitāti
    /// </summary>
    class GeneticAlgorithm
    {
        /// <summary>
        /// Ģenētiskā algoritma elementu definēšana
        /// </summary>
        public ArrayList generation = new ArrayList();
        public ArrayList genW = new ArrayList();
        public ArrayList nextGeneration = new ArrayList();
        public ArrayList weightsForSelection = new ArrayList();
        private ArrayList weights1 = new ArrayList();
        private List<int> genF = new List<int>();
        public List<int> fitness = new List<int>();
        public List<int> numbers = new List<int>();
        public List<int> fitnessForSelection = new List<int>();
        private Random rand = new Random();
        private double selection = 0;
        public double[] w = new double[16];

        public GeneticAlgorithm()
        {

        }

        /// <summary>
        /// Metode, kura ģenerē gadījuma skaitli, kuru noapaļo līdz 3 cipariem aiz komata
        /// </summary>
        /// <returns> Gadījuma veida daļskaitlis </returns>
        public double RandomNumber()
        {
            return Math.Round(rand.NextDouble(), 3);
        }

        /// <summary>
        /// Metode nodrošina intervālu piešķiršanu katram indivīdam un to ievietošanu sarakstā, 
        /// ja tie tika izvēlēti
        /// </summary>
        public void IntervalsForSelection()
        {
            for(int i = 0; i < 12; i++)
            {
                selection = RandomNumber();
                if (selection >= 0 && selection <= 0.167)
                {
                    numbers.Add(0);
                    fitnessForSelection.Add(fitness[0]);
                    for(int j = 0; j < 16; j++)
                    {
                        weights1.Add(generation[j]);   
                    }
                    weightsForSelection.AddRange(weights1);
                    weights1.Clear();
                }
                else if (selection > 0.167 && selection <= 0.333)
                {
                    numbers.Add(1);
                    fitnessForSelection.Add(fitness[1]);
                    for (int k = 16; k < 32; k++)
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
                    for (int l = 32; l < 48; l++)
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
                    for (int m = 48; m < 64; m++)
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
                    for (int n = 64; n < 80; n++)
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
                    for (int o = 80; o < 96; o++)
                    {
                        weights1.Add(generation[o]);
                    }
                    weightsForSelection.AddRange(weights1);
                    weights1.Clear();
                }
            }
            //Pēc intervālu noteikšanas, tiek izsaukta selekcijas operatora metode
            Selection();
        }

        /// <summary>
        /// Metode nodrošina selekcijas operatora izpildi, salīdzinot katra indivīda piemērotību 
        /// ar nākamā indivīda piemērotību, ierakstot vērtības sarakstā, kas domāts krustošanai
        /// </summary>
        public void Selection()
        {
            for (int j = 0; j < 12; j = j + 2)
            {
                if (fitnessForSelection[j] >= fitnessForSelection[j + 1])
                {
                    for (int l = j * 16; l < j * 16 + 16; l++)
                    {
                        weights1.Add(weightsForSelection[l]);
                    }
                    genW.AddRange(weights1);
                    genF.Add(fitnessForSelection[j]);
                    weights1.Clear();
                }
                else
                {
                    for (int l = (j + 1) * 16; l < (j + 1) * 16 + 16; l++)
                    {
                        weights1.Add(weightsForSelection[l]);
                    }
                    genW.AddRange(weights1);
                    genF.Add(fitnessForSelection[j + 1]);
                    weights1.Clear();
                }
            }
        }

        /// <summary>
        /// Nosaka vietu, kur notiks krustošana
        /// </summary>
        /// <returns> Vesels skaitlis no 0 līdz 5 </returns>
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

        /// <summary>
        /// Metode nodrošina krustošanas operatora darbības katram pārim
        /// </summary>
        public void Crossover()
        {
            object gen;
            int j = CrossoverLocation();
            int k = CrossoverLocation() + 6;
            for(int i = j; i <= k; i++)
            {
                gen = genW[i];
                genW[i] = genW[i + 16];
                genW[i + 16] = gen;
            }

            j = CrossoverLocation();
            k = CrossoverLocation() + 6;
            for (int l = j + 32; l <= k + 32; l++)
            {
                gen = genW[l];
                genW[l] = genW[l + 16];
                genW[l + 16] = gen;
            }
            
            j = CrossoverLocation();
            k = CrossoverLocation() + 6;
            for (int m = j + 64; m <= k + 64; m++)
            {
                gen = genW[m];
                genW[m] = genW[m + 16];
                genW[m + 16] = gen;
            }
        }

        /// <summary>
        /// Metode, kura pievieno visus (gan sākotnējos indivīdus, gan pēcnācējus) indivīdus sarakstā
        /// </summary>
        public void AllToList()
        {
            generation.AddRange(genW);
        }

        /// <summary>
        /// Metode, kura nodrošina mutācijas selekcijas un mutācijas darbības
        /// </summary>
        public void Mutation()
        {
            for(int i = 0; i < 12; i++)
            {
                double k = RandomNumber();
                if(k <= 0.1)
                {
                    int loc = CrossoverLocation();
                    generation[i * 16 + loc] = RandomNumber();
                }
            }
        }

        /// <summary>
        /// Metode, kura nodrošina selekciju, lai izveidotu nākamo paaudzi
        /// </summary>
        public void SelectionToNextGeneration()
        {
            for (int i = 0; i < 6; i++)
            {
                selection = RandomNumber();
                if (selection >= 0 && selection <= 0.083)
                {
                    for (int j = 0; j < 16; j++)
                    {
                        weights1.Add(generation[j]);
                    }
                    nextGeneration.AddRange(weights1);
                    weights1.Clear();
                }
                else if (selection > 0.083 && selection <= 0.167)
                {
                    for (int j = 16; j < 32; j++)
                    {
                        weights1.Add(generation[j]);
                    }
                    nextGeneration.AddRange(weights1);
                    weights1.Clear();
                }
                else if (selection > 0.167 && selection <= 0.25)
                {
                    for (int j = 32; j < 48; j++)
                    {
                        weights1.Add(generation[j]);
                    }
                    nextGeneration.AddRange(weights1);
                    weights1.Clear();
                }
                else if (selection > 0.25 && selection <= 0.333)
                {
                    for (int j = 48; j < 64; j++)
                    {
                        weights1.Add(generation[j]);
                    }
                    nextGeneration.AddRange(weights1);
                    weights1.Clear();
                }
                else if (selection > 0.333 && selection <= 0.417)
                {
                    for (int j = 64; j < 80; j++)
                    {
                        weights1.Add(generation[j]);
                    }
                    nextGeneration.AddRange(weights1);
                    weights1.Clear();
                }
                else if (selection > 0.417 && selection <= 0.5)
                {
                    for (int j = 80; j < 96; j++)
                    {
                        weights1.Add(generation[j]);
                    }
                    nextGeneration.AddRange(weights1);
                    weights1.Clear();
                }
                if (selection >= 0.5 && selection <= 0.583)
                {
                    for (int j = 96; j < 112; j++)
                    {
                        weights1.Add(generation[j]);
                    }
                    nextGeneration.AddRange(weights1);
                    weights1.Clear();
                }
                else if (selection > 0.583 && selection <= 0.667)
                {
                    for (int j = 112; j < 128; j++)
                    {
                        weights1.Add(generation[j]);
                    }
                    nextGeneration.AddRange(weights1);
                    weights1.Clear();
                }
                else if (selection > 0.667 && selection <= 0.75)
                {
                    for (int j = 128; j < 144; j++)
                    {
                        weights1.Add(generation[j]);
                    }
                    nextGeneration.AddRange(weights1);
                    weights1.Clear();
                }
                else if (selection > 0.75 && selection <= 0.833)
                {
                    for (int j = 144; j < 160; j++)
                    {
                        weights1.Add(generation[j]);
                    }
                    nextGeneration.AddRange(weights1);
                    weights1.Clear();
                }
                else if (selection > 0.833 && selection <= 0.917)
                {
                    for (int j = 160; j < 176; j++)
                    {
                        weights1.Add(generation[j]);
                    }
                    nextGeneration.AddRange(weights1);
                    weights1.Clear();
                }
                else if (selection > 0.917 && selection <= 1)
                {
                    for (int j = 176; j < 192; j++)
                    {
                        weights1.Add(generation[j]);
                    }
                    nextGeneration.AddRange(weights1);
                    weights1.Clear();
                }
            }
        }

        /// <summary>
        /// Pārveido svaru sarakstu pēc krustošanas simbolu virknē
        /// </summary>
        /// <param name="num"> Transporta numurs </param>
        /// <returns> Simbolu virkni ar konkrētā transporlīdzekļa svariem </returns>
        public string PrintWeightsAfterCrossover(int num)
        {
            string weightsString = "";
            for (int j = num * 16; j < (num * 16 + 16); j++)
            {
                weightsString += genW[j].ToString() + "  ";
            }
            return weightsString;
        }

        /// <summary>
        /// Pārveido svaru sarakstu pēc mutācijas simbolu virknē
        /// </summary>
        /// <param name="num"> Transporta numurs </param>
        /// <returns> Simbolu virkni ar konkrētā transporlīdzekļa svariem </returns>
        public string PrintWeightsAfterMutation(int num)
        {
            string weightsString = "";
            for (int j = num * 16; j < (num * 16 + 16); j++)
            {
                weightsString += generation[j].ToString() + "  ";
            }
            return weightsString;
        }

        /// <summary>
        /// Pārveido nākamās paaudzes svaru sarakstu simbolu virknē
        /// </summary>
        /// <param name="num"> Transporta numurs </param>
        /// <returns> Simbolu virkni ar konkrētā transporlīdzekļa svariem </returns>
        public string PrintNextGenerationWeights(int num)
        {
            string weightsString = "";
            for (int j = num * 16; j < (num * 16 + 16); j++)
            {
                weightsString += nextGeneration[j].ToString() + "  ";
            }
            return weightsString;
        }

        /// <summary>
        /// Pārveido svaru sarakstu selekcijai simbolu virknē
        /// </summary>
        /// <param name="num"> Transporta numurs </param>
        /// <returns> Simbolu virkni ar konkrētā transporlīdzekļa svariem </returns>
        public string PrintWeightsForSelection(int num)
        {
            string weightsString = "";
            for (int j = num * 16; j < (num * 16 + 16); j++)
            {
                weightsString += weightsForSelection[j].ToString() + "  ";
            }
            return weightsString;
        }

        /// <summary>
        /// Pārveido piemērotības sarakstu selekcijai simbolu virknē
        /// </summary>
        /// <param name="num"> Transporta numurs </param>
        /// <returns> Simbolu virkni ar konkrētā transporlīdzekļa piemērotību </returns>
        public string PrintFitnessForSelection(int num)
        {
            string fitnessString = fitnessForSelection[num].ToString();
            return fitnessString;
        }

        /// <summary>
        /// Notīra visus klases sarakstus
        /// </summary>
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
