using KSystem.CustomValidation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KSystem.Model
{
    public class DefaultReportModel
    {
        public DateModel Date { get; set; }
        public List<HouseViewModel> Houses { get; set; }

        public DefaultReportModel()
        {
            Date = new DateModel();
            Houses = new List<HouseViewModel>();
        }
    }
}