using BookStore_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Models
{
    public class BookStoreContext : IdentityDbContext
    {

        public BookStoreContext()
        {

        }

        public BookStoreContext(DbContextOptions<BookStoreContext> op) : base(op)
        {

        }


        public virtual DbSet<catlog> Catlogs { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<OrderDetails>().HasKey("order_id", "book_id");
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Name = "admin", NormalizedName = "ADMIN" },
                new IdentityRole() { Name = "customer", NormalizedName = "CUSTOMER" }
                );

            //builder.Entity<Admin>().HasData(

            //    new Admin() { });

        }

    }
}
