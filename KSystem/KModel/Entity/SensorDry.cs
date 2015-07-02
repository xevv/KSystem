//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KModel.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class SensorDry
    {
        public SensorDry()
        {
            this.ReasonForDisarming = new HashSet<ReasonForDisarming>();
            this.SensorDryData = new HashSet<SensorDryData>();
        }
    
        public int Id { get; set; }
        public int Type { get; set; }
        public int Premises { get; set; }
        public int Door { get; set; }
        public int Device { get; set; }
        public int Port { get; set; }
        public int Status { get; set; }
    
        public virtual Device Device1 { get; set; }
        public virtual Door Door1 { get; set; }
        public virtual Premises Premises1 { get; set; }
        public virtual ICollection<ReasonForDisarming> ReasonForDisarming { get; set; }
        public virtual SensorDryType SensorDryType { get; set; }
        public virtual ICollection<SensorDryData> SensorDryData { get; set; }
    }
}
