using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;



namespace cs_pp1
{
    class MyIntegral
    {
        private Func<double, double> func;

        private int th;

        private double h;

        private List<Thread> listTh;

        private double res;

        static WaitHandle[] waitHandles;

    public MyIntegral(Func<double, double> _func, int _th, double _h, double _a, double _b)
        {
            func = _func;
            h = _h;
            th = _th;
            waitHandles = new WaitHandle[th];

            listTh = new List<Thread>();
            
            double step = (_b - _a) / th;

            for (int i = 0; i < th; i++)
            {
                var handle = new EventWaitHandle(false, EventResetMode.ManualReset);

                double step2 = _a + i * step;
                if (i+1 != th)
                {
                    listTh.Add(new Thread(() => { Func(step2, step2 + step, h); handle.Set(); }));
                } else
                {
                    listTh.Add(new Thread(() => { Func(step2, _b, h); handle.Set(); }));
                }
                waitHandles[i] = handle;
                listTh[i].Start();
            }

            WaitHandle.WaitAll(waitHandles);
            this.PrintResult();
        }

        void Func(double a, double b, double h)
        {
            for (double i = a; i < b; i += h)
            {
                lock (new object())
                {
                    res = res + func(i) * h;
                }
                Thread.Sleep(10);
            }
        }
        
        public void PrintResult()
        {
            Console.WriteLine("=======================================");
            Console.WriteLine("Количество потоков: " + th);
            Console.WriteLine("Ответ: " + res);
        }
    }

}
