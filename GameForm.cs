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
        private GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm();
        private NeuralNetwork neuralNetwork;
        //private List<NeuralNetwork> neuralNetworks = new List<NeuralNetwork>();
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
        private Timer gameOverTimer = null;
        private int obstacleCount = 1;
        private Random rand1 = new Random();
        private Random rand2 = new Random();
        private int vehicleNumber;
        private int iterationNumber = 0;
        private List<int> vehicleNumbers = new List<int>();
        private Label endText1 = new Label();
        private Label endText2 = new Label();
        private Label endText3 = new Label();
        private Label endText4 = new Label();
        private Label endText5 = new Label();
        private Label endText6 = new Label();
        //public ArrayList weights = new ArrayList();


        public CarRacingGame()
        {
            InitializeComponent();
            KeyPreview = true;
            InitializeGame();
            //InitializeMainTimer();
        }

        private void InitializeGame()
        {
            //adjust game form size
            this.Size = new Size(615, 825);

            //add start picture
            InitializeStartPicture();

            //add game elements, when Start button is clicked
            InitializeIfButtonIsClickedTimer();

        }

        private void WriteInFile()
        {
            try
            {
                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter("C:\\Users\\Ermīne\\source\\repos\\CarRacingGameWithGeneticAlgorithm\\Rezults.txt");
                //Write a line of text
                sw.WriteLine(chromosomes[0].Fitness);
                //Write a second line of text
                //Close the file
                sw.Close();
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

            }

        }

        private void AddGameElements()
        {
            //this.KeyDown += new KeyEventHandler(this.Game_KeyDown);

            AddArea();
            AddVehicle();
            AddScore();
            AddObstacle();
            AddGeneticAlgorithm();
            AddChromosomes();
            AddListOfVehicleNumbers();

            InitializeMainTimer();
            InitializeScoreTimer();
            InitializeObstacleTimer();
            InitializeNeuralNetworkTimer();


        }
        private void InitializeMainTimer()
        {
            mainTimer = new Timer();
            mainTimer.Tick += MainTimer_Tick;
            mainTimer.Interval = 40;
            mainTimer.Start();
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {

            MoveObstacle();
            //VehicleBorderCollision();
            VehicleObstacleCollision();
            //UpdateScore();
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
            obstacleTimer.Interval = 3000;
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
            AddNeuralNetwork();
        }

        private void AddArea()
        {
            this.Controls.Add(area);
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
                //InitializeScoreTimer(i);

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
            }
        }


        private void AddScore()
        {
            this.Controls.Add(score);
            score.Parent = area;
            score.BringToFront();
        }

        private void AddGeneticAlgorithm()
        {

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
            int x0 = 0, x1 = 0, x2 = 0, x3 = 0;
            for (int i = 0; i < 6; i++)
            {
                vehicle = vehicles[i];
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
                    neuralNetwork = new NeuralNetwork();
                    //neuralNetworks.Add(neuralNetwork);
                    //neuralNetwork.InitializeWeights();
                    neuralNetwork.setInputData(x0, x1, x2, x3);
                    //weights.AddRange(neuralNetwork.w);

                    switch (neuralNetwork.right)
                    {
                        case true:
                            vehicle.HorizontalControl = vehicle.Step;
                            vehicle.Left += vehicle.HorizontalControl;
                            VehicleBorderCollision();
                            //VehicleObstacleCollision();
                            break;
                        case false:
                            vehicle.HorizontalControl = -vehicle.Step;
                            vehicle.Left += vehicle.HorizontalControl;
                            VehicleBorderCollision();
                            //VehicleObstacleCollision();
                            break;
                    }
                }


            }
        }

        /*private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    vehicle.HorizontalControl = vehicle.Step;
                    vehicle.Left += vehicle.HorizontalControl;
                    VehicleBorderCollision();
                    break;
                case Keys.Left:
                    vehicle.HorizontalControl = -vehicle.Step;
                    vehicle.Left += vehicle.HorizontalControl;
                    VehicleBorderCollision();
                    break;
            }
        }*/

        private void MoveVehicles()
        {
            //for (int i = 0; i < 6; i++)
            //{
            //vehicle = vehicles[i];

            //}
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
                    if (vehicle.Left + vehicle.Width >= area.Width - 40)
                    {
                        GameOverForOneVehicle(i);
                        //MessageBox.Show(vehicleCount.ToString());
                    }
                    if (vehicle.Left <= area.Left + 40)
                    {
                        GameOverForOneVehicle(i);
                        //MessageBox.Show(vehicleCount.ToString());


                    }
                }
                //vehicleCount++;
            }
            //vehicleCount++;
            //InitializeGameOverTimer();
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
                            //MessageBox.Show(vehicleCount.ToString());
                        }
                    }
                }


            }
            //vehicleCount++;
            //InitializeGameOverTimer();
        }

        private void GameOverForOneVehicle(int i)
        {
            vehicles[i].Visible = false;
            //scoreTimers[i].Stop();
            //chromosomes[i].Fitness = score.ScoreNumber;
            vehicleNumbers.Add(i);
            //MessageBox.Show(chromosomes[i].Fitness.ToString());
            switch (i)
            {
                case 0:
                    scoreTimers[0].Stop();
                    chromosomes[0].Fitness = score.ScoreNumber;
                    chromosome.AddFitnessToList(chromosomes[0].Fitness);
                    chromosome.weights.AddRange(neuralNetwork.w);
                    break;
                case 1:
                    scoreTimers[1].Stop();
                    chromosomes[1].Fitness = score.ScoreNumber;
                    chromosome.AddFitnessToList(chromosomes[1].Fitness);
                    chromosome.weights.AddRange(neuralNetwork.w);
                    break;
                case 2:
                    scoreTimers[2].Stop();
                    chromosomes[2].Fitness = score.ScoreNumber;
                    chromosomes[2].AddFitnessToList(chromosomes[2].Fitness);
                    chromosome.weights.AddRange(neuralNetwork.w);
                    break;
                case 3:
                    scoreTimers[3].Stop();
                    chromosomes[3].Fitness = score.ScoreNumber;
                    chromosome.AddFitnessToList(chromosomes[3].Fitness);
                    chromosome.weights.AddRange(neuralNetwork.w);
                    break;
                case 4:
                    scoreTimers[4].Stop();
                    chromosomes[4].Fitness = score.ScoreNumber;
                    chromosome.AddFitnessToList(chromosomes[4].Fitness);
                    chromosome.weights.AddRange(neuralNetwork.w);
                    break;
                case 5:
                    scoreTimers[5].Stop();
                    chromosomes[5].Fitness = score.ScoreNumber;
                    chromosome.AddFitnessToList(chromosomes[5].Fitness);
                    chromosome.weights.AddRange(neuralNetwork.w);
                    break;
            }



            if (vehicleNumbers.Contains(0) && vehicleNumbers.Contains(1) &&
                vehicleNumbers.Contains(2) && vehicleNumbers.Contains(3) &&
                vehicleNumbers.Contains(4) && vehicleNumbers.Contains(5))
            {
                InitializeGameOverTimer();
            }
        }

        private void MessageFitness()
        {
            MessageBox.Show(chromosomes[0].Fitness.ToString() + "," + chromosomes[1].Fitness.ToString() +
                "," + chromosomes[2].Fitness.ToString() + "," + chromosomes[3].Fitness.ToString() +
                "," + chromosomes[4].Fitness.ToString() + "," + chromosomes[5].Fitness.ToString());

        }

        private void InitializeGameOverTimer()
        {
            gameOverTimer = new Timer();
            gameOverTimer.Tick += GameOverTimer_Tick;
            gameOverTimer.Interval = 10;
            gameOverTimer.Start();
        }

        private void GameOverTimer_Tick(object sender, EventArgs e)
        {
            neuralNetworkTimer.Stop();
            gameOverTimer.Stop();
            obstacleTimer.Stop();
            mainTimer.Stop();
            for (int i = 1; i <= 6; i++)
            {
                //vehicleNumbers.RemoveAt(i);
            }
            GameOver();
        }

        public void GameOver()
        {
            InitializeEndPicture();
            MessageFitness();
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
            iterationNumber++;
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
            RestartGame();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RestartGame()
        {
            
        }

    }



}
