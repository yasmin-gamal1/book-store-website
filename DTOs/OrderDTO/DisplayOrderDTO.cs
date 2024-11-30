using System;

namespace BookStoreAPI.DTOs.OrderDTO
{
    public class DisplayOrderDTO
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public DateOnly OrderDate { get; set; }
        public List<DisplayOrderDetailDTO> Books { get; set; } = new List<DisplayOrderDetailDTO>();
    }
}

