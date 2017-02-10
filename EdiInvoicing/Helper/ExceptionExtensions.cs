using System;
using System.Collections.Generic;
using System.Text;

namespace EDI.Helper
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   An exception extensions. </summary>
    ///
    /// <remarks>   Bbrown, 9/12/2014. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public static class ExceptionExtensions
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets all exceptions, including inner, in this collection. </summary>
        ///
        /// <remarks>   Bbrown, 9/12/2014. </remarks>
        ///
        /// <param name="ex">   The ex to act on. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process all exceptions in this collection.
        /// </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static IEnumerable<Exception> GetAllExceptions(this Exception ex)
        {
            Exception currentEx = ex;
            yield return currentEx;
            while (currentEx.InnerException != null)
            {
                currentEx = currentEx.InnerException;
                yield return currentEx;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets all exception types, including inner, as strings in this collection. </summary>
        ///
        /// <remarks>   Bbrown, 9/12/2014. </remarks>
        ///
        /// <param name="ex">   The ex to act on. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process all exception as strings in this
        /// collection.
        /// </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static IEnumerable<string> GetAllExceptionAsString(this Exception ex)
        {
            Exception currentEx = ex;
            yield return currentEx.ToString();
            while (currentEx.InnerException != null)
            {
                currentEx = currentEx.InnerException;
                yield return currentEx.ToString();
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets all exception messages in this collection of strings. </summary>
        ///
        /// <remarks>   Bbrown, 9/12/2014. </remarks>
        ///
        /// <param name="ex">   The ex to act on. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process all exception messages in this
        /// collection.
        /// </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static IEnumerable<string> GetAllExceptionMessages(this Exception ex)
        {
            Exception currentEx = ex;
            yield return currentEx.Message;
            while (currentEx.InnerException != null)
            {
                currentEx = currentEx.InnerException;
                yield return currentEx.Message;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// An Exception extension method that gets all exception messages including, inner
        /// exceptions, and resturns them as one string.
        /// </summary>
        ///
        /// <remarks>   Bbrown, 9/12/2014. </remarks>
        ///
        /// <param name="ex">   The ex to act on. </param>
        ///
        /// <returns>   all exception messages as a string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static string GetAllExceptionMessagesAsStrings(this Exception ex)
        {
            StringBuilder result = new StringBuilder();

            Exception currentEx = ex;
            result.AppendLine(currentEx.Message);

            int i = 0;

            while (currentEx.InnerException != null)
            {
                i++;
                currentEx = currentEx.InnerException;
                result.AppendLineFormat("Inner Exception {0}: {1}", i, currentEx.Message);
            }

            return result.ToString();
        }


        /// <summary>An Exception extension method that gets all exception stack messages
        ///          including inner exception stack trace messages.</summary>
        ///
        /// <remarks>BBrown, 5/18/2015.</remarks>
        ///
        /// <param name="ex">The ex to act on.</param>
        ///
        /// <returns>all exception stack messages.</returns>

        public static string GetAllExceptionStackMessages(this Exception ex)
        {
            StringBuilder result = new StringBuilder();

            Exception currentEx = ex;
            result.AppendLine(currentEx.StackTrace);

            int i = 0;

            while (currentEx.InnerException != null)
            {
                i++;
                currentEx = currentEx.InnerException;
                result.AppendLineFormat("Inner Stack Trace {0}: {1}", i, currentEx.StackTrace);
            }

            return result.ToString();
        }
    }

}
