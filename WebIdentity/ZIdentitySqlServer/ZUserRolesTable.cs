using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add
using System.Data;
using ZLib;
using ZLib.DB;

namespace ZIdentitySqlServer
{
    /// <summary>
    /// Table AspNetUserRoles.
    /// </summary>
    public class ZUserRolesTable
    {
        readonly ZSqlClient MyDB1;

        public ZUserRolesTable(ZSqlClient db1)
        {
            MyDB1 = db1;
        }

        public List<string> FindByUserId(string userId)
        {
            List<string> roles = new List<string>();
            string commandText = "Select AspNetRoles.Name from AspNetUserRoles, AspNetRoles where AspNetUserRoles.UserId = @userId and AspNetUserRoles.RoleId = AspNetRoles.Id";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("@userId", userId);
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@userId", userId }
            };

            DataTable dt1 = MyDB1.QueryDataSet(commandText, parameters).Tables[0];
            foreach (DataRow dr1 in dt1.Rows)
            {
                roles.Add(dr1[0].ZObjectToString());
            }
            return roles;
        }

        public int Delete(string userId)
        {
            string commandText = "Delete from AspNetUserRoles where UserId = @userId";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("UserId", userId);
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "UserId", userId }
            };

            return MyDB1.ExecuteNonQuery(commandText, parameters);
        }

        public int Insert(ZUser user, string roleId)
        {
            string commandText = "Insert into AspNetUserRoles (UserId, RoleId) values (@userId, @roleId)";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("userId", user.Id);
            //parameters.Add("roleId", roleId);
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "userId", user.Id },
                { "roleId", roleId }
            };

            return MyDB1.ExecuteNonQuery(commandText, parameters);
        }
    }
}
