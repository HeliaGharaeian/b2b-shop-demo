namespace b2b_shop_demo_backend.Models
{
    public class CreateOrderRequest
    {
        public List<CreateOrderItemDto> Items { get; set; } = new();
    }
}
