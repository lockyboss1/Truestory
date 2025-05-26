# Product API

A simple RESTful Web API built with .NET 8 that extends a mock external API ([https://api.restful-api.dev](https://api.restful-api.dev)) by adding filtering, paging, validation, and error handling.

---

## Features

- Retrieve products with optional filtering by name (substring) and pagination.
- Add new products with comprehensive input validation.
- Delete products by ID with proper error handling.
- Clear success/error responses.
- Clean architecture with separation of concerns (Onion Architecture).

---

## Technologies Used

### Refit

[Refit](https://github.com/reactiveui/refit) is a REST library for .NET that turns REST APIs into live interfaces.  
Instead of manually writing HTTP requests, you define a C# interface with attributes representing HTTP verbs and routes.  
Refit generates the implementation that makes calls to the external API.

In this project, `IRestfulApiClient` consumes the external mock API endpoints to Get, Create, and Delete products from [https://api.restful-api.dev/objects](https://api.restful-api.dev/objects).

### FluentValidation

[FluentValidation](https://fluentvalidation.net/) is a .NET library for building strongly typed validation rules using a fluent interface.

It validates incoming product DTOs to ensure required fields are present and values are valid (e.g., no empty strings).  
Validation failures return detailed HTTP 400 responses, improving API reliability and user experience.

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Visual Studio Code](https://code.visualstudio.com/) (or another IDE/editor)
- `dotnet` CLI accessible from the terminal

### Clone the repository

### Restore dependencies

```dotnet restore 
```

### Run the API

```dotnet run
```

### By default, the API will run at: "http://localhost:5000"

# API Endpoints

### GET `/api/Products`
- Retrieves a list of products.
- Optional query parameters:
  - `name` (string): Filter products by name (substring match).
  - `page` (int): Page number (default: 1).
  - `pageSize` (int): Number of items per page (default: 10).

---

### POST `/api/Products`
- Adds a new product.
- Requires:
  - `name`: Non-empty string.
  - `data`: Non-null dictionary with at least one non-empty key-value pair.

---

### DELETE `/api/Products/{id}`
- Deletes a product by ID.
- Requires:
  - `id`: ID of the product to be deleted.
