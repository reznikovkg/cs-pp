using System;
using System.Collections.Generic;
using System.Text;

namespace cs_pp1
{
    class Philosopher
    {
        string Name;

        int Power;

        public bool LeftDevice;

        public bool RightDevice;

        public bool _short = false;
     
        public string GetShort()
        {
            if (this._short)
            {
                this._short = false;
                return " съел";
            }
            else
            {
                return "";
            }
        }


        public int waitCount = 0;
    

        public Philosopher()
        {
            this.Name = "";
            this.Power = 0;

            this.LeftDevice = false;
            this.RightDevice = false;
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
            return (this.GetName() + " " + this.GetStatLeftDev() + "|" + this.GetStatRightDev() + " : " + this.GetPower().ToString() + this.GetShort());
        }

        public string GetStatLeftDev()
        {
            if (this.LeftDevice)
            {
                return "+";
            } else
            {
                return " ";
            }
        }

        public string GetStatRightDev()
        {
            if (this.RightDevice)
            {
                return "+";
            }
            else
            {
                return " ";
            }
        }
    }
}
