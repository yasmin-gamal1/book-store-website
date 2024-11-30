using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreAPI.DTOs.OrderDTO
{
    public class AddOrderDTO
    {
       
       // public decimal totalprice { get; set; }
   
        public string cust_id { get; set; }
       public List<adddetailDTO> books { get; set; } = new List<adddetailDTO>();

    }
}
