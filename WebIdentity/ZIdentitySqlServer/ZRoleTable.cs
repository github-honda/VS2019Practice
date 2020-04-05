using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add
using ZLib.DB;


namespace ZIdentitySqlServer
{
    /// <summary>
    /// Table AspNetRoles
    /// </summary>
    public class ZRoleTable
    {
        readonly ZSqlClient MyDB1;

        public ZRoleTable(ZSqlClient db1)
        {
            MyDB1 = db1;
        }

        public int Delete(string roleId)
        {
            string commandText = "Delete from AspNetRoles where Id = @id";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("@id", roleId);
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@id", roleId }
            };

            return MyDB1.ExecuteNonQuery(commandText, parameters);
        }

        public int Insert(ZRole role)
        {
            string commandText = "Insert into AspNetRoles (Id, Name) values (@id, @name)";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("@name", role.Name);
            //parameters.Add("@id", role.Id);
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@name", role.Name },
                { "@id", role.Id }
            };

            return MyDB1.ExecuteNonQuery(commandText, parameters);
        }

        public string GetRoleName(string roleId)
        {
            string commandText = "Select Name from AspNetRoles where Id = @id";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("@id", roleId);
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@id", roleId }
            };

            return MyDB1.QueryString(commandText, parameters);
        }

        public string GetRoleId(string roleName)
        {
            string roleId = null;
            string commandText = "Select Id from AspNetRoles where Name = @name";
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@name", roleName } };

            var result = MyDB1.ExecuteScalar(commandText, parameters);
            if (result != null)
            {
                return Convert.ToString(result);
            }

            return roleId;
        }

        public ZRole GetRoleById(string roleId)
        {
            var roleName = GetRoleName(roleId);
            ZRole role = null;

            if (roleName != null)
            {
                role = new ZRole(roleName, roleId);
            }

            return role;

        }

        public ZRole GetRoleByName(string roleName)
        {
            var roleId = GetRoleId(roleName);
            ZRole role = null;

            if (roleId != null)
            {
                role = new ZRole(roleName, roleId);
            }

            return role;
        }

        public int Update(ZRole role)
        {
            string commandText = "Update AspNetRoles set Name = @name where Id = @id";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("@id", role.Id);
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@id", role.Id }
            };

            return MyDB1.ExecuteNonQuery(commandText, parameters);
        }
    }
}
