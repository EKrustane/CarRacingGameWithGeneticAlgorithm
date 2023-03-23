using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CarRacingGameWithGeneticAlgorithm
{
    public class Vehicle : PictureBox
    {
        private Point pointLocation = new Point(0, 0);
        private int imageRight = 0;
        private int imageLeft = 0;
        public int Step { get; set; } = 2;
        public int HorizontalVelocity { get; set; } = 0;
        public int VerticalVelocityRight { get; set; } = 0;
        public int VerticalVelocityLeft { get; set; } = 0;

        public Vehicle()
        {
            InitializeVehicle();
        }

        private void InitializeVehicle()
        {
            string v = "vehicle";
            this.Image = (Image)Properties.Resources.ResourceManager.GetObject(v);
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Location = new Point(100, 100);
            this.Name = "Car";
        }
        private void setVerticalVelocityRight()
        {
            //pointLocation = new Point(this.Location.X + 20, this.Location.Y + 80);
            //imageRight = pointLocation.Y;
            VerticalVelocityRight = this.Location.Y + 80;
        }

        private void setVerticalVelocityLeft()
        {
            //pointLocation = new Point(this.Location.X, this.Location.Y + 80);
            //imageLeft = pointLocation.Y;
            VerticalVelocityLeft = this.Location.Y + 80;
        }
    }
}
