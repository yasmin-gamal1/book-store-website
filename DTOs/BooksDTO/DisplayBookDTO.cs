using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookStoreAPI.DTOs.BooksDTO
{
    public class DisplayBookDTO
    {
        public int id { get; set; }
       
        public string title { get; set; }

        [JsonIgnore]
        public decimal price { get; set; }
        public int stock { get; set; }
       
        public DateOnly publishdate { get; set; }

        public string authorname { get; set; }
        public string catalog { get; set; }
    }
}
