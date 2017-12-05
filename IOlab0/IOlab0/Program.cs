using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Net;

namespace Lab_1
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(ThreadServer);
            ThreadPool.QueueUserWorkItem(ThreadClient, "no elo");
            ThreadPool.QueueUserWorkItem(ThreadClient, "no elo");

            Console.ReadKey();
        }

        static void ThreadServer(Object stateInfo)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 2050);
            server.Start();
            while (true)
            {
                Console.WriteLine("Waiting for connection");
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Connected");
            }

        }
        static void ThreadClient(Object stateInfo)
        {
            TcpClient client = new TcpClient("127.0.0.1", 2050);
            Byte[] data = System.Text.Encoding.ASCII.GetBytes((string)stateInfo);
            NetworkStream stream = client.GetStream();
            stream.Write(data, 0, data.Length);
            Console.WriteLine("Sent {0}", (string)stateInfo);
            stream.Close();
            client.Close();


        }

    }
}