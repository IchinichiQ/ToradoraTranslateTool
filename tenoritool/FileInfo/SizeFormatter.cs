// Copyright Joshua Flanagan <http://flimflan.com/blog/FileSizeFormatProvider.aspx>


namespace tenoritool
{
    using System;
    using System.Text;
    using System.Globalization;


    public class FileSizeFormatProvider : IFormatProvider, ICustomFormatter
    {
        public object GetFormat(Type formatType)
        {
            if ( formatType == typeof(ICustomFormatter) ) return this;
            return null;
        }
        private const string fileSizeFormat = "fs";
        private const Decimal OneKiloByte = 1024M;
        private const Decimal OneKiloByteThreshold = 1200M;
        private const Decimal OneMegaByte = OneKiloByte * 1024M;
        private const Decimal OneMegaByteThreshold = OneKiloByte * 922M;
        private const Decimal OneGigaByte = OneMegaByte * 1024M;
        private const Decimal OneGigaByteThreshold = OneMegaByteThreshold * 1024M;

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if ( format == null || !format.StartsWith(fileSizeFormat) )
            {
                return defaultFormat(format, arg, formatProvider);
            }
            if ( arg is string )
            {
                return defaultFormat(format, arg, formatProvider);
            }
            Decimal size;
            try
            {
                size = Convert.ToDecimal(arg);
            }
            catch ( InvalidCastException )
            {
                return defaultFormat(format, arg, formatProvider);
            }

            string suffix;
            bool smallestSuffix = false;
            if ( size > OneGigaByteThreshold )
            {
                size /= OneGigaByte;
                suffix = " GB";
            }
            else if ( size > OneMegaByteThreshold )
            {
                size /= OneMegaByte;
                suffix = " MB";
            }
            else if ( size > OneKiloByteThreshold )
            {
                size /= OneKiloByte;
                suffix = " KB";
            }
            else
            {
                suffix = " B";
                smallestSuffix = true;
            }
            string precision = format.Substring(2);
            if ( String.IsNullOrEmpty(precision) ) precision = smallestSuffix ? "0" : "2";
            return String.Format(CultureInfo.InvariantCulture, "{0:N" + precision + "}{1}", size, suffix);
        }

        private static string defaultFormat(string format, object arg, IFormatProvider formatProvider)
        {
            IFormattable formattableArg = arg as IFormattable;
            if ( formattableArg != null )
            {
                return formattableArg.ToString(format, formatProvider);
            }
            return arg.ToString();
        }
    }
}