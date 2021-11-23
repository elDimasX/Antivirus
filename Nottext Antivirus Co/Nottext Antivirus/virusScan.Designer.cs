namespace Nottext_Antivirus
{
    partial class virusScan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(virusScan));
            this.waitLabel = new System.Windows.Forms.Label();
            this.progress = new Guna.UI2.WinForms.Guna2ProgressBar();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label12 = new System.Windows.Forms.Label();
            this.playScan = new Guna.UI2.WinForms.Guna2Button();
            this.verificaoPersonalizada = new Guna.UI2.WinForms.Guna2Button();
            this.verificaoCompleta = new Guna.UI2.WinForms.Guna2Button();
            this.verificaoRapida = new Guna.UI2.WinForms.Guna2Button();
            this.back = new System.Windows.Forms.PictureBox();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.back)).BeginInit();
            this.SuspendLayout();
            // 
            // waitLabel
            // 
            this.waitLabel.Font = new System.Drawing.Font("Open Sans", 12F);
            this.waitLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.waitLabel.Location = new System.Drawing.Point(143, 383);
            this.waitLabel.Name = "waitLabel";
            this.waitLabel.Size = new System.Drawing.Size(653, 25);
            this.waitLabel.TabIndex = 54;
            this.waitLabel.Text = "Verificação em andamento..";
            this.waitLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.waitLabel.Visible = false;
            // 
            // progress
            // 
            this.progress.BackColor = System.Drawing.Color.Transparent;
            this.progress.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.progress.BorderRadius = 2;
            this.progress.BorderThickness = 2;
            this.progress.FillColor = System.Drawing.Color.Gainsboro;
            this.progress.ForeColor = System.Drawing.Color.White;
            this.progress.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.progress.Location = new System.Drawing.Point(79, 429);
            this.progress.Name = "progress";
            this.progress.ProgressBrushMode = Guna.UI2.WinForms.Enums.BrushMode.Solid;
            this.progress.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.progress.ProgressColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.progress.ShadowDecoration.Enabled = true;
            this.progress.ShadowDecoration.Parent = this.progress;
            this.progress.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(3);
            this.progress.Size = new System.Drawing.Size(756, 30);
            this.progress.TabIndex = 60;
            this.progress.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.progress.Visible = false;
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
            this.guna2Panel1.TabIndex = 61;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Open Sans", 12F);
            this.label11.Location = new System.Drawing.Point(64, 36);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(414, 22);
            this.label11.TabIndex = 38;
            this.label11.Text = "Faça uma busca no computador para encontrar vírus";
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
            this.label12.Size = new System.Drawing.Size(193, 28);
            this.label12.TabIndex = 37;
            this.label12.Text = "Procurar por vírus";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // playScan
            // 
            this.playScan.Animated = true;
            this.playScan.AutoRoundedCorners = true;
            this.playScan.BackColor = System.Drawing.Color.Transparent;
            this.playScan.BorderColor = System.Drawing.Color.White;
            this.playScan.BorderRadius = 27;
            this.playScan.BorderThickness = 1;
            this.playScan.CheckedState.Parent = this.playScan;
            this.playScan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.playScan.CustomImages.Parent = this.playScan;
            this.playScan.FillColor = System.Drawing.Color.White;
            this.playScan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.playScan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.playScan.HoverState.BorderColor = System.Drawing.Color.White;
            this.playScan.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.playScan.HoverState.Parent = this.playScan;
            this.playScan.Image = global::Nottext_Antivirus.Properties.Resources.play_button;
            this.playScan.ImageSize = new System.Drawing.Size(55, 55);
            this.playScan.Location = new System.Drawing.Point(79, 367);
            this.playScan.Name = "playScan";
            this.playScan.ShadowDecoration.Parent = this.playScan;
            this.playScan.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(3);
            this.playScan.Size = new System.Drawing.Size(58, 56);
            this.playScan.TabIndex = 59;
            this.playScan.Visible = false;
            this.playScan.Click += new System.EventHandler(this.play_Click);
            // 
            // verificaoPersonalizada
            // 
            this.verificaoPersonalizada.BackColor = System.Drawing.Color.Transparent;
            this.verificaoPersonalizada.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.verificaoPersonalizada.BorderRadius = 2;
            this.verificaoPersonalizada.BorderThickness = 1;
            this.verificaoPersonalizada.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.verificaoPersonalizada.CheckedState.Image = ((System.Drawing.Image)(resources.GetObject("verificaoPersonalizada.CheckedState.Image")));
            this.verificaoPersonalizada.CheckedState.Parent = this.verificaoPersonalizada;
            this.verificaoPersonalizada.Cursor = System.Windows.Forms.Cursors.Hand;
            this.verificaoPersonalizada.CustomImages.Parent = this.verificaoPersonalizada;
            this.verificaoPersonalizada.FillColor = System.Drawing.Color.White;
            this.verificaoPersonalizada.Font = new System.Drawing.Font("Open Sans", 12F);
            this.verificaoPersonalizada.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.verificaoPersonalizada.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.verificaoPersonalizada.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.verificaoPersonalizada.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.verificaoPersonalizada.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("verificaoPersonalizada.HoverState.Image")));
            this.verificaoPersonalizada.HoverState.Parent = this.verificaoPersonalizada;
            this.verificaoPersonalizada.Image = ((System.Drawing.Image)(resources.GetObject("verificaoPersonalizada.Image")));
            this.verificaoPersonalizada.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.verificaoPersonalizada.ImageSize = new System.Drawing.Size(48, 48);
            this.verificaoPersonalizada.Location = new System.Drawing.Point(79, 296);
            this.verificaoPersonalizada.Name = "verificaoPersonalizada";
            this.verificaoPersonalizada.ShadowDecoration.BorderRadius = 5;
            this.verificaoPersonalizada.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.verificaoPersonalizada.ShadowDecoration.Depth = 40;
            this.verificaoPersonalizada.ShadowDecoration.Parent = this.verificaoPersonalizada;
            this.verificaoPersonalizada.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(7, 7, 7, 12);
            this.verificaoPersonalizada.Size = new System.Drawing.Size(756, 56);
            this.verificaoPersonalizada.TabIndex = 58;
            this.verificaoPersonalizada.Text = "VERIFICAÇÃO PERSONALIZADA";
            this.verificaoPersonalizada.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.verificaoPersonalizada.Click += new System.EventHandler(this.adicionar_Click);
            this.verificaoPersonalizada.MouseEnter += new System.EventHandler(this.ativarEfeito1_MouseHover);
            this.verificaoPersonalizada.MouseLeave += new System.EventHandler(this.desativarEfeito1_MouseLeave);
            // 
            // verificaoCompleta
            // 
            this.verificaoCompleta.BackColor = System.Drawing.Color.Transparent;
            this.verificaoCompleta.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.verificaoCompleta.BorderRadius = 2;
            this.verificaoCompleta.BorderThickness = 1;
            this.verificaoCompleta.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.verificaoCompleta.CheckedState.Image = ((System.Drawing.Image)(resources.GetObject("verificaoCompleta.CheckedState.Image")));
            this.verificaoCompleta.CheckedState.Parent = this.verificaoCompleta;
            this.verificaoCompleta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.verificaoCompleta.CustomImages.Parent = this.verificaoCompleta;
            this.verificaoCompleta.FillColor = System.Drawing.Color.White;
            this.verificaoCompleta.Font = new System.Drawing.Font("Open Sans", 12F);
            this.verificaoCompleta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.verificaoCompleta.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.verificaoCompleta.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.verificaoCompleta.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.verificaoCompleta.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("verificaoCompleta.HoverState.Image")));
            this.verificaoCompleta.HoverState.Parent = this.verificaoCompleta;
            this.verificaoCompleta.Image = ((System.Drawing.Image)(resources.GetObject("verificaoCompleta.Image")));
            this.verificaoCompleta.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.verificaoCompleta.ImageSize = new System.Drawing.Size(48, 48);
            this.verificaoCompleta.Location = new System.Drawing.Point(79, 218);
            this.verificaoCompleta.Name = "verificaoCompleta";
            this.verificaoCompleta.ShadowDecoration.BorderRadius = 5;
            this.verificaoCompleta.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.verificaoCompleta.ShadowDecoration.Depth = 40;
            this.verificaoCompleta.ShadowDecoration.Parent = this.verificaoCompleta;
            this.verificaoCompleta.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(7, 7, 7, 12);
            this.verificaoCompleta.Size = new System.Drawing.Size(756, 56);
            this.verificaoCompleta.TabIndex = 57;
            this.verificaoCompleta.Text = "VERIFICAÇÃO COMPLETA";
            this.verificaoCompleta.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.verificaoCompleta.Click += new System.EventHandler(this.verificacoesBotoes_Click);
            this.verificaoCompleta.MouseEnter += new System.EventHandler(this.ativarEfeito1_MouseHover);
            this.verificaoCompleta.MouseLeave += new System.EventHandler(this.desativarEfeito1_MouseLeave);
            // 
            // verificaoRapida
            // 
            this.verificaoRapida.BackColor = System.Drawing.Color.Transparent;
            this.verificaoRapida.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.verificaoRapida.BorderRadius = 2;
            this.verificaoRapida.BorderThickness = 1;
            this.verificaoRapida.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.verificaoRapida.CheckedState.Image = ((System.Drawing.Image)(resources.GetObject("verificaoRapida.CheckedState.Image")));
            this.verificaoRapida.CheckedState.Parent = this.verificaoRapida;
            this.verificaoRapida.Cursor = System.Windows.Forms.Cursors.Hand;
            this.verificaoRapida.CustomImages.Parent = this.verificaoRapida;
            this.verificaoRapida.FillColor = System.Drawing.Color.White;
            this.verificaoRapida.Font = new System.Drawing.Font("Open Sans", 12F);
            this.verificaoRapida.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.verificaoRapida.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.verificaoRapida.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.verificaoRapida.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.verificaoRapida.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("verificaoRapida.HoverState.Image")));
            this.verificaoRapida.HoverState.Parent = this.verificaoRapida;
            this.verificaoRapida.Image = ((System.Drawing.Image)(resources.GetObject("verificaoRapida.Image")));
            this.verificaoRapida.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.verificaoRapida.ImageSize = new System.Drawing.Size(48, 48);
            this.verificaoRapida.Location = new System.Drawing.Point(79, 141);
            this.verificaoRapida.Name = "verificaoRapida";
            this.verificaoRapida.ShadowDecoration.BorderRadius = 5;
            this.verificaoRapida.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.verificaoRapida.ShadowDecoration.Depth = 40;
            this.verificaoRapida.ShadowDecoration.Parent = this.verificaoRapida;
            this.verificaoRapida.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(7, 7, 7, 12);
            this.verificaoRapida.Size = new System.Drawing.Size(756, 56);
            this.verificaoRapida.TabIndex = 56;
            this.verificaoRapida.Text = "VERIFICAÇÃO RÁPIDA";
            this.verificaoRapida.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.verificaoRapida.Click += new System.EventHandler(this.verificacoesBotoes_Click);
            this.verificaoRapida.MouseEnter += new System.EventHandler(this.ativarEfeito1_MouseHover);
            this.verificaoRapida.MouseLeave += new System.EventHandler(this.desativarEfeito1_MouseLeave);
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
            this.back.TabIndex = 39;
            this.back.TabStop = false;
            this.back.Click += new System.EventHandler(this.back_Click);
            // 
            // virusScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScrollMargin = new System.Drawing.Size(0, 30);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(878, 501);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.progress);
            this.Controls.Add(this.playScan);
            this.Controls.Add(this.verificaoPersonalizada);
            this.Controls.Add(this.verificaoCompleta);
            this.Controls.Add(this.verificaoRapida);
            this.Controls.Add(this.waitLabel);
            this.Controls.Add(this.back);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "virusScan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "virusScan";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.back)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox back;
        private System.Windows.Forms.Label waitLabel;
        private Guna.UI2.WinForms.Guna2Button verificaoRapida;
        private Guna.UI2.WinForms.Guna2Button verificaoCompleta;
        private Guna.UI2.WinForms.Guna2Button verificaoPersonalizada;
        private Guna.UI2.WinForms.Guna2Button playScan;
        private Guna.UI2.WinForms.Guna2ProgressBar progress;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label12;
    }
}