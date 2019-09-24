using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LmsGateway.Core.Extensions
{
    public static class StringExtensions
    {
        //[DebuggerStepThrough]
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        //[DebuggerStepThrough]
        public static bool HasValue(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        //[DebuggerStepThrough]
        public static bool IsWebUrl(this string value, bool schemeIsOptional)
        {
            return value.IsWebUrlInternal(schemeIsOptional);
        }

        //[DebuggerStepThrough]
        //public static bool IsEmail(this string value)
        //{
        //    return !String.IsNullOrEmpty(value) && RegularExpressions.IsEmail.IsMatch(value.Trim());
        //}

        //[DebuggerStepThrough]
        //public static bool IsNumeric(this string value)
        //{
        //    if (String.IsNullOrEmpty(value))
        //        return false;

        //    return !RegularExpressions.IsNotNumber.IsMatch(value) &&
        //           !RegularExpressions.HasTwoDot.IsMatch(value) &&
        //           !RegularExpressions.HasTwoMinus.IsMatch(value) &&
        //           RegularExpressions.IsNumeric.IsMatch(value);
        //}

        private static bool IsWebUrlInternal(this string value, bool schemeIsOptional)
        {
            if (String.IsNullOrEmpty(value))
                return false;

            value = value.Trim().ToLowerInvariant();

            if (schemeIsOptional && value.StartsWith("//"))
            {
                value = "http:" + value;
            }

            return Uri.IsWellFormedUriString(value, UriKind.Absolute)
                && (value.StartsWith("http://")
                || value.StartsWith("https://")
                || value.StartsWith("ftp://"));

            #region Old (obsolete)

            //// Uri.TryCreate() does not accept port numbers in uri strings.
            //if (schemeIsOptional)
            //{
            //	Uri uri;
            //	return Uri.TryCreate(value, UriKind.Absolute, out uri);
            //}

            //return RegularExpressions.IsWebUrl.IsMatch(value.Trim());

            #endregion
        }




    }
}
