//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AviaSite
{
    using System;
    using System.Collections.Generic;
    
    public partial class Flight
    {
        public int flight1 { get; set; }
        public System.DateTime Date { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int Plane { get; set; }
    
        public virtual Plane Plane1 { get; set; }
    }
}