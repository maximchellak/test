using System;
using System.Xml.Linq;

namespace StarNet.XML
{
    public class Utils
    {
        public static T GetValue<T>(XElement root, XName field, T nullValue = default(T))
        {
            try
            {
                var value = GetStringValue(root, field);

                if (string.IsNullOrWhiteSpace(value))
                {
                    return nullValue;
                }

                if ((typeof(T) == typeof(DateTime))
                    || (typeof(T) == typeof(DateTime?)))
                {
                    var dateValue = DateTime.ParseExact(value, "dd/MM/yy", System.Globalization.CultureInfo.InvariantCulture);
                    return CastValue<T>(dateValue);
                }
                else if ((typeof(T) == typeof(bool))
                    || (typeof(T) == typeof(bool?)))
                {
                    return CastValue<T>(value == "1");
                }

                return CastValue<T>(value);
            }
            catch
            {
                Logging.Logger.Instance.LogInfo(string.Format("Error reading {0}", field));
                throw;
            }
        }

        private static string GetStringValue(XElement root, XName field, string defaultValue = "")
        {
            var element = root.Element(field);

            if (element == null)
            {
                return defaultValue;
            }

            try
            {
                return element.Value.Trim();
            }
            catch
            {
                return defaultValue;
            }
        }

        private static T CastValue<T>(object value)
        {
            if (value == null)
            {
                return (T)value;
            }

            if (typeof(T) == value.GetType())
            {
                if (typeof(T) == typeof(string))
                {
                    if (string.IsNullOrEmpty((string)value))
                    {
                        return default(T);
                    }
                }

                return (T)value;
            }

            if (typeof(T) == typeof(decimal) || typeof(T) == typeof(decimal?))
            {
                try
                {
                    return (T)(object)Convert.ToDecimal(value);
                }
                catch
                { }
            }

            if (typeof(T) == typeof(int) || typeof(T) == typeof(int?))
            {
                try
                {
                    return (T)(object)Convert.ToInt32(value);
                }
                catch
                { }
            }

            if (typeof(T) == typeof(double) || typeof(T) == typeof(double?))
            {
                try
                {
                    return (T)(object)Convert.ToDouble(value);
                }
                catch
                { }
            }

            if (typeof(T) == typeof(bool) || typeof(T) == typeof(bool?))
            {
                try
                {
                    return (T)(object)Convert.ToBoolean(value);
                }
                catch
                { }
            }

            if (typeof(T) == typeof(DateTime) || typeof(T) == typeof(DateTime?))
            {
                try
                {
                    return (T)(object)Convert.ToDateTime(value);
                }
                catch
                { }
            }

            if (typeof(T) == typeof(string))
            {
                return (T)(object)value.ToString();
            }

            return default(T);
        }
    }
}
