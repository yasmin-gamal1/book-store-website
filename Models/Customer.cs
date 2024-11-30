using Microsoft.AspNetCore.Identity;

namespace BookStore_API.Models
{
    public class Customer:IdentityUser
    {
        internal string fullname;

        public string fullnamee { get; set; }
        public string address { get; set; }
        public virtual List<Order> Orders { get; set; } = new List<Order>();
    }
}
