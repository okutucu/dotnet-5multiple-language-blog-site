using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Shared.Utilities.Extensions
{
    public static class DateTimeExtensions
    {
        public static string FullDateAndTimeStringWithUnderscore(this DateTime dateTime)
        {

            return $"{dateTime.Millisecond}_{dateTime.Second}_{dateTime.Minute}_{dateTime.Hour}.{dateTime.Day}_{dateTime.Month}_{dateTime.Year}";

            /* 
                 OğuzhanKutucu_587_5_38_12_3_10_2020.png
                 KaanKutucu_601_5_38_12_3_10_2022.png
             */
        }
    }
}
