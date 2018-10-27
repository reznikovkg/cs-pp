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
            int tQpush;
            int tQpushCount;
            int tQpop;
            
            Console.WriteLine("Кол-во потоков для push: ");
            tQpush = int.Parse(Console.ReadLine());

            Console.WriteLine("Кол-во операций на один поток push: ");
            tQpushCount = int.Parse(Console.ReadLine());

            Console.WriteLine("Кол-во потоков для pop: ");
            tQpop = int.Parse(Console.ReadLine());

            HandlerTreadQueue htq = new HandlerTreadQueue(tQpush, tQpushCount, tQpop);
            
            Console.ReadKey();
        }
    }
}
