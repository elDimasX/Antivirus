namespace Nottext_Antivirus
{
    partial class KernelModeInstaller
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KernelModeInstaller));
            this.label1 = new System.Windows.Forms.Label();
            this.install = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Open Sans", 12F);
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(533, 73);
            this.label1.TabIndex = 1;
            this.label1.Text = "Para continuar instalando o driver do kernel, clique em INSTALAR.\r\nVocê não preci" +
    "sará reiniciar seu computador para iniciar a proteção do kernel.";
            // 
            // install
            // 
            this.install.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.install.BackColor = System.Drawing.Color.Transparent;
            this.install.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.install.BorderRadius = 5;
            this.install.BorderThickness = 1;
            this.install.CheckedState.Parent = this.install;
            this.install.Cursor = System.Windows.Forms.Cursors.Hand;
            this.install.CustomImages.Parent = this.install;
            this.install.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.install.Font = new System.Drawing.Font("Open Sans", 12F);
            this.install.ForeColor = System.Drawing.Color.White;
            this.install.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.install.HoverState.ForeColor = System.Drawing.Color.White;
            this.install.HoverState.Parent = this.install;
            this.install.ImageOffset = new System.Drawing.Point(-3, 0);
            this.install.ImageSize = new System.Drawing.Size(25, 25);
            this.install.Location = new System.Drawing.Point(16, 87);
            this.install.Name = "install";
            this.install.ShadowDecoration.BorderRadius = 5;
            this.install.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.install.ShadowDecoration.Depth = 40;
            this.install.ShadowDecoration.Parent = this.install;
            this.install.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(7, 7, 7, 12);
            this.install.Size = new System.Drawing.Size(169, 45);
            this.install.TabIndex = 49;
            this.install.Text = "INSTALAR";
            this.install.Click += new System.EventHandler(this.install_Click);
            this.install.MouseEnter += new System.EventHandler(this.ativarEfeito1_MouseHover);
            this.install.MouseLeave += new System.EventHandler(this.desativarEfeito1_MouseLeave);
            // 
            // KernelModeInstaller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 138);
            this.Controls.Add(this.install);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(573, 177);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(573, 177);
            this.Name = "KernelModeInstaller";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Instalar drivers de kernel";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Button install;
    }
}