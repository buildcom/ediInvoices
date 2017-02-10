using System;
using System.Globalization;
using System.Linq;
using System.Reflection;

using EdiInvoicing.Helper;
using EdiInvoicing.Properties;

using EDI.Code.EdiWriters;

using log4net;

namespace EdiInvoicing
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
                    if (argParse.ContainsKey("ediType"))
                    {
                        ediStr = argParse["ediType"].ToSafeString(string.Empty);
                    }

                    // Capture Confirmation Flag, signals ship confirmation date to be added on success
                    if (argParse.ContainsKey("markConfirmed"))
                    {
                        markShipConfirm = true;
                    }

                    // Capture supplier setting ID for dynamic processing
                    if (argParse.ContainsKey("settingId"))
                    {
                        settingId = argParse["settingId"].ToString().ToInteger() ?? 0;
                    }

                    // TODO: allow for order number only parameter (another phase)
                    if (argParse.ContainsKey("orderNumber"))
                    {
                        orderNumber = argParse["orderNumber"].ToSafeString(string.Empty);
                    }

                    if (argParse.ContainsKey("?"))
                    {
                        // Displays help dialog
                        Console.Write(Resources.helptext);
                    }
                    else
                    {
                      
                            var supplierSettings = new SupplierSettings(settingId ?? 0);
                            ProcessEdiFile(ediStr, supplierSettings, markShipConfirm);
                      
                      

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