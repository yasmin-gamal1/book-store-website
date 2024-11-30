import React from 'react';
import { Link } from 'react-router-dom';

const Navbar = () => {
  return (
    <nav className="bg-indigo-600 text-white p-4">
      <div className="container mx-auto flex justify-between items-center">
        <Link to="/" className="text-2xl font-bold">BookStore</Link>
        <div className="space-x-4">
        <Link to="/bookstorehomepage" className="hover:text-indigo-200">BookstoreHome</Link>
        <Link to="/customer" className="hover:text-indigo-200">Customer</Link>
          <Link to="/books" className="hover:text-indigo-200">Books</Link>
          <Link to="/login" className="hover:text-indigo-200">Login</Link>
          <Link to="/authors" className="hover:text-indigo-200">Authors</Link>
          <Link to="/orderservice" className="hover:text-indigo-200">OrderService</Link>
        </div>
      </div>
    </nav>
  );
};

export default Navbar;
