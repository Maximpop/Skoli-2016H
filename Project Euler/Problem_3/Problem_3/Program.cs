using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_3
{
    class Program
    {
        static void Main(string[] args)
        {
            double num = 600851475143;

            double factor = 2;

            while (factor*factor<=num)
            {
                if (num%factor==0)
                {
                    num /= factor;
                }
                else
                {
                    factor++;
                }
            }
            Console.WriteLine(num);
            Console.ReadLine();

        }
    }
}
