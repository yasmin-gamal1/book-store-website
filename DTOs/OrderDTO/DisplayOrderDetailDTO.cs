namespace BookStoreAPI.DTOs.OrderDTO
{
    public class DisplayOrderDetailDTO
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public int Quantity { get; set; }
    }
}
