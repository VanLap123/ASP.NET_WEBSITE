using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WEBGROUP_GCC0903.Models
{
    public class WebApp1User : IdentityUser
{
    [PersonalData]
    public string ? Name { get; set; }

}
}