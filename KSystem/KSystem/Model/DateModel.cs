using KSystem.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace KSystem.Model
{
    [DateCompare(ErrorMessage = "Конечная дата должна быть меньще начальной")]
    public class DateModel
    {
        [InputDate(ErrorMessage = "Дата неверного формата")]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public string StartDate { get; set; }
        public string StartDateHourses { get; set; }
        public string StartDateMinutes { get; set; }

        [InputDate(ErrorMessage = "Дата неверного формата")]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public string EndDate { get; set; }
        public string EndDateHourses { get; set; }
        public string EndDateMinutes { get; set; }

        public DateTime StartDateSummary
        {
            get
            {
                try
                {
                    return DateTime.ParseExact(
                    new StringBuilder().Append(StartDate).Append(" ").Append(StartDateHourses).Append(":").Append(StartDateMinutes).ToString(),
                    "dd.MM.yyyy HH:mm",
                    CultureInfo.InvariantCulture);
                }
                catch
                {
                    return default(DateTime);
                }
            }

        }
        public DateTime EndDateSummary
        {
            get
            {
                try
                {
                    return DateTime.ParseExact(
                    new StringBuilder().Append(EndDate).Append(" ").Append(EndDateHourses).Append(":").Append(EndDateMinutes).ToString(),
                    "dd.MM.yyyy HH:mm",
                    CultureInfo.InvariantCulture);
                }
                catch
                {
                    return default(DateTime);
                }
            }
        }

        public DateModel()
        {
            StartDate = DateTime.Today.ToString("dd.MM.yyyy");
            StartDateHourses = "00"; ;
            StartDateMinutes = "00";
            EndDate = DateTime.Today.ToString("dd.MM.yyyy");
            EndDateHourses = "00";
            EndDateMinutes = "00";
        }
    }
}