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
    /// Klase nodrošina spēles rezultāta etiķetes izskatu un funkcionalitāti
    /// </summary>
    public class Score : Label
    {
        /// <summary>
        /// Spēles rezultāta saglabāšanas mainīgais
        /// </summary>
        public int ScoreNumber { get; set; } = 0;

        public Score()
        {
            InitializeScore();
        }

        /// <summary>
        /// Metode, kura apraksta rezultāta etiķetes izvietojumu un burtu atribūtus
        /// </summary>
        private void InitializeScore()
        {
            this.BackColor = Color.Transparent;
            this.Size = new Size(150, 100);
            this.Location = new Point(370, 15);
            this.Font = new Font("Century Gothic", 18, FontStyle.Bold);
        }

        /// <summary>
        /// Metode, kura nodrošina visu rezultāta etiķetes izskatu spēlē (Nosaukums + rezultāts)
        /// </summary>
        /// <param name="score"> Spēles pašreizējais rezultāts </param>
        public void UpdatingScore(int score)
        {
            ScoreNumber += score;
            this.Text = "Score: " + ScoreNumber.ToString();
        }

        /// <summary>
        /// Metode, kura sagatavo klasi nākamajai iterācijai
        /// </summary>
        public void NextIteration()
        {
            ScoreNumber = 0;
            this.Visible = false;
        }
    }
}
