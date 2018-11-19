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

        List<bool> ListDevices;
        /**
         * Потоки
         */
        List<Thread> ListThreads;
        WaitHandle[] waitHandles;
        
        /**
         * Объекты блокировок каждого прибора
         */
        List<object> blockDevices = new List<object>();
        /**
         * Конструктор
         */
        public PhilosophersLunch(int _count)
        {
            this.PhilosophersCount = _count;

            this.ListPhilosophers = new List<Philosopher>();
            this.ListThreads = new List<Thread>();

            this.ListDevices = new List<bool>();

            for (int i = 0; i < _count; i++)
            {
                this.ListPhilosophers.Add(new Philosopher());
                this.ListDevices.Add(false);
                this.blockDevices.Add(new object());
            }
        }

        /**
         * Запуск обеда и ожидание
         */
        public void StartLunch()
        {
            waitHandles = new WaitHandle[this.PhilosophersCount];

            int lastNumberPhilosoph = this.PhilosophersCount - 1;

            for (int i = 0; i < lastNumberPhilosoph; i++)
            {
                var handle = new EventWaitHandle(false, EventResetMode.ManualReset);

                ListThreads.Add(new Thread(() => { Lunch(); handle.Set(); }) );

                waitHandles[i] = handle;
                ListThreads[i].Name = i.ToString();
                ListThreads[i].Start();
            }

            /**
             * Запуск последнего потока с философом "левшой"
             */
            var handleLast = new EventWaitHandle(false, EventResetMode.ManualReset);
            ListThreads.Add(new Thread(() => { Lunch(true); handleLast.Set(); }));
            waitHandles[lastNumberPhilosoph] = handleLast;
            ListThreads[lastNumberPhilosoph].Name = lastNumberPhilosoph.ToString();
            ListThreads[lastNumberPhilosoph].Start();
            
            /**
             * Асинхронный поток для печати результатов
             */
            Thread printer = new Thread(() => { PrintStatistics(); });
            printer.Start();

            WaitHandle.WaitAll(waitHandles);
            Console.WriteLine("Пища кончилась");
        }

        /**
         * Функция потока
         */
        public void Lunch(bool left = false)
        {
            while (true)
            {
                int n = Convert.ToInt32(Thread.CurrentThread.Name);
                Philosopher t = this.ListPhilosophers[n];


                if (t.LeftDevice && t.RightDevice)
                {
                    /**
                     * Съесть
                     */
                    this.ListPhilosophers[n].AddPower(1);
                    this.ListPhilosophers[n]._short = true;

                    /**
                     * Положить приборы
                     */
                    this.ListPhilosophers[n].LeftDevice = false;
                    this.ListPhilosophers[n].RightDevice = false;
                    lock (this.blockDevices[n])
                    {
                        this.ListDevices[n] = false;
                    }

                    if (n == this.ListDevices.Count - 1)
                    {
                        lock (this.blockDevices[0])
                        {
                            this.ListDevices[0] = false;
                        }
                    }
                    else
                    {
                        lock (this.blockDevices[n + 1])
                        {
                            this.ListDevices[n + 1] = false;
                        }
                    }
                }
                else
                {
                    /**
                     * Взять сначала прибор слева 
                     * Если указан параметр для левши
                     */
                    if (!this.ListDevices[n])
                    {
                        if (left)
                        {
                            this.ListPhilosophers[n].LeftDevice = true;
                            lock (this.blockDevices[n])
                            {
                                this.ListDevices[n] = true;
                            }
                        }
                    }

                    /**
                     * Взять прибор справа
                     */
                    if (n == this.ListDevices.Count - 1)
                    {
                        if (!this.ListDevices[0])
                        {
                            this.ListPhilosophers[n].RightDevice = true;
                            lock (this.blockDevices[0])
                            {
                                this.ListDevices[0] = true;
                            }
                        }
                    }
                    else
                    {
                        if (!this.ListDevices[n + 1])
                        {
                            this.ListPhilosophers[n].RightDevice = true;
                            lock (this.blockDevices[n + 1])
                            {
                                this.ListDevices[n + 1] = true;
                            }
                        }
                    }

                    /**
                     * Взять прибор слева 
                     * Если не указан параметр для левши
                     */
                    if (!this.ListDevices[n])
                    {
                        if (!left)
                        {
                            this.ListPhilosophers[n].LeftDevice = true;
                            lock (this.blockDevices[n])
                            {
                                this.ListDevices[n] = true;
                            }
                        }
                    }

                    /**
                     * Если в руках только один прибор
                     * Ждать одну итерацию
                     */
                    if (this.ListPhilosophers[n].LeftDevice || this.ListPhilosophers[n].RightDevice)
                    {
                        this.ListPhilosophers[n].waitCount++;
                    }

                    /**
                     * Если ждет уже больше 5 итераций
                     * То положить приборы
                     */
                    if (this.ListPhilosophers[n].waitCount > 5)
                    {
                        this.ListPhilosophers[n].waitCount = 0;
                        if (this.ListPhilosophers[n].LeftDevice)
                        {
                            this.ListPhilosophers[n].LeftDevice = false;
                            lock (this.blockDevices[n])
                            {
                                this.ListDevices[n] = false;
                            }
                        }

                        if (this.ListPhilosophers[n].RightDevice)
                        {
                            this.ListPhilosophers[n].RightDevice = false;
                            if (n == this.ListDevices.Count - 1)
                            {
                                lock (this.blockDevices[0])
                                {
                                    this.ListDevices[0] = false;
                                }
                            }
                            else
                            {
                                lock (this.blockDevices[n + 1])
                                {
                                    this.ListDevices[n + 1] = false;
                                }
                            }
                        }
                    }
                }
                Thread.Sleep(100);
            }
        }

        
        
        
        void PrintStatistics()
        {
            while (true)
            {
                Console.Clear();
                for (int i = 0; i < ListPhilosophers.Count; i++)
                {
                    Console.WriteLine(ListPhilosophers[i].GetStatistics());
                }
                Thread.Sleep(100);
            }
        }
    }
}
