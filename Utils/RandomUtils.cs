using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloStickyNotes.Utils
{
    internal class RandomUtils
    {

        /*public static void hashRemake()
        {
            int a = 497707880;
            int b = 1168645;
            
            Random random = new Random();

        }*/

        public static string RandomLABCode4(DateTime date)
        {
            /*int seed = int.Parse(seedString);
            seed = (int)((seed * 49.5F + 1521.45F) / 278.67F);
            seed *= seed;
            
            seed += 8214;
            String pwd = Math.Abs(seed).ToString();
            //new Calendar().
            if (pwd.Length < 6)
            {
                pwd += "052372";
            }
            String ori = pwd;
            pwd = pwd.Substring(2, 4*//*此处为截取长度*//*) + " "+ ori;*/
            int dayOfMonth = date.Day;
            int dayOfYear = date.DayOfYear;
            int value = dayOfYear + 365;
            value = (value * value - 67 * dayOfMonth) * (date.Year - 1999) + 64921957 / dayOfYear;
            string pwd = value.ToString();
            if (pwd.Length < 7)
            {
                pwd += "6152372";
            }
            pwd = /*pwd + " "+ */pwd.Substring(dayOfYear% 3 + 1, 4);
            return pwd;
        }

        public static string RandomLABCode4(string seedString)
        {
            DateTime date = DateTime.Parse(seedString);
            return RandomLABCode4(date);
        }

    }
}
