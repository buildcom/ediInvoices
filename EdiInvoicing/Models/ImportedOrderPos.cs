//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EdiInvoicing.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ImportedOrderPos
    {
        public int OrderPoId { get; set; }
        public string EcomOrderNo { get; set; }
        public string OrderPoNo { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string ExternalOrderNo { get; set; }
    }
}
