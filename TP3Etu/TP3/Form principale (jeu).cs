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
        int[] blockActifY = null; // initialisé a la création du bloc //felix
        int[] blockActifX = null; // "               "           "
        int[,] positionJoueur = null;
        int ligneCourante = 0;
        int coloneCourante = 0;
        Random rnd = new Random();
       
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
            //pierre
            for (int i = 0; i < tableauDeJeu.GetLength(0); i++)
            {
                for (int j = 0; j < tableauDeJeu.GetLength(1); j++)
                {
                    if (tableauDeJeu[i, j] == TypeBloc.FROZEN) // Si il trouve un objet ...
                    {
                        toutesImagesVisuelles[i, j].BackColor = Color.Gray;
                    }
                    else
                        toutesImagesVisuelles[i, j].BackColor = Color.Black;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                toutesImagesVisuelles[ligneCourante + blockActifY[i], coloneCourante + blockActifX[i]].BackColor = Color.Blue;
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
            blockActifX = new int[4];
            blockActifY = new int[4];
            for (int i = 0; i < tableauDeJeu.GetLength(0); i++)
                {
                    for (int j = 0; j < tableauDeJeu.GetLength(1); j++)
                    {
                        positionJoueur[i, j] = 0;
                            tableauDeJeu[i, j] = TypeBloc.NONE;
                        }
                }
          
      

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
                for (int i = 0; i < blockActifY.Length -1; i++)
                {//watch le zéros a la fin
                    if (positionJoueur[blockActifY[i] + ligneCourante, blockActifX[i] + coloneCourante] == 2 || blockActifY[i] + ligneCourante == positionJoueur.GetLength(0))
                    {
                        peutBouger = false;
                    }
                }
            }
            else if (sens == Deplacement.RIGHT)
            {
                for (int i = 0; i < blockActifX.Length ; i++)
                    {
                        
                            if (positionJoueur[ligneCourante + blockActifY[i],coloneCourante + blockActifX[i]+1] == 2 || coloneCourante + blockActifX[i] ==positionJoueur.GetLength(1))
                            {
                                peutBouger = false;
                            }
                        
                    }  
            }
            else if (sens == Deplacement.LEFT)
            {
                for (int i = 0; i < blockActifX.GetLength(0); i++)
                {
                    if (positionJoueur[ligneCourante + blockActifY[i], coloneCourante + blockActifX[i] ] == 2 || coloneCourante + blockActifX[i] == 0)
                    {
                        peutBouger = false;
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
            GenererBlock();
           DessinerTableDeJeu();
            timerPourDescenteDuJeu.Start();
           
        }

        private void DeplacerJoueur(Deplacement sensDuDeplacement)
        {
            //  for (int i = 0; i < positionJoueur.GetLength(0); i++)
            //   {
            int memoire = 0;
            if (BlocPeutBouger(sensDuDeplacement) == true)
              {
                if(sensDuDeplacement == Deplacement.DOWN)
                {
                    ligneCourante += 1;
                }
                else if (sensDuDeplacement ==Deplacement.LEFT)
                {
                    coloneCourante -= 1;
                }
                else if (sensDuDeplacement ==Deplacement.RIGHT)
                {
                    coloneCourante += 1;
                }
                else if (sensDuDeplacement ==Deplacement.ROTATE_CLOCKWISE)
                {
                    
                }
                else if (sensDuDeplacement ==Deplacement.ROTATE_COUNTERCLOCKWISE)
                {
                    for (int i = 0; i < blockActifX.Length; i++)
                    {
                        memoire = blockActifX[i];
                        blockActifX[i] = blockActifY[i];
                        blockActifY[i] = memoire;
                    }
                }
                DessinerTableDeJeu();
            
            }
                 
                    
                    //  for (int j = 0; j < positionJoueur.GetLength(1); j++)
                    //{
                      //  if (positionJoueur[i, j] == 1)
                        //{
                          //  if (sensDuDeplacement == Deplacement.LEFT)
                            //{
                            //toutesImagesVisuelles[i,j].BackColor = Color.Black;
                              //  positionJoueur[i, j] = 0;
                                //positionJoueur[i, j - 1] = 1;
                            //DessinerTableDeJeu();
                              //  break;
                            //}
                            //if (sensDuDeplacement == Deplacement.RIGHT)
                            //{
                            //toutesImagesVisuelles[i, j].BackColor = Color.Black;
                            //positionJoueur[i, j] = 0;
                            //positionJoueur[i, j + 1] = 1;
                            //DessinerTableDeJeu();
                            //break;
                      //  }
                     //   }
                   // }
              //  }
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
        //felix.b
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ligneCourante"></param>
        /// <param name="coloneCourant"></param>
        /// <param name="formeDuBloc"></param>
        void GenererBlock()
        {
            TypeBloc formeDuBloc = TypeBloc.SQUARE;
            int typeDeBloc = rnd.Next(0, 7 + 1);
            if (typeDeBloc == 0)
            {
                formeDuBloc = TypeBloc.J;
            }
            else if (typeDeBloc ==1)
            {
                formeDuBloc = TypeBloc.L;
            }
            else if (typeDeBloc ==2)
            {
                formeDuBloc = TypeBloc.LINE;
            }
            else if (typeDeBloc ==3)
            {
                formeDuBloc = TypeBloc.Z;
            }
            else if (typeDeBloc ==4)
            {
                formeDuBloc = TypeBloc.S;
            }
            else if (typeDeBloc ==5)
            {
                formeDuBloc = TypeBloc.SQUARE;
            }
            else if (typeDeBloc ==6)
            {
                formeDuBloc = TypeBloc.T;
            }
       
            if (formeDuBloc == TypeBloc.J)
            {
             
                blockActifX[0] =1;
                blockActifY[0] =0;

                blockActifX[1] =1;
                blockActifY[1] =1;

                blockActifX[2] =1;
                blockActifY[2] =2;

                blockActifX[3] =0;
                blockActifY[3] =2;
            }
            else if (formeDuBloc == TypeBloc.L)
            {
                blockActifX[0] =0;
                blockActifY[0] =0;

                blockActifX[1] =0;
                blockActifY[1] =1;

                blockActifX[2] =0;
                blockActifY[2] =2;

                blockActifX[3] =1;
                blockActifY[3] =2;

            }
            else if (formeDuBloc == TypeBloc.LINE)//La ligne est coucher
            {
                blockActifX[0] =0;
                blockActifY[0] =0;

                blockActifX[1] =1;
                blockActifY[1] =0;

                blockActifX[2] =2;
                blockActifY[2] =0;

                blockActifX[3] =3;
                blockActifY[3] =0;
            }
            else if (formeDuBloc == TypeBloc.S)
            {
                blockActifX[0] =2;
                blockActifY[0] =0;

                blockActifX[1] =1;
                blockActifY[1] =0;

                blockActifX[2] =1;
                blockActifY[2] =1;

                blockActifX[3] =0;
                blockActifY[3] =1;
            }
            else if (formeDuBloc == TypeBloc.SQUARE)
            {
                blockActifX[0] =0;
                blockActifY[0] =0;

                blockActifX[1] =1;
                blockActifY[1] =0;

                blockActifX[2] =0;
                blockActifY[2] =1;

                blockActifX[3] =1;
                blockActifY[3] =1;
            }
            else if (formeDuBloc == TypeBloc.T)
            {
                blockActifX[0] =0;
                blockActifY[0] =0;

                blockActifX[1] =1;
                blockActifY[1] =0;

                blockActifX[2] =2;
                blockActifY[2] =0;

                blockActifX[3] =1;
                blockActifY[3] =1;
            }
            else if (formeDuBloc == TypeBloc.Z)
            {
                blockActifX[0] =0;
                blockActifY[0] =0;

                blockActifX[1] =1;
                blockActifY[1] =0;

                blockActifX[2] =1;
                blockActifY[2] =1;

                blockActifX[3] =2;
                blockActifY[3] =1;
            }
            for(int i =0;i<blockActifX.Length;i++)
            {
                positionJoueur[ligneCourante + blockActifY[i], coloneCourante + blockActifX[i]] = 1;
            }

        }
    }
}