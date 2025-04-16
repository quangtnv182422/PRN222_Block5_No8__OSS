using System;
using System.Collections.Generic;

namespace OSS_Main.Models.Entity;

public partial class ProductSpec
{
    public int ProductSpecId { get; set; }

    public string SpecName { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal BasePrice { get; set; }

    public decimal? SalePrice { get; set; }

    public int ProductId { get; set; }

    public bool ProductStatus { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Product Product { get; set; } = null!;
}
