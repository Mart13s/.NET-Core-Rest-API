using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartynasDRestAPI.Auth.Model
{
    public class RestUserRoles
    {
        public const string Admin = nameof(Admin);
        public const string RegisteredCustomer = nameof(RegisteredCustomer);

        public static readonly IReadOnlyCollection<string> All = new[]
        {
            Admin,
            RegisteredCustomer
        };
     }
}
