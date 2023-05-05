using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CarRacingGameWithGeneticAlgorithm
{
    /// <summary>
    /// Klase nodrošina spēles transporta izskatu
    /// </summary>
    public class Vehicle : PictureBox
    {
        /// <summary>
        /// Transporta kustības elementu definēšana
        /// </summary>
        public int Step { get; set; } = 20;
        public int HorizontalControl { get; set; } = 0;

        public Vehicle()
        {
            InitializeVehicle();
        }

        /// <summary>
        /// Metode, kura inicializē transportu
        /// </summary>
        private void InitializeVehicle()
        {
            this.Image = (Image)Properties.Resources.ResourceManager.GetObject("car");
            this.Size = new Size(60, 100);
            this.BackColor = Color.Transparent;
            this.Name = "Car";
        }

        /// <summary>
        /// Metode, kura sagatavo klasi nākamajai iterācijai
        /// </summary>
        public void NextIteration()
        {
            Step = 0;
            HorizontalControl = 0;
        }
    }
}
