using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace WindowsFormsApplication1
{
    /* Enumerador para manejo de mensajes con el servidor */
    enum codigosMensajes
    {
        DESCONEXION_EXITOSA             = 0,
        CONSULTA_NRO_VICTORIAS          = 1,
        CONSULTA_MAS_PUNTOS             = 2,
        CONSULTA_GANADOR_PARTIDA        = 3,
        LOGIN                           = 4,
        REGISTRO                        = 5,
        CAMBIO_LISTA_JUGADORES          = 6,
        INVITAR_PARTIDA                 = 7,
        ACEPTA_JUEGO                    = 8,
        INICIO_PARTIDA                  = 9,
        JUGADA_HECHA                    = 10,
        FIN_PARTIDA                     = 11,
        MENSAJE_CHAT                    = 12,
        RESULTADO_CONEXION              = 13,
        ERROR_DESCONOCIDO               = 99
    }
    public partial class FormCliente : Form
    {
        public struct Invitado
        {
            public string nombreInvitado;
            public int socket;
            public Boolean aceptaInvitacion;
            public Boolean recepcionRespuesta;
        }
        public struct Jugador
        {
            public string nombreJugador;
            public int socketJugador;
        }

        static Socket server;
        static int miSocket;
        static int idPartidaEnJuego;
        public static int puntos;
        Thread atender;
        String userConnected;
        Invitado[] invitados = new Invitado[3];
        Jugador[] jugadores = new Jugador[20];
        int nroInvitados;
        Boolean anfitrion;
        FormJuego formulario_juego;

        delegate void delegadoPaneles(Boolean valor);
        delegate void delegadoJuego(Boolean valor);
        delegate void delegadoMensajes(String mensaje);
        public FormCliente()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            userConnected = "";
            miSocket=0;
            idPartidaEnJuego = 0;
            nroInvitados = 0;
            anfitrion = false;
        }

        string buscarNombre(int socketBuscado, char lista)
        {
            int i;
            if (lista.Equals('J'))
            {
                for (i= 0; i< jugadores.Length; i++)
                {
                    if (jugadores[i].socketJugador == socketBuscado)
                        return jugadores[i].nombreJugador;
                }
            }
            else
            {
                for (i = 0; i < invitados.Length; i++)
                {
                    if (invitados[i].socket == socketBuscado)
                        return invitados[i].nombreInvitado;
                }
            }
            return "";
        }

        void verificarRecepcionTodasRespuestas()
        {
            Boolean todasRespuestas = true;
            int numAceptados = 0;
            for(int i=0; i<invitados.Length;i++)
            {
                if (invitados[i].recepcionRespuesta)
                {
                    if (invitados[i].aceptaInvitacion)
                        numAceptados++;
                }
                else
                    todasRespuestas = false;
            }
            if (todasRespuestas && numAceptados >=1)
            {
                // enviar al servidor los que aceptaron para que notifique el id, el inicio de partida y el turno
                string mensaje = Convert.ToString((int)codigosMensajes.INICIO_PARTIDA) + "/" + Convert.ToString(numAceptados);
                for (int i = 0; i < invitados.Length; i++)
                {
                    if (invitados[i].aceptaInvitacion)
                    {
                        mensaje = String.Concat(mensaje, "/", Convert.ToString(invitados[i].socket));
                    }
                }
                nroInvitados = numAceptados;
                
                // Enviamos al servidor los jugadores de la partida
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
              
            }
        }

        private void rellenarDatagrid(string[] trozos)
        {
            userConnected = username.Text;
            //username.Text = "";
            password.Text = "";
            char[] charSeparators = new char[] { ';' };
            int numConectados = Convert.ToInt32(trozos[2]);
            Array.Clear(jugadores, 0, jugadores.Length);
            Array.Resize(ref jugadores, numConectados);
            dgviewListaUsuarios.Rows.Clear();
            dgviewListaUsuarios.Columns.Clear();
            dgviewListaUsuarios.Columns.Add("", "Nombre");
            dgviewListaUsuarios.Columns.Add("", "Pos");
            dgviewListaUsuarios.Columns[1].Visible = false;
            for (int j = 0; j < numConectados; j++)
            {
                string[] usuarios = trozos[j + 3].Split(charSeparators);
                string nombre = usuarios[0].Split('\0')[0];
                string socket = usuarios[1].Split('\0')[0];

                dgviewListaUsuarios.Rows.Add(nombre, j);
                //dgviewListaUsuarios.Rows[j].Cells[1].Value = j;
                jugadores[j].nombreJugador = nombre;
                jugadores[j].socketJugador = Convert.ToInt32(socket);
            }
        }

        private void rellenarDatosPartida(string[] trozos)
        {
            nroInvitados = Convert.ToInt32(trozos[3]);
            Array.Clear(invitados, 0, invitados.Length);
            Array.Resize(ref jugadores, nroInvitados);
            for (int j = 0; j < nroInvitados; j++)
            {
                invitados[j].socket = Convert.ToInt32(trozos[5+j]);
                invitados[j].nombreInvitado = buscarNombre(invitados[j].socket, 'J');
                invitados[j].aceptaInvitacion = true;
                invitados[j].recepcionRespuesta = true;
            }
            MessageBox.Show("Va a comenzar la partida, presiona boton YAHTZEE para empezar", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static void turnoFinalizado()
        {
            //MessageBox.Show("turno Finalizado");
            string mensaje = Convert.ToString((int)codigosMensajes.JUGADA_HECHA) + "/" + Convert.ToString(idPartidaEnJuego) 
                        + "/" + Convert.ToString(miSocket) + "/" + Convert.ToString(puntos) ;
            // Enviamos al servidor datos para informar que se termino de jugar y de turno al proximo jugador
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

        }

        /* ***************************************************************************************************** */
        /*                          Métodos asociados a eventos del formulario                                   */
        /* ***************************************************************************************************** */
        private void FormCliente_Load(object sender, EventArgs e)
        {
        }
        private void login_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(username.Text) || string.IsNullOrWhiteSpace(username.Text))
            {
                MessageBox.Show(null, "Indique el nombre de usuario", "Mensaje de Error",  MessageBoxButtons.OK, MessageBoxIcon.Error );
                return;
            }
            string mensaje = "4/" + username.Text + "/" + password.Text;
            // Enviamos al servidor datos para hacer login
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void register_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(username.Text) || string.IsNullOrWhiteSpace(username.Text))
            {
                MessageBox.Show(null, "Indique el nombre de usuario y/o contraseña", "Mensaje de Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string mensaje = "5/" + username.Text + "/" + password.Text;
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        /* Método del thread */
        private void AtenderServidor()
        {            
            // Procesamiento de intercambio de mensajes con el servidor
            while (true)
            {
                try
                {
                    //Recibimos mensaje del servidor
                    byte[] mensajeBytes = new byte[512];
                    string mensaje = "";
                    server.Receive(mensajeBytes);
                    char[] charSeparators = new char[] { '/' };
                    string[] trozos = Encoding.ASCII.GetString(mensajeBytes).Split(charSeparators);
                    codigosMensajes codigo = (codigosMensajes)Convert.ToInt32(trozos[0]);

                    switch (codigo)
                    {
                        case codigosMensajes.DESCONEXION_EXITOSA:
                            lblMensaje.Invoke(new delegadoMensajes(escribirMensaje), "                     DESCONEXION EXITOSA                        *** Hasta la próxima, vuelve cuando quieras ***");
                            panelConexion.Invoke(new delegadoPaneles(HabilitarDeshabilitarPaneles), false);
                            conectar.Invoke(new delegadoPaneles(HabilitarDeshabBotonConexion), true);
                            atender.Abort();
                            this.BackColor = Color.Gray;
                            server.Shutdown(SocketShutdown.Both);
                            server.Close();
                            break;
                        case codigosMensajes.RESULTADO_CONEXION:
                            char[] charParse = new char[] { ';' };
                            string[] datos = trozos[1].Split(charParse);
                            miSocket = Convert.ToInt32(datos[1]);
                            MessageBox.Show("Conectado al Servidor", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            panelConexion.Invoke(new delegadoPaneles(HabilitarPanelLogin), true);
                            conectar.Invoke(new delegadoPaneles(HabilitarDeshabBotonConexion), false);
                            break;
                        case codigosMensajes.CONSULTA_NRO_VICTORIAS:
                            mensaje = trozos[1].Split('\0')[0];
                            MessageBox.Show(mensaje);
                            break;
                        case codigosMensajes.CONSULTA_MAS_PUNTOS:
                            mensaje = trozos[1].Split('\0')[0];
                            MessageBox.Show(mensaje);
                            break;
                        case codigosMensajes.CONSULTA_GANADOR_PARTIDA:
                            mensaje = trozos[1].Split('\0')[0];
                            MessageBox.Show(mensaje);
                            break;
                        case codigosMensajes.LOGIN:
                            mensaje = trozos[1].Split('\0')[0];
                            if (mensaje.Equals("OK"))
                            {
                                panelConsultas.Invoke(new delegadoPaneles(HabilitarDeshabilitarPaneles), true);
                                rellenarDatagrid(trozos);
                                lblMensaje.Invoke(new delegadoMensajes(escribirMensaje), "                     LOGIN EXITOSO               " + userConnected);
                                panelConexion.Invoke(new delegadoPaneles(HabilitarPanelLogin), false);
                                
                            }
                            else
                                lblMensaje.Invoke(new delegadoMensajes(escribirMensaje),trozos[2].Split('\0')[0]);
                            break;
                        case codigosMensajes.REGISTRO:
                            mensaje = trozos[1].Split('\0')[0];
                            MessageBox.Show(mensaje);
                            lblMensaje.Invoke(new delegadoMensajes(escribirMensaje), "                   USUARIO REGISTRADO           Presiona Login para iniciar tu sesión");
                            break;
                        case codigosMensajes.CAMBIO_LISTA_JUGADORES:
                            rellenarDatagrid(trozos);
                            break;
                        case codigosMensajes.INVITAR_PARTIDA:
                            int socketAnfitrion = Convert.ToInt32(trozos[1]);
                            string respuesta;
                            mensaje = buscarNombre(socketAnfitrion,'J') + " dice: "  + trozos[2].Split('\0')[0];                             
                            if (MessageBox.Show("INVITACION A JUGAR", mensaje, MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                respuesta = "8/YES/" + Convert.ToString(socketAnfitrion);
                            }
                            else
                            {
                                respuesta = "8/NO/" + Convert.ToString(socketAnfitrion);
                            }
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(respuesta);
                            server.Send(msg);
                            break;
                        case codigosMensajes.ACEPTA_JUEGO:
                            mensaje = trozos[1].Split('\0')[0];
                            int socketInvitado = Convert.ToInt32(trozos[2]);
                            for (int j=0; j<invitados.Length;j++)
                            {
                                if (invitados[j].socket == socketInvitado)
                                    invitados[j].aceptaInvitacion = (mensaje.Equals("YES"));
                                invitados[j].recepcionRespuesta = true;
                            }
                            verificarRecepcionTodasRespuestas();
                            break;
                        case codigosMensajes.INICIO_PARTIDA:
                            mensaje = trozos[1].Split('\0')[0];
                            idPartidaEnJuego = Convert.ToInt32(trozos[2]);
                            // identificar el tipo de mensaje que llega: A para anfitrion y J para jugador invitado
                            if (mensaje.Equals("A"))
                            {                               
                                anfitrion = true;
                                MessageBox.Show("Va a comenzar la partida, presiona boton YAHTZEE para empezar", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else
                            {
                                anfitrion = false;
                                rellenarDatosPartida(trozos);
                            }
                            break;
                        case codigosMensajes.JUGADA_HECHA:
                            // Formato: 10/IdPartida/socketjugador/puntos
                            mensaje = trozos[1].Split('\0')[0];
                            idPartidaEnJuego = Convert.ToInt32(mensaje);
                            formulario_juego.actualizarPtosJugada(Convert.ToInt32(trozos[2]), Convert.ToInt32(trozos[3]));
                            formulario_juego.habilitarDeshabilitarTurno();
                            break;
                        case codigosMensajes.FIN_PARTIDA:
                            break;

                    }
                } catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error de Comunicación con el Servidor",MessageBoxButtons.OK);
                    break;
                }
            }
        }

        private void conectar_Click(object sender, EventArgs e) // boton conectar
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 9030);
            

            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                this.BackColor = Color.Honeydew;
                panelLogin.Enabled = true;
                conectar.Enabled = false;
                desconectar.Enabled = true;

            }
            catch (SocketException ex)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                lblMensaje.Text = "No se ha podido conectar con el servidor. Causa: " + ex.Message;
                return;
            }
            //pongo en marcha el thread que atenderá los mensajes del servidor
            ThreadStart ts = delegate { AtenderServidor(); };
            atender = new Thread(ts);
            atender.Start();
        }

        private void enviar_Click(object sender, EventArgs e)
        {
            if (victorias.Checked)
            {
                string mensaje = "1/" + nombre.Text +"/aaa";
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //////Recibimos la respuesta del servidor
                //byte[] msg2 = new byte[80];
                //server.Receive(msg2);
                //mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                //MessageBox.Show(mensaje);
            }
            else if (maspuntos.Checked)
            {
                string mensaje = "2/Juanito/aaa";
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //////Recibimos la respuesta del servidor
                //byte[] msg2 = new byte[80];
                //server.Receive(msg2);
                //mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                //MessageBox.Show(mensaje);
            }
            else if (ganador.Checked)
            {               
                string mensaje = "3/" + nombre.Text + "/aaa";
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //////Recibimos la respuesta del servidor
                //byte[] msg2 = new byte[80];
                //server.Receive(msg2);
                //mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                //MessageBox.Show(mensaje);
            }       
        }

        private void desconectar_Click(object sender, EventArgs e) // desconectar
        {
            //Mensaje de desconexión
            string mensaje = "0/";       
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
/*
            // Nos desconectamos
            atender.Abort();
            this.BackColor = Color.Gray;
            server.Shutdown(SocketShutdown.Both);
            server.Close();
*/

        }

        private void invitar_Click(object sender, EventArgs e)
        {
            // ENVIAR LOS SELECCIONADOS EN EL DATAGRID
            // primero cuantos son y luego el socket de cada uno
            // solamente se permite invitar hasta tres jugadores
            int numJugInvitados = 0;
            string mensaje="";
            Boolean seleccionados = false;
            for (int i = 0; i < dgviewListaUsuarios.RowCount; i++)
            {
                if (dgviewListaUsuarios.Rows[i].Selected)
                    numJugInvitados++;
            }
            if (numJugInvitados > 3)
                MessageBox.Show("Solo puedes invitar hasta tres jugadores", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                mensaje = Convert.ToString((int)codigosMensajes.INVITAR_PARTIDA) + "/" + Convert.ToString(numJugInvitados);
                int posInArray;
                int posInvitado = 0;
                
                for (int i = 0; i < dgviewListaUsuarios.RowCount; i++)
                {
                    if (dgviewListaUsuarios.Rows[i].Selected)
                    {
                        posInArray = Convert.ToInt32(dgviewListaUsuarios.Rows[i].Cells[1].Value);
                        mensaje = String.Concat(mensaje, "/", Convert.ToString(jugadores[posInArray].socketJugador));
                        invitados[posInvitado].nombreInvitado = dgviewListaUsuarios.Rows[i].Cells[0].Value.ToString();
                        invitados[posInvitado] .socket= jugadores[posInArray].socketJugador;
                        invitados[posInvitado].aceptaInvitacion = false;
                        invitados[posInvitado].recepcionRespuesta = false;
                        seleccionados = true;
                    }
                }
            }
            if (seleccionados)
            {
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            else
                MessageBox.Show("No hay datos para enviar", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }



        /* Métodos para los delegados */
        private void HabilitarDeshabilitarPaneles(Boolean valor)
        {
            panelConsultas.Enabled = valor;
            panelJugadores.Enabled = valor;
        }
        private void HabilitarPanelLogin(Boolean valor)
        {
            panelLogin.Enabled = valor;
        }

        private void HabilitarDeshabBotonConexion(Boolean valor)
        {
            conectar.Enabled = valor;
            desconectar.Enabled = !valor;
        }
        private void escribirMensaje(String mensaje)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.ForeColor = Color.DarkRed;
        }

        private void escribirRespuesta(String respuesta)
        {
            mensajesJuego.Text = respuesta;
        }

        private void ganador_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void iniciarPartida_Click(object sender, EventArgs e)
        {
            formulario_juego = new FormJuego();
            formulario_juego.miNombre = username.Text;
        
            formulario_juego.participantes = new FormJuego.Participante[4];
            if (anfitrion)
            {
                formulario_juego.numJugadores = nroInvitados + 1;
                formulario_juego.participantes[0].nombreJugador = username.Text;
                formulario_juego.participantes[0].scocketJugador = miSocket;
                for (int i = 0; i < nroInvitados; i++)
                {
                    formulario_juego.participantes[i+1].nombreJugador = invitados[i].nombreInvitado;
                    formulario_juego.participantes[i + 1].scocketJugador = invitados[i].socket;
                }
                formulario_juego.miTurno = 0;
            }
            else
            {
                formulario_juego.numJugadores = nroInvitados;
                for (int i = 0; i < nroInvitados; i++)
                {
                    formulario_juego.participantes[i].nombreJugador = invitados[i].nombreInvitado;
                    if (invitados[i].socket == miSocket)
                        formulario_juego.miTurno = i;
                }
            }
            formulario_juego.nroPartida = idPartidaEnJuego;
            formulario_juego.Show();
        }

        private void enviar_Click_1(object sender, EventArgs e)
        {
            if (victorias.Checked)
            {
                string mensaje = Convert.ToString((int)codigosMensajes.CONSULTA_NRO_VICTORIAS) + "/" + nombre.Text;
                // Enviamos al servidor datos para hacer login
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            if (maspuntos.Checked)
            {
                string mensaje = Convert.ToString((int)codigosMensajes.CONSULTA_MAS_PUNTOS);
                // Enviamos al servidor datos para hacer login
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            if (ganador.Checked)
            {
                string mensaje = Convert.ToString((int)codigosMensajes.CONSULTA_GANADOR_PARTIDA) + "/" + idPartida.Text;
                // Enviamos al servidor datos para hacer login
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
        }

    }   
}
