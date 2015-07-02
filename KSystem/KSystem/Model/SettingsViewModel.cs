using KModel.Entity;
using KSystem.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KSystem.Model
{
    public class SettingsViewModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }

        [Display(Name = "Имя")]
        public string FIO { get; set; }

        [Display(Name = "Организация")]
        public string Org { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [Display(Name = "EMail")]
        [EmailAddress(ErrorMessage = "Неверный Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [Display(Name = "Телефон")]
        [MobilePhone(ErrorMessage = "Неверный формат номера")]
        public string Phone { get; set; }

        [Display(Name = "Отправлять сообщения на Email")]
        public int NotificationEmail { get; set; }

        [Display(Name = "Отправлять сообщения на телефон")]
        public int NotificationPhone { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [Display(Name = "Отправлять с")]
        [Time(ErrorMessage = "Неверный формат времени")]
        public string CallTimeStart { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [Display(Name = "Отправлять до")]
        [Time(ErrorMessage = "Неверный формат времени")]
        public string CallTimeEnd { get; set; }

        public SettingsViewModel() { }
        public SettingsViewModel(AspNetUsers model)
        {
            UserId = model.Id;
            FIO = model.FIO;
            Email = model.Email;
            NotificationEmail = model.NotificationEmail;
            NotificationPhone = model.NotificationPhone;
            CallTimeStart = model.CallTimeStart.ToString().Substring(0, 5);
            CallTimeEnd = model.CallTimeEnd.ToString().Substring(0, 5);
            Phone = model.PhoneNumber;
            Org = model.Org1.Name;
        }
    }
}