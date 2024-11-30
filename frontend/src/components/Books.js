import React, { useState, useEffect } from 'react';
import api from '../services/api';

const Books = () => {
  const [books, setBooks] = useState([]);
  const [newBook, setNewBook] = useState({
    title: '',
    author: '',
    price: 0,
  });
  const [editBook, setEditBook] = useState(null);
  const [loading, setLoading] = useState(false);

  
  useEffect(() => {
    const fetchBooks = async () => {
      setLoading(true);
      try {
        const fetchedBooks = await api.getAllBooks();
        setBooks(fetchedBooks);
      } catch (error) {
        console.error('Error fetching books', error);
      } finally {
        setLoading(false);
      }
    };

    fetchBooks();
  }, []);

  
  const handleCreateBook = async (e) => {
    e.preventDefault();
    try {
      const createdBook = await api.createBook(newBook);
      setBooks([...books, createdBook]);
      setNewBook({ title: '', author: '', price: 0 });
    } catch (error) {
      console.error('Error creating book', error);
    }
  };


  const handleUpdateBook = async (e) => {
    e.preventDefault();
    if (!editBook) return;
    try {
      await api.updateBook(editBook.id, editBook);
      setBooks(
        books.map((book) =>
          book.id === editBook.id ? { ...book, ...editBook } : book
        )
      );
      setEditBook(null);
    } catch (error) {
      console.error('Error updating book', error);
    }
  };

  const handleDeleteBook = async (id) => {
    try {
      await api.deleteBook(id);
      setBooks(books.filter((book) => book.id !== id));
    } catch (error) {
      console.error('Error deleting book', error);
    }
  };

  return (
    <div className="container mx-auto p-6">
      <h1 className="text-3xl font-bold mb-6">Books Collection</h1>
      {loading ? (
        <p>Loading...</p>
      ) : (
        <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
          {books.map((book) => (
            <div key={book.id} className="bg-white shadow-md rounded-lg p-4">
              <h2 className="text-xl font-semibold">{book.title}</h2>
              <p>Author: {book.author}</p>
              <p>Price: ${book.price}</p>
              <div className="mt-4 space-x-2">
                <button
                  onClick={() => setEditBook(book)}
                  className="bg-blue-500 text-white p-2 rounded hover:bg-blue-600"
                >
                  Edit
                </button>
                <button
                  onClick={() => handleDeleteBook(book.id)}
                  className="bg-red-500 text-white p-2 rounded hover:bg-red-600"
                >
                  Delete
                </button>
              </div>
            </div>
          ))}
        </div>
      )}
     
      <form onSubmit={handleCreateBook} className="mt-6 bg-gray-100 p-6 rounded-lg">
        <h2 className="text-2xl mb-4">Add New Book</h2>
        <div className="space-y-4">
          <input
            type="text"
            placeholder="Book Title"
            value={newBook.title}
            onChange={(e) => setNewBook({ ...newBook, title: e.target.value })}
            className="w-full p-2 border rounded"
          />
          <input
            type="text"
            placeholder="Author"
            value={newBook.author}
            onChange={(e) => setNewBook({ ...newBook, author: e.target.value })}
            className="w-full p-2 border rounded"
          />
          <input
            type="number"
            placeholder="Price"
            value={newBook.price}
            onChange={(e) =>
              setNewBook({ ...newBook, price: parseFloat(e.target.value) })
            }
            className="w-full p-2 border rounded"
          />
          <button
            type="submit"
            className="bg-indigo-600 text-white p-2 rounded hover:bg-indigo-700"
          >
            Add Book
          </button>
        </div>
      </form>
     
      {editBook && (
        <form onSubmit={handleUpdateBook} className="mt-6 bg-gray-100 p-6 rounded-lg">
          <h2 className="text-2xl mb-4">Edit Book</h2>
          <div className="space-y-4">
            <input
              type="text"
              placeholder="Book Title"
              value={editBook.title}
              onChange={(e) =>
                setEditBook({ ...editBook, title: e.target.value })
              }
              className="w-full p-2 border rounded"
            />
            <input
              type="text"
              placeholder="Author"
              value={editBook.author}
              onChange={(e) =>
                setEditBook({ ...editBook, author: e.target.value })
              }
              className="w-full p-2 border rounded"
            />
            <input
              type="number"
              placeholder="Price"
              value={editBook.price}
              onChange={(e) =>
                setEditBook({
                  ...editBook,
                  price: parseFloat(e.target.value),
                })
              }
              className="w-full p-2 border rounded"
            />
            <button
              type="submit"
              className="bg-green-500 text-white p-2 rounded hover:bg-green-600"
            >
              Save Changes
            </button>
            <button
              onClick={() => setEditBook(null)}
              className="bg-gray-500 text-white p-2 rounded hover:bg-gray-600 ml-2"
            >
              Cancel
            </button>
          </div>
        </form>
      )}
    </div>
  );
};

export default Books;