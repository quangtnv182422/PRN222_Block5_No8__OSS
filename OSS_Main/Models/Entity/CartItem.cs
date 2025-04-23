using System;
using System.Collections.Generic;

namespace OSS_Main.Models.Entity;

public partial class CartItem
{
    public int CartItemId { get; set; }

    public int? CartId { get; set; }

    public int ProductSpecId { get; set; }

    public int Quantity { get; set; }

    public decimal PriceEachItem { get; set; }

	public string ProductName { get; set; } = null!;

	public string? Description { get; set; }

	public string SpecName { get; set; } = null!;

	public decimal BasePrice { get; set; }

	public decimal? SalePrice { get; set; }

	public virtual Cart Cart { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ProductSpec ProductSpec { get; set; } = null!;
}
