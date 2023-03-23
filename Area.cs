using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CarRacingGameWithGeneticAlgorithm
{
    public class Area : PictureBox
    {
        private int x, y;
        private bool groundTimerBool = false;
        private Timer groundTimer = null;
        private Panel groundPanel = new Panel();
        private Random randomY = new Random();
        public Point point = new Point();
        public Point[] groundPoints = new Point[9];
        private Pen ground = new Pen(Color.Green, 5);

        public Area()
        {
            InitializeSky();
            InitializeGround();
        }

        private void InitializeSky()
        {
            this.BackColor = Color.DeepSkyBlue;
            this.Size = new Size(800, 530);
            this.Location = new Point(0, 0);
            this.Name = "Sky";
        }

        private void InitializeGround()
        {
            InitializeGroundPanel();
            InitializeGroundTimer();
        }

        private void InitializeGroundPanel()
        {
            groundPanel.Parent = this;
            groundPanel.Location = new Point(0, 200);
            groundPanel.Name = "groundPanel";
            groundPanel.Size = new Size(800, 220);
            groundPanel.BackColor = Color.Transparent;
            groundPanel.Visible = true;
            groundPanel.BringToFront();
            groundPanel.Paint += new PaintEventHandler(this.groundPanel_Paint);
        }

        private void InitializeGroundTimer()
        {
            groundTimer = new Timer();
            groundTimer.Tick += groundTimer_Tick;
            groundTimer.Interval = 10;
            groundTimer.Start();
            groundTimerBool = true;


        }

        private void groundTimer_Tick(object sender, EventArgs e)
        {

        }

        private void groundPanel_Paint(object sender, PaintEventArgs e)
        {
            DrawGroundLine(e);
        }
        public void DrawGroundLine(PaintEventArgs e)
        {
            int i = 0;

            while (groundTimerBool)
            {

                if (i <= 8)
                {
                    for (i = 1; i <= 8; i++)
                    {
                        x += 100;
                        y = randomY.Next(0, 220);
                        point = new Point(x, y);
                        groundPoints[i] = point;
                    }
                }
                else
                {
                    i = 0;
                    x = 0;
                }
                e.Graphics.DrawCurve(ground, groundPoints);
                groundTimerBool = false;
            }
            groundTimer.Stop();
        }

    }
}
