using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;

class Client
{

    static void Main(string[] args)
    {

        int port = 8888;
        string Ipaddress = "127.0.0.1";

        Socket ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint ep = new IPEndPoint(IPAddress.Parse(Ipaddress), port);

        ClientSocket.Connect(ep);

        Console.WriteLine("Clientul este conectat");

        while (true)
        {
            string message = null;
            Console.WriteLine("Introdu mesajul");
            message = Console.ReadLine();
            ClientSocket.Send(System.Text.Encoding.ASCII.GetBytes(message), 0, message.Length, SocketFlags.None);
            byte[] messagefromserver = new byte[1024];
            int size = ClientSocket.Receive(messagefromserver);
            Console.WriteLine("Server" + System.Text.Encoding.ASCII.GetString(messagefromserver, 0, size));

        }

    }

}