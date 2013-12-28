﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace gtrackProject.Models.account
{
    [NotMapped]
    public class EmployeeAdminModel : Employee
    {
        public string UserName { get; set; }

        public IEnumerable<RoleAdminModel> EmployeeRoles { get; set; }
    }

    public class RoleAdminModel
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}