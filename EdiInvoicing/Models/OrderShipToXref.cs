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
    
    public partial class OrderShipToXref
    {
        public decimal CHG_LOG_ID { get; set; }
        public Nullable<decimal> SRC_SYS_CHG_LOG_ID { get; set; }
        public Nullable<System.DateTime> SRC_SYS_TRANS_DT { get; set; }
        public string SRC_SYS_PROC_NM { get; set; }
        public string SRC_SYS_SESSION_NM { get; set; }
        public string SRC_SYS_USER_NM { get; set; }
        public string SRC_SYS_GRP_NM { get; set; }
        public string SRC_SYS_ACCT_NM { get; set; }
        public Nullable<decimal> SRC_SYS_SESSION_ID { get; set; }
        public string SRC_CD { get; set; }
        public string ACTION_CD { get; set; }
        public string CSI_TYPE_CD { get; set; }
        public string FULL_ORDER_NO { get; set; }
        public string XREF_NO { get; set; }
        public string SEARCH_TYPE { get; set; }
        public string XREF_DATA { get; set; }
        public decimal SLOTID { get; set; }
        public string SALES_ORD_NR { get; set; }
        public string SALES_ORD_SHIP_TO_NR { get; set; }
        public decimal CUST_EDP_ID { get; set; }
    }
}
