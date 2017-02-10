using System;
using System.Collections;

namespace EDI.Helper
{
    public class ArgumentParser
    {
        #region Parser Methods


        /// <summary>
        /// Parses the specified args. Parameters/Arguments should be in pairs of values with an equal (=) sign\r\n
        /// Example:\r\n
        /// \r\n
        ///   (process name) -d=2010-09-10 -t -n="Brent Brown" 
        /// </summary>
        /// <param name="args">The args to process</param>
        /// <returns>Hashtable with keys as argument property and value as argument value</returns>
        public static Hashtable Parse(String[] args)
        {
            Hashtable argTable = new Hashtable();
            try
            {
                string[] keyvalue;
                string value;

                if ((null == args) || args.Length == 0)
                {
                    argTable.Add("Invalid", "true");
                    return argTable;
                }


                foreach (string s in args)
                {

                    if ((s.StartsWith("-") || s.StartsWith("/")) && ((s.Contains("=") && !s.Substring(0,2).Contains("?")) || s.Length == 2))
                    {
                        // Strip off - or /
                        string key = s.Substring(1, s.Length - 1);

                        // split argument on equals sign (=)
                        keyvalue = key.Split('=');

                        // set the key value
                        key = keyvalue[0].ToString();




                        if (keyvalue.Length <= 1)
                        {
                            value = "true";
                        }
                        else
                        {
                            value = keyvalue[1].ToString();

                            char[] charsToTrim = { '\u0022', '\u0027' };

                            // strip double quotes or single quotes off both ends
                            value = value.Trim(charsToTrim);
                        }

                        AddKeyValuePair(argTable, key, value);

                    }
                    else
                    {
                        // otherwise this is invalid value
                        if (s.Contains("?"))
                        {
                            AddKeyValuePair(argTable, "?", "true");
                        }
                        else
                        {
                            AddKeyValuePair(argTable, "Invalid", "true");
                        }
                    }
                }
                // return the hashtable with the command line arguments in it.
                return argTable;
            }
            catch (ArgumentOutOfRangeException outOfRangeEx)
            {
                Console.WriteLine("Arguments out of range, please check error log" + outOfRangeEx.Message + Environment.NewLine + outOfRangeEx.StackTrace);
                return argTable;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
                return argTable;
            }
        }

        //Add the arguments in the form of a keyvalue pair to Hashtable
        public static void AddKeyValuePair(Hashtable argTable, string key, string value)
        {
            try
            {
                if (!argTable.ContainsKey(key))
                {
                    // add this to table
                    argTable.Add(key, value);
                }
                else
                {
                    //substitute the value with the latest one
                    argTable[key] = value;
                }
            }
            catch (ArgumentException argEx)
            {
                Console.WriteLine(argEx.Message + Environment.NewLine + argEx.StackTrace);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }
        #endregion
    }
}