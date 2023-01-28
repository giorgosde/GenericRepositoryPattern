## Generic Repository Pattern
Demonstrate the Generic (Base) Repository Pattern. 
This repo is part of a series focusing on .NET/C#.
This repo is meant to be public.

#### Stack and Features:
- .NET 7 (upgraded)
- Entity Framework (InMemory Database)
- Automapper
- XUnit
- FluentAssertions
- Moq

#### Overview:
- GenericRepository implementing common CRUD operations
- VehicleRepository inheriting GenericRepository
- VehicleController CRUD operations
- Async implementation

#### Docker cheatsheet:
- docker build -t generic-repo .
- docker run -p 4000:80 generic-repo