using System;
using System.Collections.Generic;

namespace OSS_Main.Models.Entity;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public int ProductId { get; set; }

    public string? CustomerId { get; set; }

    public string? FeedbackContent { get; set; }

    public string? Status { get; set; }

    public int? RatedStar { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ModifyAt { get; set; }

    public virtual AspNetUser? Customer { get; set; }
    public ICollection<Media> Medias { get; set; } = new List<Media>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Product Product { get; set; } = null!;
}
