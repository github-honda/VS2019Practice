using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZLib
{
    public static class ZError
    {
        public static string ZGetMessages(this Exception e1)
        {
            Exception eCurrent = e1;
            StringBuilder sb1 = new StringBuilder();
            while (eCurrent != null)
            {
                sb1.AppendLine(eCurrent.Message);
                eCurrent = eCurrent.InnerException;
            }
            return sb1.ToString();
        }
    }
}
