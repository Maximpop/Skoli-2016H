using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_2
{
    class Program
    {
        static void Main(string[] args)
        {
            int oldi = 0, i = 1, lol =0;
            int sum = 0;

            while (i<=4000000)
            {
                if (i%2==0)
                {
                    sum += i;
                }
                lol = i + oldi;
                oldi = i;
                i = lol;
            }
            
            Console.WriteLine(sum);
            Console.ReadLine();
        }
    }
}
