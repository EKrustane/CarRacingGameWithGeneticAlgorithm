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
        public double[,] w = new double[6, 12];
        public int input0 { get; set; } = 0;
        public int input1 { get; set; } = 0;
        public int input2 { get; set; } = 0;
        public int input3 { get; set; } = 0;
        public double hidden0 { get; set; } = 0;
        public double hidden1 { get; set; } = 0;
        public double output0 { get; set; } = 0;
        public double output1 { get; set; } = 0;
        public int iterationNumber;
        public ArrayList nextGeneration = new ArrayList();
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
                    for (int j = 0; j < 12; j++)
                    {
                        w[i, j] = rand.NextDouble();
                        w[i, j] = Math.Round(w[i, j], 3);

                    }
                }
            }
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 12; j++)
                    {
                        double [] n = (double[])nextGeneration.ToArray(typeof(double));
                        w[i, j] = n[i * 12 + j];
                    }
                }
            }
        }

        public void setInputData(int x0, int x1, int x2, int x3)
        {
            input0 = x0;
            input1 = x1;
            input2 = x2;
            input3 = x3;
        }

        public void InitializeHiddenLayer(int j)
        {
            double s1, s2;
            s1 = input0 * w[j, 0] + input1 * w[j, 2] + input2 * w[j, 4] + input3 * w[j, 6];
            s2 = input0 * w[j, 1] + input1 * w[j,  3] + input2 * w[j, 5] + input3 * w[j, 7];
            hidden0 = 1 / (1 + Math.Exp(-s1));
            hidden1 = 1 / (1 + Math.Exp(-s2));
        }

        public void InitializeOutputData(int j)
        {
            double s3, s4;
            s3 = hidden0 * w[j, 8] + hidden1 * w[j, 10];
            s4 = hidden0 * w[j, 9] + hidden1 * w[j, 11];
            output0 = 1 / (1 + Math.Exp(-s3)); ;
            output1 = 1 / (1 + Math.Exp(-s4)); ;

            if(output0 >= output1)
            {
                right = true;
            }
            else
            {
                right = false;
            }
           
        }


        
    }
}
