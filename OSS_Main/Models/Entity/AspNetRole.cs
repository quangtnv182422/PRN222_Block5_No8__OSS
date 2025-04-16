using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace OSS_Main.Models.Entity;

public partial class AspNetRole : IdentityRole
{
    public virtual ICollection<AspNetUser> Users { get; set; } = new List<AspNetUser>();
}
