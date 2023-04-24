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
    public partial class CarRacingGame : Form
    {
        private PictureBox startPicture = new PictureBox();
        private PictureBox endPicture = new PictureBox();
        private Area area = new Area();
        private Vehicle vehicle;
        private List<Vehicle> vehicles = new List<Vehicle>();
        public Chromosome chromosome;
        public List<Chromosome> chromosomes = new List<Chromosome>();
        private Score score = new Score();
        private Obstacle obstacle;
        private List<Obstacle> obstacles = new List<Obstacle>();
        private GeneticAlgorithm geneticAlgorithm;
        private NeuralNetwork neuralNetwork;
        private List<NeuralNetwork> neuralNetworks = new List<NeuralNetwork>();
        private Button buttonStart = new Button();
        private Button buttonNew = new Button();
        private Button buttonNext = new Button();
        private Button buttonClose = new Button();
        private bool buttonStartClick = false;
        private Timer ifButtonIsClickedTimer = null;
        private Timer mainTimer = null;
        private Timer scoreTimer = null;
        private List<Timer> scoreTimers = new List<Timer>();
        private Timer obstacleTimer = null;
        private Timer neuralNetworkTimer = null;
        private int obstacleCount = 1;
        private Random rand1 = new Random();
        private Random rand2 = new Random();
        private int vehicleNumber;
        public int iterationNumber;
        private List<int> vehicleNumbers = new List<int>();
        private Label endText1 = new Label();
        private Label endText2 = new Label();
        private Label endText3 = new Label();
        private Label endText4 = new Label();
        private Label endText5 = new Label();
        private Label endText6 = new Label();
        //public ArrayList weights = new ArrayList();
        StreamWriter sw = new StreamWriter("C:\\Users\\Ermīne\\source\\repos\\CarRacingGameWithGeneticAlgorithm\\Rezults.txt");
        public int check = 0;

        public CarRacingGame()
        {
            InitializeComponent();
            KeyPreview = true;
            InitializeGame();
        }

        private void InitializeGame()
        {
            iterationNumber = 0;
            //adjust game form size
            this.Size = new Size(615, 825);

            //add start picture
            InitializeStartPicture();

            //add game elements, when Start button is clicked
            InitializeIfButtonIsClickedTimer();

        }

        private void InitializeStartPicture()
        {
            this.Controls.Add(startPicture);
            startPicture.Visible = true;
            startPicture.Location = new Point(0, 0);
            startPicture.Size = new Size(600, 800);
            startPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            startPicture.BringToFront();
            startPicture.Image = (Image)Properties.Resources.ResourceManager.GetObject("start_picture");
            ButtonStart();
        }
        private void ButtonStart()
        {
            buttonStart.Parent = startPicture;
            buttonStart.Size = new Size(160, 50);
            buttonStart.Location = new Point(220, 640);
            buttonStart.BackColor = Color.Black;
            buttonStart.ForeColor = Color.White;
            buttonStart.Text = "START";
            buttonStart.Font = new Font("Impact", 22, FontStyle.Bold);
            buttonStart.Visible = true;
            buttonStart.BringToFront();
            buttonStart.Click += buttonStart_Click;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            buttonStartClick = true;
        }

        private void VisibleFalse()
        {
            startPicture.Visible = false;
            buttonStart.Visible = false;
        }

        private void InitializeIfButtonIsClickedTimer()
        {

            ifButtonIsClickedTimer = new Timer();
            ifButtonIsClickedTimer.Tick += IfButtonIsClickedTimer_Tick;
            ifButtonIsClickedTimer.Interval = 1;
            ifButtonIsClickedTimer.Start();
        }

        private void IfButtonIsClickedTimer_Tick(object sender, EventArgs e)
        {
            if (buttonStartClick)
            {
                ifButtonIsClickedTimer.Stop();
                VisibleFalse();
                AddGameElements();

                InitializeMainTimer();
                InitializeObstacleTimer();
            }

        }
        
        private void AddGameElements()
        {
            check = 0;
            AddArea();
            AddVehicle();
            AddScore();
            AddObstacle();
            AddChromosomes();
            AddListOfVehicleNumbers();
            AddNeuralNetwork();
            InitializeScoreTimer();
        }
        private void InitializeMainTimer()
        {
            mainTimer = new Timer();
            mainTimer.Tick += MainTimer_Tick;
            mainTimer.Interval = 60;
            mainTimer.Start();
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            MoveObstacle();
            VehicleObstacleCollision();
        }

        private void InitializeScoreTimer()
        {
            for (int i = 0; i < 6; i++)
            {
                scoreTimer = new Timer();
                scoreTimers.Add(scoreTimer);
                scoreTimers[i].Tick += ScoreTimer_Tick;
                scoreTimers[i].Interval = 100;
                scoreTimers[i].Start();
            }
        }

        private void ScoreTimer_Tick(object sender, EventArgs e)
        {
            UpdateScore();
        }

        private void InitializeObstacleTimer()
        {
            obstacleTimer = new Timer();
            obstacleTimer.Tick += obstacleTimer_Tick;
            obstacleTimer.Interval = 2000;
            obstacleTimer.Start();
        }

        private void obstacleTimer_Tick(object sender, EventArgs e)
        {
            AddObstacle();
        }

        private void InitializeNeuralNetworkTimer()
        {
            neuralNetworkTimer = new Timer();
            neuralNetworkTimer.Tick += neuralNetworkTimer_Tick;
            neuralNetworkTimer.Interval = 500;
            neuralNetworkTimer.Start();

        }

        private void neuralNetworkTimer_Tick(object sender, EventArgs e)
        {
            MoveVehicle();
        }

        private void AddArea()
        {
            this.Controls.Add(area);
            area.Visible = true;
        }

        private void AddVehicle()
        {
            for (int i = 0; i < 6; i++)
            {
                vehicle = new Vehicle();
                vehicles.Add(vehicle);
                this.Controls.Add(vehicles[i]);
                vehicles[i].Location = new Point(rand1.Next(100, 400), 600);
                vehicles[i].Parent = area;
                vehicles[i].BringToFront();
                vehicle.Visible = true;

            }
        }

        private void AddObstacle()
        {
            for (int i = 0; i < obstacleCount; i++)
            {
                obstacle = new Obstacle();
                obstacle.Location = new Point(rand2.Next(100, 500), 100);
                obstacles.Add(obstacle);
                this.Controls.Add(obstacle);
                obstacle.Parent = area;
                obstacle.BringToFront();
                obstacle.Visible = true;
            }
        }


        private void AddScore()
        {
            this.Controls.Add(score);
            score.Parent = area;
            score.Visible = true;
            score.BringToFront();
        }

        private void AddGeneticAlgorithm()
        {
            geneticAlgorithm = new GeneticAlgorithm();
        }

        private void AddChromosomes()
        {
            for (int i = 0; i < 6; i++)
            {
                chromosome = new Chromosome();
                chromosomes.Add(chromosome);
            }
        }

        private void AddListOfVehicleNumbers()
        {
            for (int i = 0; i < 6; i++)
            {
                vehicleNumber = new int();
                vehicleNumbers.Add(vehicleNumber);
            }
        }
        private void AddNeuralNetwork()
        {
            if (iterationNumber == 0)
            {
                AddGeneticAlgorithm();
            }
            for (int i = 0; i < 6; i++)
            {
                neuralNetwork = new NeuralNetwork();
                neuralNetworks.Add(neuralNetwork);
                
                
            }
            neuralNetwork.nextGeneration.AddRange(geneticAlgorithm.nextGeneration);
            neuralNetwork.GetIterationNumber(iterationNumber);
            neuralNetwork.InitializeWeights();
            
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    chromosome.weights.Add(neuralNetwork.w[i, j]);
                    geneticAlgorithm.generation.Add(neuralNetwork.w[i, j]);
                }
                
            }
            
            InitializeNeuralNetworkTimer();
        }

        private void MoveVehicle()
        {
            double x0 = 0, x1 = 0, x2 = 0, x3 = 0;

            for (int i = 0; i < 6; i++)
            {
                vehicle = vehicles[i];
                neuralNetwork = neuralNetworks[i];
               
                for (int obstacleCounter = 0; obstacleCounter < obstacles.Count; obstacleCounter++)
                {
                    obstacle = obstacles[obstacleCounter];
                    x0 = area.Width - 40 - (vehicle.Location.X + vehicle.Width);
                    x1 = vehicle.Location.X - 40;
                    x2 = vehicle.Location.Y - (obstacle.Location.Y + obstacle.Height);
                    x3 = vehicle.Location.X - (obstacle.Location.X + obstacle.Width);
                    if (x3 < 0)
                    {
                        x3 = obstacle.Location.X - (vehicle.Location.X + vehicle.Width);
                        if (x3 < 0)
                        {
                            x3 = 0;
                        }
                    }

                    neuralNetwork.setInputData(x0, x1, x2, x3);
                    neuralNetwork.weights.AddRange(geneticAlgorithm.generation);
                    neuralNetwork.InitializeHiddenLayer(i);
                    neuralNetwork.InitializeOutputData(i);

                    switch (neuralNetwork.right)
                    {
                        case true:
                            vehicle.HorizontalControl = vehicle.Step;
                            vehicle.Left += vehicle.HorizontalControl;
                            VehicleBorderCollision();
                            break;
                        case false:
                            vehicle.HorizontalControl = -vehicle.Step;
                            vehicle.Left += vehicle.HorizontalControl;
                            VehicleBorderCollision();
                            break;
                    }
                }
            }
        }
        private void UpdateScore()
        {
            score.UpdatingScore(1);
        }

        private void MoveObstacle()
        {
            foreach (var obstacle in obstacles)
            {
                obstacle.VerticalControl = obstacle.Step;
                obstacle.Top += obstacle.VerticalControl;
            }
        }

        private void VehicleBorderCollision()
        {
            for (int i = 0; i < 6; i++)
            {
                vehicle = vehicles[i];
                if (vehicle.Visible)
                {
                    if (vehicle.Left + vehicle.Width >= (area.Width - 50))
                    {
                        GameOverForOneVehicle(i);
                    }
                    if (vehicle.Left <= (area.Left + 50))
                    {
                        GameOverForOneVehicle(i);
                    }
                }
            }
        }

        private void VehicleObstacleCollision()
        {
            for (int i = 0; i < 6; i++)
            {
                vehicle = vehicles[i];
                if (vehicle.Visible)
                {
                    for (int obstacleCounter = 0; obstacleCounter < obstacles.Count; obstacleCounter++)
                    {
                        obstacle = obstacles[obstacleCounter];
                        if (obstacle.Bounds.IntersectsWith(vehicle.Bounds))
                        {
                            GameOverForOneVehicle(i);
                        }
                    }
                }


            }
        }

        private void GameOverForOneVehicle(int i)
        {
            vehicles[i].Visible = false;
            vehicleNumbers.Add(i);
            geneticAlgorithm.nextGeneration.Clear();
            neuralNetwork.nextGeneration.Clear();


            if (i == 0)
            {
                scoreTimers[0].Stop();
                chromosomes[0].Fitness = score.ScoreNumber;
                chromosome.AddFitnessToList(chromosomes[0].Fitness);
            }

            if (i == 1)
            {
                scoreTimers[1].Stop();
                chromosomes[1].Fitness = score.ScoreNumber;
                chromosome.AddFitnessToList(chromosomes[1].Fitness);
            }

            if (i == 2)
            {
                scoreTimers[2].Stop();
                chromosomes[2].Fitness = score.ScoreNumber;
                chromosome.AddFitnessToList(chromosomes[2].Fitness);
            }

            if (i == 3)
            {
                scoreTimers[3].Stop();
                chromosomes[3].Fitness = score.ScoreNumber;
                chromosomes[3].AddFitnessToList(chromosomes[3].Fitness);
            }

            if (i == 4)
            {
                scoreTimers[4].Stop();
                chromosomes[4].Fitness = score.ScoreNumber;
                chromosome.AddFitnessToList(chromosomes[4].Fitness);
            }

            if (i == 5)
            {
                scoreTimers[5].Stop();
                chromosomes[5].Fitness = score.ScoreNumber;
                chromosome.AddFitnessToList(chromosomes[5].Fitness);
            }

            if (vehicleNumbers.Contains(0) && vehicleNumbers.Contains(1) &&
                vehicleNumbers.Contains(2) && vehicleNumbers.Contains(3) &&
                vehicleNumbers.Contains(4) && vehicleNumbers.Contains(5))
            {
                GameOver();
            }
        }

        public void GameOver()
        {
            InitializeEndPicture();
        }

        private void InitializeEndPicture()
        {
            this.Controls.Add(endPicture);
            endPicture.Image = (Image)Properties.Resources.ResourceManager.GetObject("end_picture");
            endPicture.Visible = true;
            endPicture.BringToFront();
            endPicture.Location = new Point(0, 0);
            endPicture.Size = area.Size;
            endPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            EndText();
            ButtonNew();
            ButtonNext();
            ButtonClose();
        }

        private void EndText()
        {
            this.Controls.Add(endText1);
            this.Controls.Add(endText2);
            this.Controls.Add(endText3);
            this.Controls.Add(endText4);
            this.Controls.Add(endText5);
            this.Controls.Add(endText6);
            endText1.Parent = endPicture;
            endText2.Parent = endPicture;
            endText3.Parent = endPicture;
            endText4.Parent = endPicture;
            endText5.Parent = endPicture;
            endText6.Parent = endPicture;
            endText1.Visible = true;
            endText2.Visible = true;
            endText3.Visible = true;
            endText4.Visible = true;
            endText5.Visible = true;
            endText6.Visible = true;
            endText1.Size = new Size(420, 35);
            endText2.Size = new Size(420, 25);
            endText3.Size = new Size(420, 17);
            endText4.Size = new Size(420, 5);
            endText5.Size = new Size(420, 70);
            endText6.Size = new Size(420, 200);
            endText1.Location = new Point(90, 130);
            endText2.Location = new Point(90, 165);
            endText3.Location = new Point(90, 205);
            endText4.Location = new Point(90, 222);
            endText5.Location = new Point(90, 227);
            endText6.Location = new Point(90, 307);
            endText1.BackColor = Color.Transparent;
            endText2.BackColor = Color.Transparent;
            endText3.BackColor = Color.Transparent;
            endText4.BackColor = Color.Transparent;
            endText5.BackColor = Color.Transparent;
            endText6.BackColor = Color.Transparent;
            endText1.Font = new Font("Impact", 20);
            endText2.Font = new Font("Impact", 15);
            endText3.Font = new Font("Impact", 8);
            endText4.Font = new Font("Impact", 3);
            endText5.Font = new Font("Impact", 6);
            endText6.Font = new Font("Impact", 12);
            endText1.Text = iterationNumber + ". iteration\r\n";
            endText2.Text = "Current population:\r\n";
            endText3.Text = "Nr.         w0        w1        w2        w3        w4        " +
                "w5        w6        w7        w8        w9        w10        w11   Fitness\r\n";
            endText4.Text = "_______________________________________________________________" +
                "_________________________________________________________________________" +
                "_______________________________________________________________________\r\n";
            endText5.Text =
                chromosome.PrintWeightsForChromosome(0) + "     " + chromosomes[0].Fitness + "\r\n" +
                chromosome.PrintWeightsForChromosome(1) + "     " + chromosomes[1].Fitness + "\r\n" +
                chromosome.PrintWeightsForChromosome(2) + "     " + chromosomes[2].Fitness + "\r\n" +
                chromosome.PrintWeightsForChromosome(3) + "     " + chromosomes[3].Fitness + "\r\n" +
                chromosome.PrintWeightsForChromosome(4) + "     " + chromosomes[4].Fitness + "\r\n" +
                chromosome.PrintWeightsForChromosome(5) + "     " + chromosomes[5].Fitness + "\r\n";
            endText6.Text = "Max fitness: " + chromosome.MaxFitness() + "\r\n" + "Min fitness: " + 
                chromosome.MinFitness() + "\r\n" + "Average fitness: " + chromosome.AverageFitness();

            for (int k = 0; k < 6; k++)
            {
                geneticAlgorithm.fitness.Add(chromosomes[k].Fitness);
            }
            geneticAlgorithm.IntervalsForSelection();
            geneticAlgorithm.Crossover();
            geneticAlgorithm.AllToList();
            geneticAlgorithm.Mutation();
            geneticAlgorithm.SelectionToNextGeneration();

            WriteInFile();

        }

        private void ButtonNew()
        {
            buttonNew.Parent = endPicture;
            buttonNew.Size = new Size(100, 40);
            buttonNew.Location = new Point(250, 730);
            buttonNew.Visible = true;
            buttonNew.BackColor = Color.Black;
            buttonNew.ForeColor = Color.White;
            buttonNew.Text = "New";
            buttonNew.Font = new Font("Impact", 20, FontStyle.Bold);
            buttonNew.BringToFront();
            buttonNew.Click += buttonNew_Click;

        }

        private void ButtonNext()
        {
            buttonNext.Parent = endPicture;
            buttonNext.Size = new Size(100, 40);
            buttonNext.Location = new Point(120, 730);
            buttonNext.Visible = true;
            buttonNext.BackColor = Color.Black;
            buttonNext.ForeColor = Color.White;
            buttonNext.Text = "Next";
            buttonNext.Font = new Font("Impact", 20, FontStyle.Bold);
            buttonNext.BringToFront();
            buttonNext.Click += buttonNext_Click;
            check += 1;
        }

        private void ButtonClose()
        {
            buttonClose.Parent = endPicture;
            buttonClose.Size = new Size(100, 40);
            buttonClose.Location = new Point(380, 730);
            buttonClose.Visible = true;
            buttonClose.BackColor = Color.Black;
            buttonClose.ForeColor = Color.White;
            buttonClose.Text = "Close";
            buttonClose.Font = new Font("Impact", 20, FontStyle.Bold);
            buttonClose.BringToFront();
            buttonClose.Click += buttonClose_Click;
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            sw.Close();
            neuralNetworkTimer.Stop();
            obstacleTimer.Stop();
            mainTimer.Stop();
            RestartGame();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (check > 0)
            {
                iterationNumber++;
                NextIteration();
                obstacle.NextIteration();
                vehicle.NextIteration();
                score.NextIteration();
                chromosome.NextIteration();
                geneticAlgorithm.NextIteration();
                neuralNetwork.NextIteration();
                AddGameElements();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            sw.Close();
            neuralNetworkTimer.Stop();
            obstacleTimer.Stop();
            mainTimer.Stop();
            this.Close();
        }

        private void RestartGame()
        {
            System.Diagnostics.Process.Start(Application.ExecutablePath);
            this.Close();
        }

        private void NextIteration()
        {
            endPicture.Visible = false;
            buttonNew.Visible = false;
            buttonNext.Visible = false;
            buttonClose.Visible = false;
            endText1.Visible = false;
            endText2.Visible = false;
            endText3.Visible = false;
            endText4.Visible = false;
            endText5.Visible = false;
            endText6.Visible = false;
            area.Visible = false;
            for(int i = 0; i < vehicles.Count; i++)
            {
                this.Controls.Remove(vehicles[i]);
                vehicles[i].Visible = false;
                vehicles[i].Dispose();
            }
            for (int j = 0; j < obstacles.Count; j++)
            {
                this.Controls.Remove(obstacles[j]);
                obstacles[j].Visible = false;
                obstacles[j].Dispose();
            }
            this.Controls.Remove(area);
            this.Controls.Remove(score);
            vehicles.Clear();
            vehicleNumbers.Clear();
            chromosomes.Clear();
            scoreTimers.Clear();
            //neuralNetworks.Clear();
            obstacles.Clear();
            neuralNetworkTimer.Stop();
        }

        private void WriteInFile()
        {
            try
            {
                sw.WriteLine(iterationNumber.ToString() + ". iteration");
                sw.WriteLine();
                sw.WriteLine("Population:");
                string weightsString;
                for (int i = 0; i < 6; i++)
                {
                    weightsString = "";
                    for (int j = i * 12; j < (i * 12 + 12); j++)
                    {
                        weightsString += geneticAlgorithm.generation[j].ToString() + "  ";
                    }
                    sw.WriteLine(weightsString + "  " + chromosomes[i].Fitness);
                }

                sw.WriteLine("Max fitness: " + chromosome.MaxFitness().ToString());
                sw.WriteLine("Min fitness: " + chromosome.MinFitness().ToString());
                sw.WriteLine("Average fitness: " + chromosome.AverageFitness().ToString());
                sw.WriteLine();
                sw.WriteLine("Weights and fitness for selection:");
                sw.WriteLine(geneticAlgorithm.PrintWeightsForSelection(0) + "  " + geneticAlgorithm.PrintFitnessForSelection(0));
                sw.WriteLine(geneticAlgorithm.PrintWeightsForSelection(1) + "  " + geneticAlgorithm.PrintFitnessForSelection(1));
                sw.WriteLine(geneticAlgorithm.PrintWeightsForSelection(2) + "  " + geneticAlgorithm.PrintFitnessForSelection(2));
                sw.WriteLine(geneticAlgorithm.PrintWeightsForSelection(3) + "  " + geneticAlgorithm.PrintFitnessForSelection(3));
                sw.WriteLine(geneticAlgorithm.PrintWeightsForSelection(4) + "  " + geneticAlgorithm.PrintFitnessForSelection(4));
                sw.WriteLine(geneticAlgorithm.PrintWeightsForSelection(5) + "  " + geneticAlgorithm.PrintFitnessForSelection(5));
                sw.WriteLine(geneticAlgorithm.PrintWeightsForSelection(6) + "  " + geneticAlgorithm.PrintFitnessForSelection(6));
                sw.WriteLine(geneticAlgorithm.PrintWeightsForSelection(7) + "  " + geneticAlgorithm.PrintFitnessForSelection(7));
                sw.WriteLine(geneticAlgorithm.PrintWeightsForSelection(8) + "  " + geneticAlgorithm.PrintFitnessForSelection(8));
                sw.WriteLine(geneticAlgorithm.PrintWeightsForSelection(9) + "  " + geneticAlgorithm.PrintFitnessForSelection(9));
                sw.WriteLine(geneticAlgorithm.PrintWeightsForSelection(10) + "  " + geneticAlgorithm.PrintFitnessForSelection(10));
                sw.WriteLine(geneticAlgorithm.PrintWeightsForSelection(11) + "  " + geneticAlgorithm.PrintFitnessForSelection(11));
                sw.WriteLine();
                sw.WriteLine("After crossover:");
                sw.WriteLine(geneticAlgorithm.PrintWeightsAfterCrossover(0));
                sw.WriteLine(geneticAlgorithm.PrintWeightsAfterCrossover(1));
                sw.WriteLine(geneticAlgorithm.PrintWeightsAfterCrossover(2));
                sw.WriteLine(geneticAlgorithm.PrintWeightsAfterCrossover(3));
                sw.WriteLine(geneticAlgorithm.PrintWeightsAfterCrossover(4));
                sw.WriteLine(geneticAlgorithm.PrintWeightsAfterCrossover(5));
                sw.WriteLine();
                sw.WriteLine("All chromosomes after mutation:");
                sw.WriteLine(geneticAlgorithm.PrintWeightsAfterMutation(0));
                sw.WriteLine(geneticAlgorithm.PrintWeightsAfterMutation(1));
                sw.WriteLine(geneticAlgorithm.PrintWeightsAfterMutation(2));
                sw.WriteLine(geneticAlgorithm.PrintWeightsAfterMutation(3));
                sw.WriteLine(geneticAlgorithm.PrintWeightsAfterMutation(4));
                sw.WriteLine(geneticAlgorithm.PrintWeightsAfterMutation(5));
                sw.WriteLine(geneticAlgorithm.PrintWeightsAfterMutation(6));
                sw.WriteLine(geneticAlgorithm.PrintWeightsAfterMutation(7));
                sw.WriteLine(geneticAlgorithm.PrintWeightsAfterMutation(8));
                sw.WriteLine(geneticAlgorithm.PrintWeightsAfterMutation(9));
                sw.WriteLine(geneticAlgorithm.PrintWeightsAfterMutation(10));
                sw.WriteLine(geneticAlgorithm.PrintWeightsAfterMutation(11));
                sw.WriteLine();
                sw.WriteLine("Next population:");
                sw.WriteLine(geneticAlgorithm.PrintNextGenerationWeights(0));
                sw.WriteLine(geneticAlgorithm.PrintNextGenerationWeights(1));
                sw.WriteLine(geneticAlgorithm.PrintNextGenerationWeights(2));
                sw.WriteLine(geneticAlgorithm.PrintNextGenerationWeights(3));
                sw.WriteLine(geneticAlgorithm.PrintNextGenerationWeights(4));
                sw.WriteLine(geneticAlgorithm.PrintNextGenerationWeights(5));
                sw.WriteLine();
                sw.WriteLine(neuralNetwork.PrintHiddenLayer(0));
                sw.WriteLine(neuralNetwork.PrintHiddenLayer(1));
                sw.WriteLine(neuralNetwork.PrintOutputLayer(0));
                sw.WriteLine(neuralNetwork.PrintOutputLayer(1));
                sw.WriteLine();

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }
    }



}
