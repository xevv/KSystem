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
    
    public partial class SensorDryType
    {
        public SensorDryType()
        {
            this.SensorDry = new HashSet<SensorDry>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public int ValueType { get; set; }
    
        public virtual ICollection<SensorDry> SensorDry { get; set; }
        public virtual SensorDryValueType SensorDryValueType { get; set; }
    }
}