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
    using System.ComponentModel.DataAnnotations;

    public partial class Airport
    {
        public int AirportID { get; set; }
        [Display(Name ="Airport")]
        public string AirportName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
    }
}
