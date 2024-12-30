# AviApp2

AviApp is an ASP.NET Core API application designed for managing customers.  
The project is developed using .NET 9.0 and includes built-in API documentation with Swagger.

---

## **Current Features**

1. **Customer Management:**
   - Create a new customer.
   - Update existing customer details.
   - Delete a customer by ID.
   - Retrieve a list of all customers.
   - Retrieve details of a specific customer by ID.

2. **Swagger API Documentation and Testing:**
   - Fully documented API using Swagger UI.
   - Ability to send requests (GET, POST, PUT, DELETE) and verify responses directly from the Swagger interface.

3. **Layered Architecture:**
   - **Interfaces:** Define contracts for business logic implementation.
   - **Services:** Implement business logic, including customer management.
   - **Controllers:** Handle HTTP requests and interact with services.

4. **In-Memory Storage:**
   - The application currently uses in-memory storage (lists) instead of a database.

---

## **Technologies and Tools**

- **Language**: C#
- **Framework**: ASP.NET Core 9.0
- **API Documentation**: Swagger with Swashbuckle
- **IDE**: JetBrains Rider
- **Version Control**: Git and GitHub

---

## **How to Run the Application**

1. Ensure you have **.NET 9.0** installed on your system.  
   Verify the installation with the following command:
   ```bash
   dotnet --version
