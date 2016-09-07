using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApplication1
{
    class Program
    {
        static void writeA()
        {
            for (int i = 0; i < 1000; i++)
            {
                Console.Write("A");
                Thread.Sleep(10);
            }
        }
        static void writeB()
        {
            for (int i = 0; i < 10000; i++)
            {
                Console.Write("B");
                Thread.Sleep(10);
            }
        }
        static void Main(string[] args)
        {
            List<Thread> tredz = new List<Thread>();

            Thread A = new Thread(writeA);
            Thread B = new Thread(writeB);

            tredz.Add(A);
            tredz.Add(B);

            for (int i = 0; i < 2; i++)
            {
                tredz[i].Start();
            }
            Console.ReadLine();
        }
    }
}
