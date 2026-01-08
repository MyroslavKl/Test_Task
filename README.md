**Employee Hierarchy API**

A high-performance RESTful API built to manage and visualize employee organizational structures using a tree-based hierarchy.

Key Technical Features

Recursive CTE (Common Table Expression):

Fetching the entire branch of a tree in a single database round-trip to prevent the "N+1 query" performance bottleneck.

Linear Tree Construction ($O(N)$): 

Utilizing ILookup for optimal in-memory hierarchy building, ensuring linear time complexity regardless of tree depth.

Global Exception Handling: 

Implemented via IExceptionHandler (.NET 8), mapping custom exceptions to the ProblemDetails standard.

Resilience & Performance: 

Full CancellationToken propagation from the API layer down to the SQL command to save server resources on client-side cancellations.

**Tech Stack**

Runtime: .NET 8

Data Access: ADO.NET (Microsoft.Data.SqlClient)

Testing: xUnit, Moq

Documentation: Swagger / OpenAPI
