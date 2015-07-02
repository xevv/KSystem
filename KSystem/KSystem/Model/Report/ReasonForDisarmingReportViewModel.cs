using KSystem.Model.Support;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KSystem.Model.Report
{
    public class ReasonForDisarmingReportResult
    {
        [Display(Name = "Пользователь")]
        public string User { get; set; }
        //public string House { get; set; }
        [Display(Name = "Объект")]
        public string Object { get; set; }
        //public string Premises { get; set; }
        //public string Door { get; set; }
        [Display(Name = "Датчик")]
        public string SensorType { get; set; }
        [Display(Name = "Причина снятия с охраны")]
        public string Reason { get; set; }
        [Display(Name = "Время")]
        public string Date { get; set; }
    }

    public class ReasonForDisarmingReportViewModel
    {
        public List<ReasonForDisarmingReportResult> Report { get; set; }
        public DateTime CreateDate { get; set; }
        public PageInfo PageInfo { get; set; }

        public ReasonForDisarmingReportViewModel()
        {
            CreateDate = DateTime.Now;
            Report = new List<ReasonForDisarmingReportResult>();
            PageInfo = new PageInfo();
        }

        public ReasonForDisarmingReportViewModel(ReasonForDisarmingReportViewModel model)
        {
            CreateDate = model.CreateDate;
            Report = model.Report;
            PageInfo = model.PageInfo;
        }
    }
}