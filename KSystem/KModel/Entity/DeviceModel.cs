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
    
    public partial class DeviceModel
    {
        public DeviceModel()
        {
            this.Device = new HashSet<Device>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Device> Device { get; set; }
    }
}
