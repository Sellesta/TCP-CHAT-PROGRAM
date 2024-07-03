The provided project is a Windows Forms semi completed TCP Sockets Chat project. 

How To Run the Project
========================
To run it you will need to run at least 2 instances of the project exe.
Host Server:
If you open the project in Visual Studio and run it, choose to Host Server. 
Host Client(s):
In the project folder, navigate to Windows Forms core chat\bin\Debug\netcoreapp3.1 folder and run Windows Forms core chat.exe and click Join Server. 

Clients should be able to type messages to the server and the server can broadcast those messages to all clients.

The Project
============
In Visual Studio if you open Form1.cs it should open the Windows Forms designer. If you right click textboxes or buttons you can see that they have a name property which is how we can access them via code. Take note of the main ui elements names here.

If you right click Form1.cs in the solution explorer you can 'View Code'. Explore the code and try to understand it.

The server creates an asynchronous task(thread) to keep listening for joining clients. If a client joins, a new thread is created to start listening for incoming messages from that client. So in total, the server has the main thread the UI runs on, the listening to joining client threads and as many extra threads running for joined clients sending messages in.

You will need to work through the code to work out where and how to add to it.

The Tic-Tac-Toe board is for the final assignment and only works for Cross player but detects wins and resets the board.