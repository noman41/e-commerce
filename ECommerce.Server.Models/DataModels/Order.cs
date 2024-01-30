using ECommerce.Server.Models.DTOs;

namespace ECommerce.Server.Models.DataModels
{
    public class Order
    {
        public int Id { get; set; }
        public ICollection<Product>? Products { get; set; }
        public double TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
