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
    }
}
