using System.ComponentModel.DataAnnotations;

namespace BookStore_API.Models
{
    public class catlog
    {
        public int id { get; set; }
        [StringLength(50)]
        [Required]
        public string name { get; set; }
        public string desc { get; set; }
        public virtual List<Book> Books { get; set; } = new List<Book>();
    }
}
