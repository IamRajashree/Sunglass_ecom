
namespace Sunglass_ecom.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public int UserId { get; internal set; }
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal SubTotal { get; set; }
        public object TotalAmount { get; internal set; }
        public DateTime OrderDate { get; internal set; }
        public string Status { get; internal set; }
    }
}
