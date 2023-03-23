using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CarRacingGameWithGeneticAlgorithm
{
    public partial class CarRacingGame : Form
    {
        private PictureBox startPicture = new PictureBox();
        private PictureBox endPicture = new PictureBox();
        private Area area = new Area();
        private Vehicle vehicle = new Vehicle();
        private Score score = new Score();
        private GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm();
        private Button buttonStart = new Button();
        private Button buttonNew = new Button();
        private Button buttonNext = new Button();
        private bool buttonStartClick = false;
        private Timer ifButtonIsClickedTimer = null;
        private Timer gravityTimer = null;
        private int groundValue = 500;


        public CarRacingGame()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            //adjust game form size
            this.Size = new Size(5300, 530);

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
            startPicture.Size = new Size(800, 500);
            startPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            startPicture.BringToFront();
            startPicture.Image = (Image)Properties.Resources.ResourceManager.GetObject("start_pic");
            ButtonStart();
        }
        private void ButtonStart()
        {
            buttonStart.Parent = startPicture;
            buttonStart.Size = new Size(156, 46);
            buttonStart.Location = new Point(336, 426);
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
            //add key down event handler
            //this.KeyDown += Game_KeyDown;

            //inicialize and add area
            AddArea();
            //groundValue = area.point.Y + 200;
            //adding vehicle to the game
            AddVehicle();

            //adding score to the game
            AddScore();

            //adding GA to the game
            AddGeneticAlgorithm();

            //UpdateScoreLabel();

        }

        /*private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    vehicle.HorizontalVelocity = vehicle.Step;
                    if()
                    {
                        vehicle.VerticalVelocityRight = vehicle.Step - 1;
                        vehicle.VerticalVelocityLeft = -vehicle.Step + 1;
                    }
                    else if()
                    {
                        vehicle.VerticalVelocityLeft = vehicle.Step - 1;
                        vehicle.VerticalVelocityRight = -vehicle.Step + 1;
                    }
                    break;
                case Keys.Left:
                    vehicle.HorizontalVelocity = -vehicle.Step;
                    if ()
                    {
                        vehicle.VerticalVelocityLeft = vehicle.Step - 1;
                        vehicle.VerticalVelocityRight = -vehicle.Step + 1;
                    }
                    else if()
                    {
                        vehicle.VerticalVelocityRight = vehicle.Step - 1;
                        vehicle.VerticalVelocityLeft = -vehicle.Step + 1;
                        
                    }
                    
                    break;
            }
        }*/

        private void AddArea()
        {
            this.Controls.Add(area);
            //area.BringToFront();
        }

        private void AddVehicle()
        {
            this.Controls.Add(vehicle);
            vehicle.Parent = area;
            vehicle.BringToFront();

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

        private void InitializeEndPicture()
        {
            endPicture.Visible = true;
            endPicture.Location = new Point(0, 0);
            endPicture.Size = area.Size;
            endPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            endPicture.BringToFront();
            ButtonNew();
            ButtonNext();
        }

        private void ButtonNew()
        {
            buttonNew.Parent = endPicture;
            buttonNew.Size = new Size(80, 40);
            buttonNew.Location = new Point(120, 300);
            buttonNew.Visible = true;
            buttonNew.BringToFront();
        }

        private void ButtonNext()
        {
            buttonNext.Parent = endPicture;
            buttonNext.Size = new Size(80, 40);
            buttonNext.Location = new Point(215, 300);
            buttonNext.Visible = true;
            buttonNext.BringToFront();
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            //RestartGame();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            //this.Close();
        }

        private void MoveVehicle()
        {
            //vehicle.Right += vehicle.HorizontalVelocity;
            //vehicle.Left -= vehicle.VerticalVelocity;
        }
    }


    }
}
