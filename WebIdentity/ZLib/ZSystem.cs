using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZLib
{
    public static class ZSystem
    {
        public static Boolean ZIsDBNull(this object oField)
        {
            return (oField == DBNull.Value);
        }
        public static Boolean ZIsNullOrDBNull(this object oInput)
        {
            if (oInput == null)
                return true;
            return ZIsDBNull(oInput);
        }

        public static Boolean ZObjectToBoolean(this object v1, Boolean DefaultValue = false)
        {
            if (v1.ZIsNullOrDBNull())
                return DefaultValue;
            return (Boolean)v1;
        }
        public static string ZObjectToString(this object v1, string DefaultValue = "")
        {
            if (v1.ZIsNullOrDBNull())
                return DefaultValue;
            return v1.ToString();
        }

        public static DateTime ZObjectToDateTime(this object v1, DateTime DefaultValue)
        {
            if (v1.ZIsNullOrDBNull())
                return DefaultValue;
            return (DateTime)v1;
        }

        public static DateTime ZObjectToDateTime(this object v1)
        {
            return ZObjectToDateTime(v1, DateTime.MinValue);
        }
        public static int ZObjectToInt(this object v1, int DefaultValue = 0)
        {
            if (v1.ZIsNullOrDBNull())
                return DefaultValue;
            return (int)v1;
        }


    }
}
