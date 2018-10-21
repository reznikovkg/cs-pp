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
        static double FuncForIntegral (double x)
        {
            return Math.Sin(x);
        }

        static void Main(string[] args)
        {
            Func<double, double> Func = FuncForIntegral;
            List<MyIntegral> listIn = new List<MyIntegral>();

            int st = 1;
            double h = 1;
            double a = 0;
            double b = 1;

            Console.WriteLine("Кол-во потоков: ");
            st = int.Parse(Console.ReadLine());

            while (st>0)
            {
                Console.WriteLine("Шаг интеграла: ");
                h = double.Parse(Console.ReadLine());
                Console.WriteLine("Нижняя граница: ");
                a = double.Parse(Console.ReadLine());
                Console.WriteLine("Верхняя граница: ");
                b = double.Parse(Console.ReadLine());

                MyIntegral iinn = new MyIntegral(Func, st, h, a, b);

                Console.WriteLine("Кол-во потоков (0 -> выход): ");
                st = int.Parse(Console.ReadLine());
            }
        }
        
    }
}
