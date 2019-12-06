using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SimbirsfotStaging10
{
    public static class ApplicationSettings
    {
        public static void SetCustomIdentityOptions(this IdentityOptions opts)
        {
            opts.User.RequireUniqueEmail = true;
            opts.Password.RequireDigit = false;
            opts.Password.RequireLowercase = false;
            opts.Password.RequireUppercase = false;
            opts.Password.RequireNonAlphanumeric = false;
        }
    }
}
