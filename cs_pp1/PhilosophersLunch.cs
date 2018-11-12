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

        List<bool> ListDevices;
        /**
         * Потоки
         */
        List<Thread> ListThreads;
        WaitHandle[] waitHandles;

        object blockStat = new object();
        object block = new object();
        object blockCheck = new object();

        /**
         * Конструктор
         */
        public PhilosophersLunch(int _count, int _powerLeft)
        {
            this.PhilosophersCount = _count;
            this.PowerLeft = _powerLeft;
            this.ListPhilosophers = new List<Philosopher>();
            this.ListThreads = new List<Thread>();

            this.ListDevices = new List<bool>();

            for (int i = 0; i < _count; i++)
            {
                this.ListPhilosophers.Add(new Philosopher());
                this.ListDevices.Add(false);
            }
        }

        /**
         * Запуск обеда и ожидание
         */
        public void StartLunch()
        {
            waitHandles = new WaitHandle[this.PhilosophersCount];

            for (int i = 0; i < this.PhilosophersCount; i++)
            {
                var handle = new EventWaitHandle(false, EventResetMode.ManualReset);

                ListThreads.Add(new Thread(() => { Lunch(); handle.Set(); }) );

                waitHandles[i] = handle;
                ListThreads[i].Name = i.ToString();
                ListThreads[i].Start();
            }

            WaitHandle.WaitAll(waitHandles);
            Console.WriteLine("Пища кончилась");
        }

        /**
         * Функция потока
         */
        public void Lunch()
        {
            while (this.CheckPower())
            {
                int n = Convert.ToInt32(Thread.CurrentThread.Name);
                Philosopher t = this.ListPhilosophers[n];

                if (t.LeftDevice && t.RightDevice)
                {
                    //съесть
                    this.ListPhilosophers[n].AddPower(
                        this.SubPowerLeft()
                    );
                    this.ListPhilosophers[n]._short = true;

                    //положить приборы
                    this.ListPhilosophers[n].LeftDevice = false;
                    this.ListPhilosophers[n].RightDevice = false;
                    this.ListDevices[n] = false;
                    if (n == this.ListDevices.Count - 1)
                    {
                        this.ListDevices[0] = false;
                    } else
                    {
                        this.ListDevices[n+1] = false;
                    }
                } else
                {
                    if (n == this.ListDevices.Count - 1)
                    {
                        if (!this.ListDevices[0])
                        {
                            this.ListPhilosophers[n].RightDevice = true;
                            this.ListDevices[0] = true;
                        }
                    } else
                    {
                        if (!this.ListDevices[n+1])
                        {
                            this.ListPhilosophers[n].RightDevice = true;
                            this.ListDevices[n+1] = true;
                        }
                    }

                    if (!this.ListDevices[n])
                    {
                        this.ListPhilosophers[n].LeftDevice = true;
                        this.ListDevices[n] = true;
                    }

                    if (this.ListPhilosophers[n].LeftDevice || this.ListPhilosophers[n].RightDevice)
                    {
                        this.ListPhilosophers[n].waitCount++;
                    }

                    if (this.ListPhilosophers[n].waitCount > 5)
                    {
                        this.ListPhilosophers[n].waitCount = 0;
                        if (this.ListPhilosophers[n].LeftDevice)
                        {
                            this.ListPhilosophers[n].LeftDevice = false;
                            this.ListDevices[n] = false;
                        }

                        if (this.ListPhilosophers[n].RightDevice)
                        {
                            this.ListPhilosophers[n].RightDevice = false;
                            if (n == this.ListDevices.Count - 1)
                            {
                                this.ListDevices[0] = false;
                            }
                            else
                            {
                                this.ListDevices[n + 1] = false;
                            }
                        }
                    }

                }



                lock (blockStat)
                {
                    this.PrintStatistics();
                    Thread.Sleep(100);
                }
            }
        }

        bool CheckPower()
        {
            bool t = false;
            lock (blockCheck)
            {
                if (this.PowerLeft > 0)
                {
                    t = true;
                }
            }
            return t;
        }


        int SubPowerLeft()
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
