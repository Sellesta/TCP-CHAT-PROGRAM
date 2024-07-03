using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Windows_Forms_Chat
{
    public class ClientSocket
    {
        public Socket socket;
        public const int BUFFER_SIZE = 2048;
        public byte[] buffer = new byte[BUFFER_SIZE];

        // New attributes
        public string Username { get; set; }
        public bool IsLoggedIn { get; set; } // Indicates whether the client is successfully logged in

        public ClientSocket(Socket socket)
        {
            this.socket = socket;
            Username = ""; // Initialize the username to empty string
            IsLoggedIn = false; // Initialize the login status to false
        }

        // Method to set the username
        public void SetUsername(string username)
        {
            Username = username;
        }

        // Method to set the login status
        public void SetLoginStatus(bool isLoggedIn)
        {
            IsLoggedIn = isLoggedIn;
        }
    }
}
