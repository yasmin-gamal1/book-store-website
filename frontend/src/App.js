import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';


import Navbar from './components/Navbar';
import Books from './components/Books';
import Login from './components/Auth';
import Authors from './components/Authors';
import OrderPage from './components/OrderPage';
import BookstoreHomePage from './components/BookstoreHomePage';
import CustomerPage from './components/CustomerPage';

function App() {
  return (
    <Router>
      <div className="App">
        <Navbar />
        <Routes>
        <Route path="/bookstorehomepage" element={<BookstoreHomePage />} />
          <Route path="/" element={<BookstoreHomePage />} />
          <Route path="/customer" element={<CustomerPage />} />
          <Route path="/books" element={<Books />} />
          <Route path="/login" element={<Login />} />
          <Route path="/authors" element={<Authors />} />
          <Route path="/orderservice" element={<OrderPage />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;

