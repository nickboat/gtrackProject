using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gtrackProject.Models.account
{
    public class UserAdminModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Pass { get; set; }

        public IEnumerable<RoleAdminModel> usrRoles { get; set; }
    }

    public class RoleAdminModel
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}