using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreAPI.DTOs.OrderDTO
{
    public class adddetailDTO
    {
        public int book_id { get; set; }

        public int quentity { get; set; }
     
       // public decimal unitprice { get; set; }
    }
}
