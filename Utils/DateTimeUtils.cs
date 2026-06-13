using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloStickyNotes.Utils
{
    public class DateTimeUtils
    {

        public static string GetRemainingTime(int remainingTime)
        {
            int days = remainingTime / (24 * 60 * 60);
            int hours = (remainingTime % (24 * 60 * 60)) / (60 * 60);
            int minutes = (remainingTime % (60 * 60)) / 60;
            int seconds = remainingTime % 60;
            string secondsText = seconds < 10 ? "0" + seconds : seconds.ToString();
            string minutesText = minutes < 10 ? "0" + minutes : minutes.ToString();
            string hoursText = hours < 10 ? "0" + hours : hours.ToString();

            string remainingText;//minutes + ":" + secondsText;

            if (days > 0)
            {
                // 天数时分秒的形式
                remainingText = days.ToString() + "天" + hoursText + "时" + minutesText + "分" + secondsText + "秒";
            }
            else if (hours > 0)
            {
                remainingText = hoursText + "小时" + minutesText + "分" + secondsText + "秒";
            }
            else if (minutes > 0)
            {
                // 仅分钟与秒的形式
                remainingText = minutesText + "分钟" + secondsText + "秒";
            }
            else
            {
                remainingText = secondsText + "秒";
            }
            return remainingText;
        }

    }
}
