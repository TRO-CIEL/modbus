using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace modbus
{
    public class CModbus
    {
        private Socket socket;

        public string Connexion(string adresseIP)
        {
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                var ip = IPAddress.Parse(adresseIP);
                var ep = new IPEndPoint(ip, 502);
                socket.Connect(ep);
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Deconnexion()
        {
            try
            {
                if (socket != null)
                {
                    socket.Close();
                }
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public short LireUnMot(short adresse, ref string strResultat)
        {
            try
            {
                if (socket == null || !socket.Connected)
                {
                    strResultat = "non connecté";
                    return 0;
                }

                ushort start = (ushort)(adresse - 1);
                var trameE = new byte[]
                {
                    0x00, 0x00,
                    0x00, 0x00,
                    0x00, 0x06,
                    0x01,
                    0x03,
                    (byte)(start >> 8), (byte)(start & 0xFF),
                    0x00, 0x01
                };

                socket.Send(trameE);

                var trameR = new byte[256];
                int bytes = socket.Receive(trameR);
                if (bytes >= 11 && trameR[7] == 0x03 && trameR[8] == 0x02)
                {
                    ushort val = (ushort)((trameR[9] << 8) | trameR[10]);
                    strResultat = "ok";
                    return unchecked((short)val);
                }
                strResultat = "réponse invalide";
                return 0;
            }
            catch (Exception ex)
            {
                strResultat = ex.Message;
                return 0;
            }
        }
    }

    public partial class Form1 : Form
    {
        private CModbus modbus;
        private List<double> tensionValues = new List<double>();
        private int pointCount = 0;
        
        public Form1()
        {
            InitializeComponent();
            modbus = new CModbus();
        }

        private void buttonConnexion_Click(object sender, EventArgs e)
        {
            string adresseIP = textBoxAdresseIP.Text;
            textBoxStatut.Text += $"Connexion au serveur {adresseIP}\r\n";
            var res = modbus.Connexion(adresseIP);
            if (res == "ok") textBoxStatut.Text += "Connexion ok\r\n"; else textBoxStatut.Text += "**Exception : Impossible de se connecter au serveur\r\nMessage : " + res + "\r\n";
            textBoxStatut.SelectionStart = textBoxStatut.Text.Length;
            textBoxStatut.ScrollToCaret();
        }

        private void buttonDeconnexion_Click(object sender, EventArgs e)
        {
            var res = modbus.Deconnexion();
            if (res == "ok") textBoxStatut.Text += "Déconnexion réussie\r\n"; else textBoxStatut.Text += "**Exception lors de la déconnexion\r\nMessage : " + res + "\r\n";
            textBoxStatut.SelectionStart = textBoxStatut.Text.Length;
            textBoxStatut.ScrollToCaret();
        }

        private void buttonLire_Click(object sender, EventArgs e)
        {
            try
            {
                string res = "";
                short val = modbus.LireUnMot(3207, ref res);
                if (res == "ok")
                {
                    double tension = ((ushort)val) / 10.0;
                    textBoxTension.Text = string.Format("{0:F1} V", tension);
                }
                else
                {
                    textBoxTension.Text = "Erreur";
                    textBoxStatut.Text += "**Exception lors de la lecture tension\r\nMessage : " + res + "\r\n";
                }
                textBoxStatut.SelectionStart = textBoxStatut.Text.Length;
                textBoxStatut.ScrollToCaret();
            }
            catch (System.Exception ex)
            {
                textBoxTension.Text = "Erreur";
                textBoxStatut.Text += "**Exception lors de la lecture tension\r\nMessage : " + ex.Message + "\r\n";
                textBoxStatut.SelectionStart = textBoxStatut.Text.Length;
                textBoxStatut.ScrollToCaret();
            }
        }

        private void buttonLireThermique_Click(object sender, EventArgs e)
        {
            try
            {
                string res = "";
                short val = modbus.LireUnMot(3208, ref res);
                if (res == "ok")
                {
                    double thermique = ((ushort)val) / 100.0;
                    textBoxThermique.Text = string.Format("{0:F1} %", thermique);
                }
                else
                {
                    textBoxThermique.Text = "Erreur";
                    textBoxStatut.Text += "**Exception lors de la lecture thermique\r\nMessage : " + res + "\r\n";
                }
                textBoxStatut.SelectionStart = textBoxStatut.Text.Length;
                textBoxStatut.ScrollToCaret();
            }
            catch (System.Exception ex)
            {
                textBoxThermique.Text = "Erreur";
                textBoxStatut.Text += "**Exception lors de la lecture thermique\r\nMessage : " + ex.Message + "\r\n";
                textBoxStatut.SelectionStart = textBoxStatut.Text.Length;
                textBoxStatut.ScrollToCaret();
            }
        }

        private void checkBoxAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAuto.Checked)
            {
                timer1.Start();
                buttonLire.Enabled = false;
                buttonLireThermique.Enabled = false;
                buttonConnexion.Enabled = false;
                buttonDeconnexion.Enabled = false;
            }
            else
            {
                timer1.Stop();
                buttonLire.Enabled = true;
                buttonLireThermique.Enabled = true;
                buttonConnexion.Enabled = true;
                buttonDeconnexion.Enabled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string res = "";
            short val = modbus.LireUnMot(3207, ref res);
            if (res == "ok")
            {
                double tension = ((ushort)val) / 10.0;
                textBoxTension.Text = string.Format("{0:F1} V", tension);
                
                tensionValues.Add(tension);
                if (tensionValues.Count > 50)
                {
                    tensionValues.RemoveAt(0);
                }
                pointCount++;
                pictureBoxGraph.Invalidate();
                
                textBoxStatut.Text += string.Format("ok Tension = {0:F1}\r\n", tension);
            }
            else
            {
                textBoxTension.Text = "Erreur";
                textBoxStatut.Text += "Erreur lecture automatique\r\n";
            }
            
            short valTherm = modbus.LireUnMot(3208, ref res);
            if (res == "ok")
            {
                double thermique = ((ushort)valTherm) / 100.0;
                textBoxThermique.Text = string.Format("{0:F1} %", thermique);
            }
            
            textBoxStatut.SelectionStart = textBoxStatut.Text.Length;
            textBoxStatut.ScrollToCaret();
        }

        private void pictureBoxGraph_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.White);
            
            if (tensionValues.Count < 2) return;
            
            int width = pictureBoxGraph.Width - 40;
            int height = pictureBoxGraph.Height - 40;
            int startX = 20;
            int startY = 20;
            
            using (Pen gridPen = new Pen(Color.LightGray))
            using (Pen axisPen = new Pen(Color.Black))
            using (Pen linePen = new Pen(Color.Blue, 2))
            using (Font font = new Font("Arial", 8))
            using (Brush brush = new SolidBrush(Color.Black))
            {
                g.DrawRectangle(axisPen, startX, startY, width, height);
                
                for (int i = 0; i <= 4; i++)
                {
                    int y = startY + (height * i / 4);
                    g.DrawLine(gridPen, startX, y, startX + width, y);
                    double voltage = 245 - (i * 10);
                    g.DrawString(voltage.ToString(), font, brush, 2, y - 6);
                }
                
                for (int i = 1; i < tensionValues.Count; i++)
                {
                    double v1 = tensionValues[i - 1];
                    double v2 = tensionValues[i];
                    
                    int x1 = startX + (width * (i - 1) / (tensionValues.Count - 1));
                    int y1 = startY + height - (int)((v1 - 205) * height / 40);
                    int x2 = startX + (width * i / (tensionValues.Count - 1));
                    int y2 = startY + height - (int)((v2 - 205) * height / 40);
                    
                    g.DrawLine(linePen, x1, y1, x2, y2);
                }
                
                g.DrawString("Tension (V)", font, brush, startX + width/2 - 30, 5);
                g.DrawString("Temps", font, brush, startX + width - 20, startY + height + 5);
            }
        }
    }
}
