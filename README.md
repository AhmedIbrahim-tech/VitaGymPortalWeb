# 🏋️ VitaGym Portal Web

A comprehensive web application built with **ASP.NET Core MVC** following clean architecture principles to efficiently manage gym operations, including member subscriptions, trainer scheduling, session booking, and financial reporting.

---

## ✨ Key Features

This system provides a full suite of features necessary for modern gym management:

### Member Management
- **Complete CRUD Operations:** Create, Read, Update, and Delete member records with comprehensive validation
- **Health Record Tracking:** Detailed health records for each member, enabling personalized training programs
- **Member Search:** Fast and efficient search functionality by phone ID or name
- **Photo Management:** Upload and manage member profile photos

### Trainer Management
- **Full CRUD Functionality:** Complete trainer lifecycle management
- **Specialty Management:** Assign and track trainer specialties and certifications
- **Scheduling:** Efficient scheduling system for trainer availability
- **Performance Tracking:** Monitor trainer performance and session assignments

### Session Management
- **Session CRUD Operations:** Create and manage training sessions with flexible scheduling
- **Booking System:** Members can book sessions with real-time availability checking
- **Category-Based Organization:** Organize sessions by workout categories
- **Capacity Management:** Track available slots and manage session capacity

### Membership & Plans
- **Plan Management:** Create and manage membership plans with flexible duration and pricing
- **Plan Activation/Deactivation:** Dynamic control over available membership options
- **Member Membership Tracking:** Comprehensive tracking of member subscriptions and renewals
- **Pricing Flexibility:** Support for various pricing models and promotional plans

### Analytics & Reporting
- **Dashboard Analytics:** Real-time insights into gym operations, member statistics, and revenue
- **Session Analytics:** Track session attendance, booking rates, and trainer utilization
- **Financial Reports:** Generate comprehensive financial reports for business intelligence
- **Performance Metrics:** Monitor key performance indicators across all operations

### Security & Access Control
- **Role-Based Authorization:** Secure access control with SuperAdmin and Admin roles
- **Microsoft Identity Integration:** Enterprise-grade authentication and authorization
- **Session Security:** Protected endpoints with proper authorization attributes

---

## 🛠️ Technology Stack

The project leverages modern .NET technologies and best practices:

### Backend & Framework
| Technology | Version | Purpose |
|:----------|:--------|:--------|
| **ASP.NET Core MVC** | 9.0 | Web application framework and MVC pattern implementation |
| **.NET** | 9.0 | Runtime and framework for building modern applications |
| **C#** | Latest | Primary programming language |

### Database & Data Access
| Technology | Version | Purpose |
|:----------|:--------|:--------|
| **SQL Server** | Latest | Primary relational database management system |
| **Entity Framework Core** | 9.0.9 | Object-Relational Mapping (ORM) for database operations |
| **Code First Migrations** | 9.0.9 | Database schema management and versioning |

### Architecture & Design Patterns
| Pattern/Technique | Implementation | Purpose |
|:------------------|:----------------|:--------|
| **Clean Architecture** | Layered separation (Infrastructure, Core, Web) | Separation of concerns and maintainability |
| **Repository Pattern** | Generic and specific repositories | Abstraction layer for data access |
| **Unit of Work** | Transaction management | Ensures data consistency and transaction integrity |
| **Dependency Injection** | Built-in .NET DI container | Inversion of Control for loose coupling |
| **Service Layer** | Business logic encapsulation | Centralized business rules and operations |

### Mapping & Utilities
| Technology | Version | Purpose |
|:----------|:--------|:--------|
| **AutoMapper** | 15.0.1 | Object-to-object mapping between ViewModels and Entities |
| **Global Usings** | .NET 9 | Centralized namespace imports per layer |

### Identity & Security
| Technology | Version | Purpose |
|:----------|:--------|:--------|
| **Microsoft Identity** | 9.0.10 | Authentication and authorization framework |
| **Role-Based Access Control** | Built-in | Secure role management and authorization |

### Frontend & UI
| Technology | Version | Purpose |
|:----------|:--------|:--------|
| **Razor Views** | 9.0 | Server-side rendering with Razor syntax |
| **Bootstrap** | Latest | Responsive CSS framework for modern UI |
| **JavaScript/jQuery** | Latest | Client-side interactivity and AJAX operations |
| **CSS3** | Latest | Custom styling and responsive design |

---

## 🏗️ Architecture Overview

The system follows **Clean Architecture** principles with a clear separation of concerns across three main layers:

### Infrastructure Layer
The **Infrastructure** layer is responsible for data persistence and external concerns:

- **Data Access:** Entity Framework Core DbContext and repository implementations
- **Entity Models:** Domain entities representing database tables
- **Repository Pattern:** Generic and specific repositories implementing the Unit of Work pattern
- **Data Configurations:** Entity Framework Fluent API configurations for database schema
- **Migrations:** Database migration files for schema evolution
- **Data Seeding:** Initial data seeding for roles, categories, and reference data

**Key Components:**
- `GymDbContext`: Main database context extending IdentityDbContext
- `GenericRepository<TEntity>`: Base repository with common CRUD operations
- `UnitOfWork`: Transaction management and repository coordination
- Entity configurations for data validation and relationships

### Core Layer
The **Core** layer contains the business logic and domain rules:

- **Business Services:** Service classes implementing business logic and rules
- **Service Interfaces:** Contracts defining service operations
- **ViewModels:** Data Transfer Objects (DTOs) for presentation layer communication
- **Mapping Profiles:** AutoMapper configurations for entity-to-viewmodel transformations
- **Attachment Services:** File upload and management services

**Key Components:**
- Service classes: `MemberService`, `TrainerService`, `SessionService`, `BookingService`, etc.
- ViewModels: Organized by feature (MemberViewModels, TrainerViewModels, etc.)
- `MappingProfile`: AutoMapper configuration for object mapping
- Business rules and validation logic

### Web Layer
The **Web** layer handles user interaction and presentation:

- **MVC Controllers:** Handle HTTP requests, route to services, and return views
- **Razor Views:** Server-side rendered views for UI presentation
- **Models:** ViewModels and error handling models
- **Static Assets:** CSS, JavaScript, images, and other web resources
- **Configuration:** Application settings and service registration

**Key Components:**
- Controllers: `MemberController`, `TrainerController`, `SessionController`, etc.
- Views: Organized by feature with shared layouts and partial views
- `Program.cs`: Application startup, dependency injection, and middleware configuration
- `GlobalUsings.cs`: Centralized namespace imports for the Web layer

### Data Flow

1. **Request Handling:** HTTP requests are received by MVC Controllers in the Web layer
2. **Service Orchestration:** Controllers delegate business logic to services in the Core layer
3. **Data Access:** Services use repositories from the Infrastructure layer to access data
4. **Response Generation:** ViewModels are created, mapped, and passed to Razor views
5. **View Rendering:** Views render the response using ViewModels and static assets

### Dependency Direction

```
Web Layer
    ↓ (depends on)
Core Layer
    ↓ (depends on)
Infrastructure Layer
```

This ensures that:
- Business logic is independent of data access implementation
- Presentation is independent of business logic details
- Changes in one layer don't cascade to others unnecessarily

---

## 📁 Project Structure

```
VitaGymPortalWeb/
├── Infrastructure/          # Data access and persistence layer
│   ├── Contexts/           # DbContext implementations
│   ├── Entities/           # Domain entities
│   ├── Repositories/      # Repository pattern implementation
│   ├── Data/              # Configurations and seeding
│   └── Migrations/        # Database migrations
│
├── Core/                   # Business logic layer
│   ├── Services/          # Business services and interfaces
│   ├── ViewModels/        # Data transfer objects
│   ├── MappingProfile.cs  # AutoMapper configuration
│   └── GlobalUsings.cs    # Global namespace imports
│
└── Web/                    # Presentation layer
    ├── Controllers/       # MVC controllers
    ├── Views/            # Razor views
    ├── Models/           # View models and error handling
    ├── wwwroot/          # Static files
    ├── Program.cs        # Application startup
    └── GlobalUsings.cs   # Global namespace imports
```

---

## 📝 Notes

- **Clean Code:** The codebase follows SOLID principles and clean code practices
- **Global Usings:** Each layer uses `GlobalUsings.cs` to centralize common namespace imports
- **Repository Pattern:** Ensures clean data access abstraction and testability
- **Dependency Injection:** All dependencies are injected via constructor injection
- **Migrations:** Database schema is managed through Entity Framework migrations

---

## 📄 License

This project is part of a gym management system implementation.

---

**Built with ❤️ using ASP.NET Core MVC and Clean Architecture principles**
