using System;
using System.Collections.Generic;

namespace OSS_Main.Models.Entity;

public partial class OrderItem
{
    public int OrderItemId { get; set; }

    public int CartItemId { get; set; }

    public int OrderId { get; set; }

    public int? FeedbackId { get; set; }

    public virtual CartItem CartItem { get; set; } = null!;

    public virtual Feedback? Feedback { get; set; }

    public virtual Order Order { get; set; } = null!;

}
