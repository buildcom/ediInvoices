using System;
using System.Configuration;
using System.Data.Entity;

using Edidev.FrameworkEDI;

using EdiInvoicing.Helper;

namespace EDI.Code.EdiWriters
{
    /// <summary> An edi writer 810 build. </summary>
    public class EdiWriter810Build : EdiWriterBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EdiWriter810Build"/> class.  Constructor.
        /// </summary>
        /// <param name="markShipConfirm">
        /// true to mark ship confirm. 
        /// </param>
        /// <param name="supplierSettings">
        /// The supplier settings. 
        /// </param>
        public EdiWriter810Build(
            bool markShipConfirm, 
            SupplierSettings supplierSettings)
            : this(markShipConfirm, supplierSettings, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EdiWriter810Build"/> class.
        /// </summary>
        /// <param name="markShipConfirm">
        /// The mark ship confirm.
        /// </param>
        /// <param name="supplierSettings">
        /// The supplier settings.
        /// </param>
        /// <param name="ecomedateDbContext">
        /// The ecomedate db context.
        /// </param>
        public EdiWriter810Build(
            bool markShipConfirm,
            SupplierSettings supplierSettings,
            DbContext ecomedateDbContext) 
            : base(markShipConfirm, supplierSettings, ecomedateDbContext)
        {
        }

        /// <summary> Runs this object. </summary>
        /// <returns> true if it succeeds, false if it fails. </returns>
        public override bool ProcessEdi()
        {
            var result = false;
            ediDocument ediDocument;
            ediSchema ediSchema;
            ediSchemas ediSchemas;
            ediInterchange ediInterchange;
            ediGroup ediGroup;
            ediTransactionSet ediTransactionSet;
            ediDataSegment ediDataSegment;
            var ediFileName = string.Empty;
            var sefFileName = string.Empty;

            ediDocument = new ediDocument();

            ediSchemas = ediDocument.GetSchemas();
            ediSchemas.EnableStandardReference = false;

            ediDocument.CursorType = DocumentCursorTypeConstants.Cursor_ForwardWrite;

            ediDocument.SegmentTerminator = "~{13:10}";
            ediDocument.ElementTerminator = "*";
            ediDocument.CompositeTerminator = "!";

            ediFileName = string.Format(@"{0}\{1}_810_{2:yyyyMMddHHmmss}.X12", Settings.GetOutputFilePath(), this.Settings.DatabaseSettings.SourceName, DateTime.Now);
            sefFileName = @"Code\sefs\810Build.sef";
            try
            {
                ediSchema = ediDocument.LoadSchema(sefFileName, SchemaTypeIDConstants.Schema_Standard_Exchange_Format);

                ediInterchange = ediDocument.CreateInterchange("X", "004010");
                ediDataSegment = ediInterchange.GetDataSegmentHeader();
                ediDataSegment.set_DataElementValue(1, 0, "00");
                ediDataSegment.set_DataElementValue(2, 0, "          ");
                ediDataSegment.set_DataElementValue(3, 0, "00");
                ediDataSegment.set_DataElementValue(4, 0, "          ");
                ediDataSegment.set_DataElementValue(5, 0, "12");
                ediDataSegment.set_DataElementValue(6, 0, "SENDER ID      "); // TODO get from settings (phone number "5124677170     ")
                ediDataSegment.set_DataElementValue(7, 0, "12");
                ediDataSegment.set_DataElementValue(8, 0, "RECEIVER ID    "); // TODO get from supplier setting column
                ediDataSegment.set_DataElementValue(9, 0, "010101"); // TODO today's date YYmmDD
                ediDataSegment.set_DataElementValue(10, 0, "0101"); // TODO time HHMM
                ediDataSegment.set_DataElementValue(11, 0, "U");
                ediDataSegment.set_DataElementValue(12, 0, "00401");
                ediDataSegment.set_DataElementValue(13, 0, "000000001"); // TODO unique generated ID for this transaction
                ediDataSegment.set_DataElementValue(14, 0, "0");
                ediDataSegment.set_DataElementValue(15, 0, "T"); // T for Test data P for production data
                ediDataSegment.set_DataElementValue(16, 0, ">");

                ediGroup = ediInterchange.CreateGroup("004010");
                ediDataSegment.Set(ref ediDataSegment, ediGroup.GetDataSegmentHeader());
                ediDataSegment.set_DataElementValue(1, 0, "IN");
                ediDataSegment.set_DataElementValue(2, 0, "APP SENDER"); // TODO get from settings (phone number "5124677170     ")
                ediDataSegment.set_DataElementValue(3, 0, "APP RECEIVER"); // TODO get from supplier setting column
                ediDataSegment.set_DataElementValue(4, 0, "01010101"); // TODO date in form of CCYYMMDD
                ediDataSegment.set_DataElementValue(5, 0, "01010101"); // TODO time in form of HHMMSSDD
                ediDataSegment.set_DataElementValue(6, 0, "1"); // TODO EDI_Document_ISA_ID for this document/transaction
                ediDataSegment.set_DataElementValue(7, 0, "X");
                ediDataSegment.set_DataElementValue(8, 0, "004010");

                ediTransactionSet = ediGroup.CreateTransactionSet("810");
                ediDataSegment.Set(ref ediDataSegment, ediTransactionSet.GetDataSegmentHeader());
                ediDataSegment.set_DataElementValue(1, 0, "810");
                ediDataSegment.set_DataElementValue(2, 0, "0001"); // TODO EDI_TransactionControl ID from 

                this.Process810Area1(ediTransactionSet);
                
                this.Process810Area2(ediTransactionSet);

                this.Process810Area3(ediTransactionSet);

                ediDocument.Save(ediFileName, 0);

                result = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return result;
        }

        /// <summary> Process the 810 area 1 described by ediTransactionSet. </summary>
        /// <param name="ediTransactionSet"> Set the edi transaction belongs to. </param>
        private void Process810Area1(ediTransactionSet ediTransactionSet)
        {
            ediDataSegment dataSegment;
            long segmentLoop = 0;

            dataSegment = ediTransactionSet.CreateDataSegment("BIG");
            dataSegment.set_DataElementValue(1, 0, "20170209"); // TODO: Order date in YYYYMMDD
            dataSegment.set_DataElementValue(2, 0, "B0000001"); // TODO: Ecometry Order Number
            dataSegment.set_DataElementValue(3, 0, "20170209"); // TODO: Order date in YYYYMMDD
            dataSegment.set_DataElementValue(4, 0, "871082007"); // TODO: Vendor PO Number

            ediDataSegment.Set(ref dataSegment, ediTransactionSet.CreateDataSegment("REF"));
            dataSegment.set_DataElementValue(1, 0, "VN");
            dataSegment.set_DataElementValue(2, 0, "Living Direct");

            for (segmentLoop = 1; segmentLoop <= 2; segmentLoop++)
            {
                // TODO: Make an address object and allow sending in of ST for shipto and SF for ship from
                this.Process810Area1LoopN1(ediTransactionSet, segmentLoop);
            }

            ediDataSegment.Set(ref dataSegment, ediTransactionSet.CreateDataSegment("DTM"));
            dataSegment.set_DataElementValue(1, 0, "011");
            dataSegment.set_DataElementValue(2, 0, "01010101"); // TODO: Ship Date in form of CCYYMMDD
        }

        /// <summary> Process the 810 area 1 loop n 1. </summary>
        /// <param name="ediTransactionSet"> Set the edi transaction belongs to. </param>
        /// <param name="loopN1Instance">    The loop n 1 instance. </param>
        private void Process810Area1LoopN1(ediTransactionSet ediTransactionSet, long loopN1Instance)
        {
            ediDataSegment dataSegment;
            var hierarchyString = string.Empty;

            hierarchyString = "N1(";
            hierarchyString += Convert.ToString(loopN1Instance);
            hierarchyString += ")\\";
            hierarchyString += "N1";

            dataSegment = ediTransactionSet.CreateDataSegment(hierarchyString);
            dataSegment.set_DataElementValue(1, 0, "ST"); // TODO: ST for ship to SF for ship from
            dataSegment.set_DataElementValue(2, 0, "CompanyName"); // TODO: Company

            hierarchyString = "N1(";
            hierarchyString += Convert.ToString(loopN1Instance);
            hierarchyString += ")\\";
            hierarchyString += "N3";

            ediDataSegment.Set(ref dataSegment, ediTransactionSet.CreateDataSegment(hierarchyString));
            dataSegment.set_DataElementValue(1, 0, string.Empty); // TODO: Address line 1
            dataSegment.set_DataElementValue(2, 0, string.Empty); // TODO: Address line 1

            hierarchyString = "N1(";
            hierarchyString += Convert.ToString(loopN1Instance);
            hierarchyString += ")\\";
            hierarchyString += "N4";

            ediDataSegment.Set(ref dataSegment, ediTransactionSet.CreateDataSegment(hierarchyString));
            dataSegment.set_DataElementValue(1, 0, "City"); // TODO: City
            dataSegment.set_DataElementValue(2, 0, "ST"); // TODO: State
            dataSegment.set_DataElementValue(3, 0, "00000"); // TODO: Zip
        }

        /// <summary> Process the 810 area 2 described by ediTransactionSet. </summary>
        /// <param name="ediTransactionSet"> Set the edi transaction belongs to. </param>
        private void Process810Area2(ediTransactionSet ediTransactionSet)
        {
            long loopIt1 = 0;

            for (loopIt1 = 1; loopIt1 <= 2; loopIt1++)
            {
                this.Process810Area2LoopIt1(ediTransactionSet, loopIt1);
            }
        }

        /// <summary> Process the 810 area 2 loop iterator 1. </summary>
        /// <param name="ediTransactionSet"> Set the edi transaction belongs to. </param>
        /// <param name="loopIt1">           The first loop iterator. </param>
        private void Process810Area2LoopIt1(ediTransactionSet ediTransactionSet, long loopIt1)
        {
            ediDataSegment dataSegment;
            var hierarchyString = string.Empty;
            long loopPid = 0;

            hierarchyString = "IT1(";
            hierarchyString += Convert.ToString(loopIt1);
            hierarchyString += ")\\";
            hierarchyString += "IT1";

            dataSegment = ediTransactionSet.CreateDataSegment(hierarchyString);
            dataSegment.set_DataElementValue(1, 0, "A1B2C3D4E5");  // TODO: ???
            dataSegment.set_DataElementValue(2, 0, "1234567.12");  // TODO: Quantity
            dataSegment.set_DataElementValue(3, 0, "EA"); // Each
            dataSegment.set_DataElementValue(4, 0, "1234567.12"); // TODO: EXT_PRC_AMT / ediItemLine.QTY - decimal only when needed
            dataSegment.set_DataElementValue(5, 0, "UM"); // By unit of measure
            dataSegment.set_DataElementValue(6, 0, "VN"); // Vendor Product ID Indicator
            dataSegment.set_DataElementValue(7, 0, "A1B2C3D4E5"); // TODO: Dw SKU
            // dataSegment.set_DataElementValue(8, 0, string.Empty);
            // dataSegment.set_DataElementValue(9, 0, string.Empty);

            for (loopPid = 1; loopPid <= 2; loopPid++)
            {
                this.Process810Area2LoopIt1Pid(ediTransactionSet, loopIt1, loopPid);
            }
        }

        /// <summary> Process the 810 area 2 loop iterator 1 PID. </summary>
        /// <param name="ediTransactionSet"> Set the edi transaction belongs to. </param>
        /// <param name="loopIt1">           The first loop iterator. </param>
        /// <param name="loopPid">           The loop PID. </param>
        private void Process810Area2LoopIt1Pid(
            ediTransactionSet ediTransactionSet,
            long loopIt1,
            long loopPid)
        {
            ediDataSegment dataSegment;
            var hierarchyString = string.Empty;

            hierarchyString = "IT1(";
            hierarchyString += Convert.ToString(loopIt1);
            hierarchyString += ")\\";
            hierarchyString += "PID(";
            hierarchyString += Convert.ToString(loopPid);
            hierarchyString += ")\\";
            hierarchyString += "PID";

            dataSegment = ediTransactionSet.CreateDataSegment(hierarchyString);
            dataSegment.set_DataElementValue(1, 0, "F"); // Freeform Code
            dataSegment.set_DataElementValue(5, 0, "A1B2C3D4E5"); // TODO ProductDescription
        }

        /// <summary> Process the 810 area 3 described by ediTransactionSet. </summary>
        /// <param name="ediTransactionSet"> Set the edi transaction belongs to. </param>
        private void Process810Area3(ediTransactionSet ediTransactionSet)
        {
            ediDataSegment dataSegment;

            dataSegment = ediTransactionSet.CreateDataSegment("TDS");
            dataSegment.set_DataElementValue(1, 0, "1234567891"); // TODO TotalBeforeDiscountAmount
            //dataSegment.set_DataElementValue(2, 0, "1234567891");

            ediDataSegment.Set(ref dataSegment, ediTransactionSet.CreateDataSegment("CTT"));
            dataSegment.set_DataElementValue(1, 0, "123456"); // TODO count of order line items
        }
    }
}