using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*Zaprojektuj klasę CompletedEventArgs dla zdarzenia obsługującego
asynchroniczną operację MatMulAsync w ramach operacji
asynchronicznego mnożenia macierzy.Rozważ implementację klasy
ProgressChangedEventArgs.Przyjmij założenie, że mnożone będą
macierze o rozmiarze n x m*/


namespace IOlab4
{
    
    class MyCompletedEventArgs 
    {

        public int[,] MatMulAsync(int [,] A, int[,] B)
        {
            int[,] result = new int[A.Length,A.Length];
            for(int i= 0; i<A.Length; i++)
            {
                for (int j = 0; j < A.Length; j++)
                    result[i, j] = A[i, j] * B[i, j];
            }
            return result;
        }
        
           
        }
    }

    class Program
    {

        delegate void EventHandler(EventArgs args);
        static event EventHandler foo;

        static void baseEvent(EventArgs args)
        {
            
        }

        static void myEvent(EventArgs args)
        {
            
        }

    static void Main(string[] args)
        {
        
        foo = new EventHandler(baseEvent);
        foo += myEvent;
        foo.Invoke(new EventArgs());
        Console.ReadKey();
        }

}

