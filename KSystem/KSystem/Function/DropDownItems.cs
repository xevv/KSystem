using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KSystem.Function
{
    public class DropDownItems
    {
        public static ICollection<string> HoursList()
        {
            List<string> hours = new List<string>();
            for (var i = 0; i < 24; i++)
            {
                hours.Add(i.ToString().PadLeft(2, '0'));
            }
            return hours;
        }

        public static ICollection<string> MinutesList()
        {
            List<string> minutes = new List<string>();
            for (var i = 0; i < 60; i = i + 5)
            {
                minutes.Add(i.ToString().PadLeft(2, '0'));
            }
            return minutes;
        }
    }
}