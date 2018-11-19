using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Linq;
using System.Text;


namespace cs_pp1
{
    class Program
    {

        static void Main(string[] args)
        {
            int PhilosophersCount;
            Console.WriteLine("Кол-во философов: ");
            PhilosophersCount = int.Parse(Console.ReadLine());
            
            PhilosophersLunch lunch = new PhilosophersLunch(PhilosophersCount);
            lunch.StartLunch();

            Console.ReadKey();
        }
    }
}
