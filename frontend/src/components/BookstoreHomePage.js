import React from 'react';
import { useNavigate } from 'react-router-dom';
const BookstoreHomePage = () => {

const navigate = useNavigate();

const handleShopNowClick = () => {
  navigate('/books'); 
};

  return (
    <div
      className="relative h-screen bg-cover bg-center"
      style={{
        backgroundImage: `url(https://images.pexels.com/photos/904616/pexels-photo-904616.jpeg?auto=compress&cs=tinysrgb&w=600)`,
      }}
    >
      <div className="absolute inset-0 bg-black bg-opacity-50 flex flex-col justify-center items-center text-center">
        <h1 className="text-white text-5xl font-bold mb-4">
          "Find Your Next Adventure in Every Page"
        </h1>
        <p className="text-white text-lg mb-8 max-w-2xl">
          Welcome to our bookstore! Explore a world of knowledge, inspiration, and imagination.
          Browse through our collection to find your perfect read today.
        </p>
        <button
          className="px-6 py-3 bg-blue-500 text-white text-lg font-semibold rounded-lg shadow-md hover:bg-blue-600"
          onClick={handleShopNowClick}
        >
          Shop Now
        </button>
      </div>
    </div>
  );
};

export default BookstoreHomePage;
