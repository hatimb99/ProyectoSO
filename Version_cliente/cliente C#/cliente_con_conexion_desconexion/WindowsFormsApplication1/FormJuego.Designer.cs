namespace WindowsFormsApplication1
{
    partial class FormJuego
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormJuego));
            this.panelPuntuaciones = new System.Windows.Forms.Panel();
            this.puntuaciones = new System.Windows.Forms.DataGridView();
            this.panelChat = new System.Windows.Forms.Panel();
            this.miMensaje = new System.Windows.Forms.TextBox();
            this.mensajesChat = new System.Windows.Forms.TextBox();
            this.panelMiJuego = new System.Windows.Forms.Panel();
            this.panelTirada = new System.Windows.Forms.Panel();
            this.dadoTirada5 = new System.Windows.Forms.PictureBox();
            this.dadoTirada1 = new System.Windows.Forms.PictureBox();
            this.dadoTirada4 = new System.Windows.Forms.PictureBox();
            this.dadoTirada2 = new System.Windows.Forms.PictureBox();
            this.dadoTirada3 = new System.Windows.Forms.PictureBox();
            this.detenerDados = new System.Windows.Forms.Button();
            this.panelDadosMoviendo = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.mensajeTirada = new System.Windows.Forms.Label();
            this.panelDadosFijos = new System.Windows.Forms.Panel();
            this.dadoFijo5 = new System.Windows.Forms.PictureBox();
            this.dadoFijo1 = new System.Windows.Forms.PictureBox();
            this.dadoFijo4 = new System.Windows.Forms.PictureBox();
            this.dadoFijo2 = new System.Windows.Forms.PictureBox();
            this.dadoFijo3 = new System.Windows.Forms.PictureBox();
            this.lanzar = new System.Windows.Forms.Button();
            this.panelOtroJugador = new System.Windows.Forms.Panel();
            this.lanzandoOtroJug = new System.Windows.Forms.PictureBox();
            this.nombreJugador = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ImagenesDados = new System.Windows.Forms.ImageList(this.components);
            this.panelPuntuaciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.puntuaciones)).BeginInit();
            this.panelChat.SuspendLayout();
            this.panelMiJuego.SuspendLayout();
            this.panelTirada.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dadoTirada5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dadoTirada1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dadoTirada4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dadoTirada2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dadoTirada3)).BeginInit();
            this.panelDadosMoviendo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.panelDadosFijos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dadoFijo5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dadoFijo1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dadoFijo4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dadoFijo2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dadoFijo3)).BeginInit();
            this.panelOtroJugador.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lanzandoOtroJug)).BeginInit();
            this.SuspendLayout();
            // 
            // panelPuntuaciones
            // 
            this.panelPuntuaciones.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelPuntuaciones.Controls.Add(this.puntuaciones);
            this.panelPuntuaciones.Location = new System.Drawing.Point(717, 15);
            this.panelPuntuaciones.Margin = new System.Windows.Forms.Padding(4);
            this.panelPuntuaciones.Name = "panelPuntuaciones";
            this.panelPuntuaciones.Size = new System.Drawing.Size(512, 496);
            this.panelPuntuaciones.TabIndex = 0;
            // 
            // puntuaciones
            // 
            this.puntuaciones.AllowUserToAddRows = false;
            this.puntuaciones.AllowUserToDeleteRows = false;
            this.puntuaciones.BackgroundColor = System.Drawing.Color.SeaShell;
            this.puntuaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.puntuaciones.Location = new System.Drawing.Point(12, 11);
            this.puntuaciones.Margin = new System.Windows.Forms.Padding(4);
            this.puntuaciones.Name = "puntuaciones";
            this.puntuaciones.ReadOnly = true;
            this.puntuaciones.RowTemplate.ReadOnly = true;
            this.puntuaciones.Size = new System.Drawing.Size(480, 468);
            this.puntuaciones.TabIndex = 0;
            this.puntuaciones.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.puntuaciones_CellDoubleClick);
            // 
            // panelChat
            // 
            this.panelChat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelChat.Controls.Add(this.miMensaje);
            this.panelChat.Controls.Add(this.mensajesChat);
            this.panelChat.Location = new System.Drawing.Point(717, 550);
            this.panelChat.Margin = new System.Windows.Forms.Padding(4);
            this.panelChat.Name = "panelChat";
            this.panelChat.Size = new System.Drawing.Size(512, 275);
            this.panelChat.TabIndex = 1;
            // 
            // miMensaje
            // 
            this.miMensaje.Location = new System.Drawing.Point(23, 228);
            this.miMensaje.Margin = new System.Windows.Forms.Padding(4);
            this.miMensaje.Name = "miMensaje";
            this.miMensaje.Size = new System.Drawing.Size(465, 22);
            this.miMensaje.TabIndex = 1;
            this.miMensaje.KeyDown += new System.Windows.Forms.KeyEventHandler(this.miMensaje_KeyDown);
            // 
            // mensajesChat
            // 
            this.mensajesChat.AcceptsReturn = true;
            this.mensajesChat.AcceptsTab = true;
            this.mensajesChat.Location = new System.Drawing.Point(23, 16);
            this.mensajesChat.Margin = new System.Windows.Forms.Padding(4);
            this.mensajesChat.Multiline = true;
            this.mensajesChat.Name = "mensajesChat";
            this.mensajesChat.ReadOnly = true;
            this.mensajesChat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.mensajesChat.Size = new System.Drawing.Size(468, 187);
            this.mensajesChat.TabIndex = 0;
            this.mensajesChat.TabStop = false;
            // 
            // panelMiJuego
            // 
            this.panelMiJuego.BackColor = System.Drawing.Color.SeaGreen;
            this.panelMiJuego.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelMiJuego.Controls.Add(this.panelTirada);
            this.panelMiJuego.Controls.Add(this.detenerDados);
            this.panelMiJuego.Controls.Add(this.panelDadosMoviendo);
            this.panelMiJuego.Controls.Add(this.mensajeTirada);
            this.panelMiJuego.Controls.Add(this.panelDadosFijos);
            this.panelMiJuego.Controls.Add(this.lanzar);
            this.panelMiJuego.Location = new System.Drawing.Point(31, 15);
            this.panelMiJuego.Margin = new System.Windows.Forms.Padding(4);
            this.panelMiJuego.Name = "panelMiJuego";
            this.panelMiJuego.Size = new System.Drawing.Size(647, 496);
            this.panelMiJuego.TabIndex = 2;
            // 
            // panelTirada
            // 
            this.panelTirada.Controls.Add(this.dadoTirada5);
            this.panelTirada.Controls.Add(this.dadoTirada1);
            this.panelTirada.Controls.Add(this.dadoTirada4);
            this.panelTirada.Controls.Add(this.dadoTirada2);
            this.panelTirada.Controls.Add(this.dadoTirada3);
            this.panelTirada.Location = new System.Drawing.Point(101, 63);
            this.panelTirada.Margin = new System.Windows.Forms.Padding(4);
            this.panelTirada.Name = "panelTirada";
            this.panelTirada.Size = new System.Drawing.Size(447, 212);
            this.panelTirada.TabIndex = 8;
            this.panelTirada.Visible = false;
            // 
            // dadoTirada5
            // 
            this.dadoTirada5.BackColor = System.Drawing.Color.SeaGreen;
            this.dadoTirada5.Location = new System.Drawing.Point(345, 12);
            this.dadoTirada5.Margin = new System.Windows.Forms.Padding(1);
            this.dadoTirada5.Name = "dadoTirada5";
            this.dadoTirada5.Size = new System.Drawing.Size(76, 74);
            this.dadoTirada5.TabIndex = 4;
            this.dadoTirada5.TabStop = false;
            this.dadoTirada5.Click += new System.EventHandler(this.dadoTirada5_Click);
            // 
            // dadoTirada1
            // 
            this.dadoTirada1.BackColor = System.Drawing.Color.SeaGreen;
            this.dadoTirada1.Location = new System.Drawing.Point(69, 12);
            this.dadoTirada1.Margin = new System.Windows.Forms.Padding(1);
            this.dadoTirada1.Name = "dadoTirada1";
            this.dadoTirada1.Size = new System.Drawing.Size(76, 74);
            this.dadoTirada1.TabIndex = 0;
            this.dadoTirada1.TabStop = false;
            this.dadoTirada1.DoubleClick += new System.EventHandler(this.dadoTirada1_DoubleClick);
            // 
            // dadoTirada4
            // 
            this.dadoTirada4.BackColor = System.Drawing.Color.SeaGreen;
            this.dadoTirada4.Location = new System.Drawing.Point(284, 119);
            this.dadoTirada4.Margin = new System.Windows.Forms.Padding(1);
            this.dadoTirada4.Name = "dadoTirada4";
            this.dadoTirada4.Size = new System.Drawing.Size(76, 74);
            this.dadoTirada4.TabIndex = 3;
            this.dadoTirada4.TabStop = false;
            this.dadoTirada4.Click += new System.EventHandler(this.dadoTirada4_Click);
            // 
            // dadoTirada2
            // 
            this.dadoTirada2.BackColor = System.Drawing.Color.SeaGreen;
            this.dadoTirada2.Location = new System.Drawing.Point(36, 105);
            this.dadoTirada2.Margin = new System.Windows.Forms.Padding(1);
            this.dadoTirada2.Name = "dadoTirada2";
            this.dadoTirada2.Size = new System.Drawing.Size(76, 74);
            this.dadoTirada2.TabIndex = 1;
            this.dadoTirada2.TabStop = false;
            this.dadoTirada2.Click += new System.EventHandler(this.dadoTirada2_Click);
            // 
            // dadoTirada3
            // 
            this.dadoTirada3.BackColor = System.Drawing.Color.SeaGreen;
            this.dadoTirada3.Location = new System.Drawing.Point(181, 64);
            this.dadoTirada3.Margin = new System.Windows.Forms.Padding(1);
            this.dadoTirada3.Name = "dadoTirada3";
            this.dadoTirada3.Size = new System.Drawing.Size(76, 74);
            this.dadoTirada3.TabIndex = 2;
            this.dadoTirada3.TabStop = false;
            this.dadoTirada3.Click += new System.EventHandler(this.dadoTirada3_Click);
            // 
            // detenerDados
            // 
            this.detenerDados.BackColor = System.Drawing.Color.White;
            this.detenerDados.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.detenerDados.Image = ((System.Drawing.Image)(resources.GetObject("detenerDados.Image")));
            this.detenerDados.Location = new System.Drawing.Point(340, 282);
            this.detenerDados.Margin = new System.Windows.Forms.Padding(4);
            this.detenerDados.Name = "detenerDados";
            this.detenerDados.Size = new System.Drawing.Size(119, 94);
            this.detenerDados.TabIndex = 10;
            this.detenerDados.Text = "Parar Dados";
            this.detenerDados.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.detenerDados.UseVisualStyleBackColor = false;
            this.detenerDados.Click += new System.EventHandler(this.detenerDados_Click);
            // 
            // panelDadosMoviendo
            // 
            this.panelDadosMoviendo.CausesValidation = false;
            this.panelDadosMoviendo.Controls.Add(this.pictureBox3);
            this.panelDadosMoviendo.Controls.Add(this.pictureBox1);
            this.panelDadosMoviendo.Controls.Add(this.pictureBox5);
            this.panelDadosMoviendo.Controls.Add(this.pictureBox2);
            this.panelDadosMoviendo.Controls.Add(this.pictureBox4);
            this.panelDadosMoviendo.Location = new System.Drawing.Point(101, 75);
            this.panelDadosMoviendo.Margin = new System.Windows.Forms.Padding(4);
            this.panelDadosMoviendo.Name = "panelDadosMoviendo";
            this.panelDadosMoviendo.Size = new System.Drawing.Size(431, 185);
            this.panelDadosMoviendo.TabIndex = 5;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(345, 18);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(1);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(76, 70);
            this.pictureBox3.TabIndex = 4;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(24, 33);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(76, 70);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(252, 81);
            this.pictureBox5.Margin = new System.Windows.Forms.Padding(1);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(76, 70);
            this.pictureBox5.TabIndex = 3;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(129, 106);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(1);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(76, 70);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(152, 6);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(1);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(76, 70);
            this.pictureBox4.TabIndex = 2;
            this.pictureBox4.TabStop = false;
            // 
            // mensajeTirada
            // 
            this.mensajeTirada.AutoSize = true;
            this.mensajeTirada.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mensajeTirada.ForeColor = System.Drawing.Color.White;
            this.mensajeTirada.Location = new System.Drawing.Point(60, 11);
            this.mensajeTirada.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.mensajeTirada.Name = "mensajeTirada";
            this.mensajeTirada.Size = new System.Drawing.Size(0, 27);
            this.mensajeTirada.TabIndex = 9;
            // 
            // panelDadosFijos
            // 
            this.panelDadosFijos.BackColor = System.Drawing.Color.SeaGreen;
            this.panelDadosFijos.Controls.Add(this.dadoFijo5);
            this.panelDadosFijos.Controls.Add(this.dadoFijo1);
            this.panelDadosFijos.Controls.Add(this.dadoFijo4);
            this.panelDadosFijos.Controls.Add(this.dadoFijo2);
            this.panelDadosFijos.Controls.Add(this.dadoFijo3);
            this.panelDadosFijos.Location = new System.Drawing.Point(64, 383);
            this.panelDadosFijos.Margin = new System.Windows.Forms.Padding(4);
            this.panelDadosFijos.Name = "panelDadosFijos";
            this.panelDadosFijos.Size = new System.Drawing.Size(468, 96);
            this.panelDadosFijos.TabIndex = 7;
            // 
            // dadoFijo5
            // 
            this.dadoFijo5.BackColor = System.Drawing.Color.SeaGreen;
            this.dadoFijo5.Location = new System.Drawing.Point(371, 15);
            this.dadoFijo5.Margin = new System.Windows.Forms.Padding(1);
            this.dadoFijo5.Name = "dadoFijo5";
            this.dadoFijo5.Size = new System.Drawing.Size(76, 74);
            this.dadoFijo5.TabIndex = 4;
            this.dadoFijo5.TabStop = false;
            // 
            // dadoFijo1
            // 
            this.dadoFijo1.BackColor = System.Drawing.Color.SeaGreen;
            this.dadoFijo1.Location = new System.Drawing.Point(24, 15);
            this.dadoFijo1.Margin = new System.Windows.Forms.Padding(1);
            this.dadoFijo1.Name = "dadoFijo1";
            this.dadoFijo1.Size = new System.Drawing.Size(76, 74);
            this.dadoFijo1.TabIndex = 0;
            this.dadoFijo1.TabStop = false;
            // 
            // dadoFijo4
            // 
            this.dadoFijo4.BackColor = System.Drawing.Color.SeaGreen;
            this.dadoFijo4.Location = new System.Drawing.Point(284, 15);
            this.dadoFijo4.Margin = new System.Windows.Forms.Padding(1);
            this.dadoFijo4.Name = "dadoFijo4";
            this.dadoFijo4.Size = new System.Drawing.Size(76, 74);
            this.dadoFijo4.TabIndex = 3;
            this.dadoFijo4.TabStop = false;
            // 
            // dadoFijo2
            // 
            this.dadoFijo2.BackColor = System.Drawing.Color.SeaGreen;
            this.dadoFijo2.Location = new System.Drawing.Point(111, 15);
            this.dadoFijo2.Margin = new System.Windows.Forms.Padding(1);
            this.dadoFijo2.Name = "dadoFijo2";
            this.dadoFijo2.Size = new System.Drawing.Size(76, 74);
            this.dadoFijo2.TabIndex = 1;
            this.dadoFijo2.TabStop = false;
            // 
            // dadoFijo3
            // 
            this.dadoFijo3.BackColor = System.Drawing.Color.SeaGreen;
            this.dadoFijo3.Location = new System.Drawing.Point(197, 15);
            this.dadoFijo3.Margin = new System.Windows.Forms.Padding(1);
            this.dadoFijo3.Name = "dadoFijo3";
            this.dadoFijo3.Size = new System.Drawing.Size(76, 74);
            this.dadoFijo3.TabIndex = 2;
            this.dadoFijo3.TabStop = false;
            // 
            // lanzar
            // 
            this.lanzar.BackColor = System.Drawing.Color.White;
            this.lanzar.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lanzar.Image = ((System.Drawing.Image)(resources.GetObject("lanzar.Image")));
            this.lanzar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lanzar.Location = new System.Drawing.Point(160, 282);
            this.lanzar.Margin = new System.Windows.Forms.Padding(4);
            this.lanzar.Name = "lanzar";
            this.lanzar.Size = new System.Drawing.Size(119, 94);
            this.lanzar.TabIndex = 6;
            this.lanzar.Text = "Lanzar Dados";
            this.lanzar.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lanzar.UseVisualStyleBackColor = false;
            this.lanzar.Click += new System.EventHandler(this.lanzar_Click);
            // 
            // panelOtroJugador
            // 
            this.panelOtroJugador.BackColor = System.Drawing.Color.White;
            this.panelOtroJugador.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelOtroJugador.Controls.Add(this.lanzandoOtroJug);
            this.panelOtroJugador.Controls.Add(this.nombreJugador);
            this.panelOtroJugador.Controls.Add(this.label1);
            this.panelOtroJugador.Location = new System.Drawing.Point(31, 550);
            this.panelOtroJugador.Margin = new System.Windows.Forms.Padding(4);
            this.panelOtroJugador.Name = "panelOtroJugador";
            this.panelOtroJugador.Size = new System.Drawing.Size(647, 275);
            this.panelOtroJugador.TabIndex = 3;
            // 
            // lanzandoOtroJug
            // 
            this.lanzandoOtroJug.Image = ((System.Drawing.Image)(resources.GetObject("lanzandoOtroJug.Image")));
            this.lanzandoOtroJug.Location = new System.Drawing.Point(26, 47);
            this.lanzandoOtroJug.Margin = new System.Windows.Forms.Padding(4);
            this.lanzandoOtroJug.Name = "lanzandoOtroJug";
            this.lanzandoOtroJug.Size = new System.Drawing.Size(612, 128);
            this.lanzandoOtroJug.TabIndex = 1;
            this.lanzandoOtroJug.TabStop = false;
            this.lanzandoOtroJug.Visible = false;
            // 
            // nombreJugador
            // 
            this.nombreJugador.AutoSize = true;
            this.nombreJugador.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.nombreJugador.Location = new System.Drawing.Point(405, 14);
            this.nombreJugador.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.nombreJugador.Name = "nombreJugador";
            this.nombreJugador.Size = new System.Drawing.Size(129, 17);
            this.nombreJugador.TabIndex = 1;
            this.nombreJugador.Text = "XXXXXX Lanzando";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            
            // 
            // ImagenesDados
            // 
            this.ImagenesDados.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImagenesDados.ImageStream")));
            this.ImagenesDados.TransparentColor = System.Drawing.Color.SeaGreen;
            this.ImagenesDados.Images.SetKeyName(0, "dado_1_transp.png");
            this.ImagenesDados.Images.SetKeyName(1, "dado_2_transp.png");
            this.ImagenesDados.Images.SetKeyName(2, "dado_3_transp.png");
            this.ImagenesDados.Images.SetKeyName(3, "dado_4_transp.png");
            this.ImagenesDados.Images.SetKeyName(4, "dado_5_transp.png");
            this.ImagenesDados.Images.SetKeyName(5, "dado_6_transp.png");
            // 
            // FormJuego
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1263, 841);
            this.Controls.Add(this.panelOtroJugador);
            this.Controls.Add(this.panelChat);
            this.Controls.Add(this.panelPuntuaciones);
            this.Controls.Add(this.panelMiJuego);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormJuego";
            this.Text = "Partida Yahtzee";
            this.Load += new System.EventHandler(this.FormJuego_Load);
            this.panelPuntuaciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.puntuaciones)).EndInit();
            this.panelChat.ResumeLayout(false);
            this.panelChat.PerformLayout();
            this.panelMiJuego.ResumeLayout(false);
            this.panelMiJuego.PerformLayout();
            this.panelTirada.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dadoTirada5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dadoTirada1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dadoTirada4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dadoTirada2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dadoTirada3)).EndInit();
            this.panelDadosMoviendo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.panelDadosFijos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dadoFijo5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dadoFijo1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dadoFijo4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dadoFijo2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dadoFijo3)).EndInit();
            this.panelOtroJugador.ResumeLayout(false);
            this.panelOtroJugador.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lanzandoOtroJug)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelPuntuaciones;
        private System.Windows.Forms.Panel panelChat;
        private System.Windows.Forms.Panel panelMiJuego;
        private System.Windows.Forms.Panel panelOtroJugador;
        private System.Windows.Forms.TextBox mensajesChat;
        private System.Windows.Forms.TextBox miMensaje;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView puntuaciones;
        private System.Windows.Forms.Label nombreJugador;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox lanzandoOtroJug;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Panel panelDadosMoviendo;
        private System.Windows.Forms.Button lanzar;
        private System.Windows.Forms.Panel panelTirada;
        private System.Windows.Forms.PictureBox dadoTirada5;
        private System.Windows.Forms.PictureBox dadoTirada1;
        private System.Windows.Forms.PictureBox dadoTirada4;
        private System.Windows.Forms.PictureBox dadoTirada2;
        private System.Windows.Forms.PictureBox dadoTirada3;
        private System.Windows.Forms.Panel panelDadosFijos;
        private System.Windows.Forms.PictureBox dadoFijo5;
        private System.Windows.Forms.PictureBox dadoFijo1;
        private System.Windows.Forms.PictureBox dadoFijo4;
        private System.Windows.Forms.PictureBox dadoFijo2;
        private System.Windows.Forms.PictureBox dadoFijo3;
        private System.Windows.Forms.ImageList ImagenesDados;
        private System.Windows.Forms.Label mensajeTirada;
        private System.Windows.Forms.Button detenerDados;
    }
}