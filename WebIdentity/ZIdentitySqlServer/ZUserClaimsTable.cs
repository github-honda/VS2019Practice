using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add
using System.Security.Claims; // ClaimsIdentity
using System.Data;
using ZLib;
using ZLib.DB;

namespace ZIdentitySqlServer
{
    /// <summary>
    /// Table AspNetUserClaims.
    /// </summary>
    public class ZUserClaimsTable
    {
        private readonly ZSqlClient MyDB1;

        public ZUserClaimsTable(ZSqlClient db1)
        {
            MyDB1 = db1;
        }

        /// <summary>
        /// Returns a ClaimsIdentity instance given a userId
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public ClaimsIdentity FindByUserId(string userId)
        {
            ClaimsIdentity claims = new ClaimsIdentity();
            string commandText = "Select * from AspNetUserClaims where UserId = @userId";
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@UserId", userId } };

            var t1 = MyDB1.QueryDataSet(commandText, parameters).Tables[0];
            foreach (DataRow row in t1.Rows)
            {
                Claim claim = new Claim(row["ClaimType"].ZObjectToString(), row["ClaimValue"].ZObjectToString());
                claims.AddClaim(claim);
            }

            return claims;
        }

        public int Delete(string userId)
        {
            string commandText = "Delete from AspNetUserClaims where UserId = @userId";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("userId", userId);
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "userId", userId }
            };

            return MyDB1.ExecuteNonQuery(commandText, parameters);
        }

        public int Insert(Claim userClaim, string userId)
        {
            string commandText = "Insert into AspNetUserClaims (ClaimValue, ClaimType, UserId) values (@value, @type, @userId)";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("value", userClaim.Value);
            //parameters.Add("type", userClaim.Type);
            //parameters.Add("userId", userId);
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "value", userClaim.Value },
                { "type", userClaim.Type },
                { "userId", userId }
            };

            return MyDB1.ExecuteNonQuery(commandText, parameters);
        }

        public int Delete(ZUser user, Claim claim)
        {
            string commandText = "Delete from AspNetUserClaims where UserId = @userId and @ClaimValue = @value and ClaimType = @type";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("userId", user.Id);
            //parameters.Add("value", claim.Value);
            //parameters.Add("type", claim.Type);
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "userId", user.Id },
                { "value", claim.Value },
                { "type", claim.Type }
            };

            return MyDB1.ExecuteNonQuery(commandText, parameters);
        }
    }
}
