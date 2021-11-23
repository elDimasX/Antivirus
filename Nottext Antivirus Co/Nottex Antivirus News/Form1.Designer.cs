using System.Windows.Forms;

namespace Nottex_Antivirus__News
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
            this.titleImage = new System.Windows.Forms.PictureBox();
            this.title = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.body = new System.Windows.Forms.Panel();
            this.header = new System.Windows.Forms.Panel();
            this.close = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ok = new Guna.UI2.WinForms.Guna2Button();
            this.label3 = new System.Windows.Forms.Label();
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.guna2DragControl3 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.titleImage)).BeginInit();
            this.body.SuspendLayout();
            this.header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.close)).BeginInit();
            this.SuspendLayout();
            // 
            // titleImage
            // 
            this.titleImage.BackColor = System.Drawing.Color.Transparent;
            this.titleImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("titleImage.BackgroundImage")));
            this.titleImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.titleImage.Enabled = false;
            this.titleImage.Location = new System.Drawing.Point(0, 2);
            this.titleImage.Name = "titleImage";
            this.titleImage.Size = new System.Drawing.Size(33, 35);
            this.titleImage.TabIndex = 5;
            this.titleImage.TabStop = false;
            // 
            // title
            // 
            this.title.BackColor = System.Drawing.Color.Transparent;
            this.title.Font = new System.Drawing.Font("Open Sans", 12F);
            this.title.Location = new System.Drawing.Point(39, 3);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(741, 32);
            this.title.TabIndex = 6;
            this.title.Text = "Nottext Antivirus - Atualizações recentes";
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Open Sans", 15.75F);
            this.label1.Location = new System.Drawing.Point(10, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(791, 87);
            this.label1.TabIndex = 7;
            this.label1.Text = "Parabéns, você está com a versão mais recente do Nottext Antivirus\r\nVeja as novid" +
    "ades abaixo:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Open Sans", 12F);
            this.label2.Location = new System.Drawing.Point(10, 206);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(791, 204);
            this.label2.TabIndex = 8;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // body
            // 
            this.body.BackColor = System.Drawing.Color.White;
            this.body.Controls.Add(this.header);
            this.body.Controls.Add(this.panel2);
            this.body.Controls.Add(this.ok);
            this.body.Controls.Add(this.label3);
            this.body.Controls.Add(this.label1);
            this.body.Controls.Add(this.label2);
            this.body.Location = new System.Drawing.Point(2, 2);
            this.body.Name = "body";
            this.body.Size = new System.Drawing.Size(811, 522);
            this.body.TabIndex = 24;
            // 
            // header
            // 
            this.header.Controls.Add(this.title);
            this.header.Controls.Add(this.titleImage);
            this.header.Controls.Add(this.close);
            this.header.Location = new System.Drawing.Point(0, 0);
            this.header.Name = "header";
            this.header.Size = new System.Drawing.Size(812, 37);
            this.header.TabIndex = 52;
            // 
            // close
            // 
            this.close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.close.BackColor = System.Drawing.Color.Transparent;
            this.close.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("close.BackgroundImage")));
            this.close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.close.Location = new System.Drawing.Point(789, 3);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(19, 34);
            this.close.TabIndex = 22;
            this.close.TabStop = false;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.panel2.Location = new System.Drawing.Point(0, 38);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(811, 2);
            this.panel2.TabIndex = 51;
            // 
            // ok
            // 
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
            this.ok.Location = new System.Drawing.Point(295, 413);
            this.ok.Name = "ok";
            this.ok.ShadowDecoration.BorderRadius = 5;
            this.ok.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.ok.ShadowDecoration.Depth = 40;
            this.ok.ShadowDecoration.Parent = this.ok;
            this.ok.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(7, 7, 7, 12);
            this.ok.Size = new System.Drawing.Size(253, 56);
            this.ok.TabIndex = 44;
            this.ok.Text = "OK, VAMOS INICIAR";
            this.ok.Click += new System.EventHandler(this.ok_Click);
            this.ok.MouseEnter += new System.EventHandler(this.ativarEfeito1_MouseHover);
            this.ok.MouseLeave += new System.EventHandler(this.desativarEfeito1_MouseLeave);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.label3.Font = new System.Drawing.Font("Open Sans", 12F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(0, 502);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(811, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "Nottext © | Todos os direitos revervados.";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.TargetControl = this.header;
            // 
            // guna2DragControl3
            // 
            this.guna2DragControl3.TargetControl = this.title;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.ClientSize = new System.Drawing.Size(815, 526);
            this.Controls.Add(this.body);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Novidades do Nottext Antivirus";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.titleImage)).EndInit();
            this.body.ResumeLayout(false);
            this.header.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.close)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox titleImage;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox close;
        private Label label3;
        private Panel body;
        private Guna.UI2.WinForms.Guna2Button ok;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl3;
        private Panel panel2;
        private Panel header;
    }
}

