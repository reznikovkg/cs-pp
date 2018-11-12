using System;
using System.Collections.Generic;
using System.Text;

namespace cs_pp1
{
    class Philosopher
    {
        string Name;

        int Power;


        public Philosopher()
        {

        }

        /**
         * Получить имя
         */
        public string GetName()
        {
            return Name;
        }

        /**
         * Добавить энергию
         */
        public void AddPower(int p)
        {
            this.Power += p;
        }

        /**
         * Получить энергию
         */
        public int GetPower()
        {
            return Power;
        }

        /**
         * Получить статистику
         */
        public string GetStatistics()
        {
            return (this.GetName() + ": " + this.GetPower().ToString());
        }

    }
}
