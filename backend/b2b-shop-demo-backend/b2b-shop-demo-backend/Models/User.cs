namespace b2b_shop_demo_backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!; // برای دمو فعلاً ساده
        public int CompanyId { get; set; }
        public Company Company { get; set; } = null!;
    }
}
