import React, { useState, useEffect } from 'react';
import OrderService from '../services/OrderService';

const OrderPage = () => {
  const [order, setOrder] = useState(null);
  const [orders, setOrders] = useState([]);
  const [error, setError] = useState(null);
  const [isLoading, setIsLoading] = useState(true);
  const [isCreating, setIsCreating] = useState(false);
  const [newOrderData, setNewOrderData] = useState({
    cust_id: '',
    books: [{ book_id: '', quantity: 0 }],
  });


  useEffect(() => {
    const fetchOrder = async () => {
      try {
        const orderData = await OrderService.getOrderById(1); 
        setOrder(orderData);
        setIsLoading(false);
      } catch (err) {
        setError('Failed to fetch order data');
        setIsLoading(false);
        console.error(err);
      }
    };

    fetchOrder();
  }, []);


  useEffect(() => {
    const fetchOrders = async () => {
      try {
        const allOrders = await OrderService.getAllOrders();
        setOrders(allOrders);
      } catch (err) {
        setError('Failed to fetch all orders');
        console.error(err);
      }
    };

    fetchOrders();
  }, []);

 
  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setNewOrderData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

 
  const handleNewOrderSubmit = async (e) => {
    e.preventDefault();
    try {
      const newOrder = await OrderService.createOrder(newOrderData);
      setOrders((prevOrders) => [newOrder, ...prevOrders]);
      setIsCreating(false);
      setNewOrderData({
        cust_id: '',
        books: [{ book_id: '', quantity: 0 }],
      });
    } catch (err) {
      setError('Failed to create order');
      console.error(err);
    }
  };

  return (
    <div className="max-w-4xl mx-auto p-6 bg-white rounded-lg shadow-lg">
      {error && <p className="text-red-500 text-center text-lg mb-4">{error}</p>}


      {isLoading && <p className="text-center text-xl text-gray-500">Loading order data...</p>}
      {!isLoading && order && (
        <div className="order-details bg-gray-100 p-6 rounded-lg shadow-md mb-6">
          <h1 className="text-3xl font-semibold text-gray-800 mb-4">Order Details</h1>
          <p className="text-lg text-gray-700">Order ID: {order.id}</p>
          <p className="text-lg text-gray-700">Customer ID: {order.cust_id}</p>
          <h2 className="text-2xl font-medium text-gray-800 mt-6 mb-2">Books Ordered:</h2>
          <ul className="list-disc pl-6">
            {order.books.map((book, index) => (
              <li key={index} className="text-gray-700 mb-2">
                <span className="font-semibold">{book.book_title}</span> - Quantity: {book.quantity}
              </li>
            ))}
          </ul>
          <p className="text-lg text-gray-700 mt-4">Total Price: ${order.total_price}</p>
        </div>
      )}

    
      <div className="view-all-orders mt-10">
        <h2 className="text-2xl font-semibold text-gray-800 mb-4">All Orders</h2>
        {orders.length > 0 ? (
          <ul className="space-y-4">
            {orders.map((order) => (
              <li key={order.id} className="bg-gray-100 p-4 rounded-lg shadow-md">
                <p className="text-lg text-gray-700">Order ID: {order.id}</p>
                <p className="text-lg text-gray-700">Customer ID: {order.cust_id}</p>
                <p className="text-lg text-gray-700">Total Price: ${order.total_price}</p>
              </li>
            ))}
          </ul>
        ) : (
          <p className="text-center text-lg text-gray-500">No orders found.</p>
        )}
      </div>

   
      {isCreating ? (
        <form onSubmit={handleNewOrderSubmit} className="mt-10 bg-gray-100 p-6 rounded-lg shadow-md">
          <h2 className="text-2xl font-semibold text-gray-800 mb-4">Create New Order</h2>
          <div className="mb-4">
            <label htmlFor="cust_id" className="block text-lg text-gray-700 mb-2">Customer ID</label>
            <input
              type="text"
              id="cust_id"
              name="cust_id"
              value={newOrderData.cust_id}
              onChange={handleInputChange}
              className="w-full p-3 border border-gray-300 rounded-lg"
              required
            />
          </div>
          <div className="mb-4">
            <label htmlFor="book_id" className="block text-lg text-gray-700 mb-2">Book ID</label>
            <input
              type="text"
              id="book_id"
              name="book_id"
              value={newOrderData.books[0].book_id}
              onChange={(e) => {
                const books = [...newOrderData.books];
                books[0].book_id = e.target.value;
                setNewOrderData((prevData) => ({ ...prevData, books }));
              }}
              className="w-full p-3 border border-gray-300 rounded-lg"
              required
            />
          </div>
          <div className="mb-4">
            <label htmlFor="quantity" className="block text-lg text-gray-700 mb-2">Quantity</label>
            <input
              type="number"
              id="quantity"
              name="quantity"
              value={newOrderData.books[0].quantity}
              onChange={(e) => {
                const books = [...newOrderData.books];
                books[0].quantity = e.target.value;
                setNewOrderData((prevData) => ({ ...prevData, books }));
              }}
              className="w-full p-3 border border-gray-300 rounded-lg"
              required
              min="1"
            />
          </div>
          <button type="submit" className="w-full py-3 bg-blue-600 text-white font-semibold rounded-lg">
            Create Order
          </button>
        </form>
      ) : (
        <button
          onClick={() => setIsCreating(true)}
          className="mt-10 py-3 px-6 bg-purple-600 text-white font-semibold rounded-lg"
        >
          Add New Order
        </button>
      )}
    </div>
  );
};

export default OrderPage;
