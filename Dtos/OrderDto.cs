using PR_103_2019.Models;

namespace PR_103_2019.Dtos
{
    public class OrderDto
    {
        public long Id { get; set; }
        public int ArticleQuantity { get; set; }
        public long ArticleId { get; set; }
        public long BuyerId { get; set; }
        public OrderState Status { get; set; }
        public string Address { get; set; }
        public double TotalPrice { get; set; }
        public string Comment { get; set; }
        public int Quantity { get; set; }
        public DateTime OrdredDate { get; set; }
        public DateTime ArrivalDate { get; set; }
    }
}
