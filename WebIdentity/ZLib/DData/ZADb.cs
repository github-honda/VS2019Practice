// ZADb.cs
// 20100101, Honda, Create. Abstract class for database.
// 20191209, Honda, QueryString change internal logics.
// 20200117, Honda, 函數內部不再封裝exception, 交由呼叫者自行控制.
// 20200131, Honda, 移除 AutoCloseConnection 控制. 若須關閉, 可自行呼叫 CloseConnection() 即可.
// 20200203, Honda, 跟隨 MS 命名, ExecuteNonQuery(), ExecuteScalar()... 並增加 QueryDataTable()
// 20200205, Honda, 精簡 ZADB 物件共用 DbConnection, DbDataAdapter..
// 20200318, Honda, TryConnect 改變回傳所有的 exception messages.
// 20200401, Honda, 移到 ZLib.DData, 並適用於 vs2019, .net 4.8, c# 8.0.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add
using System.Data.Common; // DbConnection
using System.Data; // DataSet


namespace ZLib.DData
{
    /// <summary>
    /// 資料庫抽象物件
    /// </summary>
    public abstract class ZADb
    {
        public string MyConnectionString { get; private set; }
        protected DbConnection MyConnection;

        /// <summary>
        /// Transaction in connection level.
        /// </summary>
        protected DbTransaction _Transaction;

        /// <summary>
        /// 最大等待指令執行時間(秒數, 預設為30秒)
        /// 這不是 ConnectionTimeout. 
        /// ConnectionTimeout 是唯讀不可設定, 預設為15秒.
        /// The time in seconds to wait for the command to execute. The default is 30 seconds.  
        /// </summary>
        public int MyCommandTimeOut { get; set; }

        public ZADb(string sConnectionString)
        {
            MyConnectionString = sConnectionString;
            MyCommandTimeOut = 30;
        }


        /// <summary>
        /// 建立 DbConnection 物件.
        /// DbConnection 是 abstract, 必須由 Instance 建立.
        /// </summary>
        /// <returns></returns>
        abstract public DbConnection CreateConnection();

        /// <summary>
        /// 建立 DbDataAdapter 物件.
        /// DbDataAdapter 是 abstract, 必須由 Instance 建立.
        /// </summary>
        /// <returns></returns>
        abstract public DbDataAdapter CreateDataAdapter();

        public void OpenConnection()
        {
            if (MyConnection == null)
                MyConnection = CreateConnection();

            if (string.IsNullOrEmpty(MyConnection.ConnectionString))
                MyConnection.ConnectionString = MyConnectionString;

            if (MyConnection.State == ConnectionState.Closed)
                MyConnection.Open();

        }
        public void CloseConnection()
        {
            if (MyConnection == null)
                return;

            if (MyConnection.State == ConnectionState.Open)
                MyConnection.Close();
        }
        public Boolean IsClosed()
        {
            if (MyConnection == null)
                return true;

            if (MyConnection.State == ConnectionState.Closed)
                return true;

            return false;
        }

        /// <summary>
        /// 測試連接資料庫. 
        /// 若連接成功, 則回傳空字串, 否則回傳錯誤訊息.
        /// </summary>
        /// <returns></returns>
        public string TryConnect()
        {
            string sError = string.Empty;
            try
            {
                OpenConnection();
            }
            catch (Exception ex)
            {
                return ex.ZGetMessages();
            }
            finally
            {
                CloseConnection();
            }
            return sError;
        }

        public int ExecuteNonQuery(string sCmd, Dictionary<string, object> parameters = null)
        {
            // 20200117, Honda, 函數內部不再封裝exception, 交由呼叫者自行控制.
            OpenConnection();
            return CreateCommand(sCmd, parameters).ExecuteNonQuery();
        }

        public object ExecuteScalar(string sCmd, Dictionary<string, object> parameters = null)
        {
            // 20200117, Honda, 函數內部不再封裝exception, 交由呼叫者自行控制.
            OpenConnection();
            return CreateCommand(sCmd, parameters).ExecuteScalar();
        }
        public DbDataReader ExecuteReader(string sCmd, Dictionary<string, object> parameters = null)
        {
            OpenConnection();
            return CreateCommand(sCmd, parameters).ExecuteReader(CommandBehavior.Default);
            //DbDataReader reader1 = command.ExecuteReader(CommandBehavior.Default);
            // CodeHelper. DbDataReader
            //while (reader1.Read())
            //{
            //    Console.WriteLine(String.Format("{0}", reader1[0]));
            //    Console.WriteLine(String.Format("{0}", FetchString(reader1, 1)));
            //}
            //return reader1;
        }
        public string QueryString(string sCmd, Dictionary<string, object> parameters = null)
        {
            return ExecuteScalar(sCmd, parameters).ZObjectToString();
        }

        public DbCommand CreateCommand(string sCmd, Dictionary<string, object> parameters = null)
        {
            // 20200205, 提供外部呼叫 Common function 簡化使用方式.
            return MyConnection.ZCreateCommand(sCmd, parameters, MyCommandTimeOut, _Transaction);
        }
        public DataSet QueryDataSet(string sCmd, Dictionary<string, object> parameters = null)
        {
            // 20200117, Honda, 函數內部不再封裝exception, 交由呼叫者自行控制.
            OpenConnection();
            DbCommand command = CreateCommand(sCmd, parameters);
            DbDataAdapter adapter1 = CreateDataAdapter();
            adapter1.SelectCommand = command;
            DataSet ds1 = new DataSet();
            adapter1.Fill(ds1);
            return ds1;
        }

        /// <summary>
        /// 取得(查詢結果DataSet 第1個 DataTable)
        /// </summary>
        /// <param name="sCmd"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataTable QueryDataTable(string sCmd, Dictionary<string, object> parameters = null)
        {
            return QueryDataSet(sCmd, parameters).Tables[0];
        }

        // Transaction.
        public void BeginTransaction()
        {
            if (IsClosed())
                return;
            _Transaction = MyConnection.BeginTransaction();
        }
        public void Commit()
        {
            if (IsTransaction())
            {
                _Transaction.Commit();
                _Transaction = null;
            }
        }
        public void Rollback()
        {
            if (IsTransaction())
            {
                _Transaction.Rollback();
                _Transaction = null;
            }
        }
        public Boolean IsTransaction()
        {
            if (IsClosed())
                return false;

            if (_Transaction == null)
                return false;

            return true;
        }
    }
}
