﻿using System;
using System.Drawing;
using System.Windows.Forms;
using WMPLib;
using System.Threading;

namespace TP3
{
    public partial class FenetreDeJeu : Form
    {
        WindowsMediaPlayer mediaPlayer = new WindowsMediaPlayer();

        #region Variable de jeu

        // Initialisation des variables partagée et paramêtre du jeu

           //Cette section est fait par Samuel et par Felix
        public int nbColones = 10;
        public int nbLignes = 20;
        public TypeBloc[,] tableauDeJeu = null;
        private int[] blocActifY = null; 
        private int[] blocActifX = null;
        bool[] etatDesLignesPleines = new bool[20] { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true };
        TypeBloc formeDuBloc = TypeBloc.SQUARE;
        int ligneCourante = 0;
        int coloneCourante = 0;
        Random rnd = new Random();
        int pointage = 0;
        string pointsString = "";
        int niveau = 1;
        int nombreDeLigneEffacerNiveau = 0;
        int cadreActif = 0;
        bool peutEncoreJouer = true;
        #endregion

        public FenetreDeJeu()
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
        /// Faites ici les appels requis pour vos tests unitaires. Les tests unitaires n'ont pas été effectuée
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

           
            if (mediaPlayer.playState == WMPPlayState.wmppsPaused || mediaPlayer.playState == WMPPlayState.wmppsStopped)    
                {
                    mediaPlayer.controls.play();
                }
            else if (mediaPlayer.playState == WMPPlayState.wmppsPlaying)
                { 
                    mediaPlayer.controls.stop();
                }
        }

        #endregion

        #region Affichage de la table de Jeu
        //pierre + felix
        /// <summary> 
        /// Permet l'affichage du tableau à chaque fois que la méthode est appelée
        /// </summary>
        private void DessinerTableDeJeu()
        {
            
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
            for (int i = 0; i < blocActifX.Length; i++)
                     {
               
                         if (formeDuBloc == TypeBloc.Z)
                         {
                         toutesImagesVisuelles[ligneCourante + blocActifY[i] , coloneCourante + blocActifX[i]].BackColor = Color.Red;
                         }
                         else if (formeDuBloc == TypeBloc.T)
                         {
                          toutesImagesVisuelles[ligneCourante + blocActifY[i], coloneCourante + blocActifX[i]].BackColor = Color.Violet;
                         }
                         else if (formeDuBloc == TypeBloc.SQUARE)
                         {
                          toutesImagesVisuelles[ligneCourante + blocActifY[i], coloneCourante + blocActifX[i]].BackColor = Color.Yellow;
                         }
                         else if (formeDuBloc == TypeBloc.S)
                         {
                          toutesImagesVisuelles[ligneCourante + blocActifY[i], coloneCourante + blocActifX[i]].BackColor = Color.Green;
                         }
                         else if (formeDuBloc == TypeBloc.LINE)
                         {
                          toutesImagesVisuelles[ligneCourante + blocActifY[i], coloneCourante + blocActifX[i]].BackColor = Color.Aqua;
                         }
                         else if (formeDuBloc == TypeBloc.L)
                         {
                          toutesImagesVisuelles[ligneCourante + blocActifY[i], coloneCourante + blocActifX[i]].BackColor = Color.Orange;
                         }
                         else if (formeDuBloc == TypeBloc.J)
                         {
                          toutesImagesVisuelles[ligneCourante + blocActifY[i], coloneCourante + blocActifX[i]].BackColor = Color.Blue;
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
            
          
            tableauDeJeu = new TypeBloc[nbLignes, nbColones];
            blocActifX = new int[4];
            blocActifY = new int[4];
            for (int i = 0; i < tableauDeJeu.GetLength(0); i++)
                {
                    for (int j = 0; j < tableauDeJeu.GetLength(1); j++)
                    {
                       
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
                // verifie si chacun des blocs est sur la limite du tableau
                if (blocActifY[0] + ligneCourante == tableauDeJeu.GetLength(0) - 1
                       || blocActifY[1] + ligneCourante == tableauDeJeu.GetLength(0) - 1
                       || blocActifY[2] + ligneCourante == tableauDeJeu.GetLength(0) - 1
                       || blocActifY[3] + ligneCourante == tableauDeJeu.GetLength(0) - 1)
                {
                    peutBouger = false;
                    for (int j = 0; j < blocActifY.Length ; j++)
                    {
                    
                        tableauDeJeu[blocActifY[j] + ligneCourante, blocActifX[j] + coloneCourante] = TypeBloc.FROZEN;
                      
                    }
                    VerifierSiLigneComplette();
                  
                    
                    
                        GenererBloc();
                    
                 

                    
                }
                //verifie si les block en dessous sint gelées
                else
                {
                    for (int i = 0; i < blocActifY.Length ; i++)
                    {//watch le zéros a la fin

                        if (tableauDeJeu[blocActifY[i] + ligneCourante + 1, blocActifX[i] + coloneCourante] == TypeBloc.FROZEN)
                        {
                            peutBouger = false;
                            for (int j = 0; j < blocActifY.Length; j++)
                            {

                                tableauDeJeu[blocActifY[j] + ligneCourante, blocActifX[j] + coloneCourante] = TypeBloc.FROZEN;
                            }
                            VerifierSiLigneComplette();

                            GenererBloc();
                        
                      
                        }
                    }

                    }
                
            }
            else if (sens == Deplacement.RIGHT)
            {
                for (int i = 0; i < blocActifX.Length ; i++)
                    {
                        
                            if (tableauDeJeu[ligneCourante + blocActifY[i],coloneCourante + blocActifX[i]] == TypeBloc.FROZEN || coloneCourante + blocActifX[i] == tableauDeJeu.GetLength(1)-1)
                            {
                                peutBouger = false;
                            }
                        
                    }  
            }
            else if (sens == Deplacement.LEFT)
            {
                for (int i = 0; i < blocActifX.GetLength(0); i++)
                {
                    if (tableauDeJeu[ligneCourante + blocActifY[i], coloneCourante + blocActifX[i] ] == TypeBloc.FROZEN || coloneCourante + blocActifX[i] == 0)
                    {
                        peutBouger = false;
                    }

                }
            }
            else if(sens == Deplacement.ROTATE_CLOCKWISE)
            {
                if (   Math.Abs(blocActifY[0]) > coloneCourante || Math.Abs(blocActifY[0]) > tableauDeJeu.GetLength(1)-1 - coloneCourante
                    || Math.Abs(blocActifY[1]) > coloneCourante || Math.Abs(blocActifY[1]) > tableauDeJeu.GetLength(1) -1- coloneCourante
                    || Math.Abs(blocActifY[2]) > coloneCourante || Math.Abs(blocActifY[2]) > tableauDeJeu.GetLength(1) -1- coloneCourante
                    || Math.Abs(blocActifY[3]) > coloneCourante || Math.Abs(blocActifY[3]) > tableauDeJeu.GetLength(1) -1- coloneCourante
                    || Math.Abs(blocActifX[0]) > ligneCourante || Math.Abs(blocActifX[0]) > tableauDeJeu.GetLength(0) -1- ligneCourante
                    || Math.Abs(blocActifX[1]) > ligneCourante || Math.Abs(blocActifX[1]) > tableauDeJeu.GetLength(0) -1- ligneCourante
                    || Math.Abs(blocActifX[2]) > ligneCourante || Math.Abs(blocActifX[2]) > tableauDeJeu.GetLength(0) -1- ligneCourante
                    || Math.Abs(blocActifX[3]) > ligneCourante || Math.Abs(blocActifX[3]) > tableauDeJeu.GetLength(0) -1- ligneCourante )
                {
                    peutBouger = false;
                }
            }
            else if(sens == Deplacement.ROTATE_COUNTERCLOCKWISE)
            {
                if (  Math.Abs(blocActifY[0]) > coloneCourante  || Math.Abs(blocActifY[0]) > Math.Abs( coloneCourante  -1- tableauDeJeu.GetLength(1))
                   || Math.Abs(blocActifY[1]) > coloneCourante  || Math.Abs(blocActifY[1]) > Math.Abs( coloneCourante  -1- tableauDeJeu.GetLength(1))
                   || Math.Abs(blocActifY[2]) > coloneCourante  || Math.Abs(blocActifY[2]) > Math.Abs( coloneCourante  -1- tableauDeJeu.GetLength(1))
                   || Math.Abs(blocActifY[3]) > coloneCourante  || Math.Abs(blocActifY[3]) > Math.Abs( coloneCourante  -1- tableauDeJeu.GetLength(1))
                   || Math.Abs(blocActifX[0]) > ligneCourante ||  Math.Abs(blocActifX[0]) > Math.Abs(ligneCourante -1- tableauDeJeu.GetLength(0))
                   || Math.Abs(blocActifX[1]) > ligneCourante ||  Math.Abs(blocActifX[1]) > Math.Abs(ligneCourante -1- tableauDeJeu.GetLength(0))
                   || Math.Abs(blocActifX[2]) > ligneCourante ||  Math.Abs(blocActifX[2]) > Math.Abs(ligneCourante -1- tableauDeJeu.GetLength(0))
                   || Math.Abs(blocActifX[3]) > ligneCourante ||  Math.Abs(blocActifX[3]) > Math.Abs(ligneCourante -1- tableauDeJeu.GetLength(0)))
                {
                    peutBouger = false;
                }
            }
            return peutBouger;
        }

        #endregion
        //Samuel +felix
        /// <summary>
        /// Déclanché par le click du bouton "Commencer" , cette fonction engendre le début d'une partie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DebuterUnePartie_btnClick(object sender, EventArgs e)
        {
            InitialiserJeu();

            if (                tableauDeJeu[blocActifY[0], blocActifX[0]] == TypeBloc.NONE
                             && tableauDeJeu[blocActifY[1], blocActifX[1]] == TypeBloc.NONE
                             && tableauDeJeu[blocActifY[2], blocActifX[2]] == TypeBloc.NONE
                             && tableauDeJeu[blocActifY[3], blocActifX[3]] == TypeBloc.NONE)
            {
                GenererBloc();
            }
            else
            {
                MessageBox.Show("souhaiter vous recommencer une partie ?","Oups partie terminé",MessageBoxButtons.RetryCancel,MessageBoxIcon.Exclamation);
                if (DialogResult == DialogResult.Retry)
                {
                    InitialiserJeu();
                }
                if (DialogResult == DialogResult.Cancel)
                {
                    Application.Exit();
                }
            }
            DessinerTableDeJeu();
            timerPourDescenteDuJeu.Start();
           
        }
        //felix
        /// <summary>
        /// la fonction modifie les coordoné du "point principale" a partir du quel le reste du bloque est affiché
        /// </summary>
        /// <param name="sensDuDeplacement">direction dans le quel va se deplacer le bloque ou tourné</param>
        private void DeplacerJoueur(Deplacement sensDuDeplacement)
        {
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
                    if(cadreActif == 4)
                    {
                        cadreActif = 1;
                    }
                    else
                    {
                        cadreActif += 1;
                    }
                    for (int i = 0; i < blocActifX.Length; i++)
                    {
                        
                        memoire = blocActifX[i];
                        blocActifX[i] = blocActifY[i];
                        blocActifY[i] = memoire;
                        if (blocActifY[i] != 0)
                        {
                            blocActifY[i] = blocActifY[i] * -1;
                        }
                        if (cadreActif==1)
                        {
                            
                        }
                        else if (cadreActif==2)
                        {

                        }
                        else if (cadreActif==3)
                        {

                        }
                        else if (cadreActif==4)
                        {

                        }
                    }
                }
                else if (sensDuDeplacement ==Deplacement.ROTATE_COUNTERCLOCKWISE)
                {
                    for (int i = 0; i < blocActifX.Length; i++)
                    {
                        memoire = blocActifX[i];
                        blocActifX[i] = blocActifY[i];
                        blocActifY[i] = memoire;
                        if (blocActifY[i] != 0)
                        {
                            blocActifY[i] = blocActifY[i] * -1;
                        }
                        if (blocActifY[i] < 0)
                        {
                      //      blocActifY[i] -= blocActifY[i] * 2;
                        }
                    }
                }
                DessinerTableDeJeu();
            
            }
                 
     
        }
        //Samuel
        /// <summary>
        /// Cette méthode prend en compte les touches entrées par le joueur et retourne des déplacement si les bonnes touches sont appuyées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToucheApuye_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (e.KeyChar == 'a'  )
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
                    DeplacerJoueur(Deplacement.DOWN);
                }
         
        }
        //Samuel
        /// <summary>
        /// Cette méthode est un timer qui permet de faire descendre le cube en un temps donné en MilliSecondes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FaireDescendreCubeDeJeu_TimerTick(object sender, EventArgs e)
        {
            
            pointsString = pointage.ToString();
            score.Text = "score :" + pointsString;
            DeplacerJoueur(Deplacement.DOWN);
            
            
        }
        //felix.b
        /// <summary>
        /// Cette méthode permet de choisir une  forme à générer au hasard
        /// et contient ses formes
        /// </summary>
        /// <param name="ligneCourante"></param>
        /// <param name="coloneCourant"></param>
        /// <param name="formeDuBloc"></param>
        void GenererBloc()
        {
            coloneCourante = 0;
            ligneCourante = 0;
           
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
             
                blocActifX[0] =1;
                blocActifY[0] =0;

                blocActifX[1] =1;
                blocActifY[1] =1;

                blocActifX[2] =1;
                blocActifY[2] =2;

                blocActifX[3] =0;
                blocActifY[3] =2;

            
            }
            else if (formeDuBloc == TypeBloc.L)
            {
                blocActifX[0] =0;
                blocActifY[0] =0;

                blocActifX[1] =0;
                blocActifY[1] =1;

                blocActifX[2] =0;
                blocActifY[2] =2;

                blocActifX[3] =1;
                blocActifY[3] =2;
             

            }
            else if (formeDuBloc == TypeBloc.LINE)//La ligne est coucher
            {
                blocActifX[0] =0;
                blocActifY[0] =0;

                blocActifX[1] =1;
                blocActifY[1] =0;

                blocActifX[2] =2;
                blocActifY[2] =0;

                blocActifX[3] =3;
                blocActifY[3] =0;
             
            }
            else if (formeDuBloc == TypeBloc.S)
            {
                blocActifX[0] =2;
                blocActifY[0] =0;

                blocActifX[1] =1;
                blocActifY[1] =0;

                blocActifX[2] =1;
                blocActifY[2] =1;

                blocActifX[3] =0;
                blocActifY[3] =1;
             
            }
            else if (formeDuBloc == TypeBloc.SQUARE)
            {
                blocActifX[0] =0;
                blocActifY[0] =0;

                blocActifX[1] =1;
                blocActifY[1] =0;

                blocActifX[2] =0;
                blocActifY[2] =1;

                blocActifX[3] =1;
                blocActifY[3] =1;
               
            }
            else if (formeDuBloc == TypeBloc.T)
            {
                blocActifX[0] =0;
                blocActifY[0] =0;

                blocActifX[1] =1;
                blocActifY[1] =0;

                blocActifX[2] =2;
                blocActifY[2] =0;

                blocActifX[3] =1;
                blocActifY[3] =1;
             
            }
            else if (formeDuBloc == TypeBloc.Z)
            {
                blocActifX[0] =0;
                blocActifY[0] =0;

                blocActifX[1] =1;
                blocActifY[1] =0;

                blocActifX[2] =1;
                blocActifY[2] =1;

                blocActifX[3] =2;
                blocActifY[3] =1;
            
            }
            if (tableauDeJeu[blocActifY[0], blocActifX[0]] != TypeBloc.NONE
                              && tableauDeJeu[blocActifY[1], blocActifX[1]] != TypeBloc.NONE
                              && tableauDeJeu[blocActifY[2], blocActifX[2]] != TypeBloc.NONE
                              && tableauDeJeu[blocActifY[3], blocActifX[3]] != TypeBloc.NONE)
            {
                timerPourDescenteDuJeu.Stop();
                MessageBox.Show("souhaiter vous recommencer une partie ?", "Oups partie terminé", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation);
                
            }



        }
        //Felix.b
        /// <summary>
        /// PErmet de valider si il y a une ligne pleine dans le jeu et d'y mettre le score en conséquence 
        /// </summary>
        void VerifierSiLigneComplette()
        {
            for(int i =0; i < etatDesLignesPleines.Length ; i++)
            {
                etatDesLignesPleines[i] = true;
            }
            for (int i = 0; i < tableauDeJeu.GetLength(0); i++)
            {

                for (int j = 0; j < tableauDeJeu.GetLength(1); j++)
                {
                    if (tableauDeJeu[i, j] != TypeBloc.FROZEN)
                    {
                        etatDesLignesPleines[i] = false;
                    }
                }
            }
            //vider les lignes pleines
            DecalerLignes();
         }
        //Felix
        /// <summary>
        /// PErmet de déclarer l'état des lignes
        /// </summary>
        void DecalerLignes()
        {
            for (int i = 1; i < 20 ; i++)
            {
                if (etatDesLignesPleines[i] == true)
                {
                    
                    // de 1 donner des points et ajuster le niveau de difficulté si requis
                    pointage += 100;
                    nombreDeLigneEffacerNiveau += 1;
                    if(nombreDeLigneEffacerNiveau >= 5)
                    {
                        nombreDeLigneEffacerNiveau = 0;
                        if (timerPourDescenteDuJeu.Interval > 200)
                        {
                            niveau += 1;
                            string difficulte = niveau.ToString();
                            timerPourDescenteDuJeu.Interval -= 200;
                            affichageDuNiveau.Text = "niveau : " + difficulte;
                        }
                    }
                    
                    //vider la ligne pleine
                    for (int j = 0; j < tableauDeJeu.GetLength(1); j++)
                    {
                        tableauDeJeu[i, j] = TypeBloc.NONE;

                    }
                    //decaler les autres lignes supérieurs
                    for(int a =i; a > 0; a--)
                    {
                        for(int b = 0; b < 10;b++)
                        {
                            tableauDeJeu[a, b] = tableauDeJeu[a - 1, b];
                        }
                        
                    }
                    //vider la premiere ligne
                    for(int j = 0; j<10;j++)
                    {
                        tableauDeJeu[0, j] = TypeBloc.NONE;
                    }
                }
            }
        }
        //Felix et Samuel
        /// <summary>
        /// Permet de quitter le jeu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}