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
    
    public partial class HouseUser
    {
        public int Id { get; set; }
        public string UserProfile { get; set; }
        public int House { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual House House1 { get; set; }
    }
}
