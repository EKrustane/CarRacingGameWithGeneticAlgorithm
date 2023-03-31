using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CarRacingGameWithGeneticAlgorithm
{
    class NeuralNetwork
    {
        public double[] w = new double[12];
        public int input0 { get; set; } = 0;
        public int input1 { get; set; } = 0;
        public int input2 { get; set; } = 0;
        public int input3 { get; set; } = 0;
        public double hidden0 { get; set; } = 0;
        public double hidden1 { get; set; } = 0;
        public double output0 { get; set; } = 0;
        public double output1 { get; set; } = 0;

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

        public void setInputData(int x0, int x1, int x2, int x3)
        {
            input0 = x0;
            input1 = x1;
            input2 = x2;
            input3 = x3;
        }

        private void InitializeHiddenLayer()
        {
            double s1, s2;
            s1 = input0 * w[0] + input1 * w[2] + input2 * w[4] + input3 * w[6];
            s2 = input0 * w[1] + input1 * w[3] + input2 * w[5] + input3 * w[7];
            hidden0 = 1 / (1 + Math.Exp(-s1));
            hidden1 = 1 / (1 + Math.Exp(-s2));
        }

        private void InitializeOutputData()
        {
            double s3, s4;
            s3 = hidden0 * w[8] + hidden1 * w[10];
            s4 = hidden0 * w[9] + hidden1 * w[11];
            output0 = 1 / (1 + Math.Exp(-s3)); ;
            output1 = 1 / (1 + Math.Exp(-s4)); ;
        }

        
    }
}
