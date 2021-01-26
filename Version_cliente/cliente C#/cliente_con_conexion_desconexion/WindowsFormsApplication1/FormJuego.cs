using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    enum JugadasYahtzee {
        UNOS                = 1,
        DOSES               = 2,
        TRESES              = 3,
        CUATROS             = 4,
        CINCOS              = 5,
        SEISES              = 6,
        CHANCE              = 7,
        TRIO                = 8,
        CUARTETO            = 9,
        ESCALERA_PEQUEÑA    = 10,
        ESCALERA_LARGA      = 11,
        YAHTZEE             = 12
    }

    public enum StatusDado
    {
        FIJO    = 0,
        TIRAR   = 1
    }

    public partial class FormJuego : Form
    {
        public struct Participante
        {
            public string nombreJugador;
            public int scocketJugador;
        }

        public struct DadoTirada
        {
            public int valor;
            public StatusDado estatus;
        }

        public static string[] nombreJugadaYahtzee = { "Unos", "Doses", "Treses", "Cuatros", "Cincos", "Seises", "Chance", "Trío", "Cuarteto", "Escalera Peq.", "Escalera Larga", "Yahtzee" };

        /* 
        * *********************************************************************************** 
        * Variables para controlar el juego 
        * *********************************************************************************** 
        */
        public int nroPartida;
        public Participante[] participantes;
        public static int turnoJuego;
        public int miTurno;
        public string miNombre;
        public int posArray;
        public Boolean finalizada;
        public int numJugadores;

        /* 
        * *********************************************************************************** 
        * Variables para las tiradas de los dados 
        * *********************************************************************************** 
        */
        DadoTirada[] dadosTirada = new DadoTirada[5];
        int numTirada;
        int posUltDadoFijo;

        /* 
        * *********************************************************************************** 
        * Constructor 
        * *********************************************************************************** 
        */
        public FormJuego()
        {
            InitializeComponent();
        }

        /* 
        * *********************************************************************************** 
        * Métodos para el juego Yahtzee 
        * *********************************************************************************** 
        */
        int random()
        {
            Random numDadoRandom = new Random((int)DateTime.Now.Ticks);
            return numDadoRandom.Next(1, 500) % 6 + 1;
        }

        void ocultarDadosTirada()
        {
            dadoTirada1.Visible = false;
            dadoTirada2.Visible = false;
            dadoTirada3.Visible = false;
            dadoTirada4.Visible = false;
            dadoTirada5.Visible = false;
        }

        void ocultarDadosFijos()
        {
            dadoFijo1.Visible = false;
            dadoFijo2.Visible = false;
            dadoFijo3.Visible = false;
            dadoFijo4.Visible = false;
            dadoFijo5.Visible = false;
        }
        void apagarDadosMov()
        {
            switch (posUltDadoFijo)
            {
                case 5:
                    //pictureBox5.Visible = false;
                    pictureBox4.Visible = false;
                    pictureBox3.Visible = false;
                    pictureBox2.Visible = false;
                    pictureBox1.Visible = false;
                    break;
                case 4:
                    //pictureBox4.Visible = false;
                    pictureBox3.Visible = false;
                    pictureBox2.Visible = false;
                    pictureBox1.Visible = false;
                    break;
                case 3:
                    //pictureBox3.Visible = false;
                    pictureBox2.Visible = false;
                    pictureBox1.Visible = false;
                    break;
                case 2:
                    //pictureBox2.Visible = false;
                    pictureBox1.Visible = false;
                    break;
            }
        }
        void mostrarDadosMoviles()
        {
            pictureBox5.Visible = true;
            pictureBox4.Visible = true;
            pictureBox3.Visible = true;
            pictureBox2.Visible = true;
            pictureBox1.Visible = true;
        }


        public void inicializarDados()
        {
            
            ocultarDadosTirada();
            panelTirada.Visible = true;
            panelDadosFijos.Visible = false;
            //dadoFijo1.Image = ImagenesDados.Images[0];
            numTirada = 0;
            posUltDadoFijo = 1;

            ocultarDadosFijos();
            mostrarDadosMoviles();
            for (int i = 0; i < 5; i++)
                dadosTirada[i].estatus = StatusDado.TIRAR;
            detenerDados.Enabled = false;
            lanzar.Enabled = true;
        }
        void ponerValorDadoFijo(int valorDado)
        {
            switch (posUltDadoFijo)
            {
                case 1:
                    dadoFijo1.Image = ImagenesDados.Images[valorDado - 1];
                    dadoFijo1.Visible = true;
                    break;
                case 2:
                    dadoFijo2.Image = ImagenesDados.Images[valorDado - 1];
                    dadoFijo2.Visible = true;
                    break;
                case 3:
                    dadoFijo3.Image = ImagenesDados.Images[valorDado - 1];
                    dadoFijo3.Visible = true;
                    break;
                case 4:
                    dadoFijo4.Image = ImagenesDados.Images[valorDado - 1];
                    dadoFijo4.Visible = true;
                    break;
                case 5:
                    dadoFijo5.Image = ImagenesDados.Images[valorDado - 1];
                    dadoFijo5.Visible = true;
                    break;
            }
            posUltDadoFijo++;
            panelDadosFijos.Visible = true;
            if (posUltDadoFijo > 5)
            {
                MessageBox.Show("Ya has fijado todos los dados, se evaluará la jugada", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                lanzar.Enabled = false;
                detenerDados.Enabled = false;
                calcularPuntos();
            }
        }

        void tirada()
        {
            int i;
            ocultarDadosTirada();
            for (i = 0; i < 5; i++)
            {
                if (dadosTirada[i].estatus == StatusDado.TIRAR)
                {
                    dadosTirada[i].valor = random();
                    switch (i+1)
                    {
                        case 1:
                            dadoTirada1.Tag = dadosTirada[i].valor;
                            dadoTirada1.Image = ImagenesDados.Images[dadosTirada[i].valor-1];
                            dadoTirada1.Visible = true;
                            break;
                        case 2:
                            dadoTirada2.Tag = dadosTirada[i].valor;
                            dadoTirada2.Image = ImagenesDados.Images[dadosTirada[i].valor - 1];
                            dadoTirada2.Visible = true;
                            break;
                        case 3:
                            dadoTirada3.Tag = dadosTirada[i].valor;
                            dadoTirada3.Image = ImagenesDados.Images[dadosTirada[i].valor - 1];
                            dadoTirada3.Visible = true;
                            break;
                        case 4:
                            dadoTirada4.Tag = dadosTirada[i].valor;
                            dadoTirada4.Image = ImagenesDados.Images[dadosTirada[i].valor - 1];
                            dadoTirada4.Visible = true;
                            break;
                        case 5:
                            dadoTirada5.Tag = dadosTirada[i].valor;
                            dadoTirada5.Image = ImagenesDados.Images[dadosTirada[i].valor - 1];
                            dadoTirada5.Visible = true;
                            break;
                    }
                }

            }
        }
        void calcularPuntos()
        {
            int mark, j, i, contadorNum = 0;
            int[] repeticion = new int[6];
            int[] escalera = new int[6] { 1, 2, 3, 4, 5, 6 };
            int[] puntos = new int[(int)JugadasYahtzee.YAHTZEE];
            Array.Clear(puntos,0,6);
            for (j = 0; j < 6; j++)
                repeticion[j] = 0;
            for(mark=1; mark <= (int)JugadasYahtzee.YAHTZEE; mark++)
            { 
                if (puntuaciones.Rows[mark-1].Cells[1].Value.Equals(""))
                {
                    contadorNum = 0;
                    switch ((JugadasYahtzee)  mark)
                    {
                        case JugadasYahtzee.UNOS:
                        case JugadasYahtzee.DOSES:
                        case JugadasYahtzee.TRESES:
                        case JugadasYahtzee.CUATROS:
                        case JugadasYahtzee.CINCOS:
                        case JugadasYahtzee.SEISES:
                            for (j = 0; j < 5; j++)
                            {
                                if (dadosTirada[j].valor == mark)
                                {
                                    contadorNum++;
                                }
                            }
                            puntos[mark - 1] = mark * contadorNum;
                            break;
                        case JugadasYahtzee.CHANCE:
                            puntos[mark - 1] = 0;
                            for (j = 0; j < 5; j++)
                            {
                                puntos[mark - 1] = dadosTirada[j].valor + puntos[mark - 1];
                            }
                            break;
                        case JugadasYahtzee.TRIO:
                            Array.Clear(repeticion,0,6);
                            for (j = 0; j < 5; j++)
                            {
                                repeticion[dadosTirada[j].valor - 1]++;
                            }
                            puntos[mark - 1] = 0;
                            for (j = 0; j < 5; j++)
                            {
                                if (repeticion[j] >= 3)
                                {
                                    for (i = 0; i < 5; i++)
                                    {
                                        puntos[mark - 1] = dadosTirada[i].valor + puntos[mark - 1];
                                    }
                                    break;
                                }
                            }
                            break;
                        case JugadasYahtzee.CUARTETO:
                            Array.Clear(repeticion, 0, 6);
                            for (j = 0; j < 5; j++)
                            {
                                repeticion[dadosTirada[j].valor - 1]++;
                            }
                            puntos[mark - 1] = 0;
                            for (j = 0; j < 5; j++)
                            {
                                if (repeticion[j] >= 4)
                                {
                                    for (i = 0; i < 5; i++)
                                    {
                                        puntos[mark - 1] = dadosTirada[i].valor + puntos[mark - 1];
                                    }
                                    break;
                                }
                            }

                            break;
                        case JugadasYahtzee.ESCALERA_PEQUEÑA:
                            {
                                Boolean hayEscalera = false;
                                int indexInicial, indexEscalera, indexTemp, consecutivos=0;
                                int[] temp = new int[5];
                                for (j=0; j<5; j++)
                                {
                                    temp[j] = dadosTirada[j].valor;
                                }
                                Array.Sort(temp);
                                for (indexInicial = 0; indexInicial < 3; indexInicial++)
                                {
                                    consecutivos = 0;
                                    indexEscalera = indexInicial;
                                    indexTemp = 0;
                                    hayEscalera = true;
                                    while ( hayEscalera && 
                                            (indexTemp < 5) && 
                                            (indexEscalera < 6) &&
                                            (consecutivos < 4))
                                    {
                                        if (temp[indexTemp] < escalera[indexEscalera])
                                            indexTemp++;
                                        else
                                        {
                                            if (indexTemp > 0)
                                            {
                                                if (temp[indexTemp] == temp[indexTemp-1])
                                                {
                                                    indexTemp++;
                                                    continue;
                                                }
                                            }
                                            if (temp[indexTemp] == escalera[indexEscalera])
                                            {
                                                indexEscalera++;
                                                indexTemp++;
                                                consecutivos++;
                                            }
                                            else
                                                hayEscalera = false;
                                        }
                                    }
                                }

                                if (hayEscalera & consecutivos == 4)
                                        puntos[mark - 1] = 30;
                            }
                            break;
                        case JugadasYahtzee.ESCALERA_LARGA:
                            {
                                Boolean hayEscalera = false;
                                int indexInicial, indexEscalera, indexTemp, consecutivos = 0;
                                int[] temp = new int[5];
                                for (j = 0; j < 5; j++)
                                {
                                    temp[j] = dadosTirada[j].valor;
                                }
                                Array.Sort(temp);
                                for (indexInicial = 0; indexInicial < 2; indexInicial++)
                                {
                                    consecutivos = 0;
                                    indexEscalera = indexInicial;
                                    indexTemp = 0;
                                    hayEscalera = true;
                                    while (hayEscalera &&
                                            (indexTemp < 5) &&
                                            (indexEscalera < 6) &&
                                            (consecutivos < 5))
                                    {
                                        if (temp[indexTemp] < escalera[indexEscalera])
                                            indexTemp++;
                                        else
                                        {
                                            if (indexTemp > 0)
                                            {
                                                if (temp[indexTemp] == temp[indexTemp - 1])
                                                {
                                                    indexTemp++;
                                                    continue;
                                                }
                                            }
                                            if (temp[indexTemp] == escalera[indexEscalera])
                                            {
                                                indexEscalera++;
                                                indexTemp++;
                                                consecutivos++;
                                            }
                                            else
                                                hayEscalera = false;
                                        }
                                    }
                                }

                                if (hayEscalera & consecutivos ==5)
                                    puntos[mark - 1] = 30;
                            }
                            break;
                        case JugadasYahtzee.YAHTZEE:
                            if ((dadosTirada[0].valor == dadosTirada[1].valor) &&
                                (dadosTirada[1].valor == dadosTirada[2].valor) &&
                                (dadosTirada[2].valor == dadosTirada[3].valor) &&
                                (dadosTirada[3].valor == dadosTirada[4].valor)  )

                                puntos[mark - 1] = 50;
                            break;


                    }
                    puntuaciones.Rows[mark - 1].Cells[1].Value = puntos[mark-1];
                    puntuaciones.Rows[mark - 1].Cells[1].Style.ForeColor = Color.Red;
                }
            }

        }

        void limpiarLineasNoSelec()
        {
            for (int i=0; i < (int)JugadasYahtzee.YAHTZEE;i++)
            {
                if (puntuaciones.Rows[i].Cells[1].Tag.Equals(""))
                    puntuaciones.Rows[i].Cells[1].Value = "";
            }
        }

        public void habilitarDeshabilitarTurno()
        {
            turnoJuego = (turnoJuego + 1) % numJugadores;
            if (miTurno == turnoJuego)
            {
                panelMiJuego.Enabled = true;
            }
            else
            {
                panelMiJuego.Enabled = false;
            }
        }

        public void actualizarPtosJugada(int socket, int puntos)
        {
            for(int i = 0; i< numJugadores; i++)
            {
                if (participantes[i].scocketJugador == socket)
                    puntuaciones.Rows[puntuaciones.Rows.Count-1].Cells[1].Value = puntos;
            }
        }

        /* 
        * *********************************************************************************** 
        * Métodos de Eventos del Formulario 
        * *********************************************************************************** 
        */
        private void FormJuego_Load(object sender, EventArgs e)
        {
            int i;
            puntuaciones.Columns.Add("", "Línea de Yahtzee");
            puntuaciones.Columns[0].Width = 115;
            for (i = 0; i < participantes.Length; i++)
            {
                puntuaciones.Columns.Add("", participantes[i].nombreJugador);
                puntuaciones.Columns[i + 1].Width = 49;
            }

            for (i = 0; i < (int)JugadasYahtzee.YAHTZEE; i++)
            {
                puntuaciones.Rows.Add(nombreJugadaYahtzee[i]);
                puntuaciones.Rows[i].Height = 27;
                puntuaciones.Rows[i].Cells[1].Value = "";
                puntuaciones.Rows[i].Cells[2].Value = "";
                puntuaciones.Rows[i].Cells[3].Value = "";
                puntuaciones.Rows[i].Cells[4].Value = "";
                puntuaciones.Rows[i].Cells[1].Tag = "";
                puntuaciones.Rows[i].Cells[2].Tag = "";
                puntuaciones.Rows[i].Cells[3].Tag = "";
                puntuaciones.Rows[i].Cells[4].Tag = "";
            }
            puntuaciones.Rows.Add("");
            puntuaciones.Rows[i].Height = 4;
            puntuaciones.Rows.Add("Puntos Totales",0,0,0,0);
            puntuaciones.Rows[i + 1].Height = 25;
            inicializarDados();
            turnoJuego = 0;
            habilitarDeshabilitarTurno();
        }

        //private void miMensaje_TextChanged(object sender, EventArgs e)
        //{
        //    if ( miMensaje.Text.EndsWith (Convert.ToString ((char)13)) )
        //    {
        //        mensajesChat.Text.Concat(miMensaje.Text);
        //    }
        //}

        private void miMensaje_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                mensajesChat.Text = mensajesChat.Text + "\r\n" + miNombre + ": " + miMensaje.Text;
                miMensaje.Text = "";
            }
        }

        private void  lanzar_Click(object sender, EventArgs e)
        {
            numTirada++;
            mensajeTirada.Text = "**** Estás en tu turno ****\r\n     Tirada número " + numTirada;
            apagarDadosMov();
            panelTirada.Visible = false;
            detenerDados.Enabled = true;
            lanzar.Enabled = false;
        }
        private void detenerDados_Click(object sender, EventArgs e)
        {
            lanzar.Enabled = true;
            detenerDados.Enabled = false;
            tirada();
            panelTirada.Visible = true;
            if (numTirada == 3)
            {
                lanzar.Enabled = false;
                detenerDados.Enabled = false;
                calcularPuntos();
            }
        }

        private void dadoTirada1_DoubleClick(object sender, EventArgs e)
        {
            // se fija el dado
            dadosTirada[0].estatus = StatusDado.FIJO;
            ponerValorDadoFijo(dadosTirada[0].valor);
            dadoTirada1.Visible = false;

        }

        private void dadoTirada2_Click(object sender, EventArgs e)
        {
            dadosTirada[1].estatus = StatusDado.FIJO;
            ponerValorDadoFijo(dadosTirada[1].valor);
            dadoTirada2.Visible = false;

        }

        private void dadoTirada3_Click(object sender, EventArgs e)
        {
            dadosTirada[2].estatus = StatusDado.FIJO;
            ponerValorDadoFijo(dadosTirada[2].valor);
            dadoTirada3.Visible = false;

        }
        private void dadoTirada4_Click(object sender, EventArgs e)
        {
            dadosTirada[3].estatus = StatusDado.FIJO;
            ponerValorDadoFijo(dadosTirada[3].valor);
            dadoTirada4.Visible = false;
        }

        private void dadoTirada5_Click(object sender, EventArgs e)
        {
            dadosTirada[4].estatus = StatusDado.FIJO;
            ponerValorDadoFijo(dadosTirada[4].valor);
            dadoTirada5.Visible = false;
        }

        private void puntuaciones_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (puntuaciones.Rows[e.RowIndex].Cells[1].Tag.Equals(""))
            {
                puntuaciones.Rows[e.RowIndex].Cells[1].Tag = puntuaciones.Rows[e.RowIndex].Cells[1].Value;
                puntuaciones.Rows[e.RowIndex].Cells[1].Style.ForeColor = Color.Black;
                puntuaciones.Rows[(int)JugadasYahtzee.YAHTZEE + 1].Cells[1].Value =
                     (int)puntuaciones.Rows[(int)JugadasYahtzee.YAHTZEE + 1].Cells[1].Value +
                     (int)puntuaciones.Rows[e.RowIndex].Cells[1].Value;
                limpiarLineasNoSelec();
                MessageBox.Show("Has terminado tu turno. Le toca al próximo jugador", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //lanzar.Enabled = false;

                habilitarDeshabilitarTurno();
                FormCliente.puntos = Convert.ToInt32(puntuaciones.Rows[puntuaciones.Rows.Count - 1].Cells[miTurno].Value);
                FormCliente.turnoFinalizado();
                inicializarDados();
            }
            else
                MessageBox.Show("Ya has usado esa línea, selecciona otra", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

            // Revisar si aun quedan líneas
            for (int i = 0; i < (int)JugadasYahtzee.YAHTZEE; i++)
            {
                if (puntuaciones.Rows[i].Cells[1].Tag.Equals(""))
                    return;
            }
            //MessageBox.Show("Has terminado tu turno. Le toca al próximo jugador", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            ////lanzar.Enabled = false;
            
            //habilitarDeshabilitarTurno();
            //FormCliente.puntos = Convert.ToInt32( puntuaciones.Rows[puntuaciones.Rows.Count-1].Cells[miTurno].Value);
            //FormCliente.turnoFinalizado();

        }

        
        


    }
}
