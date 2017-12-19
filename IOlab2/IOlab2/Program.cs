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
        static void zadanie1()
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

        delegate int DelegateType(object argumetns);
        static DelegateType delegateSilnia, delegateSilniait;

        static int silniarek(object arguments)
        {
            int liczba = (int)arguments;
            if (liczba < 2) return 1;
            return liczba * silniarek(liczba - 1); ;
        }
        static int silniait(object arguments)
        {
            int liczba, wynik = 1;
            
            liczba = (int)arguments;
            
            
            
                while (liczba > 0)
                {
                    wynik *= liczba;
                    liczba--;
                }

            return wynik;
        }
        public static void zad5() {
            delegateSilnia = new DelegateType(silniarek);
            IAsyncResult ar = delegateSilnia.BeginInvoke(10, null, null);
            int result = delegateSilnia.EndInvoke(ar);
            Console.WriteLine(result);
            delegateSilniait = new DelegateType(silniait);
            IAsyncResult arit = delegateSilnia.BeginInvoke(10, null, null);
            result = delegateSilnia.EndInvoke(arit);
            Console.WriteLine(result);
        }


        static void myAsyncCallback(IAsyncResult state)
        {
            string result = System.Text.Encoding.UTF8.GetString((byte[])((object[])state.AsyncState)[1]);
            FileStream stream = (FileStream)((object[])state.AsyncState)[0];
            stream.Close();
            Console.Write(result);

        }


        public static void zad6()
        {
            FileStream stream = new FileStream("plik.txt", FileMode.Open);
            byte[] buffer = new byte[1024];
            IAsyncResult result = stream.BeginRead(buffer, 0, buffer.Length, myAsyncCallback, new object[] { stream, buffer });
            stream.Close();
        }

        public static void zad7()
        {
            FileStream stream = new FileStream("plik.txt", FileMode.Open);
            byte[] buffer = new byte[1024];
            IAsyncResult result = stream.BeginRead(buffer, 0, buffer.Length, null, new object[] { stream, buffer });
            

     
            stream.EndRead(result);
            string message = System.Text.Encoding.UTF8.GetString((byte[])((object[])result.AsyncState)[1]);
            stream.Close();
            Console.Write(message);

            Thread.Sleep(10000);
        }

        static void Main(string[] args)
        {
            zad5();
            zad6();
            zad7();


            Console.ReadKey();

        }
      
    }
}
