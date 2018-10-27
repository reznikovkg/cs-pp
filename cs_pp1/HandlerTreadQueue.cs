using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace cs_pp1
{
    class HandlerTreadQueue
    {

        public TreadQueue tQ;

        public int tQpush;
        public int isEnd;

        List<Thread> listTreadPush;
        WaitHandle[] waitHandlesPush;

        List<Thread> listTreadPop;
        WaitHandle[] waitHandlesPop;

        /*public bool isAliveAll()
        {
            bool t = false;
            for (int i = 0; i< tQpush; i ++)
            {
                if (listTreadPush[i].IsAlive)
                {
                    t = true;
                }
            }
            return t;
        }*/

        public void FunQueuePush(int count)
        {
            for (int i = 0; i < count; i++)
            {
                tQ.push((i+1).ToString());
            }
            isEnd++;
        }

        public void FunQueuePop()
        {
            while ((isEnd != tQpush))
            {
                while (tQ.pop()) { };
            }
            while (tQ.pop()) { };
        }
    
        public HandlerTreadQueue(int _tQpush, int tQpushCount, int tQpop)
        {
            tQ = new TreadQueue();
            tQpush = _tQpush;
            isEnd = 0;

            listTreadPush = new List<Thread>();
            waitHandlesPush = new WaitHandle[tQpush];

            listTreadPop = new List<Thread>();
            waitHandlesPop = new WaitHandle[tQpop];

            Console.WriteLine("Обработка началась");

            for (int i = 0; i < tQpush; i++)
            {
                var handle = new EventWaitHandle(false, EventResetMode.ManualReset);
                listTreadPush.Add(new Thread(() => { FunQueuePush(tQpushCount); handle.Set(); }));
                listTreadPush[i].Name = "ПотокPush " + (i + 1).ToString();
                waitHandlesPush[i] = handle;
                listTreadPush[i].Start();
            }

            for (int i = 0; i < tQpop; i++)
            {
                var handle = new EventWaitHandle(false, EventResetMode.ManualReset);
                listTreadPop.Add(new Thread(() => { FunQueuePop(); handle.Set(); }));
                listTreadPop[i].Name = "ПотокPop " + (i + 1).ToString();
                waitHandlesPop[i] = handle;
                listTreadPop[i].Start();
            }
            



            WaitHandle.WaitAll(waitHandlesPush);
            WaitHandle.WaitAll(waitHandlesPop);

            Console.WriteLine("Обработка завершена");
        }


    }
}
