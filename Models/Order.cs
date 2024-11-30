using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore_API.Models
{
    public class Order
    {
        public int id { get; set; }
        [Column(TypeName = "Date")]
        public DateOnly orderdate { get; set; }
        [Column(TypeName = "money")]
        public decimal totalprice { get; set; }
        public string status { get; set; }
        [ForeignKey("customer")]
        public string cust_id { get; set; }
        public virtual Customer customer { get; set; }

        public virtual List<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
    }
}
