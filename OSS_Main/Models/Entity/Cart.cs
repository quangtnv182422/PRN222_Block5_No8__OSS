using System;
using System.Collections.Generic;

namespace OSS_Main.Models.Entity;

public partial class Cart
{
    public int CartId { get; set; }

    public string? CustomerId { get; set; }

    public decimal TotalPrice { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual AspNetUser? Customer { get; set; }
}
