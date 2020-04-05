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
    public class ZUserTable<TUser> where TUser : ZUser
    {
        private readonly ZSqlClient MyDB1;

        public ZUserTable(ZSqlClient db1)
        {
            MyDB1 = db1;
        }

        public string GetUserName(string userId)
        {
            string commandText = "Select Name from AspNetUsers where Id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@id", userId } };

            return MyDB1.QueryString(commandText, parameters);
        }

        public string GetUserId(string userName)
        {
            string commandText = "Select Id from AspNetUsers where UserName = @name";
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@name", userName } };

            return MyDB1.QueryString(commandText, parameters);
        }

        public TUser GetUserById(string userId)
        {
            TUser user = null;
            string commandText = "Select * from AspNetUsers where Id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@id", userId } };

            DataTable dt1 = MyDB1.QueryDataSet(commandText, parameters).Tables[0];
            if (dt1.ZRowsCount() == 1)
            {
                DataRow dr1 = dt1.Rows[0];
                user = (TUser)Activator.CreateInstance(typeof(TUser));
                user.Id = dr1["Id"].ZObjectToString();
                user.UserName = dr1["UserName"].ZObjectToString();
                user.PasswordHash = dr1["PasswordHash"].ZObjectToString();
                user.SecurityStamp = dr1["SecurityStamp"].ZObjectToString();
                user.Email = dr1["Email"].ZObjectToString();
                user.EmailConfirmed = dr1["EmailConfirmed"].ZObjectToBoolean();
                user.PhoneNumber = dr1["PhoneNumber"].ZObjectToString();
                user.PhoneNumberConfirmed = dr1["PhoneNumberConfirmed"].ZObjectToBoolean();
                user.LockoutEnabled = dr1["LockoutEnabled"].ZObjectToBoolean();
                user.LockoutEndDateUtc = dr1["LockoutEndDateUtc"].ZObjectToDateTime();
                user.AccessFailedCount = dr1["AccessFailedCount"].ZObjectToInt();
            }
            return user;
        }

        public List<TUser> GetUserByName(string userName)
        {
            List<TUser> users = new List<TUser>();
            string commandText = "Select * from AspNetUsers where UserName = @name";
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@name", userName } };

            var dt1 = MyDB1.QueryDataSet(commandText, parameters).Tables[0];
            foreach (DataRow row in dt1.Rows)
            {
                TUser user = (TUser)Activator.CreateInstance(typeof(TUser));
                user.Id = row["Id"].ZObjectToString();
                user.UserName = row["UserName"].ZObjectToString();
                user.PasswordHash = row["PasswordHash"].ZObjectToString();
                user.SecurityStamp = row["SecurityStamp"].ZObjectToString();
                user.Email = row["Email"].ZObjectToString();
                user.EmailConfirmed = row["EmailConfirmed"].ZObjectToBoolean();
                user.PhoneNumber = row["PhoneNumber"].ZObjectToString();
                user.PhoneNumberConfirmed = row["PhoneNumberConfirmed"].ZObjectToBoolean();
                user.LockoutEnabled = row["LockoutEnabled"].ZObjectToBoolean();
                user.TwoFactorEnabled = row["TwoFactorEnabled"].ZObjectToBoolean();
                user.LockoutEndDateUtc = row["LockoutEndDateUtc"].ZObjectToDateTime();
                user.AccessFailedCount = row["AccessFailedCount"].ZObjectToInt();
                users.Add(user);
            }
            return users;
        }

        public List<TUser> GetUserByEmail(string email)
        {
            // for Remove unused parameter 'email' if it is not part of a shipped public API.
            _ = email;


            return null;
        }

        public string GetPasswordHash(string userId)
        {
            string commandText = "Select PasswordHash from AspNetUsers where Id = @id";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("@id", userId);
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@id", userId }
            };

            var passHash = MyDB1.QueryString(commandText, parameters);
            if (string.IsNullOrEmpty(passHash))
            {
                return null;
            }
            return passHash;
        }

        public int SetPasswordHash(string userId, string passwordHash)
        {
            string commandText = "Update AspNetUsers set PasswordHash = @pwdHash where Id = @id";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("@pwdHash", passwordHash);
            //parameters.Add("@id", userId);
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@pwdHash", passwordHash },
                { "@id", userId }
            };

            return MyDB1.ExecuteNonQuery(commandText, parameters);
        }

        public string GetSecurityStamp(string userId)
        {
            string commandText = "Select SecurityStamp from AspNetUsers where Id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@id", userId } };
            var result = MyDB1.QueryString(commandText, parameters);
            return result;
        }

        public int Insert(TUser user)
        {
            string commandText = @"Insert into AspNetUsers (UserName, Id, PasswordHash, SecurityStamp,Email,EmailConfirmed,PhoneNumber,PhoneNumberConfirmed, AccessFailedCount,LockoutEnabled,LockoutEndDateUtc,TwoFactorEnabled) values (@name, @id, @pwdHash, @SecStamp,@email,@emailconfirmed,@phonenumber,@phonenumberconfirmed,@accesscount,@lockoutenabled,@lockoutenddate,@twofactorenabled)";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("@name", user.UserName);
            //parameters.Add("@id", user.Id);
            //parameters.Add("@pwdHash", user.PasswordHash);
            //parameters.Add("@SecStamp", user.SecurityStamp);
            //parameters.Add("@email", user.Email);
            //parameters.Add("@emailconfirmed", user.EmailConfirmed);
            //parameters.Add("@phonenumber", user.PhoneNumber);
            //parameters.Add("@phonenumberconfirmed", user.PhoneNumberConfirmed);
            //parameters.Add("@accesscount", user.AccessFailedCount);
            //parameters.Add("@lockoutenabled", user.LockoutEnabled);
            //parameters.Add("@lockoutenddate", user.LockoutEndDateUtc);
            //parameters.Add("@twofactorenabled", user.TwoFactorEnabled);
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@name", user.UserName },
                { "@id", user.Id },
                { "@pwdHash", user.PasswordHash },
                { "@SecStamp", user.SecurityStamp },
                { "@email", user.Email },
                { "@emailconfirmed", user.EmailConfirmed },
                { "@phonenumber", user.PhoneNumber },
                { "@phonenumberconfirmed", user.PhoneNumberConfirmed },
                { "@accesscount", user.AccessFailedCount },
                { "@lockoutenabled", user.LockoutEnabled },
                { "@lockoutenddate", user.LockoutEndDateUtc },
                { "@twofactorenabled", user.TwoFactorEnabled }
            };

            return MyDB1.ExecuteNonQuery(commandText, parameters);
        }

        private int Delete(string userId)
        {
            string commandText = "Delete from AspNetUsers where Id = @userId";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("@userId", userId);
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@userId", userId }
            };

            return MyDB1.ExecuteNonQuery(commandText, parameters);
        }

        public int Delete(TUser user)
        {
            return Delete(user.Id);
        }

        public int Update(TUser user)
        {
            string commandText = @"Update AspNetUsers set UserName = @userName, PasswordHash = @pswHash, SecurityStamp = @secStamp, 
                Email=@email, EmailConfirmed=@emailconfirmed, PhoneNumber=@phonenumber, PhoneNumberConfirmed=@phonenumberconfirmed,
                AccessFailedCount=@accesscount, LockoutEnabled=@lockoutenabled, LockoutEndDateUtc=@lockoutenddate, TwoFactorEnabled=@twofactorenabled  
                WHERE Id = @userId";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("@userName", user.UserName);
            //parameters.Add("@pswHash", user.PasswordHash);
            //parameters.Add("@secStamp", user.SecurityStamp);
            //parameters.Add("@userId", user.Id);
            //parameters.Add("@email", user.Email);
            //parameters.Add("@emailconfirmed", user.EmailConfirmed);
            //parameters.Add("@phonenumber", user.PhoneNumber);
            //parameters.Add("@phonenumberconfirmed", user.PhoneNumberConfirmed);
            //parameters.Add("@accesscount", user.AccessFailedCount);
            //parameters.Add("@lockoutenabled", user.LockoutEnabled);
            //parameters.Add("@lockoutenddate", user.LockoutEndDateUtc);
            //parameters.Add("@twofactorenabled", user.TwoFactorEnabled);
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@userName", user.UserName },
                { "@pswHash", user.PasswordHash },
                { "@secStamp", user.SecurityStamp },
                { "@userId", user.Id },
                { "@email", user.Email },
                { "@emailconfirmed", user.EmailConfirmed },
                { "@phonenumber", user.PhoneNumber },
                { "@phonenumberconfirmed", user.PhoneNumberConfirmed },
                { "@accesscount", user.AccessFailedCount },
                { "@lockoutenabled", user.LockoutEnabled },
                { "@lockoutenddate", user.LockoutEndDateUtc },
                { "@twofactorenabled", user.TwoFactorEnabled }
            };

            return MyDB1.ExecuteNonQuery(commandText, parameters);
        }
    }
}
