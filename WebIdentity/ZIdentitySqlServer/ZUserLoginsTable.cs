using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Data;
using ZLib;
using ZLib.DB;

namespace ZIdentitySqlServer
{
    /// <summary>
    /// Table AspNetUserLogins
    /// </summary>
    public class ZUserLoginsTable
    {
        private readonly ZSqlClient MyDB1;

        public ZUserLoginsTable(ZSqlClient db1)
        {
            MyDB1 = db1;
        }

        public int Delete(ZUser user, UserLoginInfo login)
        {
            string commandText = "Delete from AspNetUserLogins where UserId = @userId and LoginProvider = @loginProvider and ProviderKey = @providerKey";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("UserId", user.Id);
            //parameters.Add("loginProvider", login.LoginProvider);
            //parameters.Add("providerKey", login.ProviderKey);
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "UserId", user.Id },
                { "loginProvider", login.LoginProvider },
                { "providerKey", login.ProviderKey }
            };

            return MyDB1.ExecuteNonQuery(commandText, parameters);
        }

        public int Delete(string userId)
        {
            string commandText = "Delete from AspNetUserLogins where UserId = @userId";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("UserId", userId);
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "UserId", userId }
            };

            return MyDB1.ExecuteNonQuery(commandText, parameters);
        }

        public int Insert(ZUser user, UserLoginInfo login)
        {
            string commandText = "Insert into AspNetUserLogins (LoginProvider, ProviderKey, UserId) values (@loginProvider, @providerKey, @userId)";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("loginProvider", login.LoginProvider);
            //parameters.Add("providerKey", login.ProviderKey);
            //parameters.Add("userId", user.Id);
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "loginProvider", login.LoginProvider },
                { "providerKey", login.ProviderKey },
                { "userId", user.Id }
            };

            return MyDB1.ExecuteNonQuery(commandText, parameters);
        }

        public string FindUserIdByLogin(UserLoginInfo userLogin)
        {
            string commandText = "Select UserId from AspNetUserLogins where LoginProvider = @loginProvider and ProviderKey = @providerKey";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("loginProvider", userLogin.LoginProvider);
            //parameters.Add("providerKey", userLogin.ProviderKey);
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "loginProvider", userLogin.LoginProvider },
                { "providerKey", userLogin.ProviderKey }
            };

            return MyDB1.QueryString(commandText, parameters);
        }

        public List<UserLoginInfo> FindByUserId(string userId)
        {
            List<UserLoginInfo> logins = new List<UserLoginInfo>();
            string commandText = "Select * from AspNetUserLogins where UserId = @userId";
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@userId", userId } };

            DataTable dt1 = MyDB1.QueryDataSet(commandText, parameters).Tables[0];
            foreach (DataRow dr1 in dt1.Rows)
            {
                var login = new UserLoginInfo(dr1["LoginProvider"].ZObjectToString(), dr1["ProviderKey"].ZObjectToString());
                logins.Add(login);
            }
            return logins;
        }
    }
}
