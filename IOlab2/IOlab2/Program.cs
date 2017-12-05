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


        static void Main(string[] args)
        {
            delegateSilnia = new DelegateType(silniarek);
            IAsyncResult ar = delegateSilnia.BeginInvoke(10 , null, null);
            int result = delegateSilnia.EndInvoke(ar);
            Console.WriteLine(result);
            delegateSilniait = new DelegateType(silniait);
            IAsyncResult arit = delegateSilnia.BeginInvoke(10, null, null);
            result = delegateSilnia.EndInvoke(arit);
            Console.WriteLine(result);
            Console.ReadKey();

        }
      
    }
}
