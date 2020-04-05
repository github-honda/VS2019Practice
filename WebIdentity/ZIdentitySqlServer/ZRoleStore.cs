using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add
using Microsoft.AspNet.Identity;
using ZLib.DB;

namespace ZIdentitySqlServer
{
    /// <summary>
    /// Class that implements the key ASP.NET Identity role store iterfaces
    /// </summary>
    public class ZRoleStore<TRole> : IQueryableRoleStore<TRole>
        where TRole : ZRole
    {
        private readonly ZRoleTable roleTable;
        public ZSqlClient MyDB1 { get; private set; }

        public IQueryable<TRole> Roles
        {
            get
            {
                throw new NotImplementedException();
            }
        }


        //public ZRoleStore(string sConnectionString)
        //{
        //    new ZRoleStore<TRole>(new ZIdentityDB(sConnectionString));
        //}

        public ZRoleStore(ZSqlClient db1)
        {
            MyDB1 = db1;
            roleTable = new ZRoleTable(db1);
        }

        public Task CreateAsync(TRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            roleTable.Insert(role);

            return Task.FromResult<object>(null);
        }

        public Task DeleteAsync(TRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            roleTable.Delete(role.Id);

            return Task.FromResult<Object>(null);
        }

        public Task<TRole> FindByIdAsync(string roleId)
        {
            TRole result = roleTable.GetRoleById(roleId) as TRole;

            return Task.FromResult<TRole>(result);
        }

        public Task<TRole> FindByNameAsync(string roleName)
        {
            TRole result = roleTable.GetRoleByName(roleName) as TRole;

            return Task.FromResult<TRole>(result);
        }

        public Task UpdateAsync(TRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            roleTable.Update(role);

            return Task.FromResult<Object>(null);
        }

        public void Dispose()
        {
            if (MyDB1 != null)
            {
                MyDB1.Dispose();
                MyDB1 = null;
            }
        }
    }
}
