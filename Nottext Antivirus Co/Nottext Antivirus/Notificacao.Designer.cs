namespace Nottext_Antivirus
{
    partial class Notificacao
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Notificacao));
            this.panel1 = new System.Windows.Forms.Panel();
            this.actionButton = new Guna.UI2.WinForms.Guna2Button();
            this.message = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ok = new Guna.UI2.WinForms.Guna2Button();
            this.title = new System.Windows.Forms.Label();
            this.close = new System.Windows.Forms.PictureBox();
            this.titleImage = new System.Windows.Forms.PictureBox();
            this.guna2DragControl2 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.guna2DragControl3 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.titleImage)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.actionButton);
            this.panel1.Controls.Add(this.message);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.ok);
            this.panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(584, 257);
            this.panel1.TabIndex = 1;
            // 
            // actionButton
            // 
            this.actionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.actionButton.BackColor = System.Drawing.Color.Transparent;
            this.actionButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.actionButton.BorderRadius = 5;
            this.actionButton.BorderThickness = 1;
            this.actionButton.CheckedState.Parent = this.actionButton;
            this.actionButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.actionButton.CustomImages.Parent = this.actionButton;
            this.actionButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.actionButton.Font = new System.Drawing.Font("Open Sans", 12F);
            this.actionButton.ForeColor = System.Drawing.Color.White;
            this.actionButton.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.actionButton.HoverState.ForeColor = System.Drawing.Color.White;
            this.actionButton.HoverState.Parent = this.actionButton;
            this.actionButton.ImageOffset = new System.Drawing.Point(-3, 0);
            this.actionButton.ImageSize = new System.Drawing.Size(25, 25);
            this.actionButton.Location = new System.Drawing.Point(217, 202);
            this.actionButton.Name = "actionButton";
            this.actionButton.ShadowDecoration.BorderRadius = 5;
            this.actionButton.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.actionButton.ShadowDecoration.Depth = 40;
            this.actionButton.ShadowDecoration.Parent = this.actionButton;
            this.actionButton.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(7, 7, 7, 12);
            this.actionButton.Size = new System.Drawing.Size(169, 45);
            this.actionButton.TabIndex = 54;
            this.actionButton.Text = "REMOVER PASTA";
            this.actionButton.Visible = false;
            this.actionButton.Click += new System.EventHandler(this.actionButton_Click);
            this.actionButton.MouseEnter += new System.EventHandler(this.ativarEfeito1_MouseHover);
            this.actionButton.MouseLeave += new System.EventHandler(this.desativarEfeito1_MouseLeave);
            // 
            // message
            // 
            this.message.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.message.BackColor = System.Drawing.Color.Transparent;
            this.message.Font = new System.Drawing.Font("Open Sans", 12F);
            this.message.Location = new System.Drawing.Point(10, 59);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(564, 131);
            this.message.TabIndex = 53;
            this.message.Text = "Nottext Antivirus";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.panel2.Location = new System.Drawing.Point(0, 38);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(584, 2);
            this.panel2.TabIndex = 51;
            // 
            // ok
            // 
            this.ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ok.BackColor = System.Drawing.Color.Transparent;
            this.ok.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.ok.BorderRadius = 5;
            this.ok.BorderThickness = 1;
            this.ok.CheckedState.Parent = this.ok;
            this.ok.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ok.CustomImages.Parent = this.ok;
            this.ok.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.ok.Font = new System.Drawing.Font("Open Sans", 12F);
            this.ok.ForeColor = System.Drawing.Color.White;
            this.ok.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.ok.HoverState.ForeColor = System.Drawing.Color.White;
            this.ok.HoverState.Parent = this.ok;
            this.ok.ImageOffset = new System.Drawing.Point(-3, 0);
            this.ok.ImageSize = new System.Drawing.Size(25, 25);
            this.ok.Location = new System.Drawing.Point(10, 202);
            this.ok.Name = "ok";
            this.ok.ShadowDecoration.BorderRadius = 5;
            this.ok.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.ok.ShadowDecoration.Depth = 40;
            this.ok.ShadowDecoration.Parent = this.ok;
            this.ok.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(7, 7, 7, 12);
            this.ok.Size = new System.Drawing.Size(169, 45);
            this.ok.TabIndex = 50;
            this.ok.Text = "OK";
            this.ok.Click += new System.EventHandler(this.ok_Click);
            this.ok.MouseEnter += new System.EventHandler(this.ativarEfeito1_MouseHover);
            this.ok.MouseLeave += new System.EventHandler(this.desativarEfeito1_MouseLeave);
            // 
            // title
            // 
            this.title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.title.BackColor = System.Drawing.Color.Transparent;
            this.title.Font = new System.Drawing.Font("Open Sans", 12F);
            this.title.Location = new System.Drawing.Point(40, 0);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(515, 35);
            this.title.TabIndex = 52;
            this.title.Text = "Nottext Antivirus";
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // close
            // 
            this.close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.close.BackColor = System.Drawing.Color.Transparent;
            this.close.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("close.BackgroundImage")));
            this.close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.close.Location = new System.Drawing.Point(561, 2);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(19, 35);
            this.close.TabIndex = 14;
            this.close.TabStop = false;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // titleImage
            // 
            this.titleImage.BackColor = System.Drawing.Color.Transparent;
            this.titleImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("titleImage.BackgroundImage")));
            this.titleImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.titleImage.Enabled = false;
            this.titleImage.Location = new System.Drawing.Point(3, 0);
            this.titleImage.Name = "titleImage";
            this.titleImage.Size = new System.Drawing.Size(33, 37);
            this.titleImage.TabIndex = 13;
            this.titleImage.TabStop = false;
            // 
            // guna2DragControl2
            // 
            this.guna2DragControl2.TargetControl = this.title;
            // 
            // guna2DragControl3
            // 
            this.guna2DragControl3.TargetControl = this.panel3;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.title);
            this.panel3.Controls.Add(this.titleImage);
            this.panel3.Controls.Add(this.close);
            this.panel3.Location = new System.Drawing.Point(2, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(584, 37);
            this.panel3.TabIndex = 54;
            // 
            // Notificacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.ClientSize = new System.Drawing.Size(588, 261);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Notificacao";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Notificacao";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Notificacao_FormClosing_1);
            this.Load += new System.EventHandler(this.Notificacao_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.titleImage)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox titleImage;
        private System.Windows.Forms.PictureBox close;
        private Guna.UI2.WinForms.Guna2Button ok;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Label message;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl2;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl3;
        private System.Windows.Forms.Panel panel3;
        private Guna.UI2.WinForms.Guna2Button actionButton;
    }
}