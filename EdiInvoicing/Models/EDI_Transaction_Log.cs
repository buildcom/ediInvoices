//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EDI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EDI_Transaction_Log
    {
        public int EDI_Transaction_Log_ID { get; set; }
        public int ShipTime_ID { get; set; }
        public int EDI_Document_ISA_ID { get; set; }
        public int EDI_TransactionControl { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
    }
}
