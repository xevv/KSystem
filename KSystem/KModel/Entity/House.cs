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
    
    public partial class House
    {
        public House()
        {
            this.DeviceHouse = new HashSet<DeviceHouse>();
            this.HouseUser = new HashSet<HouseUser>();
        }
    
        public int Id { get; set; }
        public int City { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string Housing { get; set; }
        public string Description { get; set; }
        public int Org { get; set; }
        public int Region { get; set; }
    
        public virtual City City1 { get; set; }
        public virtual ICollection<DeviceHouse> DeviceHouse { get; set; }
        public virtual Org Org1 { get; set; }
        public virtual Region Region1 { get; set; }
        public virtual ICollection<HouseUser> HouseUser { get; set; }
    }
}
