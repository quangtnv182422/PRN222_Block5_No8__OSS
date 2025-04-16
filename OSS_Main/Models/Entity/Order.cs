using System;
using System.Collections.Generic;

namespace OSS_Main.Models.Entity;

public partial class Order
{
    public int OrderId { get; set; }
    public string? OrderCode_GHN { get; set; }
    public DateTime OrderAt { get; set; }

    public string? PaymentMethod { get; set; }

    public string? Notes { get; set; }

    public string? CustomerId { get; set; }

    public string? StaffId { get; set; }

    public int OrderStatusId { get; set; }

    public decimal TotalCost { get; set; }

    public int ReceiverId { get; set; }

    public virtual AspNetUser? Customer { get; set; }

    public virtual ICollection<OrderItem> OrderItemOrders { get; set; } = new List<OrderItem>();

    public virtual ReceiverInformation Receiver { get; set; } = null!;
}
