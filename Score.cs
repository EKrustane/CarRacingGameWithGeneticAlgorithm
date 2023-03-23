using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CarRacingGameWithGeneticAlgorithm
{
    public class Score : Label
    {
        public int score;

        public Score()
        {
            InitializeScore();
        }

        private void InitializeScore()
        {
            this.BackColor = Color.Transparent;
            this.Size = new Size(50, 15);
            this.Location = new Point(10, 10);
            this.Text = "Score: " + score;
        }
    }
}
