using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IOlab2
{
    class Program
    {
        
        static void Main(string[] args)
        {
            FileStream file = new FileStream("text.txt", FileMode.Open);
            Byte[] bytes = new Byte[256];
            //var ar = file.BeginRead(bytes, 0, bytes.Length, myAsynchCallback, new object[] { file, bytes });
            var ar = file.BeginRead(bytes, 0, bytes.Length, null, null);
            Console.WriteLine("po wątku");
            file.EndRead(ar);
            Console.WriteLine(Encoding.ASCII.GetString(bytes));
            Console.ReadKey();
        }
        private static void myAsynchCallback(IAsyncResult ar)
        {
            object[] dane = (object[])ar.AsyncState;
            FileStream file = (FileStream)dane[0];
            Byte[] buffer = (Byte[])dane[1];
            Console.WriteLine(System.Text.Encoding.ASCII.GetString(buffer));
            file.Close();            
        }
    }
}
