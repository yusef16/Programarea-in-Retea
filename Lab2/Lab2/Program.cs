using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices.JavaScript;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        string nick = "";
        string AdresaTemp = "";
        string[] Temp;
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        IPAddress iPAddress = IPAddress.Parse("0.0.0.0");

        IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 4444);
        EndPoint broadcastEndpoint = null;
        socket.Bind(iPEndPoint);



        while (true)
        {

            meniu();
            Console.WriteLine();
            Console.WriteLine("----------------------Alege optiunea----------------------");
            string alegere = Console.ReadLine();


            switch (alegere)
            {
                case "1":


                    Console.Clear();
                    Thread receivingMessage = new Thread(new ThreadStart(ReceiveMessages));
                    receivingMessage.Start();

                    Console.WriteLine("Insert Ip address");
                    string targetIp = Console.ReadLine();

                    broadcastEndpoint = new IPEndPoint(IPAddress.Parse(targetIp), 4444);



                    break;

                case "2":
                    Console.Clear();
                    Console.WriteLine("Insert Nickname:");
                    nick = Console.ReadLine();
                    Console.WriteLine("Nickname setat" + nick);

                    break;
                case "3":
                    try
                    {
                        Console.Clear();
                        while (true)
                        {

                            string data = Console.ReadLine() ?? string.Empty;
                            if (data == "exit")
                            {
                                break;
                            }
                            AdresaTemp = broadcastEndpoint.ToString();
                            Temp = AdresaTemp.Split(':');
                            AdresaTemp = Temp[0];
                            if (data == "b")
                            {
                                targetIp = "192.168.37.255";

                                Console.WriteLine(AdresaTemp);
                                broadcastEndpoint = new IPEndPoint(IPAddress.Parse(targetIp), 4444);
                                Console.WriteLine("S-a trecut la broadcast");

                            }
                            else if (data == "p")
                            {
                                targetIp = AdresaTemp.ToString();
                                broadcastEndpoint = new IPEndPoint(IPAddress.Parse(targetIp), 4445);
                                Console.WriteLine("S-a trecut la privat");
                            }
                            byte[] buffer = Encoding.UTF8.GetBytes(nick + ": " + data);
                            socket.SendTo(buffer, 0, buffer.Length, SocketFlags.None, broadcastEndpoint);

                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);

                    }

                    break;
                default:
                    Console.WriteLine("Valoare invalida");
                    break;


            }
        }


        void meniu()
        {
            Console.WriteLine("1 -- Introdu adresa ip");
            Console.WriteLine("2 -- Introdu numele de utilizator");
            Console.WriteLine("3 -- Introdu mesajul");
            Console.WriteLine("Scrieti exit pentru a accesa meniul");
        }
        void ReceiveMessages()
        {
            byte[] buffer;


            while (true)
            {
                EndPoint remote = new IPEndPoint(IPAddress.Any, 4444);

                buffer = new byte[1024];

                int receivedDataLength = socket.ReceiveFrom(buffer, ref remote);

                
Console.WriteLine("\n");
        Console.WriteLine(remote.ToString());
        Console.WriteLine(Encoding.ASCII.GetString(buffer, 0, receivedDataLength));
    }
}
    }
}