using BookStore_API.Models;
using BookStoreAPI.Models;
using BookStoreAPI.Repository;

namespace BookStoreAPI.UnitOfWork
{
    public class UnitOFWork
    {
        private readonly BookStoreContext db;
        private GenericRepository<Book> booksRepository;
        private GenericRepository<Order> orderRepository;
        private GenericRepository<OrderDetails> orderDetailsRepository;
        private GenericRepository<Author> authorsRepository;

        public UnitOFWork(BookStoreContext db)
        {
            this.db = db;
        }

        public GenericRepository<Book> BooksRepository
        {
            get
            {
                if (booksRepository == null)
                {
                    booksRepository = new GenericRepository<Book>(db);
                }
                return booksRepository;
            }
        }

        public GenericRepository<Order> OrderRepository
        {
            get
            {
                if (orderRepository == null)
                {
                    orderRepository = new GenericRepository<Order>(db);
                }
                return orderRepository;
            }
        }

        public GenericRepository<OrderDetails> OrderDetailsRepository
        {
            get
            {
                if (orderDetailsRepository == null)
                {
                    orderDetailsRepository = new GenericRepository<OrderDetails>(db);
                }
                return orderDetailsRepository;
            }
        }

        public GenericRepository<Author> AuthorsRepository
        {
            get
            {
                if (authorsRepository == null)
                {
                    authorsRepository = new GenericRepository<Author>(db);
                }
                return authorsRepository;
            }
        }

        public void savechanges()
        {
            db.SaveChanges();
        }
    }
}
