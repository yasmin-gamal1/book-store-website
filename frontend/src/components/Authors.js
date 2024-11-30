import React, { useState, useEffect } from 'react';
import axios from 'axios';

const API_BASE_URL = 'https://localhost:7259/api';

const Authors = () => {
  const [authors, setAuthors] = useState([]);
  const [newAuthor, setNewAuthor] = useState({
    name: '',
    bio: '',
    age: 0,
  });
  const [editAuthor, setEditAuthor] = useState(null);
  const [loading, setLoading] = useState(false);

 
  useEffect(() => {
    const fetchAuthors = async () => {
      setLoading(true);
      try {
        const response = await axios.get(`${API_BASE_URL}/Authors`);
        setAuthors(response.data);
      } catch (error) {
        console.error('Error fetching authors', error);
      } finally {
        setLoading(false);
      }
    };

    fetchAuthors();
  }, []);

  
  const handleCreateAuthor = async (e) => {
    e.preventDefault();
    try {
      const response = await axios.post(`${API_BASE_URL}/Authors`, newAuthor);
      setAuthors([...authors, response.data]);
      setNewAuthor({ name: '', bio: '', age: 0 });
    } catch (error) {
      console.error('Error creating author', error);
    }
  };


  const handleUpdateAuthor = async (e) => {
    e.preventDefault();
    if (!editAuthor) return;
    try {
      await axios.put(`${API_BASE_URL}/Authors/${editAuthor.id}`, editAuthor);
      setAuthors(
        authors.map((author) =>
          author.id === editAuthor.id ? { ...author, ...editAuthor } : author
        )
      );
      setEditAuthor(null);
    } catch (error) {
      console.error('Error updating author', error);
    }
  };

 
  const handleDeleteAuthor = async (id) => {
    try {
      await axios.delete(`${API_BASE_URL}/Authors/${id}`);
      setAuthors(authors.filter((author) => author.id !== id));
    } catch (error) {
      console.error('Error deleting author', error);
    }
  };

  return (
    <div className="container mx-auto p-6">
      <h1 className="text-3xl font-bold mb-6">Authors Collection</h1>
      {loading ? (
        <p>Loading...</p>
      ) : (
        <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
          {authors.map((author) => (
            <div key={author.id} className="bg-white shadow-md rounded-lg p-4">
              <h2 className="text-xl font-semibold">{author.name}</h2>
              <p>Age: {author.age}</p>
              <p>Bio: {author.bio}</p>
              <p>Number of Books: {author.numberOfBooks}</p>
              <div className="mt-4 space-x-2">
                <button
                  onClick={() => setEditAuthor(author)}
                  className="bg-blue-500 text-white p-2 rounded hover:bg-blue-600"
                >
                  Edit
                </button>
                <button
                  onClick={() => handleDeleteAuthor(author.id)}
                  className="bg-red-500 text-white p-2 rounded hover:bg-red-600"
                >
                  Delete
                </button>
              </div>
            </div>
          ))}
        </div>
      )}
      
      <form onSubmit={handleCreateAuthor} className="mt-6 bg-gray-100 p-6 rounded-lg">
        <h2 className="text-2xl mb-4">Add New Author</h2>
        <div className="space-y-4">
          <input
            type="text"
            placeholder="Author Name"
            value={newAuthor.name}
            onChange={(e) => setNewAuthor({ ...newAuthor, name: e.target.value })}
            className="w-full p-2 border rounded"
          />
          <input
            type="text"
            placeholder="Author Bio"
            value={newAuthor.bio}
            onChange={(e) => setNewAuthor({ ...newAuthor, bio: e.target.value })}
            className="w-full p-2 border rounded"
          />
          <input
            type="number"
            placeholder="Author Age"
            value={newAuthor.age}
            onChange={(e) =>
              setNewAuthor({ ...newAuthor, age: parseInt(e.target.value) })
            }
            className="w-full p-2 border rounded"
          />
          <button
            type="submit"
            className="bg-indigo-600 text-white p-2 rounded hover:bg-indigo-700"
          >
            Add Author
          </button>
        </div>
      </form>
    
      {editAuthor && (
        <form onSubmit={handleUpdateAuthor} className="mt-6 bg-gray-100 p-6 rounded-lg">
          <h2 className="text-2xl mb-4">Edit Author</h2>
          <div className="space-y-4">
            <input
              type="text"
              placeholder="Author Name"
              value={editAuthor.name}
              onChange={(e) =>
                setEditAuthor({ ...editAuthor, name: e.target.value })
              }
              className="w-full p-2 border rounded"
            />
            <input
              type="text"
              placeholder="Author Bio"
              value={editAuthor.bio}
              onChange={(e) =>
                setEditAuthor({ ...editAuthor, bio: e.target.value })
              }
              className="w-full p-2 border rounded"
            />
            <input
              type="number"
              placeholder="Author Age"
              value={editAuthor.age}
              onChange={(e) =>
                setEditAuthor({
                  ...editAuthor,
                  age: parseInt(e.target.value),
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
              onClick={() => setEditAuthor(null)}
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

export default Authors;