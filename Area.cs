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

        public Area()
        {
            InitializeRoad();
        }

        private void InitializeRoad()
        {
            string v = "road";
            this.Image = (Image)Properties.Resources.ResourceManager.GetObject(v);
            this.Size = new Size(600, 800);
            this.Location = new Point(-8, 0);
            this.Name = "Road";
        }

        

        
    }
}
