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
        
        ConsoleColor actualColor;
        Byte[] bytes;
        TcpClient client;
        String data;
        static void writeConsoleMessage(string message, ConsoleColor color)
        {
                Console.ForegroundColor = color;
                Console.WriteLine(message);
                Console.ResetColor();
        }
        public MyThread(ConsoleColor color, TcpClient client, Byte[] bytes, String data)
        {
            
            actualColor = color;
            this.client = client;
            this.bytes = bytes;
            this.data = data;
        }

        public void Run()
        {
            NetworkStream stream = client.GetStream();
            int i = stream.Read(bytes, 0, bytes.Length);
            while (i  != 0)
            {
                data = Encoding.ASCII.GetString(bytes, 0, i);
                writeConsoleMessage("Server Received:" + data, ConsoleColor.Green);

                byte[] msg = Encoding.ASCII.GetBytes(data);

                // Send back a response.
                stream.Write(msg, 0, msg.Length);
                writeConsoleMessage("Server sent:" + data, ConsoleColor.Red);
                client.Close();
            }
        }
    }
    class Program
    {

        static void ThreadProc(Object stateInfo)
        {
            Info inf = (Info)stateInfo;
            long result = 0;
            for (int i = inf.end; i <= inf.end; i++)
            {
                result += inf.tab[i];
            }
            lock (inf.locker)
            {
                inf.result.result += result; 
            }
            AutoResetEvent are = (AutoResetEvent)inf.handle;
            are.Set(); 


        }
        public static void zad()
        {

            int n = 50000;
            int seg = 10050;
            
            int[] tab = new int[n];  
            object locker = new object();   
            Value result = new Value();       
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {

                tab[i] = rnd.Next(1, 1);
            }
            int threadAmount = n / seg; 
            if (n % seg != 0) threadAmount++; 
            WaitHandle[] waitHandles = new WaitHandle[threadAmount]; 
            for (int i = 0; i < threadAmount; i++)
            {
                waitHandles[i] = new AutoResetEvent(false); 
            }
            for (int i = 0; i < threadAmount; i++)  
            {
                Info inf = new Info(i * seg, ((i + 1) * seg) - 1, tab, locker, result, waitHandles[i]);
                if (((i + 1) * seg) > n) inf.end = n - 1;
                ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadProc), inf); 
            }
            WaitHandle.WaitAll(waitHandles); 
            Console.Write("Wynik: " + result.result);
            Thread.Sleep(6000);

        }

        class Info 
        {
            public int start;
            public int end;
            public int[] tab;
            public object locker;
            public Value result;
            public WaitHandle handle;
            public Info(int start, int end, int[] tab, object locker, Value result, WaitHandle handle)
            {
                this.start = start;
                this.end = end;
                this.tab = tab;
                this.locker = locker;
                this.result = result;
                this.handle = handle;
            }


        }
        class Value 
        {
            public long result = 0;
        }

        static void Main()
        {
            zad();
            //ThreadPool.QueueUserWorkItem(ThreadServer);
            //ThreadPool.QueueUserWorkItem(ThreadClient, "Hello");
            Console.ReadKey();
        }

        static void writeConsoleMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        static void ThreadServer(Object stateinfo)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 2048);
            String data = null;
            Byte[] bytes = new Byte[256];
            server.Start();
            
                Console.WriteLine("Waiting for connection...");
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Connected!");
                MyThread myThread = new MyThread(ConsoleColor.Red, client, bytes, data);
                Thread readThread = new Thread(new ThreadStart(myThread.Run));
                readThread.Start();
           
                
            
        }

        static void ThreadClient(Object stateinfo)
        {
            TcpClient client = new TcpClient("127.0.0.1", 2048);
            Byte[] data = Encoding.ASCII.GetBytes((string)stateinfo);
            NetworkStream stream = client.GetStream();
            stream.Write(data, 0, data.Length);
            writeConsoleMessage("Client Sent:" + (string)stateinfo, ConsoleColor.Green);

            String responseData = String.Empty;
            data = new Byte[256];
            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = Encoding.ASCII.GetString(data, 0, bytes);
            writeConsoleMessage("Clint recived:" + responseData, ConsoleColor.Red);

            stream.Close();
            client.Close();
        }
    }
}