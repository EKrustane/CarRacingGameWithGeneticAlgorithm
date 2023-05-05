
/// *************************************************************************************************************
/// Autore: Ermīne Krustāne (RTU DITF IT 3. kurss 1. grupa)
/// Bakalaura darba "Ģenētiskā algoritma izpēte spēles labākās iziešanas stratēģijas meklēsanai" praktiskā daļa.
/// Spēles "Car Racing Game" neirona tīkla optimizēšana, izmantojot ģenētisko algoritmu.
/// *************************************************************************************************************


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
    /// Spēles galvenā klase, kura nodrošina visu funkcionalitāšu kopumu
    /// </summary>
    public partial class CarRacingGame : Form
    {
        /// <summary>
        /// Spēles elementu definēšana
        /// </summary>
        private PictureBox startPicture = new PictureBox();
        private PictureBox endPicture = new PictureBox();
        private Area area = new Area();
        private Vehicle vehicle;
        public Chromosome chromosome;
        private Score score = new Score();
        private Obstacle obstacle;
        private GeneticAlgorithm geneticAlgorithm;
        private NeuralNetwork neuralNetwork;
        private List<Vehicle> vehicles = new List<Vehicle>();
        public List<Chromosome> chromosomes = new List<Chromosome>();
        private List<Obstacle> obstacles = new List<Obstacle>();
        private List<NeuralNetwork> neuralNetworks = new List<NeuralNetwork>();
        private List<int> vehicleNumbers = new List<int>();
        private Button buttonStart = new Button();
        private Button buttonNew = new Button();
        private Button buttonNext = new Button();
        private Button buttonClose = new Button();
        private Timer ifButtonIsClickedTimer = null;
        private Timer mainTimer = null;
        private Timer obstacleTimer = null;
        private Timer neuralNetworkTimer = null;
        private Timer scoreTimer = null;
        private List<Timer> scoreTimers = new List<Timer>();
        private Random rand1 = new Random();
        private Random rand2 = new Random();
        private Label endText1 = new Label();
        private Label endText2 = new Label();
        private Label endText3 = new Label();
        private Label endText4 = new Label();
        private Label endText5 = new Label();
        private Label endText6 = new Label();
        StreamWriter sw = new StreamWriter("C:\\Users\\Ermīne\\source\\repos\\CarRacingGameWithGeneticAlgorithm\\Results.txt");
        StreamWriter eksp = new StreamWriter("C:\\Users\\Ermīne\\Desktop\\Svarīgi\\Dokumenti\\RTU nodarbības\\3.kurss_2.semestris\\Bakalaura_darbs\\eksperimenti.csv");
        private bool buttonStartClick = false;
        private int obstacleCount = 1;
        private int vehicleNumber;
        public int iterationNumber;
        public int check = 0; 

        public CarRacingGame()
        {
            InitializeComponent();
            InitializeGame();
        }

        /// <summary>
        /// Spēles inicializēšana
        /// </summary>
        private void InitializeGame()
        {
            iterationNumber = 0;
            this.Size = new Size(900, 825);
            InitializeStartPicture();
            InitializeIfButtonIsClickedTimer();
        }

        /// <summary>
        /// Sākuma bildes izveide
        /// </summary>
        private void InitializeStartPicture()
        {
            this.Controls.Add(startPicture);
            startPicture.Visible = true;
            startPicture.Location = new Point(0, 0);
            startPicture.Size = new Size(900, 800);
            startPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            startPicture.BringToFront();
            startPicture.Image = (Image)Properties.Resources.ResourceManager.GetObject("start_picture1");
            ButtonStart();
        }

        /// <summary>
        /// Sākuma pogas izveide
        /// </summary>
        private void ButtonStart()
        {
            buttonStart.Parent = startPicture;
            buttonStart.Size = new Size(160, 50);
            buttonStart.Location = new Point(375, 640);
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

        /// <summary>
        /// IfButtonIsClicked taimera inicializēšana
        /// </summary>
        private void InitializeIfButtonIsClickedTimer()
        {

            ifButtonIsClickedTimer = new Timer();
            ifButtonIsClickedTimer.Tick += IfButtonIsClickedTimer_Tick;
            ifButtonIsClickedTimer.Interval = 1;
            ifButtonIsClickedTimer.Start();
        }

        /// <summary>
        /// Ja sākuma poga ir nospiesta, tad tiek izsaukta spēles elementu pievienošanas metode un taimeri
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IfButtonIsClickedTimer_Tick(object sender, EventArgs e)
        {
            if (buttonStartClick)
            {
                ifButtonIsClickedTimer.Stop();
                startPicture.Visible = false;
                buttonStart.Visible = false;
                AddGameElements();
                InitializeMainTimer();
                InitializeObstacleTimer();
            }

        }

        /// <summary>
        /// Tiek pievienoti spēles elementi
        /// </summary>
        private void AddGameElements()
        {
            check = 0;
            AddArea();
            AddVehicle();
            AddObstacle();
            AddScore();
            AddChromosomes();
            AddListOfVehicleNumbers();
            AddNeuralNetwork();
            InitializeScoreTimer();
        }

        /// <summary>
        /// Taimera mainTimer inicializācija
        /// </summary>
        private void InitializeMainTimer()
        {
            mainTimer = new Timer();
            mainTimer.Tick += MainTimer_Tick;
            mainTimer.Interval = 60;
            mainTimer.Start();
        }

        /// <summary>
        /// Ar katru milisekundi tiks izsauktas šķēršļu pārvietošanās metode un pārbaude, vai mašīna 
        /// ar šķērsli nav saskārušās
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainTimer_Tick(object sender, EventArgs e)
        {
            MoveObstacle();
            VehicleObstacleCollision();
        }

        /// <summary>
        /// scoreTimer inicializācija
        /// </summary>
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

        /// <summary>
        /// Ar katru milisekundi tiks izsaukta rezultātu atjaunošanas metode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScoreTimer_Tick(object sender, EventArgs e)
        {
            UpdateScore();
        }

        /// <summary>
        /// obstacleTimer inicializācija
        /// </summary>
        private void InitializeObstacleTimer()
        {
            obstacleTimer = new Timer();
            obstacleTimer.Tick += obstacleTimer_Tick;
            obstacleTimer.Interval = 2000;
            obstacleTimer.Start();
        }

        /// <summary>
        /// Ar katru milisekundi tiks izsaukta šķēršļu pievienošanās metode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obstacleTimer_Tick(object sender, EventArgs e)
        {
            AddObstacle();
        }

        /// <summary>
        /// neuralNetworkTimer inicializācija
        /// </summary>
        private void InitializeNeuralNetworkTimer()
        {
            neuralNetworkTimer = new Timer();
            neuralNetworkTimer.Tick += neuralNetworkTimer_Tick;
            neuralNetworkTimer.Interval = 500;
            neuralNetworkTimer.Start();
        }

        /// <summary>
        /// Ar katru milisekundi tiks izsaukta transportu pievienošanās metode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuralNetworkTimer_Tick(object sender, EventArgs e)
        {
            MoveVehicle();
        }

        /// <summary>
        /// Tiek pievienota spēles vide
        /// </summary>
        private void AddArea()
        {
            this.Controls.Add(area);
            area.Visible = true;
        }

        /// <summary>
        /// Tiek pievienoti 6 spēles transporti
        /// </summary>
        private void AddVehicle()
        {
            for (int i = 0; i < 6; i++)
            {
                vehicle = new Vehicle();
                vehicles.Add(vehicle);
                this.Controls.Add(vehicles[i]);
                vehicles[i].Location = new Point(rand1.Next(100, 800), 600);
                vehicles[i].Parent = area;
                vehicles[i].BringToFront();
                vehicle.Visible = true;
            }
        }

        /// <summary>
        /// Tiek pievienoti spēles šķēršļi
        /// </summary>
        private void AddObstacle()
        {
            for (int i = 0; i < obstacleCount; i++)
            {
                obstacle = new Obstacle();
                obstacle.Location = new Point(rand2.Next(100, 800), 100);
                obstacles.Add(obstacle);
                this.Controls.Add(obstacle);
                obstacle.Parent = area;
                obstacle.BringToFront();
                obstacle.Visible = true;
            }
        }

        /// <summary>
        /// Tiek pievienota rezultātu etiķete
        /// </summary>
        private void AddScore()
        {
            this.Controls.Add(score);
            score.Parent = area;
            score.Visible = true;
            score.BringToFront();
        }

        /// <summary>
        /// Tiek pievienots ģenētiskais algoritms
        /// </summary>
        private void AddGeneticAlgorithm()
        {
            geneticAlgorithm = new GeneticAlgorithm();
        }

        /// <summary>
        /// Tiek pievienota hromosoma
        /// </summary>
        private void AddChromosomes()
        {
            for (int i = 0; i < 6; i++)
            {
                chromosome = new Chromosome();
                chromosomes.Add(chromosome);
            }
        }

        /// <summary>
        /// Tiek izveidots saraksts 6 elementu lielumā no transporta numuriem
        /// </summary>
        private void AddListOfVehicleNumbers()
        {
            for (int i = 0; i < 6; i++)
            {
                vehicleNumber = new int();
                vehicleNumbers.Add(vehicleNumber);
            }
        }

        /// <summary>
        /// Tiek pievienots neironu tīkls, kur tiek inicializēti svari un pievienoti tie sarakstiem
        /// </summary>
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
                for (int j = 0; j < 16; j++)
                {
                    chromosome.weights.Add(neuralNetwork.w[i, j]);
                    geneticAlgorithm.generation.Add(neuralNetwork.w[i, j]);
                }

            }

            InitializeNeuralNetworkTimer();
        }

        /// <summary>
        /// Metode nodrošina transporta kustību 6 elementiem, izmantojot neironu tīklu, nosakot to attālumus
        /// </summary>
        private void MoveVehicle()
        {
            double x1 = 0, x2 = 0, x3 = 0, x4 = 0;

            for (int i = 0; i < 6; i++)
            {
                vehicle = vehicles[i];
                neuralNetwork = neuralNetworks[i];

                for (int obstacleCounter = 0; obstacleCounter < obstacles.Count; obstacleCounter++)
                {
                    obstacle = obstacles[obstacleCounter];
                    //Tiek noskaidroti attalumi līdz konkrētajiem parametriem
                    x1 = area.Width - 50 - (vehicle.Location.X + vehicle.Width);
                    x2 = vehicle.Location.X - 50;
                    x3 = vehicle.Location.Y - (obstacle.Location.Y + obstacle.Height);
                    x4 = vehicle.Location.X - (obstacle.Location.X + obstacle.Width);
                    if (x4 < 0)
                    {
                        x4 = obstacle.Location.X - (vehicle.Location.X + vehicle.Width);
                        if (x4 < 0)
                        {
                            x4 = 0;
                        }
                    }

                    //Tiek izpildīts neironu tīkls
                    neuralNetwork.setInputData(x1, x2, x3, x4);
                    neuralNetwork.weights.AddRange(geneticAlgorithm.generation);
                    neuralNetwork.InitializeHiddenLayer(i);
                    neuralNetwork.InitializeOutputData(i);

                    //Nodrošina transporta kustību vai nu pa labi, vai pa kreisi
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

        /// <summary>
        /// Metode palielina spēles rezultātu par 1
        /// </summary>
        private void UpdateScore()
        {
            score.UpdatingScore(1);
        }

        /// <summary>
        /// Metode nodrošina šķēršļa kustību
        /// </summary>
        private void MoveObstacle()
        {
            foreach (var obstacle in obstacles)
            {
                obstacle.VerticalControl = obstacle.Step;
                obstacle.Top += obstacle.VerticalControl;
            }
        }

        /// <summary>
        /// Metode pārbauda, vai nav notikusi sadursme ar speles malām. Ja tā notiek, tad tiek izsaukta metode 
        /// par viena transporta zaudēšanu
        /// </summary>
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

        /// <summary>
        /// Metode pārbauda, vai nav notikusi sadursme ar speles šķēršļiem. Ja tā notiek, tad tiek izsaukta metode 
        /// par viena transporta zaudēšanu
        /// </summary>
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

        /// <summary>
        /// Metode nodrošina darbības, kas notiek, kad viens transports zaudē
        /// </summary>
        /// <param name="vehicleNum"> Transporta, kurš zaudēja, numurs </param>
        private void GameOverForOneVehicle(int vehicleNum)
        {
            vehicles[vehicleNum].Visible = false;
            vehicleNumbers.Add(vehicleNum);
            geneticAlgorithm.nextGeneration.Clear();
            neuralNetwork.nextGeneration.Clear();

            //Tiek pārbaudīts, kurš transports zaudēja, apstādinot tā rezultāta taimeri,
            //ierakstot hromosomā kā piemērotību un pievienojot to sarakstā
            if (vehicleNum == 0)
            {
                scoreTimers[0].Stop();
                chromosomes[0].Fitness = score.ScoreNumber;
                chromosome.AddFitnessToList(chromosomes[0].Fitness);
            }

            if (vehicleNum == 1)
            {
                scoreTimers[1].Stop();
                chromosomes[1].Fitness = score.ScoreNumber;
                chromosome.AddFitnessToList(chromosomes[1].Fitness);
            }

            if (vehicleNum == 2)
            {
                scoreTimers[2].Stop();
                chromosomes[2].Fitness = score.ScoreNumber;
                chromosome.AddFitnessToList(chromosomes[2].Fitness);
            }

            if (vehicleNum == 3)
            {
                scoreTimers[3].Stop();
                chromosomes[3].Fitness = score.ScoreNumber;
                chromosomes[3].AddFitnessToList(chromosomes[3].Fitness);
            }

            if (vehicleNum == 4)
            {
                scoreTimers[4].Stop();
                chromosomes[4].Fitness = score.ScoreNumber;
                chromosome.AddFitnessToList(chromosomes[4].Fitness);
            }

            if (vehicleNum == 5)
            {
                scoreTimers[5].Stop();
                chromosomes[5].Fitness = score.ScoreNumber;
                chromosome.AddFitnessToList(chromosomes[5].Fitness);
            }

            //Notiek pārbaude, vai visi transportlīdzekļi ir zaudējuši. Ja tas apstiprinās,
            //tad tiek izsaukta spēles beigu metode
            if (vehicleNumbers.Contains(0) && vehicleNumbers.Contains(1) &&
                vehicleNumbers.Contains(2) && vehicleNumbers.Contains(3) &&
                vehicleNumbers.Contains(4) && vehicleNumbers.Contains(5))
            {
                GameOver();
            }
        }

        /// <summary>
        /// Spēles beigu metode
        /// </summary>
        public void GameOver()
        {
            InitializeEndPicture();
        }

        /// <summary>
        /// Metode inicializē spēles beigu attēlu, izsaucot beigu teksta metodi un pogu metodes
        /// </summary>
        private void InitializeEndPicture()
        {
            this.Controls.Add(endPicture);
            endPicture.Image = (Image)Properties.Resources.ResourceManager.GetObject("end_picture");
            endPicture.Visible = true;
            endPicture.BringToFront();
            endPicture.Location = new Point(-8, 0);
            endPicture.Size = area.Size;
            endPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            EndText();
            ButtonNew();
            ButtonNext();
            ButtonClose();
        }

        /// <summary>
        /// Metode nodrošina spēles rezultātu parādīšanu beigu ekrānā, ka arī nodrošina ģenētiskā algoritma sākumu
        /// </summary>
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
            endText1.Size = new Size(720, 35);
            endText2.Size = new Size(720, 35);
            endText3.Size = new Size(720, 17);
            endText4.Size = new Size(720, 10);
            endText5.Size = new Size(720, 100);
            endText6.Size = new Size(720, 100);
            endText1.Location = new Point(90, 130);
            endText2.Location = new Point(90, 165);
            endText3.Location = new Point(90, 205);
            endText4.Location = new Point(90, 222);
            endText5.Location = new Point(90, 232);
            endText6.Location = new Point(90, 332);
            endText1.BackColor = Color.Transparent;
            endText2.BackColor = Color.Transparent;
            endText3.BackColor = Color.Transparent;
            endText4.BackColor = Color.Transparent;
            endText5.BackColor = Color.Transparent;
            endText6.BackColor = Color.Transparent;
            endText1.Font = new Font("Impact", 22);
            endText2.Font = new Font("Impact", 20);
            endText3.Font = new Font("Impact", 10);
            endText4.Font = new Font("Impact", 5);
            endText5.Font = new Font("Impact", 8);
            endText6.Font = new Font("Impact", 15);
            endText1.Text = iterationNumber + ". iteration\r\n";
            endText2.Text = "Current population:\r\n";
            endText3.Text = "Nr.           w0          w1          w2          w3          w4          " +
                "w5          w6          w7          w8          w9          w10          w11          w12          w13" +
                "          w14          w15      Fitness\r\n";
            endText4.Text = "_______________________________________________________________" +
                "_________________________________________________________________________" +
                "_______________________________________________________________________\r\n";
            endText5.Text =
                chromosome.PrintWeightsForChromosome(0) + "       " + chromosomes[0].Fitness + "\r\n" +
                chromosome.PrintWeightsForChromosome(1) + "       " + chromosomes[1].Fitness + "\r\n" +
                chromosome.PrintWeightsForChromosome(2) + "       " + chromosomes[2].Fitness + "\r\n" +
                chromosome.PrintWeightsForChromosome(3) + "       " + chromosomes[3].Fitness + "\r\n" +
                chromosome.PrintWeightsForChromosome(4) + "       " + chromosomes[4].Fitness + "\r\n" +
                chromosome.PrintWeightsForChromosome(5) + "       " + chromosomes[5].Fitness + "\r\n";
            endText6.Text = "Max fitness: " + chromosome.MaxFitness() + "\r\n" + "Min fitness: " +
                chromosome.MinFitness() + "\r\n" + "Average fitness: " + chromosome.AverageFitness();

            //Tiek aktivizēts ģenētiskais algoritms
            for (int k = 0; k < 6; k++)
            {
                geneticAlgorithm.fitness.Add(chromosomes[k].Fitness);
            }
            geneticAlgorithm.IntervalsForSelection();
            geneticAlgorithm.Crossover();
            geneticAlgorithm.AllToList();
            geneticAlgorithm.Mutation();
            geneticAlgorithm.SelectionToNextGeneration();

            //Rezultāti tiek ierakstīti teksta failā (ģenētiskā algoritma darbības) un CSV failā
            //(maksimālā piemērotība konkrētajā iterācijā)
            WriteInFile();
            WriteInCSVFile();
        }

        /// <summary>
        /// Pogas buttonNew definēšana
        /// </summary>
        private void ButtonNew()
        {
            buttonNew.Parent = endPicture;
            buttonNew.Size = new Size(100, 40);
            buttonNew.Location = new Point(400, 730);
            buttonNew.Visible = true;
            buttonNew.BackColor = Color.Black;
            buttonNew.ForeColor = Color.White;
            buttonNew.Text = "New";
            buttonNew.Font = new Font("Impact", 20, FontStyle.Bold);
            buttonNew.BringToFront();
            buttonNew.Click += buttonNew_Click;

        }

        /// <summary>
        /// Pogas buttonNext definēšana
        /// </summary>
        private void ButtonNext()
        {
            buttonNext.Parent = endPicture;
            buttonNext.Size = new Size(100, 40);
            buttonNext.Location = new Point(280, 730);
            buttonNext.Visible = true;
            buttonNext.BackColor = Color.Black;
            buttonNext.ForeColor = Color.White;
            buttonNext.Text = "Next";
            buttonNext.Font = new Font("Impact", 20, FontStyle.Bold);
            buttonNext.BringToFront();
            buttonNext.Click += buttonNext_Click;
            check += 1;
        }

        /// <summary>
        /// Pogas buttonClose definēšana
        /// </summary>
        private void ButtonClose()
        {
            buttonClose.Parent = endPicture;
            buttonClose.Size = new Size(100, 40);
            buttonClose.Location = new Point(530, 730);
            buttonClose.Visible = true;
            buttonClose.BackColor = Color.Black;
            buttonClose.ForeColor = Color.White;
            buttonClose.Text = "Close";
            buttonClose.Font = new Font("Impact", 20, FontStyle.Bold);
            buttonClose.BringToFront();
            buttonClose.Click += buttonClose_Click;
        }

        /// <summary>
        /// Nospiežot pogu, lai sāktu jaunu spēli, tiek aizvērti teksta un CSV faili, apstādināti taimeri
        /// un izsaukta metode, lai atsāktu jaunu spēli
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNew_Click(object sender, EventArgs e)
        {
            sw.Close();
            eksp.Close();
            neuralNetworkTimer.Stop();
            obstacleTimer.Stop();
            mainTimer.Stop();
            RestartGame();
        }

        /// <summary>
        /// Nospiežot pogu, lai sāktu jaunu iterāciju, tiek palielināts iterācijas numurs, izsauktas iterācijas,
        /// kuras attīra visus sarakstus, pievienoti jauni elementi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Nospiežot pogu, lai aizvērtu spēli, tiek aizvērti teksta un CSV faili, apstādināti taimeri
        /// un tiek aizvērta spēle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            sw.Close();
            eksp.Close();
            neuralNetworkTimer.Stop();
            obstacleTimer.Stop();
            mainTimer.Stop();
            this.Close();
        }

        /// <summary>
        /// Metode nodrošina spēles sākšanu no jauna
        /// </summary>
        private void RestartGame()
        {
            System.Diagnostics.Process.Start(Application.ExecutablePath);
            this.Close();
        }

        /// <summary>
        /// Metode nodrošina visu sarakstu attīrīšanu un elementu noņemšanu no ekrāna
        /// </summary>
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

            for (int i = 0; i < vehicles.Count; i++)
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
            obstacles.Clear();
            neuralNetworkTimer.Stop();
        }

        /// <summary>
        /// Metode nodrošina ģenētiskā algoritma darbību ierakstīšanu teksta failā
        /// </summary>
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
                    for (int j = i * 16; j < (i * 16 + 16); j++)
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

        /// <summary>
        /// Metode nodrošina maksimālās piemērotības ierakstīšanu CSV failā
        /// </summary>
        private void WriteInCSVFile()
        {
            try
            {
                eksp.WriteLine(chromosome.MaxFitness() + ",");

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
