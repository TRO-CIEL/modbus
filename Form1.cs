using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace modbus
{
    public partial class Form1 : Form
    {
        // Socket - connexion
        private Socket socket;
        
        public Form1()
        {
            InitializeComponent();
        }

        // Événement - clic
        private void buttonConnexion_Click(object sender, EventArgs e)
        {
            try
            {
                // Récupération - adresse
                string adresseIP = textBoxAdresseIP.Text;
                
                // Affichage - tentative
                textBoxStatut.Text += $"Connexion au serveur {adresseIP}\r\n";
                
                // Création - socket
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                
                // Conversion - adresse
                IPAddress ipAddress = IPAddress.Parse(adresseIP);
                
                // Création - endpoint
                IPEndPoint endPoint = new IPEndPoint(ipAddress, 502); // Port Modbus standard
                
                // Connexion - serveur
                socket.Connect(endPoint);
                
                // Affichage - succès
                textBoxStatut.Text += "Connexion ok\r\n";
                
                // Défilement - automatique
                textBoxStatut.SelectionStart = textBoxStatut.Text.Length;
                textBoxStatut.ScrollToCaret();
            }
            catch (System.Net.Sockets.SocketException se)
            {
                // Erreur - socket
                textBoxStatut.Text += "**Exception : Impossible de se connecter au serveur\r\n";
                textBoxStatut.Text += "Message : " + se.Message + "\r\n";
                
                // Défilement - automatique
                textBoxStatut.SelectionStart = textBoxStatut.Text.Length;
                textBoxStatut.ScrollToCaret();
            }
            catch (System.Exception ex)
            {
                // Erreur - générale
                textBoxStatut.Text += "**Exception : Impossible de se connecter au serveur\r\n";
                textBoxStatut.Text += "Message : " + ex.Message + "\r\n";
                
                // Défilement - automatique
                textBoxStatut.SelectionStart = textBoxStatut.Text.Length;
                textBoxStatut.ScrollToCaret();
            }
        }

        // Événement - déconnexion
        private void buttonDeconnexion_Click(object sender, EventArgs e)
        {
            try
            {
                // Vérification - socket
                if (socket != null)
                {
                    // Fermeture - connexion
                    socket.Close();
                    
                    // Affichage - succès
                    textBoxStatut.Text += "Déconnexion réussie\r\n";
                }
                else
                {
                    // Affichage - aucune
                    textBoxStatut.Text += "Aucune connexion active\r\n";
                }
                
                // Défilement - automatique
                textBoxStatut.SelectionStart = textBoxStatut.Text.Length;
                textBoxStatut.ScrollToCaret();
            }
            catch (System.Exception ex)
            {
                // Erreur - déconnexion
                textBoxStatut.Text += "**Exception lors de la déconnexion\r\n";
                textBoxStatut.Text += "Message : " + ex.Message + "\r\n";
                
                // Défilement - automatique
                textBoxStatut.SelectionStart = textBoxStatut.Text.Length;
                textBoxStatut.ScrollToCaret();
            }
        }
    }
}
