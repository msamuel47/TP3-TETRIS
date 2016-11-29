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

        private void ChangerLaTextBoxNbLignes_Scoll(object sender, EventArgs e)
        {
            nbLignes_TextBox.Text = nbLignes_TrackBar.Value.ToString();
        }

        private void InitialiserConfig_Load(object sender, EventArgs e)
        {
            nbLignes_TextBox.Text = nbLignes_TrackBar.Value.ToString();
            nbColones_TextBox.Text = nbColones_TrackBar.Value.ToString();
        }

        private void ChangerLeNombreDeColones_Scroll(object sender, EventArgs e)
        {
            nbColones_TextBox.Text = nbColones_TrackBar.Value.ToString();
        }
    }

}
