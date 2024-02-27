using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using System.Linq;

class server
{
    static void Main(string[] args)
    {
        int port = 8888;
        string IpAddress = "127.0.0.1";

        Socket sk = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint ep = new IPEndPoint(IPAddress.Parse(IpAddress), port);
        sk.Bind(ep);
        sk.Listen(100);
        Console.WriteLine("Server is Listening...");
        Socket ClientSocket = default(Socket);

        server S = new server();
        int counter = 0;

        while (true)
        {
            counter++;
            ClientSocket = sk.Accept();
            Console.WriteLine("Clientul " + counter + " este conectat");
            //S.User(ClientSocket);
            Thread UserThread = new Thread(new ThreadStart(() => S.User(ClientSocket)));
            UserThread.Start();
        }
    }

    public void User(Socket client)
    {

        while (true)
        {
            byte[] message = new byte[1024];
            int size = client.Receive(message);
            string receivedMessage = System.Text.Encoding.ASCII.GetString(message, 0, size);
            Console.WriteLine("Mesjul de la client: " + receivedMessage);
            string responseMessage = "Serverul a primit mesajul: " + receivedMessage;
            client.Send(System.Text.Encoding.ASCII.GetBytes(responseMessage));
        }


    }


}
