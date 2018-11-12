using System;
using System.Collections.Generic;
using System.Text;

using System.Threading;

namespace cs_pp1
{
    class PhilosophersLunch
    {
        List<Philosopher> ListPhilosophers;
        int PhilosophersCount;
        int PowerLeft;
        /**
         * Потоки
         */
        List<Thread> ListThreads;
        WaitHandle[] waitHandles;

        object block = new object();


        /**
         * Конструктор
         */ 
        public PhilosophersLunch(int _count, int _powerLeft)
        {
            this.PhilosophersCount = _count;
            this.PowerLeft = _powerLeft;
            this.ListPhilosophers = new List<Philosopher>();
            this.ListThreads = new List<Thread>();

            for (int i = 0; i < _count; i++)
            {
                this.ListPhilosophers.Add(new Philosopher());
            }
        }
        
        public void StartLunch()
        {
            waitHandles = new WaitHandle[this.PhilosophersCount];

            for (int i = 0; i < this.PhilosophersCount; i++)
            {
                var handle = new EventWaitHandle(false, EventResetMode.ManualReset);

                ListThreads.Add(new Thread(() => { Lunch(); handle.Set(); }) );

                waitHandles[i] = handle;
                ListThreads[i].Start();
            }

            WaitHandle.WaitAll(waitHandles);
        }

        public void Lunch()
        {
            lock (block)
            {


                
                this.PrintStatistics();
            }
            Thread.Sleep(1);
        }

        int subPowerLeft()
        {
            int t = 0;
            lock (block)
            {
                if (this.PowerLeft > 0)
                {
                    this.PowerLeft--;
                    t++;
                }
            }
            Thread.Sleep(1);
            return t;
        }

        string GetLunchStatistics()
        {
            return "Осталось: " + this.PowerLeft;
        }

        void PrintStatistics()
        {
            Console.Clear();
            Console.WriteLine(GetLunchStatistics());
            for (int i = 0; i < ListPhilosophers.Count; i++)
            {
                Console.WriteLine(ListPhilosophers[i].GetStatistics());
            }
        }
    }
}
