namespace PR_103_2019.Models
{
    public class Order
    {
        public long Id { get; set; }
        public Article Article { get; set; }
        public User Buyer { get; set; }
        public ArticleState Status { get; set; }
        public string Address { get; set; }
        public int Quantity { get; set; }
        public DateTime OrdredDate { get; set; }
        public DateTime ArrivalDate { get; set; }

    }
}
