namespace b2b_shop_demo_backend.Models
{
    public class PurchaseLimit
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; } = null!;

        public int MonthlyLimitAmount { get; set; } // مثلاً مجموع مبلغ مجاز در ماه
    }
}
