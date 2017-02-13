using System;
using System.Collections.Generic;

namespace EdiInvoicing.Models
{
    public class Edi_Document_dto
    {
        public List<string> ediList = new List<string>();
        public List<string> ediTransactionList = new List<string>();

        public EDI_Document_ISA_dto ediDocISA;

        public string filePath = "";
        public string ediFileSpec = "";
        public string ediElement = "";
        public string elementTerm = "*";
        public string segmentTerm = "~";
        public string compositTerm = ">";
        //FileStream, MemoryStream, StringBuilder

        public DateTime dateShortStr;
        public DateTime dateLongStr;
        public DateTime timeStr;

        public string interchangeSenderID;
        public string interchangeReceiverID;
    }
}
