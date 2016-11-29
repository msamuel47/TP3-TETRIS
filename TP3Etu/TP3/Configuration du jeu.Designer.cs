namespace TP3
{
    partial class Configuration_du_jeu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nbLignes_TrackBar = new System.Windows.Forms.TrackBar();
            this.nbColones_TrackBar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nbLignes_TextBox = new System.Windows.Forms.TextBox();
            this.nbColones_TextBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbLignes_TrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbColones_TrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nbColones_TextBox);
            this.groupBox1.Controls.Add(this.nbLignes_TextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.nbColones_TrackBar);
            this.groupBox1.Controls.Add(this.nbLignes_TrackBar);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 120);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Grandeur de la table";
            // 
            // nbLignes_TrackBar
            // 
            this.nbLignes_TrackBar.Location = new System.Drawing.Point(90, 19);
            this.nbLignes_TrackBar.Maximum = 30;
            this.nbLignes_TrackBar.Minimum = 10;
            this.nbLignes_TrackBar.Name = "nbLignes_TrackBar";
            this.nbLignes_TrackBar.Size = new System.Drawing.Size(104, 45);
            this.nbLignes_TrackBar.TabIndex = 0;
            this.nbLignes_TrackBar.Value = 10;
            this.nbLignes_TrackBar.Scroll += new System.EventHandler(this.ChangerLaTextBoxNbLignes_Scoll);
            // 
            // nbColones_TrackBar
            // 
            this.nbColones_TrackBar.Location = new System.Drawing.Point(90, 69);
            this.nbColones_TrackBar.Maximum = 20;
            this.nbColones_TrackBar.Minimum = 4;
            this.nbColones_TrackBar.Name = "nbColones_TrackBar";
            this.nbColones_TrackBar.Size = new System.Drawing.Size(104, 45);
            this.nbColones_TrackBar.TabIndex = 1;
            this.nbColones_TrackBar.Value = 4;
            this.nbColones_TrackBar.Scroll += new System.EventHandler(this.ChangerLeNombreDeColones_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nombre de lignes :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Nb de colones      :";
            // 
            // nbLignes_TextBox
            // 
            this.nbLignes_TextBox.Enabled = false;
            this.nbLignes_TextBox.Location = new System.Drawing.Point(4, 44);
            this.nbLignes_TextBox.Name = "nbLignes_TextBox";
            this.nbLignes_TextBox.Size = new System.Drawing.Size(29, 20);
            this.nbLignes_TextBox.TabIndex = 4;
            // 
            // nbColones_TextBox
            // 
            this.nbColones_TextBox.Enabled = false;
            this.nbColones_TextBox.Location = new System.Drawing.Point(4, 94);
            this.nbColones_TextBox.Name = "nbColones_TextBox";
            this.nbColones_TextBox.Size = new System.Drawing.Size(29, 20);
            this.nbColones_TextBox.TabIndex = 5;
            // 
            // Configuration_du_jeu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 297);
            this.Controls.Add(this.groupBox1);
            this.Name = "Configuration_du_jeu";
            this.Text = "Configuration_du_jeu";
            this.Load += new System.EventHandler(this.InitialiserConfig_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbLignes_TrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbColones_TrackBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nbLignes_TextBox;
        public System.Windows.Forms.TrackBar nbColones_TrackBar;
        public System.Windows.Forms.TrackBar nbLignes_TrackBar;
        private System.Windows.Forms.TextBox nbColones_TextBox;
    }
}