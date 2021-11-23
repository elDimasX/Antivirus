namespace Nottext_Antivirus
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.header = new Guna.UI2.WinForms.Guna2Panel();
            this.close = new System.Windows.Forms.PictureBox();
            this.about = new System.Windows.Forms.Label();
            this.minimize = new System.Windows.Forms.PictureBox();
            this.titleImage = new System.Windows.Forms.PictureBox();
            this.title = new System.Windows.Forms.Label();
            this.items = new System.Windows.Forms.Panel();
            this.bodyControls = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.link4 = new Guna.UI2.WinForms.Guna2TileButton();
            this.link6 = new Guna.UI2.WinForms.Guna2TileButton();
            this.link5 = new Guna.UI2.WinForms.Guna2TileButton();
            this.link3 = new Guna.UI2.WinForms.Guna2TileButton();
            this.link1 = new Guna.UI2.WinForms.Guna2TileButton();
            this.link2 = new Guna.UI2.WinForms.Guna2TileButton();
            this.label1 = new System.Windows.Forms.Label();
            this.gp4 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.loading = new Guna.UI2.WinForms.Guna2CircleProgressBar();
            this.resolveButton = new Guna.UI2.WinForms.Guna2Button();
            this.subStatusLabel = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.statusImage = new System.Windows.Forms.PictureBox();
            this.notify = new System.Windows.Forms.NotifyIcon(this.components);
            this.context = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.abrirToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.proteçõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.habilitarToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.desabilitarToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.sairToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.drag1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.drag2 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.guna2ShadowForm1 = new Guna.UI2.WinForms.Guna2ShadowForm(this.components);
            this.headerLine = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2.SuspendLayout();
            this.header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.titleImage)).BeginInit();
            this.items.SuspendLayout();
            this.bodyControls.SuspendLayout();
            this.gp4.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusImage)).BeginInit();
            this.context.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.header);
            this.panel2.Controls.Add(this.items);
            this.panel2.ForeColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.MaximumSize = new System.Drawing.Size(878, 539);
            this.panel2.MinimumSize = new System.Drawing.Size(878, 539);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(878, 539);
            this.panel2.TabIndex = 0;
            // 
            // header
            // 
            this.header.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.header.BackColor = System.Drawing.Color.Transparent;
            this.header.BorderColor = System.Drawing.Color.Black;
            this.header.BorderRadius = 5;
            this.header.Controls.Add(this.close);
            this.header.Controls.Add(this.about);
            this.header.Controls.Add(this.minimize);
            this.header.Controls.Add(this.titleImage);
            this.header.Controls.Add(this.title);
            this.header.Location = new System.Drawing.Point(0, 0);
            this.header.Name = "header";
            this.header.ShadowDecoration.Parent = this.header;
            this.header.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(1);
            this.header.Size = new System.Drawing.Size(878, 40);
            this.header.TabIndex = 27;
            // 
            // close
            // 
            this.close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.close.BackColor = System.Drawing.Color.Transparent;
            this.close.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("close.BackgroundImage")));
            this.close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.close.Location = new System.Drawing.Point(856, 0);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(19, 35);
            this.close.TabIndex = 6;
            this.close.TabStop = false;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // about
            // 
            this.about.BackColor = System.Drawing.Color.Transparent;
            this.about.Cursor = System.Windows.Forms.Cursors.Hand;
            this.about.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.about.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(145)))), ((int)(((byte)(207)))));
            this.about.Location = new System.Drawing.Point(806, -2);
            this.about.Name = "about";
            this.about.Size = new System.Drawing.Size(19, 35);
            this.about.TabIndex = 9;
            this.about.Text = "i";
            this.about.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.about.Click += new System.EventHandler(this.aboutBtn_Click);
            // 
            // minimize
            // 
            this.minimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.minimize.BackColor = System.Drawing.Color.Transparent;
            this.minimize.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("minimize.BackgroundImage")));
            this.minimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.minimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.minimize.Location = new System.Drawing.Point(831, 0);
            this.minimize.Name = "minimize";
            this.minimize.Size = new System.Drawing.Size(19, 35);
            this.minimize.TabIndex = 7;
            this.minimize.TabStop = false;
            this.minimize.Click += new System.EventHandler(this.minimize_Click);
            // 
            // titleImage
            // 
            this.titleImage.BackColor = System.Drawing.Color.Transparent;
            this.titleImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("titleImage.BackgroundImage")));
            this.titleImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.titleImage.Enabled = false;
            this.titleImage.Location = new System.Drawing.Point(2, 0);
            this.titleImage.Name = "titleImage";
            this.titleImage.Size = new System.Drawing.Size(33, 37);
            this.titleImage.TabIndex = 4;
            this.titleImage.TabStop = false;
            // 
            // title
            // 
            this.title.BackColor = System.Drawing.Color.Transparent;
            this.title.Font = new System.Drawing.Font("Open Sans", 12F);
            this.title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.title.Location = new System.Drawing.Point(0, 0);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(878, 35);
            this.title.TabIndex = 5;
            this.title.Text = "Nottext Antivirus";
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // items
            // 
            this.items.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.items.AutoSize = true;
            this.items.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.items.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.items.Controls.Add(this.bodyControls);
            this.items.Controls.Add(this.label1);
            this.items.Controls.Add(this.gp4);
            this.items.Location = new System.Drawing.Point(0, 38);
            this.items.MaximumSize = new System.Drawing.Size(878, 501);
            this.items.MinimumSize = new System.Drawing.Size(878, 501);
            this.items.Name = "items";
            this.items.Size = new System.Drawing.Size(878, 501);
            this.items.TabIndex = 5;
            // 
            // bodyControls
            // 
            this.bodyControls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bodyControls.BackColor = System.Drawing.Color.Transparent;
            this.bodyControls.Controls.Add(this.link4);
            this.bodyControls.Controls.Add(this.link6);
            this.bodyControls.Controls.Add(this.link5);
            this.bodyControls.Controls.Add(this.link3);
            this.bodyControls.Controls.Add(this.link1);
            this.bodyControls.Controls.Add(this.link2);
            this.bodyControls.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.bodyControls.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.bodyControls.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.bodyControls.Location = new System.Drawing.Point(0, 126);
            this.bodyControls.Name = "bodyControls";
            this.bodyControls.ShadowDecoration.Parent = this.bodyControls;
            this.bodyControls.Size = new System.Drawing.Size(878, 351);
            this.bodyControls.TabIndex = 27;
            // 
            // link4
            // 
            this.link4.BackColor = System.Drawing.Color.Transparent;
            this.link4.BorderColor = System.Drawing.Color.Gainsboro;
            this.link4.BorderRadius = 2;
            this.link4.BorderThickness = 2;
            this.link4.CheckedState.Parent = this.link4;
            this.link4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.link4.CustomBorderColor = System.Drawing.Color.White;
            this.link4.CustomImages.ImageAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.link4.CustomImages.ImageSize = new System.Drawing.Size(50, 50);
            this.link4.CustomImages.Parent = this.link4;
            this.link4.FillColor = System.Drawing.Color.White;
            this.link4.Font = new System.Drawing.Font("Shannon Std", 15F, System.Drawing.FontStyle.Bold);
            this.link4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.link4.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.link4.HoverState.Parent = this.link4;
            this.link4.Image = ((System.Drawing.Image)(resources.GetObject("link4.Image")));
            this.link4.ImageSize = new System.Drawing.Size(50, 50);
            this.link4.Location = new System.Drawing.Point(49, 195);
            this.link4.Name = "link4";
            this.link4.ShadowDecoration.BorderRadius = 5;
            this.link4.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.link4.ShadowDecoration.Depth = 40;
            this.link4.ShadowDecoration.Parent = this.link4;
            this.link4.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(7, 7, 7, 12);
            this.link4.Size = new System.Drawing.Size(160, 127);
            this.link4.TabIndex = 24;
            this.link4.Text = "Configurações";
            this.link4.Click += new System.EventHandler(this.settings_Click);
            this.link4.MouseEnter += new System.EventHandler(this.ativarEfeito1_MouseHover);
            this.link4.MouseLeave += new System.EventHandler(this.desativarEfeito1_MouseLeave);
            // 
            // link6
            // 
            this.link6.BackColor = System.Drawing.Color.Transparent;
            this.link6.BorderColor = System.Drawing.Color.Gainsboro;
            this.link6.BorderRadius = 2;
            this.link6.BorderThickness = 2;
            this.link6.CheckedState.Parent = this.link6;
            this.link6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.link6.CustomBorderColor = System.Drawing.Color.White;
            this.link6.CustomImages.ImageAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.link6.CustomImages.ImageSize = new System.Drawing.Size(50, 50);
            this.link6.CustomImages.Parent = this.link6;
            this.link6.FillColor = System.Drawing.Color.White;
            this.link6.Font = new System.Drawing.Font("Shannon Std", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.link6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.link6.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.link6.HoverState.Parent = this.link6;
            this.link6.Image = ((System.Drawing.Image)(resources.GetObject("link6.Image")));
            this.link6.ImageSize = new System.Drawing.Size(50, 50);
            this.link6.Location = new System.Drawing.Point(666, 195);
            this.link6.Name = "link6";
            this.link6.ShadowDecoration.BorderRadius = 5;
            this.link6.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.link6.ShadowDecoration.Depth = 40;
            this.link6.ShadowDecoration.Parent = this.link6;
            this.link6.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(7, 7, 7, 12);
            this.link6.Size = new System.Drawing.Size(160, 127);
            this.link6.TabIndex = 26;
            this.link6.Text = "Banco de dados";
            this.link6.Click += new System.EventHandler(this.database_Click);
            this.link6.MouseEnter += new System.EventHandler(this.ativarEfeito1_MouseHover);
            this.link6.MouseLeave += new System.EventHandler(this.desativarEfeito1_MouseLeave);
            // 
            // link5
            // 
            this.link5.BackColor = System.Drawing.Color.Transparent;
            this.link5.BorderColor = System.Drawing.Color.Gainsboro;
            this.link5.BorderRadius = 2;
            this.link5.BorderThickness = 2;
            this.link5.CheckedState.Parent = this.link5;
            this.link5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.link5.CustomBorderColor = System.Drawing.Color.White;
            this.link5.CustomImages.ImageAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.link5.CustomImages.ImageSize = new System.Drawing.Size(50, 50);
            this.link5.CustomImages.Parent = this.link5;
            this.link5.FillColor = System.Drawing.Color.White;
            this.link5.Font = new System.Drawing.Font("Shannon Std", 15F, System.Drawing.FontStyle.Bold);
            this.link5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.link5.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.link5.HoverState.Parent = this.link5;
            this.link5.Image = ((System.Drawing.Image)(resources.GetObject("link5.Image")));
            this.link5.ImageSize = new System.Drawing.Size(50, 50);
            this.link5.Location = new System.Drawing.Point(355, 195);
            this.link5.Name = "link5";
            this.link5.ShadowDecoration.BorderRadius = 5;
            this.link5.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.link5.ShadowDecoration.Depth = 40;
            this.link5.ShadowDecoration.Parent = this.link5;
            this.link5.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(7, 7, 7, 12);
            this.link5.Size = new System.Drawing.Size(160, 127);
            this.link5.TabIndex = 25;
            this.link5.Text = "Windows";
            this.link5.Click += new System.EventHandler(this.windows_Click);
            this.link5.MouseEnter += new System.EventHandler(this.ativarEfeito1_MouseHover);
            this.link5.MouseLeave += new System.EventHandler(this.desativarEfeito1_MouseLeave);
            // 
            // link3
            // 
            this.link3.BackColor = System.Drawing.Color.Transparent;
            this.link3.BorderColor = System.Drawing.Color.Gainsboro;
            this.link3.BorderRadius = 2;
            this.link3.BorderThickness = 2;
            this.link3.CheckedState.Parent = this.link3;
            this.link3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.link3.CustomBorderColor = System.Drawing.Color.White;
            this.link3.CustomImages.ImageAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.link3.CustomImages.ImageSize = new System.Drawing.Size(50, 50);
            this.link3.CustomImages.Parent = this.link3;
            this.link3.FillColor = System.Drawing.Color.White;
            this.link3.Font = new System.Drawing.Font("Shannon Std", 15F, System.Drawing.FontStyle.Bold);
            this.link3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.link3.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.link3.HoverState.Parent = this.link3;
            this.link3.Image = ((System.Drawing.Image)(resources.GetObject("link3.Image")));
            this.link3.ImageSize = new System.Drawing.Size(50, 50);
            this.link3.Location = new System.Drawing.Point(666, 30);
            this.link3.Name = "link3";
            this.link3.ShadowDecoration.BorderRadius = 5;
            this.link3.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.link3.ShadowDecoration.Depth = 40;
            this.link3.ShadowDecoration.Parent = this.link3;
            this.link3.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(7, 7, 7, 12);
            this.link3.Size = new System.Drawing.Size(160, 127);
            this.link3.TabIndex = 23;
            this.link3.Text = "Quarentena";
            this.link3.Click += new System.EventHandler(this.quarentine_Click);
            this.link3.MouseEnter += new System.EventHandler(this.ativarEfeito1_MouseHover);
            this.link3.MouseLeave += new System.EventHandler(this.desativarEfeito1_MouseLeave);
            // 
            // link1
            // 
            this.link1.BackColor = System.Drawing.Color.Transparent;
            this.link1.BorderColor = System.Drawing.Color.Gainsboro;
            this.link1.BorderRadius = 2;
            this.link1.BorderThickness = 2;
            this.link1.CheckedState.Parent = this.link1;
            this.link1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.link1.CustomBorderColor = System.Drawing.Color.White;
            this.link1.CustomImages.ImageAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.link1.CustomImages.ImageSize = new System.Drawing.Size(50, 50);
            this.link1.CustomImages.Parent = this.link1;
            this.link1.FillColor = System.Drawing.Color.White;
            this.link1.Font = new System.Drawing.Font("Shannon Std", 15F, System.Drawing.FontStyle.Bold);
            this.link1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.link1.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.link1.HoverState.Parent = this.link1;
            this.link1.Image = ((System.Drawing.Image)(resources.GetObject("link1.Image")));
            this.link1.ImageSize = new System.Drawing.Size(50, 50);
            this.link1.Location = new System.Drawing.Point(49, 30);
            this.link1.Name = "link1";
            this.link1.ShadowDecoration.BorderRadius = 5;
            this.link1.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.link1.ShadowDecoration.Depth = 40;
            this.link1.ShadowDecoration.Parent = this.link1;
            this.link1.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(7, 7, 7, 12);
            this.link1.Size = new System.Drawing.Size(160, 127);
            this.link1.TabIndex = 21;
            this.link1.Text = "Proteções";
            this.link1.Click += new System.EventHandler(this.protections_Click);
            this.link1.MouseEnter += new System.EventHandler(this.ativarEfeito1_MouseHover);
            this.link1.MouseLeave += new System.EventHandler(this.desativarEfeito1_MouseLeave);
            // 
            // link2
            // 
            this.link2.BackColor = System.Drawing.Color.Transparent;
            this.link2.BorderColor = System.Drawing.Color.Gainsboro;
            this.link2.BorderRadius = 2;
            this.link2.BorderThickness = 2;
            this.link2.CheckedState.Parent = this.link2;
            this.link2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.link2.CustomBorderColor = System.Drawing.Color.White;
            this.link2.CustomImages.ImageAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.link2.CustomImages.ImageSize = new System.Drawing.Size(50, 50);
            this.link2.CustomImages.Parent = this.link2;
            this.link2.FillColor = System.Drawing.Color.White;
            this.link2.Font = new System.Drawing.Font("Shannon Std", 15F, System.Drawing.FontStyle.Bold);
            this.link2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.link2.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.link2.HoverState.Parent = this.link2;
            this.link2.Image = ((System.Drawing.Image)(resources.GetObject("link2.Image")));
            this.link2.ImageSize = new System.Drawing.Size(50, 50);
            this.link2.Location = new System.Drawing.Point(355, 30);
            this.link2.Name = "link2";
            this.link2.ShadowDecoration.BorderRadius = 5;
            this.link2.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.link2.ShadowDecoration.Depth = 40;
            this.link2.ShadowDecoration.Parent = this.link2;
            this.link2.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(7, 7, 7, 12);
            this.link2.Size = new System.Drawing.Size(160, 127);
            this.link2.TabIndex = 22;
            this.link2.Text = "Verificações";
            this.link2.Click += new System.EventHandler(this.scan_Click);
            this.link2.MouseEnter += new System.EventHandler(this.ativarEfeito1_MouseHover);
            this.link2.MouseLeave += new System.EventHandler(this.desativarEfeito1_MouseLeave);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.label1.Font = new System.Drawing.Font("Open Sans", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 480);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(878, 21);
            this.label1.TabIndex = 10;
            this.label1.Text = "Nottext 2021 © | Todos os direitos revervados.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gp4
            // 
            this.gp4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gp4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.gp4.BackgroundImage = global::Nottext_Antivirus.Properties.Resources.bg3;
            this.gp4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gp4.Controls.Add(this.panel1);
            this.gp4.Controls.Add(this.subStatusLabel);
            this.gp4.Controls.Add(this.statusLabel);
            this.gp4.Controls.Add(this.statusImage);
            this.gp4.Location = new System.Drawing.Point(0, 2);
            this.gp4.Name = "gp4";
            this.gp4.Size = new System.Drawing.Size(878, 126);
            this.gp4.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.loading);
            this.panel1.Controls.Add(this.resolveButton);
            this.panel1.Location = new System.Drawing.Point(171, 86);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(153, 37);
            this.panel1.TabIndex = 0;
            // 
            // loading
            // 
            this.loading.Animated = true;
            this.loading.AnimationSpeed = 1.2F;
            this.loading.BackColor = System.Drawing.Color.Transparent;
            this.loading.FillColor = System.Drawing.Color.Transparent;
            this.loading.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.loading.Location = new System.Drawing.Point(114, 2);
            this.loading.Name = "loading";
            this.loading.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.loading.ProgressColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.loading.ProgressThickness = 3;
            this.loading.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.loading.ShadowDecoration.Parent = this.loading;
            this.loading.Size = new System.Drawing.Size(31, 32);
            this.loading.TabIndex = 49;
            this.loading.Value = 75;
            this.loading.Visible = false;
            // 
            // resolveButton
            // 
            this.resolveButton.BackColor = System.Drawing.Color.Transparent;
            this.resolveButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.resolveButton.BorderRadius = 2;
            this.resolveButton.BorderThickness = 1;
            this.resolveButton.CheckedState.Parent = this.resolveButton;
            this.resolveButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.resolveButton.CustomImages.Parent = this.resolveButton;
            this.resolveButton.FillColor = System.Drawing.Color.White;
            this.resolveButton.Font = new System.Drawing.Font("Open Sans", 12F);
            this.resolveButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.resolveButton.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.resolveButton.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.resolveButton.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.resolveButton.HoverState.Parent = this.resolveButton;
            this.resolveButton.Location = new System.Drawing.Point(3, 2);
            this.resolveButton.Name = "resolveButton";
            this.resolveButton.ShadowDecoration.BorderRadius = 5;
            this.resolveButton.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.resolveButton.ShadowDecoration.Depth = 40;
            this.resolveButton.ShadowDecoration.Parent = this.resolveButton;
            this.resolveButton.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(7, 7, 7, 12);
            this.resolveButton.Size = new System.Drawing.Size(105, 32);
            this.resolveButton.TabIndex = 48;
            this.resolveButton.Text = "Resolver";
            this.resolveButton.Visible = false;
            this.resolveButton.Click += new System.EventHandler(this.resolveButton_Click);
            this.resolveButton.MouseEnter += new System.EventHandler(this.ativarEfeito1_MouseHover);
            this.resolveButton.MouseLeave += new System.EventHandler(this.desativarEfeito1_MouseLeave);
            // 
            // subStatusLabel
            // 
            this.subStatusLabel.AutoSize = true;
            this.subStatusLabel.BackColor = System.Drawing.Color.Transparent;
            this.subStatusLabel.Font = new System.Drawing.Font("Open Sans", 12F);
            this.subStatusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.subStatusLabel.Location = new System.Drawing.Point(167, 61);
            this.subStatusLabel.Name = "subStatusLabel";
            this.subStatusLabel.Size = new System.Drawing.Size(439, 22);
            this.subStatusLabel.TabIndex = 2;
            this.subStatusLabel.Text = "O Nottext Antivirus está monitorando o seu computador";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.BackColor = System.Drawing.Color.Transparent;
            this.statusLabel.Font = new System.Drawing.Font("Open Sans", 15.75F);
            this.statusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.statusLabel.Location = new System.Drawing.Point(167, 26);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(267, 28);
            this.statusLabel.TabIndex = 1;
            this.statusLabel.Text = "A proteção está habilitada";
            // 
            // statusImage
            // 
            this.statusImage.BackColor = System.Drawing.Color.Transparent;
            this.statusImage.BackgroundImage = global::Nottext_Antivirus.Properties.Resources.cloud_computing;
            this.statusImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.statusImage.Location = new System.Drawing.Point(39, 10);
            this.statusImage.Name = "statusImage";
            this.statusImage.Size = new System.Drawing.Size(113, 114);
            this.statusImage.TabIndex = 0;
            this.statusImage.TabStop = false;
            // 
            // notify
            // 
            this.notify.ContextMenuStrip = this.context;
            this.notify.Icon = ((System.Drawing.Icon)(resources.GetObject("notify.Icon")));
            this.notify.Text = "A proteção está ativada.";
            this.notify.Visible = true;
            this.notify.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notify_MouseDoubleClick);
            // 
            // context
            // 
            this.context.BackColor = System.Drawing.Color.White;
            this.context.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.context.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.context.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirToolStripMenuItem1,
            this.proteçõesToolStripMenuItem,
            this.toolStripSeparator2,
            this.sairToolStripMenuItem1});
            this.context.Name = "context";
            this.context.Size = new System.Drawing.Size(156, 88);
            // 
            // abrirToolStripMenuItem1
            // 
            this.abrirToolStripMenuItem1.Font = new System.Drawing.Font("Open Sans", 12F);
            this.abrirToolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.abrirToolStripMenuItem1.Name = "abrirToolStripMenuItem1";
            this.abrirToolStripMenuItem1.Size = new System.Drawing.Size(155, 26);
            this.abrirToolStripMenuItem1.Text = "Abrir";
            this.abrirToolStripMenuItem1.Click += new System.EventHandler(this.abrirToolStripMenuItem_Click);
            // 
            // proteçõesToolStripMenuItem
            // 
            this.proteçõesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.habilitarToolStripMenuItem1,
            this.desabilitarToolStripMenuItem1});
            this.proteçõesToolStripMenuItem.Font = new System.Drawing.Font("Open Sans", 12F);
            this.proteçõesToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.proteçõesToolStripMenuItem.Name = "proteçõesToolStripMenuItem";
            this.proteçõesToolStripMenuItem.Size = new System.Drawing.Size(155, 26);
            this.proteçõesToolStripMenuItem.Text = "Proteções";
            // 
            // habilitarToolStripMenuItem1
            // 
            this.habilitarToolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.habilitarToolStripMenuItem1.Name = "habilitarToolStripMenuItem1";
            this.habilitarToolStripMenuItem1.Size = new System.Drawing.Size(162, 26);
            this.habilitarToolStripMenuItem1.Text = "Habilitar";
            this.habilitarToolStripMenuItem1.Click += new System.EventHandler(this.habilitarToolStripMenuItem_Click);
            // 
            // desabilitarToolStripMenuItem1
            // 
            this.desabilitarToolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.desabilitarToolStripMenuItem1.Name = "desabilitarToolStripMenuItem1";
            this.desabilitarToolStripMenuItem1.Size = new System.Drawing.Size(162, 26);
            this.desabilitarToolStripMenuItem1.Text = "Desabilitar";
            this.desabilitarToolStripMenuItem1.Click += new System.EventHandler(this.desabilitarToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(152, 6);
            // 
            // sairToolStripMenuItem1
            // 
            this.sairToolStripMenuItem1.Font = new System.Drawing.Font("Open Sans", 12F);
            this.sairToolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.sairToolStripMenuItem1.Name = "sairToolStripMenuItem1";
            this.sairToolStripMenuItem1.Size = new System.Drawing.Size(155, 26);
            this.sairToolStripMenuItem1.Text = "Sair";
            this.sairToolStripMenuItem1.Click += new System.EventHandler(this.sairToolStripMenuItem_Click);
            // 
            // drag1
            // 
            this.drag1.TargetControl = this.title;
            // 
            // drag2
            // 
            this.drag2.TargetControl = this.header;
            // 
            // headerLine
            // 
            this.headerLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.headerLine.Location = new System.Drawing.Point(2, 41);
            this.headerLine.Name = "headerLine";
            this.headerLine.Size = new System.Drawing.Size(879, 2);
            this.headerLine.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(31, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(253, 137);
            this.pictureBox1.TabIndex = 25;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.ClientSize = new System.Drawing.Size(884, 545);
            this.Controls.Add(this.headerLine);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(884, 545);
            this.MinimumSize = new System.Drawing.Size(884, 545);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nottext Antivirus";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.header.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.titleImage)).EndInit();
            this.items.ResumeLayout(false);
            this.bodyControls.ResumeLayout(false);
            this.gp4.ResumeLayout(false);
            this.gp4.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.statusImage)).EndInit();
            this.context.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox titleImage;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Panel items;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.NotifyIcon notify;
        private System.Windows.Forms.Panel gp4;
        private System.Windows.Forms.PictureBox statusImage;
        private System.Windows.Forms.Label subStatusLabel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.PictureBox minimize;
        private System.Windows.Forms.PictureBox close;
        private System.Windows.Forms.Label about;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2TileButton link1;
        private Guna.UI2.WinForms.Guna2TileButton link2;
        private Guna.UI2.WinForms.Guna2TileButton link3;
        private Guna.UI2.WinForms.Guna2TileButton link4;
        private Guna.UI2.WinForms.Guna2TileButton link5;
        private Guna.UI2.WinForms.Guna2TileButton link6;
        private Guna.UI2.WinForms.Guna2Panel header;
        private Guna.UI2.WinForms.Guna2DragControl drag1;
        private Guna.UI2.WinForms.Guna2DragControl drag2;
        private System.Windows.Forms.ContextMenuStrip context;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem proteçõesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem habilitarToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem desabilitarToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem sairToolStripMenuItem1;
        private Guna.UI2.WinForms.Guna2ShadowForm guna2ShadowForm1;
        private Guna.UI2.WinForms.Guna2GradientPanel bodyControls;
        private System.Windows.Forms.Panel headerLine;
        private Guna.UI2.WinForms.Guna2Button resolveButton;
        private Guna.UI2.WinForms.Guna2CircleProgressBar loading;
        private System.Windows.Forms.Panel panel1;
    }
}

