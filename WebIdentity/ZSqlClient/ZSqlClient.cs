// ZSqlClient.cs
// 20200131, Honda, 移除 AutoCloseConnection 控制. 若須關閉, 可自行呼叫 CloseConnection() 即可.
// 20200203, Honda, 跟隨 MS 命名, ExecuteNonQuery(), ExecuteScalar()... 並增加 QueryDataTable()
// 20200205, Honda, 精簡 ZADB 物件共用 DbConnection, DbDataAdapter..
// 20200401, Honda, 移為外掛 ZSqlClient, 並適用於 vs2019, .net 4.8, c# 8.0.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add
using System.Data.Common;
using System.Data.SqlClient;
using ZLib.DData;

// namespace 不要跟 class 名稱相同, 否則使用時會混淆.
namespace ZLib.DB
{
    public class ZSqlClient : ZADb, IDisposable
    {
        public ZSqlClient(string sConnectionString)
            : base(sConnectionString)
        {
        }

        /// <summary>
        /// 建立 DbConnection 物件.
        /// DbConnection 是 abstract, 必須由 Instance 建立.
        /// </summary>
        /// <returns></returns>
        public override DbConnection CreateConnection()
        {
            return new SqlConnection();
        }

        /// <summary>
        /// 建立 DbDataAdapter 物件.
        /// DbDataAdapter 是 abstract, 必須由 Instance 建立.
        /// </summary>
        /// <returns></returns>
        public override DbDataAdapter CreateDataAdapter()
        {
            return new SqlDataAdapter();
        }


        #region Dispose Part
        Boolean _Disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_Disposed)
            {
                if (disposing)
                {
                    if (MyConnection != null)
                    {
                        CloseConnection();
                        MyConnection.Dispose();
                    }
                }
                MyConnection = null;
                _Disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            //GC.SuppressFinalize(this);
        }
        #endregion
    }
}
