import axios from 'axios';

const API_BASE_URL = 'https://localhost:7259/api';

class OrderService {

    async createOrder(orderData) {
        try {
            this.validateOrderData(orderData);
            const response = await axios.post(`${API_BASE_URL}/Orders`, orderData);
            return response.data;
        } catch (error) {
            console.error('Error creating order:', error);
            throw error;
        }
    }

   
    async getOrderById(orderId) {
        try {
            const response = await axios.get(`${API_BASE_URL}/Orders/${orderId}`);
            return response.data;
        } catch (error) {
            console.error('Error fetching order:', error);
            throw error;
        }
    }

   
    async getAllOrders() {
        try {
            const response = await axios.get(`${API_BASE_URL}/Orders`);
            return response.data;
        } catch (error) {
            console.error('Error fetching orders:', error);
            throw error;
        }
    }

    
    async updateOrder(orderId, orderData) {
        try {
            this.validateOrderData(orderData);
            const response = await axios.put(`${API_BASE_URL}/Orders/${orderId}`, orderData);
            return response.data;
        } catch (error) {
            console.error('Error updating order:', error);
            throw error;
        }
    }

   
    async cancelOrder(orderId) {
        try {
            const response = await axios.delete(`${API_BASE_URL}/Orders/${orderId}`);
            return response.data;
        } catch (error) {
            console.error('Error cancelling order:', error);
            throw error;
        }
    }


    validateOrderData(orderData) {
    
        if (!orderData.cust_id) {
            throw new Error('Customer ID is required');
        }

        if (!orderData.books || orderData.books.length === 0) {
            throw new Error('At least one book must be added to the order');
        }

       
        orderData.books.forEach(book => {
            if (!book.book_id || !book.quantity) {
                throw new Error('Each book must have a book ID and quantity');
            }
            if (book.quantity <= 0) {
                throw new Error('Book quantity must be greater than zero');
            }
        });

        return true;
    }

    
    prepareOrderData(customerId, books) {
        return {
            cust_id: customerId,
            books: books.map(book => ({
                book_id: book.bookId,
                quantity: book.quantity
            }))
        };
    }
}

export default new OrderService();
