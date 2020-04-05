using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add
using ZLib.DLib;

namespace ZIdentitySqlServer
{
    public class CDB: ZSqlClient
    {
        public CDB(string sConnectionString)
            : base(sConnectionString)
        { }

    }
}
