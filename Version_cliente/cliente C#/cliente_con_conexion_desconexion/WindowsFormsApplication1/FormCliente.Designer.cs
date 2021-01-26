namespace WindowsFormsApplication1
{
    partial class FormCliente
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCliente));
            this.nombre = new System.Windows.Forms.TextBox();
            this.panelPeticion = new System.Windows.Forms.GroupBox();
            this.maspuntos = new System.Windows.Forms.RadioButton();
            this.ganador = new System.Windows.Forms.RadioButton();
            this.victorias = new System.Windows.Forms.RadioButton();
            this.dgviewListaUsuarios = new System.Windows.Forms.DataGridView();
            this.invitar = new System.Windows.Forms.Button();
            this.panelConexion = new System.Windows.Forms.Panel();
            this.desconectar = new System.Windows.Forms.Button();
            this.conectar = new System.Windows.Forms.Button();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.panelLogin = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.TextBox();
            this.username = new System.Windows.Forms.TextBox();
            this.register = new System.Windows.Forms.Button();
            this.login = new System.Windows.Forms.Button();
            this.panelConsultas = new System.Windows.Forms.Panel();
            this.enviar = new System.Windows.Forms.Button();
            this.panelJugadores = new System.Windows.Forms.Panel();
            this.iniciarPartida = new System.Windows.Forms.Button();
            this.mensajesJuego = new System.Windows.Forms.TextBox();
            this.idPartida = new System.Windows.Forms.TextBox();
            this.panelPeticion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgviewListaUsuarios)).BeginInit();
            this.panelConexion.SuspendLayout();
            this.panelLogin.SuspendLayout();
            this.panelConsultas.SuspendLayout();
            this.panelJugadores.SuspendLayout();
            this.SuspendLayout();
            // 
            // nombre
            // 
            this.nombre.Location = new System.Drawing.Point(172, 19);
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(164, 20);
            this.nombre.TabIndex = 3;
            // 
            // panelPeticion
            // 
            this.panelPeticion.BackColor = System.Drawing.SystemColors.Control;
            this.panelPeticion.Controls.Add(this.idPartida);
            this.panelPeticion.Controls.Add(this.maspuntos);
            this.panelPeticion.Controls.Add(this.ganador);
            this.panelPeticion.Controls.Add(this.victorias);
            this.panelPeticion.Controls.Add(this.nombre);
            this.panelPeticion.Location = new System.Drawing.Point(13, 18);
            this.panelPeticion.Name = "panelPeticion";
            this.panelPeticion.Size = new System.Drawing.Size(342, 164);
            this.panelPeticion.TabIndex = 6;
            this.panelPeticion.TabStop = false;
            // 
            // maspuntos
            // 
            this.maspuntos.AutoSize = true;
            this.maspuntos.Location = new System.Drawing.Point(19, 56);
            this.maspuntos.Name = "maspuntos";
            this.maspuntos.Size = new System.Drawing.Size(136, 17);
            this.maspuntos.TabIndex = 7;
            this.maspuntos.TabStop = true;
            this.maspuntos.Text = "Quien tiene mas puntos";
            this.maspuntos.UseVisualStyleBackColor = true;
            // 
            // ganador
            // 
            this.ganador.Location = new System.Drawing.Point(19, 92);
            this.ganador.Name = "ganador";
            this.ganador.Size = new System.Drawing.Size(178, 46);
            this.ganador.TabIndex = 7;
            this.ganador.TabStop = true;
            this.ganador.Text = "Quien ha ganado  partida número:";
            this.ganador.UseVisualStyleBackColor = true;
            this.ganador.CheckedChanged += new System.EventHandler(this.ganador_CheckedChanged);
            // 
            // victorias
            // 
            this.victorias.AutoSize = true;
            this.victorias.Location = new System.Drawing.Point(19, 19);
            this.victorias.Name = "victorias";
            this.victorias.Size = new System.Drawing.Size(132, 17);
            this.victorias.TabIndex = 8;
            this.victorias.TabStop = true;
            this.victorias.Text = "Cuantas victorias tiene";
            this.victorias.UseVisualStyleBackColor = true;
            // 
            // dgviewListaUsuarios
            // 
            this.dgviewListaUsuarios.AllowUserToAddRows = false;
            this.dgviewListaUsuarios.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgviewListaUsuarios.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgviewListaUsuarios.BackgroundColor = System.Drawing.Color.Snow;
            this.dgviewListaUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgviewListaUsuarios.Location = new System.Drawing.Point(15, 10);
            this.dgviewListaUsuarios.Margin = new System.Windows.Forms.Padding(2);
            this.dgviewListaUsuarios.Name = "dgviewListaUsuarios";
            this.dgviewListaUsuarios.RowHeadersWidth = 51;
            this.dgviewListaUsuarios.RowTemplate.Height = 24;
            this.dgviewListaUsuarios.Size = new System.Drawing.Size(279, 170);
            this.dgviewListaUsuarios.TabIndex = 20;
            // 
            // invitar
            // 
            this.invitar.BackColor = System.Drawing.Color.White;
            this.invitar.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.invitar.Image = ((System.Drawing.Image)(resources.GetObject("invitar.Image")));
            this.invitar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.invitar.Location = new System.Drawing.Point(317, 10);
            this.invitar.Margin = new System.Windows.Forms.Padding(2);
            this.invitar.Name = "invitar";
            this.invitar.Size = new System.Drawing.Size(90, 85);
            this.invitar.TabIndex = 21;
            this.invitar.Text = "INVITAR JUGADORES";
            this.invitar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.invitar.UseVisualStyleBackColor = false;
            this.invitar.Click += new System.EventHandler(this.invitar_Click);
            // 
            // panelConexion
            // 
            this.panelConexion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelConexion.Controls.Add(this.desconectar);
            this.panelConexion.Controls.Add(this.conectar);
            this.panelConexion.Controls.Add(this.lblMensaje);
            this.panelConexion.Location = new System.Drawing.Point(23, 10);
            this.panelConexion.Margin = new System.Windows.Forms.Padding(2);
            this.panelConexion.Name = "panelConexion";
            this.panelConexion.Size = new System.Drawing.Size(698, 109);
            this.panelConexion.TabIndex = 22;
            // 
            // desconectar
            // 
            this.desconectar.BackColor = System.Drawing.Color.White;
            this.desconectar.Enabled = false;
            this.desconectar.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.desconectar.Image = ((System.Drawing.Image)(resources.GetObject("desconectar.Image")));
            this.desconectar.Location = new System.Drawing.Point(173, 6);
            this.desconectar.Name = "desconectar";
            this.desconectar.Size = new System.Drawing.Size(113, 90);
            this.desconectar.TabIndex = 11;
            this.desconectar.Text = "DESCONECTAR";
            this.desconectar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.desconectar.UseVisualStyleBackColor = false;
            this.desconectar.Click += new System.EventHandler(this.desconectar_Click);
            // 
            // conectar
            // 
            this.conectar.BackColor = System.Drawing.Color.White;
            this.conectar.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.conectar.Image = ((System.Drawing.Image)(resources.GetObject("conectar.Image")));
            this.conectar.Location = new System.Drawing.Point(14, 6);
            this.conectar.Name = "conectar";
            this.conectar.Size = new System.Drawing.Size(122, 90);
            this.conectar.TabIndex = 5;
            this.conectar.Text = "CONECTAR";
            this.conectar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.conectar.UseVisualStyleBackColor = false;
            this.conectar.Click += new System.EventHandler(this.conectar_Click);
            // 
            // lblMensaje
            // 
            this.lblMensaje.BackColor = System.Drawing.Color.AliceBlue;
            this.lblMensaje.Font = new System.Drawing.Font("Comic Sans MS", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensaje.Location = new System.Drawing.Point(319, 30);
            this.lblMensaje.Margin = new System.Windows.Forms.Padding(0);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(337, 45);
            this.lblMensaje.TabIndex = 2;
            this.lblMensaje.Text = "        ******   Bienvenido   ******                 Presione Conectar para inici" +
    "ar el Servicio     ";
            this.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelLogin
            // 
            this.panelLogin.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelLogin.Controls.Add(this.label4);
            this.panelLogin.Controls.Add(this.label1);
            this.panelLogin.Controls.Add(this.password);
            this.panelLogin.Controls.Add(this.username);
            this.panelLogin.Controls.Add(this.register);
            this.panelLogin.Controls.Add(this.login);
            this.panelLogin.Enabled = false;
            this.panelLogin.Location = new System.Drawing.Point(23, 126);
            this.panelLogin.Margin = new System.Windows.Forms.Padding(2);
            this.panelLogin.Name = "panelLogin";
            this.panelLogin.Size = new System.Drawing.Size(296, 235);
            this.panelLogin.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(23, 78);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 16);
            this.label4.TabIndex = 23;
            this.label4.Text = "CONTRASEÑA";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 22;
            this.label1.Text = "USUARIO";
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(173, 77);
            this.password.Margin = new System.Windows.Forms.Padding(2);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(101, 20);
            this.password.TabIndex = 21;
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(174, 33);
            this.username.Margin = new System.Windows.Forms.Padding(2);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(100, 20);
            this.username.TabIndex = 20;
            // 
            // register
            // 
            this.register.BackColor = System.Drawing.Color.White;
            this.register.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.register.Image = ((System.Drawing.Image)(resources.GetObject("register.Image")));
            this.register.Location = new System.Drawing.Point(150, 123);
            this.register.Margin = new System.Windows.Forms.Padding(2);
            this.register.Name = "register";
            this.register.Size = new System.Drawing.Size(79, 83);
            this.register.TabIndex = 19;
            this.register.Text = "REGISTRO";
            this.register.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.register.UseVisualStyleBackColor = false;
            this.register.Click += new System.EventHandler(this.register_Click);
            // 
            // login
            // 
            this.login.BackColor = System.Drawing.Color.White;
            this.login.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login.Image = ((System.Drawing.Image)(resources.GetObject("login.Image")));
            this.login.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.login.Location = new System.Drawing.Point(26, 122);
            this.login.Margin = new System.Windows.Forms.Padding(2);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(82, 84);
            this.login.TabIndex = 18;
            this.login.Text = "INICIAR SESIÓN";
            this.login.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.login.UseVisualStyleBackColor = false;
            this.login.Click += new System.EventHandler(this.login_Click);
            // 
            // panelConsultas
            // 
            this.panelConsultas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelConsultas.Controls.Add(this.enviar);
            this.panelConsultas.Controls.Add(this.panelPeticion);
            this.panelConsultas.Enabled = false;
            this.panelConsultas.Location = new System.Drawing.Point(348, 126);
            this.panelConsultas.Name = "panelConsultas";
            this.panelConsultas.Size = new System.Drawing.Size(370, 235);
            this.panelConsultas.TabIndex = 24;
            // 
            // enviar
            // 
            this.enviar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.enviar.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enviar.Image = ((System.Drawing.Image)(resources.GetObject("enviar.Image")));
            this.enviar.Location = new System.Drawing.Point(287, 188);
            this.enviar.Name = "enviar";
            this.enviar.Size = new System.Drawing.Size(68, 40);
            this.enviar.TabIndex = 6;
            this.enviar.Text = "ENVIAR";
            this.enviar.UseVisualStyleBackColor = false;
            this.enviar.Click += new System.EventHandler(this.enviar_Click_1);
            // 
            // panelJugadores
            // 
            this.panelJugadores.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelJugadores.Controls.Add(this.iniciarPartida);
            this.panelJugadores.Controls.Add(this.mensajesJuego);
            this.panelJugadores.Controls.Add(this.invitar);
            this.panelJugadores.Controls.Add(this.dgviewListaUsuarios);
            this.panelJugadores.Enabled = false;
            this.panelJugadores.Location = new System.Drawing.Point(23, 366);
            this.panelJugadores.Name = "panelJugadores";
            this.panelJugadores.Size = new System.Drawing.Size(695, 186);
            this.panelJugadores.TabIndex = 25;
            // 
            // iniciarPartida
            // 
            this.iniciarPartida.BackColor = System.Drawing.Color.White;
            this.iniciarPartida.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iniciarPartida.Image = ((System.Drawing.Image)(resources.GetObject("iniciarPartida.Image")));
            this.iniciarPartida.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.iniciarPartida.Location = new System.Drawing.Point(317, 110);
            this.iniciarPartida.Margin = new System.Windows.Forms.Padding(2);
            this.iniciarPartida.Name = "iniciarPartida";
            this.iniciarPartida.Size = new System.Drawing.Size(90, 70);
            this.iniciarPartida.TabIndex = 23;
            this.iniciarPartida.Text = "A JUGAR!!!";
            this.iniciarPartida.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.iniciarPartida.UseVisualStyleBackColor = false;
            this.iniciarPartida.Click += new System.EventHandler(this.iniciarPartida_Click);
            // 
            // mensajesJuego
            // 
            this.mensajesJuego.BackColor = System.Drawing.Color.AliceBlue;
            this.mensajesJuego.Location = new System.Drawing.Point(421, 14);
            this.mensajesJuego.Multiline = true;
            this.mensajesJuego.Name = "mensajesJuego";
            this.mensajesJuego.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.mensajesJuego.Size = new System.Drawing.Size(253, 165);
            this.mensajesJuego.TabIndex = 22;
            // 
            // idPartida
            // 
            this.idPartida.Location = new System.Drawing.Point(203, 100);
            this.idPartida.Name = "idPartida";
            this.idPartida.Size = new System.Drawing.Size(90, 20);
            this.idPartida.TabIndex = 9;
            // 
            // FormCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 564);
            this.Controls.Add(this.panelJugadores);
            this.Controls.Add(this.panelConsultas);
            this.Controls.Add(this.panelLogin);
            this.Controls.Add(this.panelConexion);
            this.Name = "FormCliente";
            this.Text = "FormCliente";
            this.Load += new System.EventHandler(this.FormCliente_Load);
            this.panelPeticion.ResumeLayout(false);
            this.panelPeticion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgviewListaUsuarios)).EndInit();
            this.panelConexion.ResumeLayout(false);
            this.panelLogin.ResumeLayout(false);
            this.panelLogin.PerformLayout();
            this.panelConsultas.ResumeLayout(false);
            this.panelJugadores.ResumeLayout(false);
            this.panelJugadores.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox nombre;
        private System.Windows.Forms.GroupBox panelPeticion;
        private System.Windows.Forms.RadioButton maspuntos;
        private System.Windows.Forms.RadioButton victorias;
        private System.Windows.Forms.RadioButton ganador;
        private System.Windows.Forms.DataGridView dgviewListaUsuarios;
        private System.Windows.Forms.Button invitar;
        private System.Windows.Forms.Panel panelConexion;
        private System.Windows.Forms.Button conectar;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Button desconectar;
        private System.Windows.Forms.Panel panelLogin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.Button register;
        private System.Windows.Forms.Button login;
        private System.Windows.Forms.Panel panelConsultas;
        private System.Windows.Forms.Panel panelJugadores;
        private System.Windows.Forms.TextBox mensajesJuego;
        private System.Windows.Forms.Button iniciarPartida;
        public System.Windows.Forms.Button enviar;
        private System.Windows.Forms.TextBox idPartida;
    }
}

