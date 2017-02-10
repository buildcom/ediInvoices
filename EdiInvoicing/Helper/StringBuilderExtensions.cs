using System;
using System.Text;

namespace EdiInvoicing.Helper
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>A string builder extension.</summary>
    ///
    /// <remarks>BBrown, 1/26/2015.</remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public static class StringBuilderExtension
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>Appends a formatted string and the default line terminator to to this StringBuilder instance.  </summary>
        ///
        /// <example><para>Source: http://extensionmethod.net/csharp/stringbuilder/appendlineformat</para>
        ///          <code>StringBuilder builder = new StringBuilder();
        /// builder.AppendLineFormat("File name: {0} (line: {1}, column: {2})", fileName, lineNumber, column);
        /// builder.AppendLineFormat("Message: {0}", exception.Message);</code>
        ///          </example>
        /// <remarks>BBrown, 1/26/2015.</remarks>
        /// 
        /// <param name="builder">  The builder to act on.</param>
        /// <param name="format">   Describes the format to use.</param>
        /// <param name="arguments">A variable-length parameters list containing arguments.</param>
        ///
        /// <returns>A StringBuilder.</returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static StringBuilder AppendLineFormat(this StringBuilder builder, string format, params object[] arguments)
        {
            string value = String.Format(format, arguments);

            builder.AppendLine(value);

            return builder;
        }



        
    }
}
