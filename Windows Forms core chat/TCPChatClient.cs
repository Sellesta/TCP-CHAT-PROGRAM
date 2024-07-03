using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Windows_Forms_Chat
{
    public class TCPChatClient : TCPChatBase
    {
        public Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public ClientSocket clientSocket = new ClientSocket();
        public int serverPort;
        public string serverIP;

        public static TCPChatClient CreateInstance(int port, int serverPort, string serverIP, TextBox chatTextBox)
        {
            TCPChatClient tcp = null;
            //if port values are valid and ip worth attempting to join
            if (port > 0 && port < 65535 &&
                serverPort > 0 && serverPort < 65535 &&
                serverIP.Length > 0 &&
                chatTextBox != null)
            {
                tcp = new TCPChatClient();
                tcp.port = port;
                tcp.serverPort = serverPort;
                tcp.serverIP = serverIP;
                tcp.chatTextBox = chatTextBox;
                tcp.clientSocket.socket = tcp.socket;
            }
            return tcp;
        }

        public void ConnectToServer(string username)
        {
            try
            {
                socket.Connect(serverIP, serverPort);
                SetChat("Connected");
                SendString($"!username {username}"); // Send username to the server
                Task.Factory.StartNew(ReceiveLoop); // Start receiving messages from the server
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public void SendString(string text)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(text);
            socket.Send(buffer, 0, buffer.Length, SocketFlags.None);
        }

        public void ReceiveCallback(IAsyncResult AR)
        {
            ClientSocket currentClientSocket = (ClientSocket)AR.AsyncState;
            try
            {
                int received = currentClientSocket.socket.EndReceive(AR);
                if (received > 0)
                {
                    string text = Encoding.ASCII.GetString(currentClientSocket.buffer, 0, received);
                    HandleReceivedMessage(text);
                    currentClientSocket.socket.BeginReceive(currentClientSocket.buffer, 0, ClientSocket.BUFFER_SIZE, SocketFlags.None, ReceiveCallback, currentClientSocket);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void HandleReceivedMessage(string message)
        {
            if (message.StartsWith("!"))
            {
                // Handle command
                HandleCommand(message);
            }
            else
            {
                // Display chat message
                AddToChatWithUsername(clientSocket.Username, message);
            }
        }

        // Method to handle commands received from the server
        private void HandleCommand(string command)
        {
            if (command.StartsWith("!"))
            {
                string[] parts = command.Split(' ');
                string cmd = parts[0].ToLower();

                switch (cmd)
                {
                    case "!who":
                        // Logic to handle the !who command
                        break;
                    case "!about":
                        // Logic to handle the !about command
                        break;
                    case "!whisper":
                        // Logic to handle the !whisper command
                        break;
                    case "!mod":
                        // Logic to handle the !mod command
                        break;
                    case "!kick":
                        // Logic to handle the !kick command
                        break;
                    case "!mods":
                        // Logic to handle the !mods command
                        break;
                    default:
                        // Handle unknown command
                        AddToChat($"Unknown command: {cmd}");
                        break;
                }
            }
        }

        public void Close()
        {
            socket.Close();
        }
    }
}
