using System;
using System.Collections.Generic;
using System.Text;

namespace cs_pp1
{
    class IntegralResult
    {
        public double value;

        static object locker = new object();

        IntegralResult()
        {
            this.value = 0;
        }

        public void add(double x)
        {
            lock (locker) {
                this.value += x;
            }
        }
    }
}
