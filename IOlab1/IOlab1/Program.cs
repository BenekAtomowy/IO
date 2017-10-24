using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IOlab1
{
    class MyThread
    {
        string message = "Wiadomość";
        ConsoleColor actualColor;
        static void writeConsoleMessage(string message, ConsoleColor color)
        {

                Console.ForegroundColor = color;
                Console.WriteLine(message);
                Console.ResetColor();
            
        }
        public MyThread(ConsoleColor color, string msg)
        {
                message = msg;
                actualColor = color;
        }

        public void Run()
        {
                writeConsoleMessage(message, actualColor);
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(ThreadServer);
            ThreadPool.QueueUserWorkItem(ThreadClient, "Hallo");
            ThreadPool.QueueUserWorkItem(ThreadClient, "Hi");
            ThreadPool.QueueUserWorkItem(ThreadClient, "Hola");
            ThreadPool.QueueUserWorkItem(ThreadClient, "Hej");
            ThreadPool.QueueUserWorkItem(ThreadClient, "Cześć");
            ThreadPool.QueueUserWorkItem(ThreadClient, "Heil");
            Console.ReadKey();

        }

        static void ThreadServer (Object stateinfo)
        {
            
            TcpListener server = new TcpListener(IPAddress.Any, 2048);
            server.Start();
            

            while (true)
            {
                    Console.WriteLine("Waiting for connection....");
                    TcpClient client = server.AcceptTcpClient();
 
                
                Console.WriteLine("Connected");
                    MyThread myThread = new MyThread(ConsoleColor.Red, "ok");
                    lock (myThread)
                    {
                        Thread writeThread = new Thread(new ThreadStart(myThread.Run));
                        writeThread.Start(); 
                }
            }
        }

        static void ThreadClient(Object stateinfo)
        {
           
            TcpClient client  = new TcpClient("127.0.0.1", 2048);
            Byte[] data = System.Text.Encoding.ASCII.GetBytes((string)stateinfo);
            NetworkStream stream = client.GetStream();

            stream.Write(data, 0, data.Length);
            MyThread myThread = new MyThread(ConsoleColor.Green, stateinfo.ToString());
            lock (myThread)
            {
                Thread writeThread = new Thread(new ThreadStart(myThread.Run));
                writeThread.Start();
            }
            Console.WriteLine("Received: {0}", (string)stateinfo);

            stream.Close();
            client.Close();
        }
    }
}
