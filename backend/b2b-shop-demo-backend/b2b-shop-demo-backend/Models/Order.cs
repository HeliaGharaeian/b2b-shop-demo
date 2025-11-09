namespace b2b_shop_demo_backend.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
        public int TotalAmount => Items.Sum(i => i.Quantity * i.UnitPrice);
        public string Status { get; set; } = "Pending"; // Pending / Confirmed / Rejected
    }
}
