namespace Pokemon
{
    partial class Pokedex
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pokedex));
            this.PBCartas = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PBCartas)).BeginInit();
            this.SuspendLayout();
            // 
            // PBCartas
            // 
            this.PBCartas.Location = new System.Drawing.Point(0, 0);
            this.PBCartas.Name = "PBCartas";
            this.PBCartas.Size = new System.Drawing.Size(568, 804);
            this.PBCartas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PBCartas.TabIndex = 0;
            this.PBCartas.TabStop = false;
            // 
            // Pokedex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(568, 804);
            this.Controls.Add(this.PBCartas);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Pokedex";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pokedex";
            ((System.ComponentModel.ISupportInitialize)(this.PBCartas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox PBCartas;
    }
}