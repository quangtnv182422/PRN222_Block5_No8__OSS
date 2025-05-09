namespace OSS_Main.Models.Entity
{

    public partial class OrderStatus
    {
        public int OrderStatusId { get; set; }
        public string OrderStatusName { get; set; } = null!;
        public string OrderDisplay { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
