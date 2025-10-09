using System;
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

        // public int LireTension()
        // {
        //     throw new NotImplementedException();
        // }

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
}
