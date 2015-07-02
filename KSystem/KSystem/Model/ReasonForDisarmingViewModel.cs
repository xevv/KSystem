using KModel.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KSystem.Model
{
    public class ReasonForDisarmingViewModel
    {
        public int HouseId { get; set; }
        public string Door { get; set; }
        public int SensorDryId { get; set; }
        public string UserId { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено!")]
        [Display(Name = "Причина")]
        public string Reason { get; set; }

        public ReasonForDisarmingViewModel() { }
    }
}