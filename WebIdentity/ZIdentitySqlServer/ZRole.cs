using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add
using Microsoft.AspNet.Identity; // install Microsoft.AspNet.Identity.Core.2.2.1 from NuGet.

namespace ZIdentitySqlServer
{
    // Implements the ASP.NET Identity IRole interface.
    public class ZRole : IRole
    {
        public ZRole()
        {
            Id = Guid.NewGuid().ToString();
        }
        public ZRole(string name) : this()
        {
            Name = name;
        }
        public ZRole(string name, string id)
        {
            Name = name;
            Id = id;
        }


        public string Id { get; set; }

        public string Name { get; set; }
    }
}
