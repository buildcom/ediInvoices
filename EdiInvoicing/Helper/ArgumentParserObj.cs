using System;
using System.Collections;

namespace EdiInvoicing.Helper
{
    public class ArgumentParserObj
    {
        public Hashtable ArgumentHashtable { get; set; }
        public Hashtable ArgumentDescTable { get; set; }



        public ArgumentParserObj()
        {
        }



        public ArgumentParserObj(String[] args)
        {
            this.ArgumentHashtable = new Hashtable();
            this.ArgumentDescTable = new Hashtable();
            this.Parse(args);
        }


        #region Parser Methods


        /// <summary>
        /// Parses the specified args. Parameters/Arguments should be in pairs of values with an equal (=) sign\r\n
        /// Example:\r\n
        /// \r\n
        ///   (process name) -d=2010-09-10 -t -n="Brent Brown" 
        /// </summary>
        /// <param name="args">The args to process</param>
        /// <returns>Hashtable with keys as argument property and value as argument value</returns>
        protected void Parse(String[] args)
        {
            
            try
            {
                if (( null == args ) || args.Length == 0)
                {
                    this.ArgumentHashtable.Add("Invalid", "true");
                }
                else
                {
                    foreach (string s in args)
                    {

                        if (( s.StartsWith("-") || s.StartsWith("/") ) &&
                            ( ( s.Contains("=") && !s.Substring(0, 2).Contains("?") ) || s.Length == 2 ))
                        {
                            // Strip off - or /
                            string key = s.Substring(1, s.Length - 1);

                            // split argument on equals sign (=)
                            var keyvalue = key.Split('=');

                            // set the key value
                            key = keyvalue[0].ToString();


                            string value;
                            if (keyvalue.Length <= 1)
                            {
                                value = "true";
                            }
                            else
                            {
                                value = keyvalue[1].ToString();

                                char[] charsToTrim = {'\u0022', '\u0027'};

                                // strip double quotes or single quotes off both ends
                                value = value.Trim(charsToTrim);
                            }

                            this.AddKeyValuePair(key, value);

                        }
                        else
                        {
                            // otherwise this is invalid value
                            if (s.Contains("?"))
                            {
                                this.AddKeyValuePair("?", "true");
                            }
                            else
                            {
                                this.AddKeyValuePair("Invalid", "true");
                            }
                        }
                    }
                }

            }
            catch (ArgumentOutOfRangeException outOfRangeEx)
            {
                Console.WriteLine("Arguments out of range, please check error log" + outOfRangeEx.Message + Environment.NewLine + outOfRangeEx.StackTrace);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }


        /// <summary>Adds a key value pair to 'value'.</summary>
        ///
        /// <remarks>BBrown, 8/5/2015.</remarks>
        ///
        /// <param name="key">  The key.</param>
        /// <param name="value">The value.</param>

        public void AddKeyValuePair(string key, string value)
        {
            
            try
            {
                if (!this.ArgumentHashtable.ContainsKey(key))
                {
                    // add this to table
                    this.ArgumentHashtable.Add(key, value);
                }
                else
                {
                    //substitute the value with the latest one
                    this.ArgumentHashtable[key] = value;
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


        /// <summary>Adds a key description to 'value'.</summary>
        ///
        /// <remarks>BBrown, 8/5/2015.</remarks>
        ///
        /// <param name="key">  The key.</param>
        /// <param name="value">The value.</param>

        public void AddKeyDescription(string key, string value)
        {
            try
            {
                if (!this.ArgumentDescTable.ContainsKey(key))
                {
                    // add this to table
                    this.ArgumentDescTable.Add(key, value);
                }
                else
                {
                    //substitute the value with the latest one
                    this.ArgumentDescTable[key] = value;
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