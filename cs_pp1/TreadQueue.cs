using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace cs_pp1
{
    class TreadQueue
    {
        private Queue<String> list;

        private object block = new object();

        public TreadQueue()
        {
            list = new Queue<String>();
        }

        public void push(String _e)
        {
            lock (block)
            {
                list.Enqueue(_e);
                Console.WriteLine(Thread.CurrentThread.Name + ", добавил " + _e);
            }
            Thread.Sleep(1);
        }

        public bool pop()
        {
            String first = null;
            lock (block)
            {
                if (list.Count > 0)
                {
                    first = list.Dequeue();
                    Console.WriteLine(Thread.CurrentThread.Name + ", удалил " + first);
                }
            }
            Thread.Sleep(1);
            if (first != null) {
                return true;
            } else {
                return false;
            }
        }

    }
}
