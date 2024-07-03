using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Windows_Forms_Chat
{
    public class TCPChatBase
    {
        public TextBox chatTextBox;
        public int port;

        public void SetChat(string str)
        {
            chatTextBox.Invoke((Action)delegate
            {
                chatTextBox.Text = str;
                chatTextBox.AppendText(Environment.NewLine);
            });
        }

        public void AddToChat(string str)
        {
            chatTextBox.Invoke((Action)delegate
            {
                chatTextBox.AppendText(str);
                chatTextBox.AppendText(Environment.NewLine);
            });
        }

        // Method to display chat messages with usernames
        public void AddToChatWithUsername(string username, string message)
        {
            chatTextBox.Invoke((Action)delegate
            {
                chatTextBox.AppendText($"{username}: {message}");
                chatTextBox.AppendText(Environment.NewLine);
            });
        }

        // Method to handle commands received from the server
        public void HandleCommand(string command)
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
            else
            {
                // Handle regular chat message
                AddToChat(command);
            }
        }
    }
}
