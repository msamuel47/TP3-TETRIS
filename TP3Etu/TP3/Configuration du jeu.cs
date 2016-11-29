using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP3
{
    public partial class Configuration_du_jeu : Form
    {
        public Configuration_du_jeu()
        {
            InitializeComponent();
        }

        public void ChangerLaTextBoxNbLignes_Scoll(object sender, EventArgs e)
        {
            nbLignes_TextBox.Text = nbLignes_TrackBar.Value.ToString();
            
        }

        private void InitialiserConfig_Load(object sender, EventArgs e)
        {
            nbLignes_TrackBar.Value = 20;
            nbColones_TrackBar.Value = 10;
            nbLignes_TextBox.Text = nbLignes_TrackBar.Value.ToString();
            nbColones_TextBox.Text = nbColones_TrackBar.Value.ToString();
        }

        public void ChangerLeNombreDeColones_Scroll(object sender, EventArgs e)
        {
            nbColones_TextBox.Text = nbColones_TrackBar.Value.ToString();
        }
        /// <summary>
        /// Permet d'obtenir la dimension des colones
        /// </summary>
        public int ObtenirDimensionColones()
        {
            int retour = nbColones_TrackBar.Value;
            return retour;
        }
        /// <summary>
        /// Permet d'obtenir la dimension des colones
        /// </summary>
        /// <returns></returns>
        public int ObtenirDimensionLignes()
        {
            int retour = nbLignes_TrackBar.Value;
            return retour;
        }
    }

}
