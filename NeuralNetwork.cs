using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace CarRacingGameWithGeneticAlgorithm
{
    class NeuralNetwork
    {
        public double[,] w = new double[6, 13];
        public double input0 { get; set; } = 1;
        public double input1 { get; set; } = 0;
        public double input2 { get; set; } = 0;
        public double input3 { get; set; } = 0;
        public double input4 { get; set; } = 0;
        public double hidden0 { get; set; } = 1;
        public double hidden1 { get; set; } = 0;
        public double hidden2 { get; set; } = 0;
        public double output { get; set; } = 0;
        public double output1 { get; set; } = 0;

        public int iterationNumber;
        public ArrayList nextGeneration = new ArrayList();
        public ArrayList weights = new ArrayList();
        //public GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm();

        Random rand = new Random();
        public bool right = false;

        public NeuralNetwork()
        {
            InitializeNeuralNetwork();
        }

        private void InitializeNeuralNetwork()
        {
            //InitializeWeights();
            //InitializeHiddenLayer();
            //InitializeOutputData();
        }

        public void GetIterationNumber(int iterNum)
        {
            iterationNumber = iterNum;
        }

        public void GetNextIterationWeights(ArrayList nextGen)
        {
            nextGeneration.AddRange(nextGen);
        }

        public void InitializeWeights()
        {
            if(iterationNumber == 0)
            {
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 13; j++)
                    {
                        w[i, j] = rand.NextDouble();
                        w[i, j] = Math.Round(w[i, j], 3);
                        //weights.Add(w[i, j]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 13; j++)
                    {
                        double [] n = (double[])nextGeneration.ToArray(typeof(double));
                        w[i, j] = n[i * 13 + j];
                        //weights.Add(w[i, j]);
                    }
                }
            }
        }

        public void setInputData(double x1, double x2, double x3, double x4)
        {
            input1 = x1;
            input2 = x2;
            input3 = x3;
            input4 = x4;
        }

        public void InitializeHiddenLayer(int j)
        {
            double s1, s2;
            double[] n = (double[])weights.ToArray(typeof(double));
            s1 = input0 * n[j * 13 + 0] + input1 * n[j * 13 + 2] + input2 * n[j * 13 + 4] + input3 * n[j * 13 + 6] + input4 * n[j * 13 + 8];
            s2 = input0 * n[j * 13 + 1] + input1 * n[j * 13 + 3] + input2 * n[j * 13 + 5] + input3 * n[j * 13 + 7] + input4 * n[j * 13 + 9];
            hidden1 = 1 / (1 + Math.Exp(-s1/1000));
            hidden2 = 1 / (1 + Math.Exp(-s2/1000));
        }

        public void InitializeOutputData(int j)
        {
            double s3;
            double[] n = (double[])weights.ToArray(typeof(double));
            s3 = hidden0 * n[j * 13 + 10] + hidden1 * n[j * 13 + 11] + hidden2 * n[j * 13 + 12];
            output = 1 / (1 + Math.Exp(-s3)); ;

            if(output < 0.75)
            {
                right = true;
            }
            else
            {
                right = false;
            }
           
        }

        public string PrintHiddenLayer(int i)
        {
            string hidden = "";
            if (i == 1)
            {
                hidden = "Hidden1 = " + hidden1.ToString();
            }
            else
            {
                hidden = "Hidden2 = " + hidden2.ToString();
            }
            return hidden;
        }
        public string PrintOutputLayer()
        {
            string outputS = "Output = " + output.ToString();
            return outputS;
        }

        public void NextIteration()
        {
            weights.Clear();

        }


        
    }
}
