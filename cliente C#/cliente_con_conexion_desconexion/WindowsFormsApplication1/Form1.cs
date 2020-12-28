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
    public partial class Form1 : Form
    {
        Socket server;
        Thread atender;
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void AtenderServidor()
        {

            while (true)
            {
                //Recibimos mensaje del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                char[] charSeparators = new char[] { '/' };
                string[] trozos = Encoding.ASCII.GetString(msg2).Split(charSeparators);
                int codigo = Convert.ToInt32(trozos[0]);
                string mensaje = trozos[1].Split('\0')[0];

                switch (codigo)
                {

                    case 1:
                        MessageBox.Show(mensaje);
                        break;
                    case 2:
                        MessageBox.Show(mensaje);
                        break;
                    case 3:
                        MessageBox.Show(mensaje);
                        break;
                    case 4:
                        MessageBox.Show(mensaje);
                        break;
                    case 5:
                        MessageBox.Show(mensaje);
                        break;
                    case 6:
                        rellenarDatagrid(mensaje);
                        break;
                    case 8:
                        DialogResult dialogResult = MessageBox.Show(mensaje, "Invitar", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            string mensaje2 = "9/aaa/aaa";
                            // Enviamos al servidor el nombre tecleado
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje2);
                            server.Send(msg);
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            string mensaje2 = "10/aaa/aaa";
                            // Enviamos al servidor el nombre tecleado
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje2);
                            server.Send(msg);
                        }
                        break;
                    case 9:
                        rellenarDatagridEstado(mensaje);
                        break;



                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 9050);


            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                this.BackColor = Color.Green;
                MessageBox.Show("Conectado");

            }
            catch (SocketException ex)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }
            //pongo en marcha el thread que atenderá los mensajes del servidor
            ThreadStart ts = delegate { AtenderServidor(); };
            atender = new Thread(ts);
            atender.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Bonito.Checked)
            {
                string mensaje = "1/" + nombre.Text + "/aaa";
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //////Recibimos la respuesta del servidor
                //byte[] msg2 = new byte[80];
                //server.Receive(msg2);
                //mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                //MessageBox.Show(mensaje);
            }
            else if (Longitud.Checked)
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
            else if (altura.Checked)
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

        private void button3_Click(object sender, EventArgs e)
        {
            //Mensaje de desconexión
            string mensaje = "0/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos
            atender.Abort();
            this.BackColor = Color.Gray;
            server.Shutdown(SocketShutdown.Both);
            server.Close();


        }

        private void login_Click(object sender, EventArgs e)
        {
            string mensaje = "4/" + username.Text + "/" + password.Text;
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            ////Recibimos la respuesta del servidor
            //byte[] msg2 = new byte[80];
            //server.Receive(msg2);
            //mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            //MessageBox.Show(mensaje);
        }

        private void register_Click(object sender, EventArgs e)
        {
            string mensaje = "5/" + username.Text + "/" + password.Text;
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            ////Recibimos la respuesta del servidor
            //byte[] msg2 = new byte[80];
            //server.Receive(msg2);
            //mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            //MessageBox.Show(mensaje);
        }

        private void listaconectados_Click(object sender, EventArgs e)
        {
            //string mensaje = "6/"+ username.Text + "/aaa";
            //// Enviamos al servidor el nombre tecleado
            //byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            //server.Send(msg);

            ////Recibimos la respuesta del servidor
            //byte[] msg2 = new byte[80];
            //server.Receive(msg2);
            //mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            ////MessageBox.Show(mensaje);
            ////string textoP = mensaje;
            ////List<string> list = new List<string>(mensaje.Split('\n'));
            //rellenarDatagrid(mensaje);



        }

        private void rellenarDatagrid(string text)
        {
            //string text = "9/ana/juan/pedro/luis/maite/emma/maria/david/juanjo";
            char[] charSeparators = new char[] { ',' };
            string[] items = text.Split(charSeparators);
            //MessageBox.Show(items[0]);
            int filas = Int32.Parse(items.ElementAt(0));
            dataGridView1.RowCount = filas;
            for (int i = 0; i < filas; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = items[i + 1];
            }

        }

        private void rellenarDatagridEstado(string text)
        {
            //string text = "9,ana-Lista,juan/pedro/luis/maite/emma/maria/david/juanjo";
            char[] charSeparators = new char[] { ',' };
            string[] items = text.Split(charSeparators);
            //MessageBox.Show(items[0]);
            int filas = Int32.Parse(items.ElementAt(0));
            //dataGridView1.RowCount = filas;

            for (int i = 0; i < items.Length; i++)
            {
                char[] charSeparators2 = new char[] { '-' };
                string[] estados = items[i + 1].Split(charSeparators2);
                string nombre = (estados.ElementAt(0));
                string estado = (estados.ElementAt(1));
                if (dataGridView1.Rows[i].Cells[0].Value == nombre)
                    dataGridView1.Rows[i].Cells[1].Value = estado;
            }

        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(Object sender, DataGridViewCellEventArgs e)
        {
            //obtienes el valor reservado
            string valorCelda = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            string mensaje = "8/" + username.Text + "/" + valorCelda;
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }


        private void invitar_Click(object sender, EventArgs e)
        {
            string mensaje = "7/Juanito/aaa";
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);


        }
    }
}
