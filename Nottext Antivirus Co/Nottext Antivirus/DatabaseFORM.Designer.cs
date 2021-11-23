namespace Nottext_Antivirus
{
    partial class DatabaseFORM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseFORM));
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.loading = new Guna.UI2.WinForms.Guna2CircleProgressBar();
            this.opcoes = new Guna.UI2.WinForms.Guna2ComboBox();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label12 = new System.Windows.Forms.Label();
            this.update = new Guna.UI2.WinForms.Guna2Button();
            this.back = new System.Windows.Forms.PictureBox();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.back)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Open Sans", 12F);
            this.label5.Location = new System.Drawing.Point(351, 372);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 22);
            this.label5.TabIndex = 45;
            this.label5.Text = "Atualizando..";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Open Sans", 12F);
            this.label6.Location = new System.Drawing.Point(351, 415);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(334, 22);
            this.label6.TabIndex = 42;
            this.label6.Text = "O banco de dados foi atualizado com êxito.";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.Visible = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Open Sans", 12F);
            this.label2.Location = new System.Drawing.Point(76, 277);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(759, 69);
            this.label2.TabIndex = 41;
            this.label2.Text = "A atualização do banco de dados é necessário para um antivírus, estas atualizaçõe" +
    "s são necessárias para detectar os mais novos vírus que estão rodando na interne" +
    "t e proteger o seu computador.\r\n";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Open Sans", 12F);
            this.label3.Location = new System.Drawing.Point(76, 179);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(759, 58);
            this.label3.TabIndex = 40;
            this.label3.Text = "As opções de atualizações decide se você vai atualizar o banco de dados manualmen" +
    "te ou esperar até que o Nottext Antivirus atualize automaticamente para você.";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Shannon Std", 15.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(74, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(231, 26);
            this.label1.TabIndex = 38;
            this.label1.Text = "Opções de atualização:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // loading
            // 
            this.loading.Animated = true;
            this.loading.AnimationSpeed = 1.2F;
            this.loading.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.loading.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.loading.Location = new System.Drawing.Point(464, 366);
            this.loading.Name = "loading";
            this.loading.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.loading.ProgressColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.loading.ProgressThickness = 5;
            this.loading.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.loading.ShadowDecoration.Parent = this.loading;
            this.loading.Size = new System.Drawing.Size(40, 40);
            this.loading.TabIndex = 0;
            this.loading.Value = 75;
            this.loading.Visible = false;
            // 
            // opcoes
            // 
            this.opcoes.Animated = true;
            this.opcoes.BackColor = System.Drawing.Color.Transparent;
            this.opcoes.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.opcoes.BorderRadius = 2;
            this.opcoes.BorderThickness = 2;
            this.opcoes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.opcoes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.opcoes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.opcoes.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.opcoes.FocusedColor = System.Drawing.Color.Empty;
            this.opcoes.FocusedState.Parent = this.opcoes;
            this.opcoes.Font = new System.Drawing.Font("Open Sans", 12F);
            this.opcoes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.opcoes.FormattingEnabled = true;
            this.opcoes.HoverState.Parent = this.opcoes;
            this.opcoes.IntegralHeight = false;
            this.opcoes.ItemHeight = 30;
            this.opcoes.Items.AddRange(new object[] {
            "Automaticamente",
            "Manualmente"});
            this.opcoes.ItemsAppearance.Parent = this.opcoes;
            this.opcoes.Location = new System.Drawing.Point(311, 136);
            this.opcoes.Name = "opcoes";
            this.opcoes.ShadowDecoration.Parent = this.opcoes;
            this.opcoes.Size = new System.Drawing.Size(204, 36);
            this.opcoes.StartIndex = 0;
            this.opcoes.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;
            this.opcoes.TabIndex = 49;
            this.opcoes.SelectedIndexChanged += new System.EventHandler(this.opcoes_SelectedIndexChanged);
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
            this.guna2Panel1.TabIndex = 50;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Open Sans", 12F);
            this.label11.Location = new System.Drawing.Point(64, 36);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(592, 22);
            this.label11.TabIndex = 38;
            this.label11.Text = "Atualize o banco de dados do aplicativo ou procure uma versão mais recente";
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
            this.label12.Size = new System.Drawing.Size(170, 28);
            this.label12.TabIndex = 37;
            this.label12.Text = "Banco de dados";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // update
            // 
            this.update.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.update.BackColor = System.Drawing.Color.Transparent;
            this.update.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.update.BorderRadius = 5;
            this.update.BorderThickness = 1;
            this.update.CheckedState.Parent = this.update;
            this.update.Cursor = System.Windows.Forms.Cursors.Hand;
            this.update.CustomImages.Parent = this.update;
            this.update.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.update.Font = new System.Drawing.Font("Open Sans", 12F);
            this.update.ForeColor = System.Drawing.Color.White;
            this.update.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.update.HoverState.ForeColor = System.Drawing.Color.White;
            this.update.HoverState.Parent = this.update;
            this.update.Image = ((System.Drawing.Image)(resources.GetObject("update.Image")));
            this.update.ImageOffset = new System.Drawing.Point(-3, 0);
            this.update.ImageSize = new System.Drawing.Size(25, 25);
            this.update.Location = new System.Drawing.Point(80, 376);
            this.update.Name = "update";
            this.update.ShadowDecoration.BorderRadius = 5;
            this.update.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.update.ShadowDecoration.Depth = 40;
            this.update.ShadowDecoration.Parent = this.update;
            this.update.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(7, 7, 7, 12);
            this.update.Size = new System.Drawing.Size(253, 56);
            this.update.TabIndex = 48;
            this.update.Text = "ATUALIZAR";
            this.update.Click += new System.EventHandler(this.update_Click);
            this.update.MouseEnter += new System.EventHandler(this.ativarEfeito1_MouseHover);
            this.update.MouseLeave += new System.EventHandler(this.desativarEfeito1_MouseLeave);
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
            this.back.TabIndex = 46;
            this.back.TabStop = false;
            this.back.Click += new System.EventHandler(this.back_Click);
            // 
            // DatabaseFORM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMargin = new System.Drawing.Size(0, 30);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(878, 501);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.opcoes);
            this.Controls.Add(this.loading);
            this.Controls.Add(this.update);
            this.Controls.Add(this.back);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "DatabaseFORM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DatabaseFORM";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.back)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox back;
        private Guna.UI2.WinForms.Guna2Button update;
        private Guna.UI2.WinForms.Guna2CircleProgressBar loading;
        private Guna.UI2.WinForms.Guna2ComboBox opcoes;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label12;
    }
}