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
        public int ScoreNumber { get; set; } = 0;

        public Score()
        {
            InitializeScore();
        }

        private void InitializeScore()
        {
            this.BackColor = Color.Transparent;
            this.Size = new Size(150, 100);
            this.Location = new Point(230, 15);
            this.Font = new Font("Century Gothic", 18, FontStyle.Bold);
        }

        public void UpdatingScore(int score)
        {
            ScoreNumber += score;
            this.Text = "Score: " + ScoreNumber.ToString();
        }

        public void NextIteration()
        {
            ScoreNumber = 0;
            this.Visible = false;
        }
    }
}
