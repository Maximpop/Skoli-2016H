using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_4
{
    class methods
    {
        public string Reverse(string text)
        {
            char[] cArray = text.ToCharArray();
            string reverse = String.Empty;
            for (int i = cArray.Length - 1; i > -1; i--)
            {
                reverse += cArray[i];
            }
            return reverse;
        }
        public int[] nums(int[] tolur)
        {

        }
    }
    class Program
    {
        

        static void Main(string[] args)
        {
            int num = 1000;
            int tala = 0;
            string lol;
            string largest = null;


            
            Console.WriteLine(largest);
            Console.ReadLine();

        }
    }
}
