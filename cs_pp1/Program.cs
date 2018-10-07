using System;
using System.Collections.Generic;

namespace cs_pp1
{
    class Program
    {
        static double Fu2 (double x)
        {
            return Math.Sin(x);
        }

        static void Main(string[] args)
        {
            Func<double, double> Fu = Fu2;
            List<myIntegral> listIn = new List<myIntegral>();

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

                myIntegral iinn = new myIntegral(Fu, st, h, a, b);
                Console.WriteLine(iinn.getResult());

                Console.WriteLine("Кол-во потоков (0 -> выход): ");
                st = int.Parse(Console.ReadLine());
            }
        }
    }
}
