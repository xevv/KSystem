using KSystem.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace KSystem.CustomValidation
{
    public class MobilePhone : ValidationAttribute
    {
        Regex reg = new Regex(@"\+7[0-9]{10}$");
        public override bool IsValid(object value)
        {
            if (value != null)
                return reg.IsMatch((string)value);
            else
                return false;
        }
    }

    public class DateCompare : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContex)
        {
            DateModel model = value as DateModel;
            if (value != null && (model.EndDateSummary != default(DateTime) || model.EndDateSummary != default(DateTime)))
            {
                if (model.StartDateSummary >= model.EndDateSummary)
                    return new ValidationResult(ErrorMessage);
                else
                    return ValidationResult.Success;
            }
            return new ValidationResult(ErrorMessage = "Неверный формат данных");
        }
    }

    public class InputDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                try
                {
                    DateTime StartDateTime = DateTime.ParseExact(value.ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
    }

    public class Time : ValidationAttribute
    {
        Regex reg = new Regex(@"^([0-1]\d|2[0-3])(:[0-5]\d)$");
        protected override ValidationResult IsValid(object value, ValidationContext validationContex)
        {
            try
            {
                if (reg.IsMatch(value.ToString()))
                    return ValidationResult.Success;
            }
            catch (NullReferenceException)
            {
                return new ValidationResult("Поле не заполнено!");
            }
            return new ValidationResult(ErrorMessage);
        }
    }
}