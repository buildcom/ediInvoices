using System;
using System.Collections.Generic;
using System.Linq;

namespace LDLibraries.dto
{
	public class EDI_Document_ISA_dto
	{
		public int EDI_Document_ISA_ID { get; set; }
        public string EDI_Document_ISA_ID_str9Pad { get; set; }
        public string EDI_Document_ISA_ID_str6Pad { get; set; }
        public string EDI_Document_ISA_Type { get; set; }
		public DateTime EDI_Document_ISA_Date { get; set; }
        public int EDI_TransactionControl_Start { get; set; }
        public int EDI_TransactionControl_End { get; set; }
    }
}


