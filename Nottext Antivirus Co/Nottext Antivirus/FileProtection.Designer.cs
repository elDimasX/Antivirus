namespace Nottext_Antivirus
{
    partial class FileProtection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileProtection));
            this.restart = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.subStatusLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.realTimeProtection = new Guna.UI2.WinForms.Guna2CustomCheckBox();
            this.kernelProtection = new Guna.UI2.WinForms.Guna2CustomCheckBox();
            this.antiRansomware = new Guna.UI2.WinForms.Guna2CustomCheckBox();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label12 = new System.Windows.Forms.Label();
            this.folders = new Guna.UI2.WinForms.Guna2Button();
            this.exceptionsRanso = new Guna.UI2.WinForms.Guna2Button();
            this.advancedScan = new Guna.UI2.WinForms.Guna2CustomCheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.opcoes = new Guna.UI2.WinForms.Guna2ComboBox();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // restart
            // 
            this.restart.AutoSize = true;
            this.restart.BackColor = System.Drawing.Color.Transparent;
            this.restart.Font = new System.Drawing.Font("Open Sans", 12F);
            this.restart.ForeColor = System.Drawing.Color.Crimson;
            this.restart.Location = new System.Drawing.Point(259, 197);
            this.restart.Name = "restart";
            this.restart.Size = new System.Drawing.Size(294, 22);
            this.restart.TabIndex = 22;
            this.restart.Text = "Reinicie o computador para desativar";
            this.restart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.restart.Visible = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Open Sans", 12F);
            this.label3.Location = new System.Drawing.Point(23, 230);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(626, 71);
            this.label3.TabIndex = 21;
            this.label3.Text = "Analisa arquivos quando eles são abertos, lidos ou modificados, além da auto-prot" +
    "eção, para impedir que malwares desativem o Nottext Antivirus, a proteção essenc" +
    "ial para um antivírus funcionar.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Shannon Std", 15.75F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(22, 193);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 26);
            this.label2.TabIndex = 19;
            this.label2.Text = "Proteção em kernel:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // subStatusLabel
            // 
            this.subStatusLabel.AutoSize = true;
            this.subStatusLabel.BackColor = System.Drawing.Color.Transparent;
            this.subStatusLabel.Font = new System.Drawing.Font("Open Sans", 12F);
            this.subStatusLabel.Location = new System.Drawing.Point(23, 149);
            this.subStatusLabel.Name = "subStatusLabel";
            this.subStatusLabel.Size = new System.Drawing.Size(503, 22);
            this.subStatusLabel.TabIndex = 17;
            this.subStatusLabel.Text = "Analisa processos e arquivos para encontrar e remover malware.";
            this.subStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Shannon Std", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 26);
            this.label1.TabIndex = 16;
            this.label1.Text = "Proteção em tempo real:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Shannon Std", 15.75F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(22, 320);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(260, 26);
            this.label4.TabIndex = 23;
            this.label4.Text = "Proteção anti-ransomware:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Open Sans", 12F);
            this.label5.Location = new System.Drawing.Point(23, 354);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(626, 44);
            this.label5.TabIndex = 25;
            this.label5.Text = "Impede que processsos maliciosos modifiquem seus arquivos, você será notificado c" +
    "aso ocorra algum evento.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Open Sans", 12F);
            this.label6.ForeColor = System.Drawing.Color.Crimson;
            this.label6.Location = new System.Drawing.Point(319, 324);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(244, 22);
            this.label6.TabIndex = 26;
            this.label6.Text = "Proteção em kernel necessário";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label6.Visible = false;
            // 
            // realTimeProtection
            // 
            this.realTimeProtection.Animated = true;
            this.realTimeProtection.Checked = true;
            this.realTimeProtection.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.realTimeProtection.CheckedState.BorderRadius = 2;
            this.realTimeProtection.CheckedState.BorderThickness = 0;
            this.realTimeProtection.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.realTimeProtection.CheckedState.Parent = this.realTimeProtection;
            this.realTimeProtection.CheckState = System.Windows.Forms.CheckState.Checked;
            this.realTimeProtection.Cursor = System.Windows.Forms.Cursors.Hand;
            this.realTimeProtection.Location = new System.Drawing.Point(273, 116);
            this.realTimeProtection.Name = "realTimeProtection";
            this.realTimeProtection.ShadowDecoration.Enabled = true;
            this.realTimeProtection.ShadowDecoration.Parent = this.realTimeProtection;
            this.realTimeProtection.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(2);
            this.realTimeProtection.Size = new System.Drawing.Size(25, 25);
            this.realTimeProtection.TabIndex = 37;
            this.realTimeProtection.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.realTimeProtection.UncheckedState.BorderRadius = 2;
            this.realTimeProtection.UncheckedState.BorderThickness = 0;
            this.realTimeProtection.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.realTimeProtection.UncheckedState.Parent = this.realTimeProtection;
            this.realTimeProtection.Click += new System.EventHandler(this.protecaoTempoReal_Click);
            // 
            // kernelProtection
            // 
            this.kernelProtection.Animated = true;
            this.kernelProtection.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.kernelProtection.CheckedState.BorderRadius = 2;
            this.kernelProtection.CheckedState.BorderThickness = 0;
            this.kernelProtection.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.kernelProtection.CheckedState.Parent = this.kernelProtection;
            this.kernelProtection.Cursor = System.Windows.Forms.Cursors.Hand;
            this.kernelProtection.Location = new System.Drawing.Point(228, 194);
            this.kernelProtection.Name = "kernelProtection";
            this.kernelProtection.ShadowDecoration.Enabled = true;
            this.kernelProtection.ShadowDecoration.Parent = this.kernelProtection;
            this.kernelProtection.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(2);
            this.kernelProtection.Size = new System.Drawing.Size(25, 25);
            this.kernelProtection.TabIndex = 38;
            this.kernelProtection.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.kernelProtection.UncheckedState.BorderRadius = 2;
            this.kernelProtection.UncheckedState.BorderThickness = 0;
            this.kernelProtection.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.kernelProtection.UncheckedState.Parent = this.kernelProtection;
            this.kernelProtection.Click += new System.EventHandler(this.protecaoKernel_Click);
            // 
            // antiRansomware
            // 
            this.antiRansomware.Animated = true;
            this.antiRansomware.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.antiRansomware.CheckedState.BorderRadius = 2;
            this.antiRansomware.CheckedState.BorderThickness = 0;
            this.antiRansomware.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.antiRansomware.CheckedState.Parent = this.antiRansomware;
            this.antiRansomware.Cursor = System.Windows.Forms.Cursors.Hand;
            this.antiRansomware.Location = new System.Drawing.Point(288, 321);
            this.antiRansomware.Name = "antiRansomware";
            this.antiRansomware.ShadowDecoration.Enabled = true;
            this.antiRansomware.ShadowDecoration.Parent = this.antiRansomware;
            this.antiRansomware.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(2);
            this.antiRansomware.Size = new System.Drawing.Size(25, 25);
            this.antiRansomware.TabIndex = 39;
            this.antiRansomware.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.antiRansomware.UncheckedState.BorderRadius = 2;
            this.antiRansomware.UncheckedState.BorderThickness = 0;
            this.antiRansomware.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.antiRansomware.UncheckedState.Parent = this.antiRansomware;
            this.antiRansomware.Click += new System.EventHandler(this.antiRansomware_Click);
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
            this.guna2Panel1.Location = new System.Drawing.Point(21, 23);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.ShadowDecoration.Parent = this.guna2Panel1;
            this.guna2Panel1.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.guna2Panel1.Size = new System.Drawing.Size(628, 71);
            this.guna2Panel1.TabIndex = 45;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Open Sans", 12F);
            this.label11.Location = new System.Drawing.Point(65, 37);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(343, 22);
            this.label11.TabIndex = 38;
            this.label11.Text = "As principais proteções do Nottext Antivirus";
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
            this.label12.Size = new System.Drawing.Size(232, 28);
            this.label12.TabIndex = 37;
            this.label12.Text = "Proteções de arquivos";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // folders
            // 
            this.folders.BackColor = System.Drawing.Color.Transparent;
            this.folders.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.folders.BorderRadius = 2;
            this.folders.BorderThickness = 1;
            this.folders.CheckedState.Parent = this.folders;
            this.folders.Cursor = System.Windows.Forms.Cursors.Hand;
            this.folders.CustomImages.Parent = this.folders;
            this.folders.FillColor = System.Drawing.Color.White;
            this.folders.Font = new System.Drawing.Font("Open Sans", 12F);
            this.folders.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.folders.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.folders.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.folders.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.folders.HoverState.Parent = this.folders;
            this.folders.Location = new System.Drawing.Point(27, 412);
            this.folders.Name = "folders";
            this.folders.ShadowDecoration.BorderRadius = 5;
            this.folders.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.folders.ShadowDecoration.Depth = 40;
            this.folders.ShadowDecoration.Parent = this.folders;
            this.folders.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(7, 7, 7, 12);
            this.folders.Size = new System.Drawing.Size(207, 32);
            this.folders.TabIndex = 46;
            this.folders.Text = "Pastas protegidas";
            this.folders.Click += new System.EventHandler(this.folders_Click);
            this.folders.MouseEnter += new System.EventHandler(this.ativarEfeito1_MouseHover);
            this.folders.MouseLeave += new System.EventHandler(this.desativarEfeito1_MouseLeave);
            // 
            // exceptionsRanso
            // 
            this.exceptionsRanso.BackColor = System.Drawing.Color.Transparent;
            this.exceptionsRanso.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.exceptionsRanso.BorderRadius = 2;
            this.exceptionsRanso.BorderThickness = 1;
            this.exceptionsRanso.CheckedState.Parent = this.exceptionsRanso;
            this.exceptionsRanso.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exceptionsRanso.CustomImages.Parent = this.exceptionsRanso;
            this.exceptionsRanso.FillColor = System.Drawing.Color.White;
            this.exceptionsRanso.Font = new System.Drawing.Font("Open Sans", 12F);
            this.exceptionsRanso.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.exceptionsRanso.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.exceptionsRanso.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.exceptionsRanso.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.exceptionsRanso.HoverState.Parent = this.exceptionsRanso;
            this.exceptionsRanso.Location = new System.Drawing.Point(259, 412);
            this.exceptionsRanso.Name = "exceptionsRanso";
            this.exceptionsRanso.ShadowDecoration.BorderRadius = 5;
            this.exceptionsRanso.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.exceptionsRanso.ShadowDecoration.Depth = 40;
            this.exceptionsRanso.ShadowDecoration.Parent = this.exceptionsRanso;
            this.exceptionsRanso.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(7, 7, 7, 12);
            this.exceptionsRanso.Size = new System.Drawing.Size(207, 32);
            this.exceptionsRanso.TabIndex = 47;
            this.exceptionsRanso.Text = "Adicionar exeções";
            this.exceptionsRanso.Click += new System.EventHandler(this.exceptionsRanso_Click);
            this.exceptionsRanso.MouseEnter += new System.EventHandler(this.ativarEfeito1_MouseHover);
            this.exceptionsRanso.MouseLeave += new System.EventHandler(this.desativarEfeito1_MouseLeave);
            // 
            // advancedScan
            // 
            this.advancedScan.Animated = true;
            this.advancedScan.Checked = true;
            this.advancedScan.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.advancedScan.CheckedState.BorderRadius = 2;
            this.advancedScan.CheckedState.BorderThickness = 0;
            this.advancedScan.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.advancedScan.CheckedState.Parent = this.advancedScan;
            this.advancedScan.CheckState = System.Windows.Forms.CheckState.Checked;
            this.advancedScan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.advancedScan.Location = new System.Drawing.Point(203, 467);
            this.advancedScan.Name = "advancedScan";
            this.advancedScan.ShadowDecoration.Enabled = true;
            this.advancedScan.ShadowDecoration.Parent = this.advancedScan;
            this.advancedScan.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(2);
            this.advancedScan.Size = new System.Drawing.Size(25, 25);
            this.advancedScan.TabIndex = 50;
            this.advancedScan.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.advancedScan.UncheckedState.BorderRadius = 2;
            this.advancedScan.UncheckedState.BorderThickness = 0;
            this.advancedScan.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.advancedScan.UncheckedState.Parent = this.advancedScan;
            this.advancedScan.Click += new System.EventHandler(this.advancedScan_Click);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Open Sans", 12F);
            this.label7.Location = new System.Drawing.Point(23, 500);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(626, 47);
            this.label7.TabIndex = 49;
            this.label7.Text = "Analisa o código fonte de arquivos usando engenharia reversa para tentar encontra" +
    "r código malicioso — aprimora a taxa de detecção de malware.";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Shannon Std", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(22, 466);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(175, 26);
            this.label8.TabIndex = 48;
            this.label8.Text = "Leitura profunda:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Open Sans", 12F);
            this.label9.Location = new System.Drawing.Point(23, 603);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(600, 30);
            this.label9.TabIndex = 59;
            this.label9.Text = "Selecione a ação que o antivírus deve realizar após encontrar algum malware.";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Shannon Std", 15.75F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(22, 569);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(258, 26);
            this.label10.TabIndex = 58;
            this.label10.Text = "Ação ao detectar ameaça:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            "Bloquear ameaça"});
            this.opcoes.ItemsAppearance.Parent = this.opcoes;
            this.opcoes.Location = new System.Drawing.Point(288, 564);
            this.opcoes.Name = "opcoes";
            this.opcoes.ShadowDecoration.Parent = this.opcoes;
            this.opcoes.Size = new System.Drawing.Size(241, 36);
            this.opcoes.StartIndex = 0;
            this.opcoes.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;
            this.opcoes.TabIndex = 61;
            this.opcoes.SelectedIndexChanged += new System.EventHandler(this.opcoes_SelectedIndexChanged);
            // 
            // FileProtection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMargin = new System.Drawing.Size(0, 30);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(746, 500);
            this.Controls.Add(this.opcoes);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.advancedScan);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.exceptionsRanso);
            this.Controls.Add(this.folders);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.antiRansomware);
            this.Controls.Add(this.kernelProtection);
            this.Controls.Add(this.realTimeProtection);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.restart);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.subStatusLabel);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FileProtection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Protections";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.MouseEnter += new System.EventHandler(this.FileProtection_MouseEnter);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label subStatusLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label restart;
        public Guna.UI2.WinForms.Guna2CustomCheckBox realTimeProtection;
        public Guna.UI2.WinForms.Guna2CustomCheckBox kernelProtection;
        public Guna.UI2.WinForms.Guna2CustomCheckBox antiRansomware;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label12;
        private Guna.UI2.WinForms.Guna2Button folders;
        private Guna.UI2.WinForms.Guna2Button exceptionsRanso;
        public Guna.UI2.WinForms.Guna2CustomCheckBox advancedScan;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private Guna.UI2.WinForms.Guna2ComboBox opcoes;
    }
}