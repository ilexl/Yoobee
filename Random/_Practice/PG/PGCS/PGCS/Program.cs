using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGCS
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialise values
            int a = 5;
            int b = 2;

            float c = 0;
            Console.WriteLine(c);

            c += 10;
            Console.WriteLine(c);

            c += (a / (float)b);
            Console.WriteLine(c);

            Console.ReadLine();
        }
    }
}
