namespace Nottext_Antivirus
{
    partial class Windows
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Windows));
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.back = new System.Windows.Forms.PictureBox();
            this.restorePoint = new Guna.UI2.WinForms.Guna2CustomCheckBox();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label12 = new System.Windows.Forms.Label();
            this.vulnerability = new Guna.UI2.WinForms.Guna2CustomCheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.back)).BeginInit();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Open Sans", 12F);
            this.label3.Location = new System.Drawing.Point(75, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(760, 54);
            this.label3.TabIndex = 34;
            this.label3.Text = "Sempre que possível, o Nottext Antivirus cria um ponto de restauração, para que v" +
    "ocê possa restaurar o computador caso ocorra alguma falha grave no sistema ou al" +
    "gum erro inesperado.";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Shannon Std", 15.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(74, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 26);
            this.label1.TabIndex = 32;
            this.label1.Text = "Ponto de restauração:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // back
            // 
            this.back.BackColor = System.Drawing.Color.Transparent;
            this.back.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("back.BackgroundImage")));
            this.back.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.back.Cursor = System.Windows.Forms.Cursors.Hand;
            this.back.Location = new System.Drawing.Point(12, 12);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(47, 38);
            this.back.TabIndex = 35;
            this.back.TabStop = false;
            this.back.Click += new System.EventHandler(this.back_Click);
            // 
            // restorePoint
            // 
            this.restorePoint.Animated = true;
            this.restorePoint.Checked = true;
            this.restorePoint.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.restorePoint.CheckedState.BorderRadius = 2;
            this.restorePoint.CheckedState.BorderThickness = 0;
            this.restorePoint.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.restorePoint.CheckedState.Parent = this.restorePoint;
            this.restorePoint.CheckState = System.Windows.Forms.CheckState.Checked;
            this.restorePoint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.restorePoint.Location = new System.Drawing.Point(296, 142);
            this.restorePoint.Name = "restorePoint";
            this.restorePoint.ShadowDecoration.Enabled = true;
            this.restorePoint.ShadowDecoration.Parent = this.restorePoint;
            this.restorePoint.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(2);
            this.restorePoint.Size = new System.Drawing.Size(25, 25);
            this.restorePoint.TabIndex = 55;
            this.restorePoint.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.restorePoint.UncheckedState.BorderRadius = 2;
            this.restorePoint.UncheckedState.BorderThickness = 0;
            this.restorePoint.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.restorePoint.UncheckedState.Parent = this.restorePoint;
            this.restorePoint.Click += new System.EventHandler(this.restorePoint_Click);
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.White;
            this.guna2Panel1.BorderColor = System.Drawing.Color.Gainsboro;
            this.guna2Panel1.BorderRadius = 2;
            this.guna2Panel1.BorderThickness = 2;
            this.guna2Panel1.Controls.Add(this.label11);
            this.guna2Panel1.Controls.Add(this.pictureBox2);
            this.guna2Panel1.Controls.Add(this.label12);
            this.guna2Panel1.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.guna2Panel1.Location = new System.Drawing.Point(79, 50);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.ShadowDecoration.Parent = this.guna2Panel1;
            this.guna2Panel1.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.guna2Panel1.Size = new System.Drawing.Size(756, 71);
            this.guna2Panel1.TabIndex = 62;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Open Sans", 12F);
            this.label11.Location = new System.Drawing.Point(64, 36);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(293, 22);
            this.label11.TabIndex = 38;
            this.label11.Text = "Proteções adicionais para o Windows";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox2.Location = new System.Drawing.Point(6, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(53, 65);
            this.pictureBox2.TabIndex = 37;
            this.pictureBox2.TabStop = false;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Open Sans", 15.75F);
            this.label12.Location = new System.Drawing.Point(64, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(246, 28);
            this.label12.TabIndex = 37;
            this.label12.Text = "Proteção para Windows";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // vulnerability
            // 
            this.vulnerability.Animated = true;
            this.vulnerability.Checked = true;
            this.vulnerability.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.vulnerability.CheckedState.BorderRadius = 2;
            this.vulnerability.CheckedState.BorderThickness = 0;
            this.vulnerability.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.vulnerability.CheckedState.Parent = this.vulnerability;
            this.vulnerability.CheckState = System.Windows.Forms.CheckState.Checked;
            this.vulnerability.Cursor = System.Windows.Forms.Cursors.Hand;
            this.vulnerability.Location = new System.Drawing.Point(441, 247);
            this.vulnerability.Name = "vulnerability";
            this.vulnerability.ShadowDecoration.Enabled = true;
            this.vulnerability.ShadowDecoration.Parent = this.vulnerability;
            this.vulnerability.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(2);
            this.vulnerability.Size = new System.Drawing.Size(25, 25);
            this.vulnerability.TabIndex = 65;
            this.vulnerability.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.vulnerability.UncheckedState.BorderRadius = 2;
            this.vulnerability.UncheckedState.BorderThickness = 0;
            this.vulnerability.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.vulnerability.UncheckedState.Parent = this.vulnerability;
            this.vulnerability.Click += new System.EventHandler(this.vulnerability_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Open Sans", 12F);
            this.label2.Location = new System.Drawing.Point(75, 275);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(748, 54);
            this.label2.TabIndex = 64;
            this.label2.Text = "Procura por aplicativos potencialmente prejudiciais que podem comprometer sua seg" +
    "urança, além de corrigir falhas e erros no sistema.";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Shannon Std", 15.75F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(74, 247);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(361, 26);
            this.label4.TabIndex = 63;
            this.label4.Text = "Proteção de vulnerabilidades (BETA):";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Windows
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(878, 501);
            this.Controls.Add(this.vulnerability);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.restorePoint);
            this.Controls.Add(this.back);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Windows";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Windows";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.back)).EndInit();
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox back;
        public Guna.UI2.WinForms.Guna2CustomCheckBox restorePoint;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label12;
        public Guna.UI2.WinForms.Guna2CustomCheckBox vulnerability;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
    }
}