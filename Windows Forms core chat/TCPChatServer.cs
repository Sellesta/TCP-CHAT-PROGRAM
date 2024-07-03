using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Windows_Forms_Chat
{
    public class TCPChatServer : TCPChatBase
    {
        public Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public List<ClientSocket> clientSockets = new List<ClientSocket>();

        public static TCPChatServer createInstance(int port, TextBox chatTextBox)
        {
            TCPChatServer tcp = null;
            if (port > 0 && port < 65535 && chatTextBox != null)
            {
                tcp = new TCPChatServer();
                tcp.port = port;
                tcp.chatTextBox = chatTextBox;
            }
            return tcp;
        }

        public void SetupServer()
        {
            chatTextBox.Text += "Setting up server...\n";
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, port));
            serverSocket.Listen(0);
            serverSocket.BeginAccept(AcceptCallback, null);
            chatTextBox.Text += "Server setup complete\n";
        }

        public void CloseAllSockets()
        {
            foreach (ClientSocket clientSocket in clientSockets)
            {
                clientSocket.socket.Shutdown(SocketShutdown.Both);
                clientSocket.socket.Close();
            }
            clientSockets.Clear();
            serverSocket.Close();
        }

        public void AcceptCallback(IAsyncResult AR)
        {
            Socket joiningSocket;
            try
            {
                joiningSocket = serverSocket.EndAccept(AR);
            }
            catch (ObjectDisposedException)
            {
                return;
            }
            ClientSocket newClientSocket = new ClientSocket();
            newClientSocket.socket = joiningSocket;
            clientSockets.Add(newClientSocket);
            joiningSocket.BeginReceive(newClientSocket.buffer, 0, ClientSocket.BUFFER_SIZE, SocketFlags.None, ReceiveCallback, newClientSocket);
            AddToChat("Client connected, waiting for request...");
            serverSocket.BeginAccept(AcceptCallback, null);
        }

        public void ReceiveCallback(IAsyncResult AR)
        {
            ClientSocket currentClientSocket = (ClientSocket)AR.AsyncState;
            int received;
            try
            {
                received = currentClientSocket.socket.EndReceive(AR);
            }
            catch (SocketException)
            {
                AddToChat("Client forcefully disconnected");
                currentClientSocket.socket.Close();
                clientSockets.Remove(currentClientSocket);
                return;
            }
            byte[] recBuf = new byte[received];
            Array.Copy(currentClientSocket.buffer, recBuf, received);
            string text = Encoding.ASCII.GetString(recBuf);
            AddToChat(text);
            if (text.ToLower() == "!commands")
            {
                byte[] data = Encoding.ASCII.GetBytes("Commands are !commands !about !who !whisper !exit");
                currentClientSocket.socket.Send(data);
                AddToChat("Commands sent to client");
            }
            else if (text.ToLower() == "!exit")
            {
                currentClientSocket.socket.Shutdown(SocketShutdown.Both);
                currentClientSocket.socket.Close();
                clientSockets.Remove(currentClientSocket);
                AddToChat("Client disconnected");
                return;
            }
            else
            {
                SendToAll(text, currentClientSocket);
            }
            currentClientSocket.socket.BeginReceive(currentClientSocket.buffer, 0, ClientSocket.BUFFER_SIZE, SocketFlags.None, ReceiveCallback, currentClientSocket);
        }

        public void SendToAll(string str, ClientSocket from)
        {
            foreach (ClientSocket c in clientSockets)
            {
                if (from == null || !from.socket.Equals(c))
                {
                    byte[] data = Encoding.ASCII.GetBytes(str);
                    c.socket.Send(data);
                }
            }
        }
    }
}
