﻿using Microsoft.AspNetCore.Identity;

namespace GammaWear.Models 
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }

        public ApplicationRole(string roleName) : base(roleName) { }
    }
}
