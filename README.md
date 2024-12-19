# BookStore API

## Project Description
The BookStore API provides a comprehensive RESTful interface for managing an online bookstore’s inventory, customer orders, and author information. This project includes both a backend API built with .NET and a client-side frontend implemented in React. The API facilitates operations such as viewing available books, placing orders, and managing author details, while ensuring secure and efficient handling of data.

---

## Features

### Backend (API) Features
- **Books Management**: CRUD operations to manage books.
- **Authors Management**: Manage author profiles and link books to authors.
- **Orders Management**: Place, update, and track customer orders.
- **Authentication & Authorization**:
  - JWT-based authentication.
  - Role-based access control (Admin and Customer roles).
- **Error Handling**:
  - Comprehensive error messages with proper HTTP status codes.
- **CORS Support**: Configured CORS policies to allow specific or all origins.
- **Swagger Integration**: Interactive API documentation for developers.
- **DTOs**: Data Transfer Objects for clean and secure communication between layers.
- **Repository and Unit of Work Pattern**: Ensures a maintainable and scalable codebase.

### Frontend Features (React)
- **Dynamic User Interface**: User-friendly interface to browse books, view authors, and manage orders.
- **State Management**: Efficient state handling using React hooks and context.
- **Authentication**: JWT-based login and role-based access.
- **Integration with Backend API**: Fully integrated with the BookStore API.

---

## Tech Stack

### Backend
- **Language**: C#
- **Framework**: .NET 8
- **Database**: SQL Server
- **Authentication**: JSON Web Tokens (JWT) and ASP.NET Identity
- **Design Patterns**: Repository Pattern, Unit of Work
- **Tools**:
  - Swagger for API Documentation
  - Entity Framework Core for ORM

### Frontend
- **Framework**: React.js
- **Tools**:
  - Axios for API communication
  - React Router for navigation

---

## API Endpoints

### Books
- `GET /books` - Fetch all books.
- `GET /books/{id}` - Fetch details of a specific book.
- `POST /books` - Add a new book (Admin only).
- `PUT /books/{id}` - Update a book (Admin only).
- `DELETE /books/{id}` - Delete a book (Admin only).

### Authors
- `GET /authors` - Fetch all authors.
- `GET /authors/{id}` - Fetch details of a specific author.
- `POST /authors` - Add a new author (Admin only).
- `PUT /authors/{id}` - Update an author (Admin only).
- `DELETE /authors/{id}` - Delete an author (Admin only).

### Orders
- `POST /orders` - Create a new order.
- `GET /orders/{id}` - Fetch details of an order.
- `PUT /orders/{id}` - Update order status (Admin only).

---

## Installation and Setup

### Backend Setup
1. Clone the repository.
2. Navigate to the project directory.
3. Configure the database connection string in `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "bookcon": "Your SQL Server connection string here"
     }
   }
   ```
4. Run database migrations:
   ```bash
   dotnet ef database update
   ```
5. Start the application:
   ```bash
   dotnet run
   ```
6. Access Swagger UI at `http://localhost:<port>/swagger`.

### Frontend Setup
1. Navigate to the `frontend` folder.
2. Install dependencies:
   ```bash
   npm install
   ```
3. Start the React development server:
   ```bash
   npm start
   ```
4. Access the frontend at `http://localhost:3000`.

---

## Usage
- **Admin Role**: Manage books, authors, and orders.
- **Customer Role**: Browse books, view author details, and place orders.

---

## Project Structure

### Backend
- **Controllers**: Handle API endpoints.
- **DTOs**: Data Transfer Objects for clean data exchange.
- **Migrations**: Database migrations for schema changes.
- **Models**: Entity classes for database tables.
- **Repository**: Data access logic.
- **UnitOfWork**: Manages repository instances.
- **Program.cs**: Configures services and middleware.

### Frontend
- **Components**: Reusable UI components.
- **Services**: API communication logic.
- **Pages**: Screens and routes.
- **State**: Application state management.

---

## Example Configuration

### appsettings.json
```json
{
  "ConnectionStrings": {
    "bookcon": "Server=(localdb)\\mssqllocaldb;Database=BookStoreDB;Trusted_Connection=True;"
  },
  "Jwt": {
    "Key": "welcome to my secret key",
    "Issuer": "BookStoreAPI"
  }
}
```

---

## Screenshots
- **Homepage**
- **Book Details**
- **Order Management**

![Screenshot](Screenshot%20(129).png)
![Screenshot](Screenshot%20(130).png)
![Screenshot](Screenshot%20(131).png)

---

## License
This project is licensed under the MIT License.

---

## Contact
For any questions or feedback, please contact Yasmin Gamal Ali.

