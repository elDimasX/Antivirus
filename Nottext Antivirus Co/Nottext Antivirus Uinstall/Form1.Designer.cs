namespace Nottext_Antivirus_Uinstall
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.titleImage = new System.Windows.Forms.PictureBox();
            this.close = new System.Windows.Forms.PictureBox();
            this.minimize = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.passwordText = new Guna.UI2.WinForms.Guna2TextBox();
            this.desinstalar = new Guna.UI2.WinForms.Guna2Button();
            this.title = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.guna2ShadowForm1 = new Guna.UI2.WinForms.Guna2ShadowForm(this.components);
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.guna2DragControl2 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.titleImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimize)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.passwordText);
            this.panel1.Controls.Add(this.desinstalar);
            this.panel1.Controls.Add(this.title);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(696, 467);
            this.panel1.TabIndex = 38;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.titleImage);
            this.panel3.Controls.Add(this.close);
            this.panel3.Controls.Add(this.minimize);
            this.panel3.Location = new System.Drawing.Point(0, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(696, 34);
            this.panel3.TabIndex = 39;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Open Sans", 12F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.label1.Location = new System.Drawing.Point(39, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(595, 22);
            this.label1.TabIndex = 52;
            this.label1.Text = "Nottext Antivirus";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // titleImage
            // 
            this.titleImage.BackColor = System.Drawing.Color.Transparent;
            this.titleImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("titleImage.BackgroundImage")));
            this.titleImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.titleImage.Enabled = false;
            this.titleImage.Location = new System.Drawing.Point(0, 0);
            this.titleImage.Name = "titleImage";
            this.titleImage.Size = new System.Drawing.Size(33, 35);
            this.titleImage.TabIndex = 36;
            this.titleImage.TabStop = false;
            // 
            // close
            // 
            this.close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.close.BackColor = System.Drawing.Color.Transparent;
            this.close.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("close.BackgroundImage")));
            this.close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.close.Location = new System.Drawing.Point(670, 0);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(19, 34);
            this.close.TabIndex = 37;
            this.close.TabStop = false;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // minimize
            // 
            this.minimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.minimize.BackColor = System.Drawing.Color.Transparent;
            this.minimize.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("minimize.BackgroundImage")));
            this.minimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.minimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.minimize.Location = new System.Drawing.Point(640, 0);
            this.minimize.Name = "minimize";
            this.minimize.Size = new System.Drawing.Size(19, 34);
            this.minimize.TabIndex = 38;
            this.minimize.TabStop = false;
            this.minimize.Click += new System.EventHandler(this.minimize_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.panel2.Location = new System.Drawing.Point(0, 36);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(696, 2);
            this.panel2.TabIndex = 51;
            // 
            // passwordText
            // 
            this.passwordText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.passwordText.Animated = true;
            this.passwordText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.passwordText.BorderRadius = 2;
            this.passwordText.BorderThickness = 2;
            this.passwordText.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.passwordText.DefaultText = "";
            this.passwordText.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.passwordText.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.passwordText.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.passwordText.DisabledState.Parent = this.passwordText;
            this.passwordText.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.passwordText.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.passwordText.FocusedState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.passwordText.FocusedState.Parent = this.passwordText;
            this.passwordText.Font = new System.Drawing.Font("Open Sans", 12F);
            this.passwordText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.passwordText.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.passwordText.HoverState.Parent = this.passwordText;
            this.passwordText.Location = new System.Drawing.Point(69, 267);
            this.passwordText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.passwordText.Name = "passwordText";
            this.passwordText.PasswordChar = '*';
            this.passwordText.PlaceholderText = "";
            this.passwordText.SelectedText = "";
            this.passwordText.ShadowDecoration.Parent = this.passwordText;
            this.passwordText.Size = new System.Drawing.Size(569, 36);
            this.passwordText.TabIndex = 50;
            this.passwordText.Visible = false;
            // 
            // desinstalar
            // 
            this.desinstalar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.desinstalar.BackColor = System.Drawing.Color.Transparent;
            this.desinstalar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.desinstalar.BorderRadius = 5;
            this.desinstalar.BorderThickness = 1;
            this.desinstalar.CheckedState.Parent = this.desinstalar;
            this.desinstalar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.desinstalar.CustomImages.Parent = this.desinstalar;
            this.desinstalar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.desinstalar.Font = new System.Drawing.Font("Open Sans", 12F);
            this.desinstalar.ForeColor = System.Drawing.Color.White;
            this.desinstalar.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.desinstalar.HoverState.ForeColor = System.Drawing.Color.White;
            this.desinstalar.HoverState.Parent = this.desinstalar;
            this.desinstalar.ImageOffset = new System.Drawing.Point(-3, 0);
            this.desinstalar.ImageSize = new System.Drawing.Size(25, 25);
            this.desinstalar.Location = new System.Drawing.Point(193, 354);
            this.desinstalar.Name = "desinstalar";
            this.desinstalar.ShadowDecoration.BorderRadius = 5;
            this.desinstalar.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.desinstalar.ShadowDecoration.Depth = 40;
            this.desinstalar.ShadowDecoration.Parent = this.desinstalar;
            this.desinstalar.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(7, 7, 7, 12);
            this.desinstalar.Size = new System.Drawing.Size(314, 56);
            this.desinstalar.TabIndex = 49;
            this.desinstalar.Text = "REMOVER NOTTEXT ANTIVIRUS";
            this.desinstalar.Click += new System.EventHandler(this.desinstalar_Click);
            this.desinstalar.MouseEnter += new System.EventHandler(this.ativarEfeito1_MouseHover);
            this.desinstalar.MouseLeave += new System.EventHandler(this.desativarEfeito1_MouseLeave);
            // 
            // title
            // 
            this.title.BackColor = System.Drawing.Color.Transparent;
            this.title.Font = new System.Drawing.Font("Open Sans", 15.75F);
            this.title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.title.Location = new System.Drawing.Point(43, 50);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(618, 37);
            this.title.TabIndex = 32;
            this.title.Text = "Remover Nottext Antivirus";
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Open Sans", 12F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.label3.Location = new System.Drawing.Point(10, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(675, 157);
            this.label3.TabIndex = 33;
            this.label3.Text = resources.GetString("label3.Text");
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.TargetControl = this.label1;
            // 
            // guna2DragControl2
            // 
            this.guna2DragControl2.TargetControl = this.panel3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.ClientSize = new System.Drawing.Size(700, 471);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(700, 471);
            this.MinimumSize = new System.Drawing.Size(642, 471);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nottext Antivirus - Desinstalar";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.titleImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox minimize;
        private System.Windows.Forms.PictureBox close;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.PictureBox titleImage;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2Button desinstalar;
        private Guna.UI2.WinForms.Guna2ShadowForm guna2ShadowForm1;
        private Guna.UI2.WinForms.Guna2TextBox passwordText;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl2;
    }
}

