namespace EdiInvoicing.Models
{
    public class EDI_Transaction_Log_dto
	{
        public int EDI_Transaction_Log_ID { get; set; }
        public int ShipTime_ID { get; set; }
        public int EDI_Document_ISA_ID { get; set; }
        public int EDI_TransactionControl { get; set; }
        public int CreateDate { get; set; }
    }
}


