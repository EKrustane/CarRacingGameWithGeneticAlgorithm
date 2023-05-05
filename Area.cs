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
    /// Klase nodrošina spēles vides izskatu
    /// </summary>
    public class Area : PictureBox
    {
        public Area()
        {
            InitializeRoad();
        }

        /// <summary>
        /// Metode, kura inicializē spēles vidi jeb ceļu
        /// </summary>
        private void InitializeRoad()
        {
            string v = "road";
            this.Image = (Image)Properties.Resources.ResourceManager.GetObject(v);
            this.Size = new Size(900, 800);
            this.Location = new Point(-8, 0);
            this.Name = "Road";
        }
    }
}
