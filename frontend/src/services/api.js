import axios from 'axios';

const API_BASE_URL = 'https://localhost:7259/api';

const api = {
  // Authentication
  login: async (credentials) => {
    try {
      const response = await axios.post(`${API_BASE_URL}/Account`, credentials);
      return response.data;
    } catch (error) {
      console.error('Login failed', error);
      throw error;
    }
  },

  // Books
  getAllBooks: async () => {
    try {
      const response = await axios.get(`${API_BASE_URL}/Books`);
      return response.data; // This should return a list of books
    } catch (error) {
      console.error('Failed to fetch books', error);
      throw error;
    }
  },

  createBook: async (bookData) => {
    try {
      const response = await axios.post(`${API_BASE_URL}/Books`, bookData);
      return response.data; // This should return the created book
    } catch (error) {
      console.error('Failed to create book', error);
      throw error;
    }
  },

  updateBook: async (id, updatedData) => {
    try {
      const response = await axios.put(`${API_BASE_URL}/Books/${id}`, updatedData);
      return response.data; // This should return the updated book
    } catch (error) {
      console.error(`Failed to update book with ID ${id}`, error);
      throw error;
    }
  },

  deleteBook: async (id) => {
    try {
      const response = await axios.delete(`${API_BASE_URL}/Books/${id}`);
      return response.data; // This might return a success message or the deleted book
    } catch (error) {
      console.error(`Failed to delete book with ID ${id}`, error);
      throw error;
    }
  },
  // Authors
  getAllAuthors: async () => {
    try {
      const response = await axios.get(`${API_BASE_URL}/Authors`);
      return response.data;
    } catch (error) {
      console.error('Failed to fetch authors', error);
      throw error;
    }
  },
  createAuthor: async (authorData) => {
    try {
      const response = await axios.post(`${API_BASE_URL}/Authors`, authorData);
      return response.data;
    } catch (error) {
      console.error('Failed to create author', error);
      throw error;
    }
  },
  updateAuthor: async (id, updatedData) => {
    try {
      const response = await axios.put(`${API_BASE_URL}/Authors/${id}`, updatedData);
      return response.data;
    } catch (error) {
      console.error(`Failed to update author with ID ${id}`, error);
      throw error;
    }
  },
  deleteAuthor: async (id) => {
    try {
      const response = await axios.delete(`${API_BASE_URL}/Authors/${id}`);
      return response.data;
    } catch (error) {
      console.error(`Failed to delete author with ID ${id}`, error);
      throw error;
    }
  },
  // Orders
  getAllOrders: async () => {
    try {
        const response = await axios.get(`${API_BASE_URL}/Order`);
        return response.data;
    } catch (error) {
        console.error('Failed to fetch orders', error);
        throw error;
    }
},

getOrderById: async (id) => {
    try {
        const response = await axios.get(`${API_BASE_URL}/Order/${id}`);
        return response.data;
    } catch (error) {
        console.error(`Failed to fetch order with ID ${id}`, error);
        throw error;
    }
},

createOrder: async (orderData) => {
    try {
        const response = await axios.post(`${API_BASE_URL}/Order`, orderData);
        return response.data;
    } catch (error) {
        console.error('Failed to create order', error);
        throw error;
    }
},

updateOrder: async (id, updatedData) => {
    try {
        const response = await axios.put(`${API_BASE_URL}/Order/${id}`, updatedData);
        return response.data;
    } catch (error) {
        console.error(`Failed to update order with ID ${id}`, error);
        throw error;
    }
},

deleteOrder: async (id) => {
    try {
        const response = await axios.delete(`${API_BASE_URL}/Order/${id}`);
        return response.data;
    } catch (error) {
        console.error(`Failed to delete order with ID ${id}`, error);
        throw error;
    }
},
// Customers
createCustomer: async (customerData) => {
  try {
      const response = await axios.post(`${API_BASE_URL}/Customer`, customerData);
      return response.data;
  } catch (error) {
      console.error('Failed to create customer', error);
      throw error;
  }
},

updateCustomer: async (id, updatedData) => {
  try {
      const response = await axios.put(`${API_BASE_URL}/Customer`, {
          ...updatedData,
          id: id
      });
      return response.data;
  } catch (error) {
      console.error(`Failed to update customer with ID ${id}`, error);
      throw error;
  }
},

changeCustomerPassword: async (passwordData) => {
  try {
      const response = await axios.post(`${API_BASE_URL}/Customer/changepassword`, passwordData);
      return response.data;
  } catch (error) {
      console.error('Failed to change customer password', error);
      throw error;
  }
},

getCustomerById: async (id) => {
  try {
      const response = await axios.get(`${API_BASE_URL}/Customer/${id}`);
      return response.data;
  } catch (error) {
      console.error(`Failed to fetch customer with ID ${id}`, error);
      throw error;
  }
},

getAllCustomers: async () => {
  try {
      const response = await axios.get(`${API_BASE_URL}/Customer`);
      return response.data;
  } catch (error) {
      console.error('Failed to fetch customers', error);
      throw error;
  }
}
};

export default api;
