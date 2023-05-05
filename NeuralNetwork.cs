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
    /// <summary>
    /// Spēles klase, kura nodrošina neironu tīkla funkcionalitāti
    /// </summary>
    class NeuralNetwork
    {
        /// <summary>
        /// Neironu tīkla elementu definēšana
        /// </summary>
        public double[,] w = new double[6, 16];
        public double input0 { get; set; } = 1;
        public double input1 { get; set; } = 0;
        public double input2 { get; set; } = 0;
        public double input3 { get; set; } = 0;
        public double input4 { get; set; } = 0;
        public double hidden0 { get; set; } = 1;
        public double hidden1 { get; set; } = 0;
        public double hidden2 { get; set; } = 0;
        public double output0 { get; set; } = 0;
        public double output1 { get; set; } = 0;
        public ArrayList nextGeneration = new ArrayList();
        public ArrayList weights = new ArrayList();
        private Random rand = new Random();
        public bool right = false;
        public int iterationNumber;

        public NeuralNetwork()
        {

        }

        /// <summary>
        /// Metode, kura saņem iterācijas numura vērtību no galvenās klases un piešķir savas klases mainīgajam
        /// </summary>
        /// <param name="iterNum"> Iterācijas numurs </param>
        public void GetIterationNumber(int iterNum)
        {
            iterationNumber = iterNum;
        }

        /// <summary>
        /// Metode, kura inicializē svarus neironu tīklam
        /// </summary>
        public void InitializeWeights()
        {
            //Notiek pārbaude, vai jāizveido jauni svari ar gadījuma skaitļiem, vai arī tie tiek piešķirti
            //no ģenētiskā algoritma klases
            if(iterationNumber == 0)
            {
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 16; j++)
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
                    for (int j = 0; j < 16; j++)
                    {
                        double [] n = (double[])nextGeneration.ToArray(typeof(double));
                        w[i, j] = n[i * 16 + j];
                    }
                }
            }
        }

        /// <summary>
        /// Metode, kura inicializē neironu tīkla ieejas signālus no galvenās klases
        /// </summary>
        /// <param name="x1"> Attālums no labās spēles malas līdz transportam </param>
        /// <param name="x2"> Attālums no kreisās spēles malas līdz transportam </param>
        /// <param name="x3"> Attālums no šķērslim līdz transportam vertikāli </param>
        /// <param name="x4"> Attālums no šķērslim līdz transportam horizontāli </param>
        public void setInputData(double x1, double x2, double x3, double x4)
        {
            input1 = x1;
            input2 = x2;
            input3 = x3;
            input4 = x4;
        }

        /// <summary>
        /// Metode, kura nodrošina neironu tīkla slēptā slāņa aprēķināšanu
        /// </summary>
        /// <param name="num"> Transporta numurs </param>
        public void InitializeHiddenLayer(int num)
        {
            double s1, s2;
            double[] n = (double[])weights.ToArray(typeof(double));
            s1 = input0 * n[num * 16 + 0] + input1 * n[num * 16 + 2] + input2 * n[num * 16 + 4] + 
                input3 * n[num * 16 + 6] + input4 * n[num * 16 + 8];
            s2 = input0 * n[num * 16 + 1] + input1 * n[num * 16 + 3] + input2 * n[num * 16 + 5] + 
                input3 * n[num * 16 + 7] + input4 * n[num * 16 + 9];
            hidden1 = 1 / (1 + Math.Exp(-s1/1000));
            hidden2 = 1 / (1 + Math.Exp(-s2/1000));
        }

        /// <summary>
        /// Metode, kura nodrošina neironu tīkla izejas slāņa izeju aprēķināšanu
        /// </summary>
        /// <param name="num"> Transporta numurs </param>
        public void InitializeOutputData(int num)
        {
            double s3, s4;
            double[] n = (double[])weights.ToArray(typeof(double));
            s3 = hidden0 * n[num * 16 + 10] + hidden1 * n[num * 16 + 12] + hidden2 * n[num * 16 + 14];
            s4 = hidden0 * n[num * 16 + 11] + hidden1 * n[num * 16 + 13] + hidden2 * n[num * 16 + 15];
            output0 = 1 / (1 + Math.Exp(-s3));
            output1 = 1 / (1 + Math.Exp(-s4));

            //Notiek pārbaude, uz kuru pusi kustēsies transports
            if (output0 <= output1)
            {
                right = true;
            }
            else
            {
                right = false;
            }
        }

        /// <summary>
        /// Metode, kura attīra sarakstu
        /// </summary>
        public void NextIteration()
        {
            weights.Clear();
        }


        
    }
}
