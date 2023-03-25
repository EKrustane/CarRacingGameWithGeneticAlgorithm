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
        public int Step { get; set; } = 20;
        public int HorizontalControl { get; set; } = 0;

        public Vehicle()
        {
            InitializeVehicle();
        }

        private void InitializeVehicle()
        {
            this.Image = (Image)Properties.Resources.ResourceManager.GetObject("car");
            this.Size = new Size(60, 100);
            this.BackColor = Color.Transparent;
            this.Location = new Point(270, 600);
            this.Name = "Car";
        }
    }
}
