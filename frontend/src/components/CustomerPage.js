import React, { useState, useEffect } from 'react';
import Customer from '../services/Customer';

const CustomerPage = () => {
  const [customers, setCustomers] = useState([]);
  const [error, setError] = useState(null);
  const [isAdding, setIsAdding] = useState(false);
  const [isEditing, setIsEditing] = useState(false);
  const [currentCustomer, setCurrentCustomer] = useState(null);
  const [formData, setFormData] = useState({
    fullname: '',
    email: '',
    address: '',
    phonenumber: '',
  });
  const [searchQuery, setSearchQuery] = useState('');

  useEffect(() => {
    
    const fetchCustomers = async () => {
      try {
        const customerList = await Customer.getAllCustomers(); 
        setCustomers(customerList);
      } catch (err) {
        setError('Failed to fetch customers');
        console.error(err);
      }
    };

    fetchCustomers();
  }, []);


  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };


  const handleAddCustomer = async (e) => {
    e.preventDefault();
    try {
      const newCustomer = await Customer.addCustomer(formData); 
      setCustomers([...customers, newCustomer]);
      setIsAdding(false);
      setFormData({ fullname: '', email: '', address: '', phonenumber: '' }); 
    } catch (err) {
      setError('Failed to add new customer');
      console.error(err);
    }
  };

  
  const handleEditSubmit = async (e) => {
    e.preventDefault();
    try {
      const updatedCustomer = await Customer.editProfile(currentCustomer.id, formData); 
      setCustomers(customers.map((cust) => (cust.id === currentCustomer.id ? updatedCustomer : cust)));
      setIsEditing(false);
      setCurrentCustomer(null);
    } catch (err) {
      setError('Failed to update customer');
      console.error(err);
    }
  };

 
  const filteredCustomers = customers.filter((customer) =>
    customer.fullname.toLowerCase().includes(searchQuery.toLowerCase())
  );

  return (
    <div className="max-w-4xl mx-auto p-6 bg-white rounded-lg shadow-lg">
      {error && <p className="text-red-500 text-center text-lg mb-4">{error}</p>}

     
      <div className="mb-6">
        <input
          type="text"
          placeholder="Search customers by id..."
          value={searchQuery}
          onChange={(e) => setSearchQuery(e.target.value)}
          className="w-full p-2 border border-gray-300 rounded-lg"
        />
      </div>

 
      {isAdding && (
        <div className="bg-gray-100 p-6 rounded-lg shadow-md mb-6">
          <h2 className="text-2xl font-semibold text-gray-800 mb-4">Add New Customer</h2>
          <form onSubmit={handleAddCustomer}>
            <div className="mb-4">
              <label htmlFor="fullname" className="block text-gray-700">Full Name</label>
              <input
                type="text"
                id="fullname"
                name="fullname"
                value={formData.fullname}
                onChange={handleChange}
                className="mt-1 block w-full p-2 border border-gray-300 rounded-lg"
                required
              />
            </div>
            <div className="mb-4">
              <label htmlFor="email" className="block text-gray-700">Email</label>
              <input
                type="email"
                id="email"
                name="email"
                value={formData.email}
                onChange={handleChange}
                className="mt-1 block w-full p-2 border border-gray-300 rounded-lg"
                required
              />
            </div>
            <div className="mb-4">
              <label htmlFor="address" className="block text-gray-700">Address</label>
              <input
                type="text"
                id="address"
                name="address"
                value={formData.address}
                onChange={handleChange}
                className="mt-1 block w-full p-2 border border-gray-300 rounded-lg"
                required
              />
            </div>
            <div className="mb-4">
              <label htmlFor="phonenumber" className="block text-gray-700">Phone Number</label>
              <input
                type="text"
                id="phonenumber"
                name="phonenumber"
                value={formData.phonenumber}
                onChange={handleChange}
                className="mt-1 block w-full p-2 border border-gray-300 rounded-lg"
                required
              />
            </div>
            <div className="flex justify-end space-x-4">
              <button
                type="button"
                className="px-4 py-2 bg-gray-300 text-black rounded-lg hover:bg-gray-400"
                onClick={() => setIsAdding(false)}
              >
                Cancel
              </button>
              <button
                type="submit"
                className="px-4 py-2 bg-blue-500 text-white rounded-lg hover:bg-blue-600"
              >
                Add Customer
              </button>
            </div>
          </form>
        </div>
      )}

      
      <div className="customer-list">
        {filteredCustomers.map((customer) => (
          <div key={customer.id} className="bg-gray-100 p-6 rounded-lg shadow-md mb-4">
            <h3 className="text-xl font-semibold text-gray-800">{customer.fullname}</h3>
            <p>Email: {customer.email}</p>
            <p>Address: {customer.address}</p>
            <p>Phone: {customer.phonenumber}</p>
            <button
              className="mt-4 px-4 py-2 bg-blue-500 text-white rounded-lg hover:bg-blue-600 mr-4"
              onClick={() => {
                setCurrentCustomer(customer);
                setFormData({
                  fullname: customer.fullname,
                  email: customer.email,
                  address: customer.address,
                  phonenumber: customer.phonenumber,
                });
                setIsEditing(true);
              }}
            >
              Edit
            </button>
          </div>
        ))}
      </div>

     
      {isEditing && (
        <div className="bg-gray-100 p-6 rounded-lg shadow-md mb-6">
          <h2 className="text-2xl font-semibold text-gray-800 mb-4">Edit Customer</h2>
          <form onSubmit={handleEditSubmit}>
      
            <div className="mb-4">
              <label htmlFor="fullname" className="block text-gray-700">Full Name</label>
              <input
                type="text"
                id="fullname"
                name="fullname"
                value={formData.fullname}
                onChange={handleChange}
                className="mt-1 block w-full p-2 border border-gray-300 rounded-lg"
                required
              />
            </div>
           
            <div className="flex justify-end space-x-4">
              <button
                type="button"
                className="px-4 py-2 bg-gray-300 text-black rounded-lg hover:bg-gray-400"
                onClick={() => setIsEditing(false)}
              >
                Cancel
              </button>
              <button
                type="submit"
                className="px-4 py-2 bg-blue-500 text-white rounded-lg hover:bg-blue-600"
              >
                Save Changes
              </button>
            </div>
          </form>
        </div>
      )}

    
      {!isAdding && !isEditing && (
        <button
          className="mt-6 px-4 py-2 bg-green-500 text-white rounded-lg hover:bg-green-600"
          onClick={() => setIsAdding(true)}
        >
          Add New Customer
        </button>
      )}
    </div>
  );
};

export default CustomerPage;
