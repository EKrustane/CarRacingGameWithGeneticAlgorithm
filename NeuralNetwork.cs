using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CarRacingGameWithGeneticAlgorithm
{
    class NeuralNetwork
    {
        public double[] w;
        public int input1 { get; set; } = 0;
        public int input2 { get; set; } = 0;
        public int input3 { get; set; } = 0;
        public int input4 { get; set; } = 0;
        public double hidden1 { get; set; } = 0;
        public double hidden2 { get; set; } = 0;
        public double output1 { get; set; } = 0;
        public double output2 { get; set; } = 0;

        Random rand = new Random();

        public NeuralNetwork()
        {
            InitializeNeuralNetwork();
        }

        private void InitializeNeuralNetwork()
        {
            InitializeWeights();
            InitializeHiddenLayer();
            InitializeOutputData();
        }

        public void InitializeWeights()
        {
            for (int i = 0; i < w.Length; i++)
            {
                w[i] = rand.NextDouble() * (-0.5);
            }
        }

        public void setInputData(int x1, int x2, int x3, int x4)
        {
            input1 = x1;
            input2 = x2;
            input3 = x3;
            input4 = x4;
        }

        private void InitializeHiddenLayer()
        {
            double s1, s2;
            s1 = input1 * w[0] + input2 * w[2] + input3 * w[4] + input4 * w [6];
            s2 = input1 * w[1] + input2 * w[3] + input3 * w[5] + input4 * w[7];
            hidden1 = 1 / (1 + Math.Exp(-s1));
            hidden2 = 1 / (1 + Math.Exp(-s2));
        }

        private void InitializeOutputData()
        {
            double s3, s4;
            s3 = hidden1 * w[8] + hidden2 * w[10];
            s4 = hidden1 * w[9] + hidden2 * w[11];
            output1 = 1 / (1 + Math.Exp(-s3)); ;
            output2 = 1 / (1 + Math.Exp(-s4)); ;
        }

        
    }
}
