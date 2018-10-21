using System;
using System.Collections.Generic;
using System.Text;



namespace cs_pp1
{
    class myIntegral
    {
        private Func<double, double> func;

        private double th;

        private double h;

        private List<myThread> listTh;

        private int isEnd;
    
        public myIntegral(Func<double, double> _func, double _th, double _h, double _a, double _b)
        {
            this.func = _func;
            this.th = _th;
            this.h = _h;
            listTh = new List<myThread>();
            isEnd = 0;

            double step = (_b - _a) / _th;

            for (int i = 0; i < this.th; i++)
            {
                double step2 = _a + i * step;
                if (i+1 != this.th)
                {
                    listTh.Add(new myThread("Thread", i, func, step2, step2 + step, h));
                } else
                {
                    listTh.Add(new myThread("Thread", i, func, step2, _b, h));
                }
            }
        }

        public string getResult()
        {
            double res = 0;
            TimeSpan maxTime = listTh[0].time;

            while (isEnd != th)
            {
                isEnd = 0;
                for (int i = 0; i < this.th; i++)
                {
                    if (listTh[i].isEnd)
                    {
                        isEnd += 1;
                    }
                }
            }

            for (int i = 0; i < this.th; i++)
            {
                res += listTh[i].result;
                if (listTh[i].time > maxTime) maxTime = listTh[i].time;
            }


            Console.WriteLine("=======================================");
            return "Количество потоков: "+th+"\n" +
                "Ответ: " + res + ", время выполнения:" + maxTime + "\n\n";
        }
    }

}
