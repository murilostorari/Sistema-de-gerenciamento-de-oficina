
namespace TCC
{
    partial class Home
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            this.Icone = new System.Windows.Forms.PictureBox();
            this.Endereco = new System.Windows.Forms.Label();
            this.Fone = new System.Windows.Forms.Label();
            this.Aberto = new System.Windows.Forms.Label();
            this.Nome = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Icone)).BeginInit();
            this.SuspendLayout();
            // 
            // Icone
            // 
            this.Icone.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Icone.BackColor = System.Drawing.Color.Transparent;
            this.Icone.Image = ((System.Drawing.Image)(resources.GetObject("Icone.Image")));
            this.Icone.Location = new System.Drawing.Point(123, 203);
            this.Icone.Margin = new System.Windows.Forms.Padding(0);
            this.Icone.Name = "Icone";
            this.Icone.Size = new System.Drawing.Size(330, 330);
            this.Icone.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Icone.TabIndex = 35;
            this.Icone.TabStop = false;
            // 
            // Endereco
            // 
            this.Endereco.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Endereco.BackColor = System.Drawing.Color.Transparent;
            this.Endereco.Font = new System.Drawing.Font("Segoe UI Semibold", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Endereco.Location = new System.Drawing.Point(500, 355);
            this.Endereco.Margin = new System.Windows.Forms.Padding(0);
            this.Endereco.Name = "Endereco";
            this.Endereco.Size = new System.Drawing.Size(357, 80);
            this.Endereco.TabIndex = 34;
            this.Endereco.Text = "Rua Lucélia - Colônia Fepasa - Flórida Paulista";
            // 
            // Fone
            // 
            this.Fone.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Fone.BackColor = System.Drawing.Color.Transparent;
            this.Fone.Font = new System.Drawing.Font("Segoe UI Semibold", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Fone.Location = new System.Drawing.Point(500, 305);
            this.Fone.Margin = new System.Windows.Forms.Padding(0);
            this.Fone.Name = "Fone";
            this.Fone.Size = new System.Drawing.Size(357, 40);
            this.Fone.TabIndex = 33;
            this.Fone.Text = "Fone: (18) 99728-4652";
            // 
            // Aberto
            // 
            this.Aberto.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Aberto.BackColor = System.Drawing.Color.Transparent;
            this.Aberto.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Aberto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(81)))), ((int)(((byte)(81)))));
            this.Aberto.Location = new System.Drawing.Point(503, 445);
            this.Aberto.Margin = new System.Windows.Forms.Padding(0);
            this.Aberto.Name = "Aberto";
            this.Aberto.Size = new System.Drawing.Size(224, 60);
            this.Aberto.TabIndex = 32;
            this.Aberto.Text = "Aberto de Segunda a Sábado\r\ndas 08:00 às 16:00";
            // 
            // Nome
            // 
            this.Nome.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Nome.BackColor = System.Drawing.Color.Transparent;
            this.Nome.Font = new System.Drawing.Font("Segoe UI Semibold", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Nome.Location = new System.Drawing.Point(500, 255);
            this.Nome.Margin = new System.Windows.Forms.Padding(0);
            this.Nome.Name = "Nome";
            this.Nome.Size = new System.Drawing.Size(357, 40);
            this.Nome.TabIndex = 31;
            this.Nome.Text = "Clínica Car";
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(957, 736);
            this.Controls.Add(this.Icone);
            this.Controls.Add(this.Endereco);
            this.Controls.Add(this.Fone);
            this.Controls.Add(this.Aberto);
            this.Controls.Add(this.Nome);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Home";
            ((System.ComponentModel.ISupportInitialize)(this.Icone)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Icone;
        private System.Windows.Forms.Label Endereco;
        private System.Windows.Forms.Label Fone;
        private System.Windows.Forms.Label Aberto;
        private System.Windows.Forms.Label Nome;
    }
}