
namespace Nottext_Antivirus
{
    partial class Protections
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Protections));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.exceptionsButton = new Guna.UI2.WinForms.Guna2Button();
            this.protectionsForm = new Guna.UI2.WinForms.Guna2Button();
            this.back = new System.Windows.Forms.PictureBox();
            this.forms = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.back)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.exceptionsButton);
            this.panel1.Controls.Add(this.protectionsForm);
            this.panel1.Controls.Add(this.back);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(205, 502);
            this.panel1.TabIndex = 16;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.panel3.Location = new System.Drawing.Point(0, 67);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(207, 2);
            this.panel3.TabIndex = 6;
            // 
            // exceptionsButton
            // 
            this.exceptionsButton.BackColor = System.Drawing.Color.Transparent;
            this.exceptionsButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.exceptionsButton.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.exceptionsButton.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.exceptionsButton.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.exceptionsButton.CheckedState.Parent = this.exceptionsButton;
            this.exceptionsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exceptionsButton.CustomImages.Parent = this.exceptionsButton;
            this.exceptionsButton.FillColor = System.Drawing.Color.White;
            this.exceptionsButton.Font = new System.Drawing.Font("Open Sans", 12F);
            this.exceptionsButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.exceptionsButton.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(213)))), ((int)(((byte)(213)))));
            this.exceptionsButton.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.exceptionsButton.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.exceptionsButton.HoverState.Parent = this.exceptionsButton;
            this.exceptionsButton.Location = new System.Drawing.Point(0, 111);
            this.exceptionsButton.Name = "exceptionsButton";
            this.exceptionsButton.ShadowDecoration.Parent = this.exceptionsButton;
            this.exceptionsButton.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(3);
            this.exceptionsButton.Size = new System.Drawing.Size(204, 41);
            this.exceptionsButton.TabIndex = 38;
            this.exceptionsButton.Text = "Exeções do antivírus";
            this.exceptionsButton.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.exceptionsButton.CheckedChanged += new System.EventHandler(this.exceptionsButton_CheckedChanged);
            // 
            // protectionsForm
            // 
            this.protectionsForm.BackColor = System.Drawing.Color.Transparent;
            this.protectionsForm.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.protectionsForm.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.protectionsForm.Checked = true;
            this.protectionsForm.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.protectionsForm.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.protectionsForm.CheckedState.Parent = this.protectionsForm;
            this.protectionsForm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.protectionsForm.CustomImages.Parent = this.protectionsForm;
            this.protectionsForm.FillColor = System.Drawing.Color.White;
            this.protectionsForm.Font = new System.Drawing.Font("Open Sans", 12F);
            this.protectionsForm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.protectionsForm.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(213)))), ((int)(((byte)(213)))));
            this.protectionsForm.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.protectionsForm.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.protectionsForm.HoverState.Parent = this.protectionsForm;
            this.protectionsForm.Location = new System.Drawing.Point(0, 69);
            this.protectionsForm.Name = "protectionsForm";
            this.protectionsForm.ShadowDecoration.Parent = this.protectionsForm;
            this.protectionsForm.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(3);
            this.protectionsForm.Size = new System.Drawing.Size(204, 41);
            this.protectionsForm.TabIndex = 37;
            this.protectionsForm.Text = "Proteções principais";
            this.protectionsForm.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.protectionsForm.CheckedChanged += new System.EventHandler(this.protectionsForm_CheckedChanged);
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
            this.back.TabIndex = 15;
            this.back.TabStop = false;
            this.back.Click += new System.EventHandler(this.back_Click);
            // 
            // forms
            // 
            this.forms.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.forms.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.forms.Location = new System.Drawing.Point(207, 0);
            this.forms.Name = "forms";
            this.forms.Size = new System.Drawing.Size(673, 502);
            this.forms.TabIndex = 17;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.panel2.Location = new System.Drawing.Point(205, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(2, 500);
            this.panel2.TabIndex = 5;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.panel4.Location = new System.Drawing.Point(0, 152);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(207, 2);
            this.panel4.TabIndex = 7;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.panel5.Location = new System.Drawing.Point(0, 109);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(207, 2);
            this.panel5.TabIndex = 8;
            // 
            // Protections
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(878, 501);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.forms);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Protections";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Protections";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Protections_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.back)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox back;
        private System.Windows.Forms.Panel panel1;
        private Guna.UI2.WinForms.Guna2Button protectionsForm;
        private System.Windows.Forms.Panel forms;
        private System.Windows.Forms.Panel panel2;
        private Guna.UI2.WinForms.Guna2Button exceptionsButton;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
    }
}