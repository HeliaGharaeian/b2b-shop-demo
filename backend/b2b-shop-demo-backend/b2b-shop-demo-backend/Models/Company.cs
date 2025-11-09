namespace b2b_shop_demo_backend.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<PurchaseLimit> PurchaseLimits { get; set; } = new List<PurchaseLimit>();
    }
}
