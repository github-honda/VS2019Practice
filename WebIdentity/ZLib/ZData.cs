using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add
using System.Data;
using System.Data.Common;

namespace ZLib
{
    public static class ZData
    {
        public static int ZRowsCount(this DataTable dt)
        {
            if (dt == null)
                return -1;
            return dt.Rows.Count;
        }


        public static DbCommand ZCreateCommand(this DbConnection cn1, string sCmd, Dictionary<string, object> parameters = null, int iTimeout = 30, DbTransaction transaction1 = null)
        {
            //DbCommand command = _Connection.CreateCommand();
            DbCommand command = cn1.CreateCommand();
            command.CommandText = sCmd;
            //command.CommandTimeout = _CommandTimeOut;
            command.CommandTimeout = iTimeout;
            //command.Transaction = _Transaction; // todo: Transaction control verify.
            command.Transaction = transaction1; // todo: Transaction control verify.
            ZAddParameters(command, parameters);

            return command;
        }
        public static void ZAddParameters(this DbCommand command, Dictionary<string, object> parameters)
        {
            if (parameters == null)
                return;

            foreach (var param in parameters)
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = param.Key;
                parameter.Value = param.Value ?? DBNull.Value; // CodeHelper.
                command.Parameters.Add(parameter);
            }
        }

    }
}
