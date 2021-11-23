namespace Nottext_Antivirus
{
    partial class Quarentine
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Quarentine));
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.select = new Guna.UI2.WinForms.Guna2Button();
            this.deselect = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label12 = new System.Windows.Forms.Label();
            this.restore = new Guna.UI2.WinForms.Guna2Button();
            this.delete = new Guna.UI2.WinForms.Guna2Button();
            this.back = new System.Windows.Forms.PictureBox();
            this.panel2.SuspendLayout();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.back)).BeginInit();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.BackColor = System.Drawing.SystemColors.Window;
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBox1.Font = new System.Drawing.Font("Open Sans", 12F);
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 22;
            this.listBox1.Location = new System.Drawing.Point(81, 171);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox1.Size = new System.Drawing.Size(754, 200);
            this.listBox1.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Shannon Std", 15.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(76, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 26);
            this.label1.TabIndex = 20;
            this.label1.Text = "Itens em quarentena";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.select);
            this.panel2.Controls.Add(this.deselect);
            this.panel2.Location = new System.Drawing.Point(621, 377);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(214, 80);
            this.panel2.TabIndex = 39;
            // 
            // select
            // 
            this.select.BackColor = System.Drawing.Color.Transparent;
            this.select.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.select.BorderRadius = 2;
            this.select.BorderThickness = 1;
            this.select.CheckedState.Parent = this.select;
            this.select.Cursor = System.Windows.Forms.Cursors.Hand;
            this.select.CustomImages.Parent = this.select;
            this.select.FillColor = System.Drawing.Color.White;
            this.select.Font = new System.Drawing.Font("Open Sans", 12F);
            this.select.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.select.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.select.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.select.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.select.HoverState.Parent = this.select;
            this.select.Location = new System.Drawing.Point(3, 42);
            this.select.Name = "select";
            this.select.ShadowDecoration.BorderRadius = 5;
            this.select.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.select.ShadowDecoration.Depth = 40;
            this.select.ShadowDecoration.Parent = this.select;
            this.select.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(7, 7, 7, 12);
            this.select.Size = new System.Drawing.Size(207, 32);
            this.select.TabIndex = 41;
            this.select.Text = "Marcar tudo";
            this.select.Click += new System.EventHandler(this.select_LinkClicked);
            this.select.MouseEnter += new System.EventHandler(this.ativarEfeito1_MouseHover);
            this.select.MouseLeave += new System.EventHandler(this.desativarEfeito1_MouseLeave);
            // 
            // deselect
            // 
            this.deselect.BackColor = System.Drawing.Color.Transparent;
            this.deselect.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.deselect.BorderRadius = 2;
            this.deselect.BorderThickness = 1;
            this.deselect.CheckedState.Parent = this.deselect;
            this.deselect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.deselect.CustomImages.Parent = this.deselect;
            this.deselect.FillColor = System.Drawing.Color.White;
            this.deselect.Font = new System.Drawing.Font("Open Sans", 12F);
            this.deselect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.deselect.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.deselect.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.deselect.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.deselect.HoverState.Parent = this.deselect;
            this.deselect.Location = new System.Drawing.Point(3, 4);
            this.deselect.Name = "deselect";
            this.deselect.ShadowDecoration.BorderRadius = 5;
            this.deselect.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.deselect.ShadowDecoration.Depth = 40;
            this.deselect.ShadowDecoration.Parent = this.deselect;
            this.deselect.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(7, 7, 7, 12);
            this.deselect.Size = new System.Drawing.Size(207, 32);
            this.deselect.TabIndex = 40;
            this.deselect.Text = "Desmarcar tudo";
            this.deselect.Click += new System.EventHandler(this.deselect_LinkClicked);
            this.deselect.MouseEnter += new System.EventHandler(this.ativarEfeito1_MouseHover);
            this.deselect.MouseLeave += new System.EventHandler(this.desativarEfeito1_MouseLeave);
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.White;
            this.guna2Panel1.BorderColor = System.Drawing.Color.Gainsboro;
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
            this.guna2Panel1.TabIndex = 44;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Open Sans", 12F);
            this.label11.Location = new System.Drawing.Point(64, 36);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(504, 22);
            this.label11.TabIndex = 38;
            this.label11.Text = "Todos os malwares que o Nottext Antivirus encontrou e removeu";
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
            this.label12.Size = new System.Drawing.Size(131, 28);
            this.label12.TabIndex = 37;
            this.label12.Text = "Quarentena";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // restore
            // 
            this.restore.BackColor = System.Drawing.Color.Transparent;
            this.restore.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.restore.BorderRadius = 5;
            this.restore.BorderThickness = 1;
            this.restore.CheckedState.Parent = this.restore;
            this.restore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.restore.CustomImages.Parent = this.restore;
            this.restore.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.restore.Font = new System.Drawing.Font("Open Sans", 12F);
            this.restore.ForeColor = System.Drawing.Color.White;
            this.restore.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.restore.HoverState.ForeColor = System.Drawing.Color.White;
            this.restore.HoverState.Parent = this.restore;
            this.restore.Image = ((System.Drawing.Image)(resources.GetObject("restore.Image")));
            this.restore.ImageOffset = new System.Drawing.Point(-3, 0);
            this.restore.ImageSize = new System.Drawing.Size(25, 25);
            this.restore.Location = new System.Drawing.Point(79, 391);
            this.restore.Name = "restore";
            this.restore.ShadowDecoration.BorderRadius = 5;
            this.restore.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.restore.ShadowDecoration.Depth = 40;
            this.restore.ShadowDecoration.Parent = this.restore;
            this.restore.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(7, 7, 7, 12);
            this.restore.Size = new System.Drawing.Size(253, 56);
            this.restore.TabIndex = 43;
            this.restore.Text = "RESTAURAR";
            this.restore.Click += new System.EventHandler(this.restore_Click);
            this.restore.MouseEnter += new System.EventHandler(this.ativarEfeito1_MouseHover);
            this.restore.MouseLeave += new System.EventHandler(this.desativarEfeito1_MouseLeave);
            // 
            // delete
            // 
            this.delete.BackColor = System.Drawing.Color.Transparent;
            this.delete.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.delete.BorderRadius = 5;
            this.delete.BorderThickness = 1;
            this.delete.CheckedState.Parent = this.delete;
            this.delete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.delete.CustomImages.Parent = this.delete;
            this.delete.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.delete.Font = new System.Drawing.Font("Open Sans", 12F);
            this.delete.ForeColor = System.Drawing.Color.White;
            this.delete.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.delete.HoverState.ForeColor = System.Drawing.Color.White;
            this.delete.HoverState.Parent = this.delete;
            this.delete.Image = ((System.Drawing.Image)(resources.GetObject("delete.Image")));
            this.delete.ImageOffset = new System.Drawing.Point(-3, 0);
            this.delete.ImageSize = new System.Drawing.Size(25, 25);
            this.delete.Location = new System.Drawing.Point(353, 391);
            this.delete.Name = "delete";
            this.delete.ShadowDecoration.BorderRadius = 5;
            this.delete.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.delete.ShadowDecoration.Depth = 40;
            this.delete.ShadowDecoration.Parent = this.delete;
            this.delete.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(7, 7, 7, 12);
            this.delete.Size = new System.Drawing.Size(253, 56);
            this.delete.TabIndex = 42;
            this.delete.Text = "EXCLUIR";
            this.delete.Click += new System.EventHandler(this.delete_Click);
            this.delete.MouseEnter += new System.EventHandler(this.ativarEfeito1_MouseHover);
            this.delete.MouseLeave += new System.EventHandler(this.desativarEfeito1_MouseLeave);
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
            this.back.TabIndex = 26;
            this.back.TabStop = false;
            this.back.Click += new System.EventHandler(this.back_Click);
            // 
            // Quarentine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScrollMargin = new System.Drawing.Size(0, 30);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(878, 501);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.restore);
            this.Controls.Add(this.delete);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.back);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximumSize = new System.Drawing.Size(878, 501);
            this.MinimumSize = new System.Drawing.Size(878, 501);
            this.Name = "Quarentine";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quarentine";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel2.ResumeLayout(false);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.back)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.ListBox listBox1;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox back;
        private System.Windows.Forms.Panel panel2;
        private Guna.UI2.WinForms.Guna2Button select;
        private Guna.UI2.WinForms.Guna2Button deselect;
        private Guna.UI2.WinForms.Guna2Button delete;
        private Guna.UI2.WinForms.Guna2Button restore;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label12;
    }
}