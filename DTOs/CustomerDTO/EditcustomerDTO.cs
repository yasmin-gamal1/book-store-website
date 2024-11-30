using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.DTOs.CustomerDTO
{
    public class EditcustomerDTO
    {
        [Required]
        public string id { get; set; }
        public string fullname { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
         public string email { get; set; }

        public string address { get; set; }
        [Required]
        public string phonenumber { get; set; }
    }
}
