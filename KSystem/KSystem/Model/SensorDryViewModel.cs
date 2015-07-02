using KModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KSystem.Model
{
    public class SensorDryViewModel
    {
        public int Id { get; set; }
        public string Door { get; set; }
        public int Status { get; set; }
        public string DisplayValue { get; set; }
        public int Value { get; set; }
        public int Device { get; set; }
        public int Online { get; set; }
        public DateTime Time { get; set; }
    }

    public class SensorDryRoomViewModel
    {
        public string Room { get; set; }
        public List<SensorDryViewModel> Sensor { get; set; }

        public SensorDryRoomViewModel()
        {
            Sensor = new List<SensorDryViewModel>();
        }

        public SensorDryRoomViewModel(string room, SensorDryViewModel model)
            : this()
        {
            Room = room;
            Sensor.Add(model);
        }
    }

}