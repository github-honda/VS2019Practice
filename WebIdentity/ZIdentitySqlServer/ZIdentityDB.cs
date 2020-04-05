using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add
using System.Configuration; // System.Configuration
using ZLib.DB;

namespace ZIdentitySqlServer
{
    public class ZIdentityDB : ZSqlClient  // 本專案應只有本 Class 使用 ZSqlClient.
    {
        public ZIdentityDB(string sConnectionString)
            : base(sConnectionString)
        {
        }
    }
}
