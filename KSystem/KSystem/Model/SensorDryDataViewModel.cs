using KModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KSystem.Model
{
    public class SummaryLastDataViewModel
    {
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public string StringValue { get; set; }
        public int Value { get; set; }
    }
}