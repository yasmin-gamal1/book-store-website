

import api from './api';

class Customer {
  async createCustomer(customerData) {
    this.validateCustomerData(customerData);
    
    try {
        const response = await api.createCustomer(customerData);
        return response;
    } catch (error) {
        console.error('Customer creation failed:', error);
        throw error;
    }
  }

  async editProfile(customerId, profileData) {
    this.validateEditProfileData(profileData);
    
    try {
        const response = await api.updateCustomer(customerId, profileData);
        return response;
    } catch (error) {
        console.error('Profile update failed:', error);
        throw error;
    }
  }

  async changePassword(passwordData) {
    this.validatePasswordChange(passwordData);
    
    try {
        const response = await api.changeCustomerPassword(passwordData);
        return response;
    } catch (error) {
        console.error('Password change failed:', error);
        throw error;
    }
  }

  async getCustomerById(customerId) {
    try {
        const customer = await api.getCustomerById(customerId);
        return customer;
    } catch (error) {
        console.error('Fetch customer failed:', error);
        throw error;
    }
  }

  async getAllCustomers() {
    try {
        const customers = await api.getAllCustomers();
        return customers;
    } catch (error) {
        console.error('Fetch customers failed:', error);
        throw error;
    }
  }

  validateCustomerData(data) {
    const requiredFields = ['email', 'username', 'fullname', 'address', 'phonenumber', 'password'];
    
    requiredFields.forEach(field => {
        if (!data[field]) {
            throw new Error(`${field} is required`);
        }
    });

    if (data.password.length < 6) {
        throw new Error('Password must be at least 6 characters long');
    }

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(data.email)) {
        throw new Error('Invalid email format');
    }
  }

  validateEditProfileData(data) {
    const allowedFields = ['fullname', 'address', 'username', 'email', 'phonenumber'];
    
    
    Object.keys(data).forEach(key => {
        if (!allowedFields.includes(key)) {
            throw new Error(`Invalid field: ${key}`);
        }
    });

    if (data.email) {
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!emailRegex.test(data.email)) {
            throw new Error('Invalid email format');
        }
    }
  }

  validatePasswordChange(data) {
    const requiredFields = ['id', 'oldpassword', 'newpassword'];
    
    requiredFields.forEach(field => {
        if (!data[field]) {
            throw new Error(`${field} is required`);
        }
    });

    if (data.newpassword.length < 6) {
        throw new Error('New password must be at least 6 characters long');
    }

    if (data.oldpassword === data.newpassword) {
        throw new Error('New password cannot be the same as the old password');
    }
  }
}


export default new Customer();
