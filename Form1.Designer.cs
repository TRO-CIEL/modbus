namespace modbus
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelAdresseIP = new System.Windows.Forms.Label();
            this.textBoxAdresseIP = new System.Windows.Forms.TextBox();
            this.buttonConnexion = new System.Windows.Forms.Button();
            this.buttonDeconnexion = new System.Windows.Forms.Button();
            this.labelTensionMoteur = new System.Windows.Forms.Label();
            this.buttonLire = new System.Windows.Forms.Button();
            this.textBoxTension = new System.Windows.Forms.TextBox();
            this.textBoxStatut = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            
            // labelAdresseIP - étiquette
            this.labelAdresseIP.AutoSize = true;
            this.labelAdresseIP.Location = new System.Drawing.Point(12, 15);
            this.labelAdresseIP.Name = "labelAdresseIP";
            this.labelAdresseIP.Size = new System.Drawing.Size(65, 13);
            this.labelAdresseIP.TabIndex = 0;
            this.labelAdresseIP.Text = "Ip serveur :";
            
            // textBoxAdresseIP - saisie
            this.textBoxAdresseIP.Location = new System.Drawing.Point(83, 12);
            this.textBoxAdresseIP.Name = "textBoxAdresseIP";
            this.textBoxAdresseIP.Size = new System.Drawing.Size(100, 20);
            this.textBoxAdresseIP.TabIndex = 1;
            this.textBoxAdresseIP.Text = "172.17.50.180";
            
            // buttonConnexion - bouton
            this.buttonConnexion.Location = new System.Drawing.Point(200, 10);
            this.buttonConnexion.Name = "buttonConnexion";
            this.buttonConnexion.Size = new System.Drawing.Size(75, 23);
            this.buttonConnexion.TabIndex = 2;
            this.buttonConnexion.Text = "Connexion";
            this.buttonConnexion.UseVisualStyleBackColor = true;
            this.buttonConnexion.Click += new System.EventHandler(this.buttonConnexion_Click);
            
            // buttonDeconnexion - bouton
            this.buttonDeconnexion.Location = new System.Drawing.Point(290, 10);
            this.buttonDeconnexion.Name = "buttonDeconnexion";
            this.buttonDeconnexion.Size = new System.Drawing.Size(85, 23);
            this.buttonDeconnexion.TabIndex = 3;
            this.buttonDeconnexion.Text = "Deconnexion";
            this.buttonDeconnexion.UseVisualStyleBackColor = true;
            this.buttonDeconnexion.Click += new System.EventHandler(this.buttonDeconnexion_Click);
            
            // labelTensionMoteur - étiquette
            this.labelTensionMoteur.AutoSize = true;
            this.labelTensionMoteur.Location = new System.Drawing.Point(335, 120);
            this.labelTensionMoteur.Name = "labelTensionMoteur";
            this.labelTensionMoteur.Size = new System.Drawing.Size(110, 13);
            this.labelTensionMoteur.TabIndex = 4;
            this.labelTensionMoteur.Text = "Tension moteur en Volt";
            
            // buttonLire - bouton
            this.buttonLire.Location = new System.Drawing.Point(340, 145);
            this.buttonLire.Name = "buttonLire";
            this.buttonLire.Size = new System.Drawing.Size(50, 23);
            this.buttonLire.TabIndex = 5;
            this.buttonLire.Text = "Lire";
            this.buttonLire.UseVisualStyleBackColor = true;
            this.buttonLire.Click += new System.EventHandler(this.buttonLire_Click);
            
            // textBoxTension - affichage
            this.textBoxTension.Location = new System.Drawing.Point(400, 145);
            this.textBoxTension.Name = "textBoxTension";
            this.textBoxTension.ReadOnly = true;
            this.textBoxTension.Size = new System.Drawing.Size(80, 20);
            this.textBoxTension.TabIndex = 6;
            this.textBoxTension.BackColor = System.Drawing.Color.Blue;
            this.textBoxTension.ForeColor = System.Drawing.Color.White;
            this.textBoxTension.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxTension.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            
            // textBoxStatut - affichage
            this.textBoxStatut.Location = new System.Drawing.Point(520, 50);
            this.textBoxStatut.Multiline = true;
            this.textBoxStatut.Name = "textBoxStatut";
            this.textBoxStatut.ReadOnly = true;
            this.textBoxStatut.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxStatut.Size = new System.Drawing.Size(268, 388);
            this.textBoxStatut.TabIndex = 7;
            
            // Form1 - fenêtre
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBoxStatut);
            this.Controls.Add(this.textBoxTension);
            this.Controls.Add(this.buttonLire);
            this.Controls.Add(this.labelTensionMoteur);
            this.Controls.Add(this.buttonDeconnexion);
            this.Controls.Add(this.buttonConnexion);
            this.Controls.Add(this.textBoxAdresseIP);
            this.Controls.Add(this.labelAdresseIP);
            this.Name = "Form1";
            this.Text = "Barrière Modbus";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
        
        private System.Windows.Forms.Label labelAdresseIP;
        private System.Windows.Forms.TextBox textBoxAdresseIP;
        private System.Windows.Forms.Button buttonConnexion;
        private System.Windows.Forms.Button buttonDeconnexion;
        private System.Windows.Forms.Label labelTensionMoteur;
        private System.Windows.Forms.Button buttonLire;
        private System.Windows.Forms.TextBox textBoxTension;
        private System.Windows.Forms.TextBox textBoxStatut;
    }
}

