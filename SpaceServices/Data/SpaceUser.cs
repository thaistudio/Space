using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceServices.Data
{
    public class SpaceUser : IdentityUser
    {
        public string Gender { get; set; }
    }
}
