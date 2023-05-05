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
    /// Klase nodrošina spēles šķēršļa izskatu
    /// </summary>
    class Obstacle :PictureBox
    {
        /// <summary>
        /// Šķēršļa kustības elementu definēšana
        /// </summary>
        public int Step { get; set; } = 5;
        public int VerticalControl { get; set; } = 0;

        public Obstacle()
        {
            InitializeObstacle();
        }

        /// <summary>
        /// Metode, kura inicializē šķērsli
        /// </summary>
        private void InitializeObstacle()
        {
            this.Image = (Image)Properties.Resources.ResourceManager.GetObject("obstacle");
            this.Size = new Size(60, 60);
            this.BackColor = Color.Transparent;
            this.Name = "Obstacle";
        }

        /// <summary>
        /// Metode, kura sagatavo klasi nākamajai iterācijai
        /// </summary>
        public void NextIteration()
        {
            Step = 0;
            VerticalControl = 0;
            this.Image = (Image)Properties.Resources.ResourceManager.GetObject("transparent");
            this.Size = new Size(60, 60);
            this.BackColor = Color.Transparent;
        }
    }

    

}
