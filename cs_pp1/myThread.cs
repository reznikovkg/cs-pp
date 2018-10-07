using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace cs_pp1
{
    class myThread
    {
        Thread thread;

        private Func<double, double> FuncUser;
        private double a;
        private double b;
        private double h;

        public bool isEnd;

        public TimeSpan time;

        public double result = 0;

        public myThread(string name, int num,Func<double,double> _func, double _a, double _b, double _h)
        {
            FuncUser = _func;
            a = _a;
            b = _b;
            h = _h;
            isEnd = false;

            thread = new Thread(Func);
            thread.Name = name + num;
            thread.Start(num);
        }

        void Func(object num = null)
        {
            DateTime time1 = DateTime.Now;
            for (double i = a; i < b; i+=h)
            {
                result = result + FuncUser(i)*h;
                Thread.Sleep(0);
            }
            DateTime time2 = DateTime.Now;
            time = time2 - time1;
            Console.WriteLine("Поток " + thread.Name + " завершен, время операции: " + time);
            isEnd = thread.IsAlive;
        }

    }
}
