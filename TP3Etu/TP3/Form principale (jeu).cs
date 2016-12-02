using System;
using System.Drawing;
using System.Windows.Forms;
using WMPLib;
using System.Threading;

namespace TP3
{
    public partial class Form1 : Form
    {
        WindowsMediaPlayer mediaPlayer = new WindowsMediaPlayer();

        #region Variable de jeu


        public int nbColones = 10;
        public int nbLignes = 20;
        public TypeBloc[,] tableauDeJeu = null;
        int[] blocActifY = null; // initialisé a la création du bloc //felix
        int[] blocActifX = null; // "               "           "
        private int[,] positionJoueur = null;
       private int ligneCourante = 0;
         private int coloneCourante = 0;
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        #region Code fourni

        // Représentation visuelles du jeu en mémoire.
        PictureBox[,] toutesImagesVisuelles = null;

        /// <summary>
        /// Gestionnaire de l'événement se produisant lors du premier affichage 
        /// du formulaire principal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLoad(object sender, EventArgs e)
        {
            // Ne pas oublier de mettre en place les valeurs nécessaires à une partie.
            ExecuterTestsUnitaires();
            InitialiserJeu();
            InitialiserSurfaceDeJeu(nbLignes, nbColones);
        }

        private void InitialiserSurfaceDeJeu(int nbLignes, int nbCols)
        {
            // Création d'une surface de jeu 10 colonnes x 20 lignes
            toutesImagesVisuelles = new PictureBox[nbLignes, nbCols];
            tableauJeu.Controls.Clear();
            tableauJeu.ColumnCount = toutesImagesVisuelles.GetLength(1);
            tableauJeu.RowCount = toutesImagesVisuelles.GetLength(0);
            for (int i = 0; i < tableauJeu.RowCount; i++)
                {
                    tableauJeu.RowStyles[i].Height = tableauJeu.Height/tableauJeu.RowCount;
                    for (int j = 0; j < tableauJeu.ColumnCount; j++)
                        {
                            tableauJeu.ColumnStyles[j].Width = tableauJeu.Width/tableauJeu.ColumnCount;
                            // Création dynamique des PictureBox qui contiendront les pièces de jeu
                            PictureBox newPictureBox = new PictureBox();
                            newPictureBox.Width = tableauJeu.Width/tableauJeu.ColumnCount;
                            newPictureBox.Height = tableauJeu.Height/tableauJeu.RowCount;
                            newPictureBox.BackColor = Color.Black;
                            newPictureBox.Margin = new Padding(0, 0, 0, 0);
                            newPictureBox.BorderStyle = BorderStyle.FixedSingle;
                            newPictureBox.Dock = DockStyle.Fill;

                            // Assignation de la représentation visuelle.
                            toutesImagesVisuelles[i, j] = newPictureBox;
                            // Ajout dynamique du PictureBox créé dans la grille de mise en forme.
                            // A noter que l' "origine" du repère dans le tableau est en haut à gauche.
                            tableauJeu.Controls.Add(newPictureBox, j, i);
                        }
                }
        }

        #endregion

        #region Code à développer

        /// <summary>
        /// Faites ici les appels requis pour vos tests unitaires.
        /// </summary>
        void ExecuterTestsUnitaires()
        {
            ExecuterTestABC();
            // A compléter...
        }

        // A renommer et commenter!
        void ExecuterTestABC()
        {
            // Mise en place des données du test

            // Exécuter de la méthode à tester

            // Validation des résultats

            // Clean-up
        }

        #endregion

        #region Code des gestions d'évènements

        private void personnaliserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuration_du_jeu config = new Configuration_du_jeu();
            config.ShowDialog();
            nbColones = config.ObtenirDimensionColones();
            nbLignes = config.ObtenirDimensionLignes();
            InitialiserSurfaceDeJeu(nbColones, nbLignes);
        }
        //fait par Félix
        /// <summary>
        /// la fonction démare le lecteur si il est arreter ou en pause
        /// et l'arrête si il est en pleine lecture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void musiqueDambiacneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mediaPlayer.URL = "art/Wakfu AMV - Are U Ready _ Goultard Le Barbare Tribute _.mp3";

            mediaPlayer.controls.play();
            if (mediaPlayer.playState == WMPPlayState.wmppsPaused || mediaPlayer.playState == WMPPlayState.wmppsStopped)    
                {
                    mediaPlayer.controls.play();
                    mediaPlayer.controls.play();
                }
            else
            if (mediaPlayer.playState == WMPPlayState.wmppsPlaying)
                { 
                    mediaPlayer.controls.stop();
                }
        }

        #endregion

        #region Affichage de la table de Jeu
        /// <summary> //Sam V.
        /// Permet l'affichage du tableau à chaque fois que la méthode est appelée
        /// </summary>
        private void DessinerTableDeJeu()
        {
            for (int i = 0; i < tableauDeJeu.GetLength(0); i++)
                {
                    for (int j = 0; j < tableauDeJeu.GetLength(1); j++)
                        {
                            if (positionJoueur[i, j] == 1) // Si il trouve un objet ...
                            {
                        toutesImagesVisuelles[i,j].BackColor = Color.Blue; //Il l'assigne à la couleur bleu et brise la boucle ...
                                break;
                            }
                            if (tableauDeJeu[i, j] == TypeBloc.NONE)
                                {
                                    toutesImagesVisuelles[i, j].BackColor = Color.Black;
                                }
                            if (tableauDeJeu[i, j] == TypeBloc.FROZEN)
                                {
                                    toutesImagesVisuelles[i, j].BackColor = Color.Gray;
                                }
                        }
                }
        }
       

#endregion

        #region méthodes d'initialisation
        //Sam v.
        /// <summary>
        /// Initialise la surface de jeu avec les paramètres que le joueur a choisi 
        /// </summary>
        void InitialiserJeu()
        {
            positionJoueur = new int[nbLignes, nbColones];
            tableauDeJeu = new TypeBloc[nbLignes, nbColones];
            blocActifX = new int[4];
            blocActifY = new int[4];
            for (int i = 0; i < tableauDeJeu.GetLength(0); i++)
                {
                    for (int j = 0; j < tableauDeJeu.GetLength(1); j++)
                    {
                        positionJoueur[i, j] = 0;
                            tableauDeJeu[i, j] = TypeBloc.NONE;
                        }
                }
            positionJoueur[0, (positionJoueur.GetLength(1)/2) - 1] = 1;

        }
        //fait par félix
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sens"></param>
        /// <returns></returns>
        bool BlocPeutBouger(Deplacement sens)
        {
            bool peutBouger = true;
            if (sens == Deplacement.DOWN)
            {
                for (int i = 0; 0 < blocActifY.Length - 1; i++)
                {//watch le zéros a la fin
                    if (tableauDeJeu[blocActifY[i] + ligneCourante, blocActifX[i] + coloneCourante] == TypeBloc.FROZEN || blocActifY[i] + ligneCourante == tableauDeJeu.GetLength(0))
                    {
                        peutBouger = false;
                    }
                }
            }
            else if (sens == Deplacement.RIGHT)
            {
                for (int i = 0; i < positionJoueur.GetLength(0) ; i++)
                    {
                        for (int j = 0; j < positionJoueur.GetLength(1); j++)
                        {
                            if (positionJoueur[i, positionJoueur.GetLength(1) -1] == 1)
                            {
                                peutBouger = false;
                            }
                        }
                    }  //Fait par Sam V.
            }
            else if (sens == Deplacement.LEFT)
            {
                for (int i = 0; i < positionJoueur.GetLength(0); i++)
                {
                    for (int j = 0; j < positionJoueur.GetLength(1); j++)
                    {
                        if (positionJoueur[i, 0] == 1)
                        {
                            peutBouger = false;
                        }
                    }
                }
            }
            else if(sens == Deplacement.ROTATE_CLOCKWISE)
            {

            }
            else if(sens == Deplacement.ROTATE_COUNTERCLOCKWISE)
            {

            }
            return peutBouger;
        }

        #endregion
        /// <summary>
        /// Déclanché par le click du bouton "Commencer" , cette fonction engendre le début d'une partie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DebuterUnePartie_btnClick(object sender, EventArgs e)
        {
            InitialiserJeu();
           DessinerTableDeJeu();
            timerPourDescenteDuJeu.Start();
           
        }

        private void DeplacerJoueur(Deplacement sensDuDeplacement)
        {
            for (int i = 0; i < positionJoueur.GetLength(0); i++)
                {
                    if (BlocPeutBouger(sensDuDeplacement) == false)
                    {
                        break;
                    }
                    for (int j = 0; j < positionJoueur.GetLength(1); j++)
                    {
                        if (positionJoueur[i, j] == 1)
                        {
                            if (sensDuDeplacement == Deplacement.LEFT)
                            {
                            toutesImagesVisuelles[i,j].BackColor = Color.Black;
                                positionJoueur[i, j] = 0;
                                positionJoueur[i, j - 1] = 1;
                            DessinerTableDeJeu();
                                break;
                            }
                            if (sensDuDeplacement == Deplacement.RIGHT)
                            {
                            toutesImagesVisuelles[i, j].BackColor = Color.Black;
                            positionJoueur[i, j] = 0;
                            positionJoueur[i, j + 1] = 1;
                            DessinerTableDeJeu();
                            break;
                        }
                        }
                    }
                }
        }

        private void ToucheApuye_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (e.KeyChar == 'a' || e.KeyChar == (char)Keys.Left)
                {
                   DeplacerJoueur(Deplacement.LEFT); 
                }
            else if (e.KeyChar == 'd' || e.KeyChar == (char) Keys.Right)
                {
                    DeplacerJoueur(Deplacement.RIGHT);
                }
            else if (e.KeyChar == 'w' || e.KeyChar == (char) Keys.Up)
                {
                    DeplacerJoueur(Deplacement.ROTATE_CLOCKWISE);
                }
            else if (e.KeyChar == 's' || e.KeyChar == (char) Keys.Down)
                {
                    DeplacerJoueur(Deplacement.ROTATE_COUNTERCLOCKWISE);
                }
        }

        private void FaireDescendreCubeDeJeu_TimerTick(object sender, EventArgs e)
        {
            DeplacerJoueur(Deplacement.DOWN);
        }
    }
}