using KModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KSystem.Model
{
    public class HouseViewModel
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public int LabelForValue { get; set; }

        public HouseViewModel() { }
        public HouseViewModel(House model, int labelForValue)
        {
            Id = model.Id;
            Street = model.Street;
            Number = model.Number;
            LabelForValue = labelForValue;
        }
    }
}