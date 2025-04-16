using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace OSS_Main.Models.Entity;

public partial class AspNetUser : IdentityUser
{
    public bool Gender { get; set; }

    public DateTime Dob { get; set; }

    public DateTime CreatedDate { get; set; }


    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<AspNetRole> Roles { get; set; } = new List<AspNetRole>();
    public virtual ICollection<ReceiverInformation> ReceiverInformations { get; set; } = new List<ReceiverInformation>();
    
}
