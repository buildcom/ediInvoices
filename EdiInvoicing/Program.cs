using System;
using System.Globalization;
using System.Linq;
using System.Reflection;

using EDI.Code.EdiWriters;
using EDI.Helper;
using EDI.Properties;

using log4net;

namespace EDI
{
    /// <summary> Program to export 856 and 810 EDI files. </summary>
    internal class Program
    {
        /// <summary> The log. </summary>
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary> true to mark ship confirm. </summary>
        private static bool markShipConfirm;

        /// <summary> The order number. </summary>
        private static string orderNumber = string.Empty;

        /// <summary> Main entry-point for this application Allows for type and confirmation flag arguments to determine what EDI output type to process.</summary>
        /// <param name="args"> An array of command-line argument strings. </param>
        public static void Main(string[] args)
        {
            try
            {
                var startTime = DateTime.Now;

                int? settingId = null;

                if (args != null && args.Any())
                {
                    var argParse = ArgumentParser.Parse(args);
                    var ediStr = string.Empty;

                    // Capture EDI Type. 810 or 856
                    if (argParse.ContainsKey("e"))
                    {
                        ediStr = argParse["e"].ToSafeString(string.Empty);
                    }

                    // Capture Confirmation Flag, signals ship confirmation date to be added on success
                    if (argParse.ContainsKey("c"))
                    {
                        markShipConfirm = true;
                    }

                    // Capture supplier setting ID for dynamic processing
                    if (argParse.ContainsKey("settingId"))
                    {
                        settingId = argParse["settingId"].ToString().ToInteger() ?? 0;
                    }

                    // TODO: allow for order number only parameter (another phase)
                    if (argParse.ContainsKey("o"))
                    {
                        orderNumber = argParse["o"].ToSafeString(string.Empty);
                    }

                    if (argParse.ContainsKey("?") || argParse.ContainsKey("o"))
                    {
                        // Displays help dialog
                        // TODO: remove arg check for "o" after order number only is implemented
                        Console.Write(Resources.helptext);
                    }
                    else
                    {
                        if ((settingId ?? 0) > 0)
                        {
                            var supplierSettings = new SupplierSettings(settingId ?? 0);
                            ProcessEdiFile(ediStr, supplierSettings, markShipConfirm);
                        }
                        else
                        {
                            ////Maintain old edi process until dynamic is complete
                            //switch (ediStr)
                            //{
                            //    case "810":
                            //        {
                            //            Log.InfoFormat("EdiExportHelper_810");
                            //            Log.InfoFormat("{0} - Init", DateTime.Now.ToLongTimeString());
                            //            var ediHelper810 = new EdiExportHelper_810();
                            //            ediHelper810.markShipConfirm = markShipConfirm;
                            //            ediHelper810.CreateEdiDocument();
                            //            return;
                            //        }

                            //    case "856":
                            //        {
                            //            Log.InfoFormat("EdiExportHelper_856");
                            //            Log.InfoFormat("{0} - Init", DateTime.Now.ToLongTimeString());
                            //            var ediHelper856 = new EdiExportHelper_856();
                            //            ediHelper856.markShipConfirm = markShipConfirm;
                            //            ediHelper856.CreateEdiDocument();
                            //            return;
                            //        }
                            //}
                        }

                        var endTime = DateTime.Now;
                        Log.InfoFormat("Start Time: {0}", startTime.ToLongTimeString());
                        Log.InfoFormat("End Time: {0}", endTime.ToLongTimeString());
                        Log.InfoFormat("Elapsed Time: {0} sec", endTime.Subtract(startTime).TotalSeconds);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Warn("Exception in EcomExport Main:  " + e.GetAllExceptionMessagesAsStrings(), e);
            }
        }

        /// <summary> Process the edi file.</summary>
        /// <param name="ediType">         Type of the edi. </param>
        /// <param name="settings">        Options for controlling the operation. </param>
        /// <param name="markShipConfirm"> true to mark ship confirm. </param>
        private static void ProcessEdiFile(string ediType, SupplierSettings settings, bool markShipConfirm)
        {
            var type = GetCalculatedType(ediType, settings);

            Log.Info("Creating edi writer of type: " + type.Name);
            var ediWriter = (EdiWriterBase)Activator.CreateInstance(type, markShipConfirm, settings);
            ediWriter.ProcessEdi();
        }

        /// <summary> Gets calculated type.</summary>
        /// <exception cref="TypeAccessException"> Thrown when a Type Access error condition occurs. </exception>
        /// <param name="ediType">  Type of the edi. </param>
        /// <param name="settings"> Options for controlling the operation. </param>
        /// <returns> The calculated type.</returns>
        private static Type GetCalculatedType(string ediType, SupplierSettings settings)
        {
            var writerPrefix = "EDI.Code.EdiWriters.EdiWriter";

            var textInfo = new CultureInfo("en-US", false).TextInfo;
            var supplierName = textInfo.ToTitleCase(settings.DatabaseSettings.SourceName);
            var typeName = writerPrefix + ediType + supplierName;
            var type = Type.GetType(typeName);

            if (type == null)
            {
                throw new TypeAccessException("Could not access type " + typeName);
            }

            return type;
        }
    }
}