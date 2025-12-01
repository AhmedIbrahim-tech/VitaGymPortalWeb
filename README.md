# 🏋️ VitaGym Portal Web

A comprehensive web application built with **ASP.NET Core MVC** following clean architecture principles to efficiently manage gym operations, including member subscriptions, trainer scheduling, session booking, attendance tracking, payment processing, and financial reporting.

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
- **Attendance Marking:** Mark member attendance for booked sessions

### Membership & Plans
- **Plan Management:** Create and manage membership plans with flexible duration and pricing
- **Plan Activation/Deactivation:** Dynamic control over available membership options
- **Member Membership Tracking:** Comprehensive tracking of member subscriptions and renewals
- **Pricing Flexibility:** Support for various pricing models and promotional plans
- **Membership Lifecycle:** Create, view, and cancel memberships with automatic date calculations

### Attendance Management
- **Check-In System:** Members can check in when visiting the gym
- **Membership Validation:** Automatic validation of active membership before check-in
- **Attendance History:** Track and view member attendance records
- **Member-Specific Reports:** View attendance history for individual members

### Payment Management
- **Payment Processing:** Record and track payments from members
- **Multiple Payment Methods:** Support for Cash, Card, Online, and Bank Transfer
- **Payment History:** Comprehensive payment tracking and reporting
- **Member Payment Reports:** View payment history for individual members
- **Total Payment Calculation:** Calculate total payments by member

### User Management
- **User CRUD Operations:** Create, Read, Update, and Delete system users
- **User Status Management:** Enable/disable user accounts
- **Role Assignment:** Assign roles to users (SuperAdmin, Admin, Trainer, Member)
- **Profile Management:** Users can update their own profiles with photo uploads

### Roles & Permissions
- **Role Management:** Create, edit, and delete roles with user count tracking
- **Permission-Based Authorization:** Granular permission system for fine-grained access control
- **Permission Assignment:** Assign specific permissions to roles
- **Protected Roles:** SuperAdmin role protection with automatic full permissions
- **Permission Modules:** Organized permissions by feature modules (Users, Members, Trainers, Sessions, etc.)

### Analytics & Reporting
- **Dashboard Analytics:** Real-time insights into gym operations, member statistics, and revenue
- **Session Analytics:** Track session attendance, booking rates, and trainer utilization
- **Financial Reports:** Generate comprehensive financial reports for business intelligence
- **Performance Metrics:** Monitor key performance indicators across all operations

### Security & Access Control
- **Role-Based Authorization:** Secure access control with SuperAdmin, Admin, Trainer, and Member roles
- **Permission-Based Authorization:** Fine-grained access control using custom permission attributes
- **Microsoft Identity Integration:** Enterprise-grade authentication and authorization
- **Session Security:** Protected endpoints with proper authorization attributes
- **Global Exception Handling:** Centralized error handling middleware for consistent error responses

---

## 🛠️ Technology Stack

The project leverages modern .NET technologies and best practices:

### Backend & Framework
| Technology | Version | Purpose |
|:----------|:--------|:--------|
| **ASP.NET Core MVC** | 10.0 | Web application framework and MVC pattern implementation |
| **.NET** | 10.0 | Runtime and framework for building modern applications |
| **C#** | Latest | Primary programming language |

### Database & Data Access
| Technology | Version | Purpose |
|:----------|:--------|:--------|
| **SQL Server** | Latest | Primary relational database management system |
| **Entity Framework Core** | 10.0.0 | Object-Relational Mapping (ORM) for database operations |
| **Code First Migrations** | 10.0.0 | Database schema management and versioning |

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
| **EntityMappers** | Custom | Extension methods for object-to-object mapping between ViewModels and Entities |
| **Global Usings** | .NET 10 | Centralized namespace imports per layer |
| **FluentValidation** | 11.3.1 | Fluent validation library for input validation |
| **Bogus** | 35.6.5 | Fake data generator for testing and seeding |

### Identity & Security
| Technology | Version | Purpose |
|:----------|:--------|:--------|
| **Microsoft Identity** | 10.0.0 | Authentication and authorization framework |
| **Role-Based Access Control** | Built-in | Secure role management and authorization |
| **Permission-Based Authorization** | Custom | Fine-grained permission system with custom authorization handlers |

### UI & Notifications
| Technology | Version | Purpose |
|:----------|:--------|:--------|
| **NToastNotify** | 8.0.0 | Toast notification library for user feedback |
| **Razor Views** | 10.0 | Server-side rendering with Razor syntax |
| **Bootstrap** | Latest | Responsive CSS framework for modern UI |
| **JavaScript/jQuery** | Latest | Client-side interactivity and AJAX operations |
| **CSS3** | Latest | Custom styling and responsive design |

### Third-Party Services
| Technology | Purpose |
|:----------|:--------|
| **Email Service** | Email notification service for system communications |
| **Attachment Service** | File upload and management service for photos and documents |

---

## 🏗️ Architecture Overview

The system follows **Clean Architecture** principles with a clear separation of concerns across three main layers:

### Infrastructure Layer
The **Infrastructure** layer is responsible for data persistence and external concerns:

- **Data Access:** Entity Framework Core DbContext and repository implementations
- **Entity Models:** Domain entities representing database tables (Members, Trainers, Sessions, Bookings, Memberships, Plans, Payments, Attendances, etc.)
- **Repository Pattern:** Generic and specific repositories implementing the Unit of Work pattern
- **Data Configurations:** Entity Framework Fluent API configurations for database schema
- **Migrations:** Database migration files for schema evolution
- **Data Seeding:** Initial data seeding for roles, categories, and reference data
- **Constants:** Role and permission constants for authorization

**Key Components:**
- `ApplicationDbContext`: Main database context extending IdentityDbContext with audit field tracking
- `GenericRepository<TEntity>`: Base repository with common CRUD operations
- `UnitOfWork`: Transaction management and repository coordination
- Specific repositories: `MemberRepository`, `TrainerRepository`, `SessionRepository`, `BookingRepository`, `MembershipRepository`, `PlanRepository`, `CategoryRepository`
- Entity configurations for data validation and relationships
- `BaseEntity`: Base class with Id, CreatedAt, and UpdatedAt audit fields

### Core Layer
The **Core** layer contains the business logic and domain rules, organized into feature modules:

- **Module-Based Organization:** Business logic organized by feature modules
- **Business Services:** Service classes implementing business logic and rules
- **Service Interfaces:** Contracts defining service operations
- **ViewModels:** Data Transfer Objects (DTOs) for presentation layer communication
- **Validators:** FluentValidation validators for input validation
- **Entity Mappers:** Extension methods for entity-to-viewmodel transformations
- **Third-Party Services:** Email and attachment services

**Key Modules:**
- **Accounts:** Authentication and login services
- **Analyticals:** Dashboard analytics and reporting
- **Attendances:** Member check-in and attendance tracking
- **Bookings:** Session booking management
- **Members:** Member CRUD operations and health records
- **Memberships:** Membership lifecycle management
- **Payments:** Payment processing and tracking
- **Plans:** Membership plan management
- **Sessions:** Training session management
- **Trainers:** Trainer management
- **UserManagement:** User and role management with permissions

**Key Components:**
- Service classes: `MemberService`, `TrainerService`, `SessionService`, `BookingService`, `MembershipService`, `PaymentService`, `AttendanceService`, `UserManagementService`, `RolePermissionsService`, `AnalyticalService`, `AccountService`
- ViewModels: Organized by module with Create, Update, and View models
- `EntityMappers`: Extension methods for object mapping
- FluentValidation validators for each module
- Business rules and validation logic

### Web Layer
The **Web** layer handles user interaction and presentation:

- **MVC Controllers:** Handle HTTP requests, route to services, and return views
- **Razor Views:** Server-side rendered views for UI presentation
- **Models:** ViewModels and error handling models
- **Static Assets:** CSS, JavaScript, images, and other web resources
- **Configuration:** Application settings and service registration
- **Middleware:** Global exception handling and request processing
- **Authorization:** Custom permission-based authorization attributes and handlers
- **View Components:** Reusable UI components (e.g., UserInfo)

**Key Components:**
- Controllers: `MemberController`, `TrainerController`, `SessionController`, `BookingController`, `MembershipController`, `PlanController`, `UserManagementController`, `RolesPermissionsController`, `ProfileController`, `AccountController`, `SearchController`, `HomeController`, `ErrorController`
- Views: Organized by feature with shared layouts and partial views
- `Program.cs`: Application startup, dependency injection, and middleware configuration
- `GlobalExceptionHandlingMiddleware`: Centralized error handling
- `RequirePermissionAttribute`: Custom authorization attribute for permission-based access control
- `PermissionAuthorizationHandler`: Custom authorization handler for permissions
- Configuration extensions: `ServiceCollectionExtensions`, `ApplicationBuilderExtensions`, `AuthorizationExtensions`, `DatabaseServiceExtensions`
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
├── Infrastructure/                    # Data access and persistence layer
│   ├── Data/                         # Database context and configurations
│   │   ├── ApplicationDbContext.cs  # Main database context
│   │   ├── Configurations/          # EF Core entity configurations
│   │   ├── DataSeed/                # Initial data seeding
│   │   └── Migrations/              # Database migrations
│   ├── Entities/                     # Domain entities
│   │   ├── Users/                   # User-related entities (Member, Trainer, ApplicationUser)
│   │   ├── Sessions/                # Session and Category entities
│   │   ├── Membership/              # Membership, Plan, Payment entities
│   │   ├── Attendances/             # Attendance entity
│   │   ├── HumanResources/         # HR entities (TrainerPayroll, LeaveType, LeaveRequest)
│   │   ├── Shared/                  # BaseEntity, Address
│   │   └── Enums/                   # Enumeration types
│   ├── Repositories/                # Repository pattern implementation
│   │   ├── GenericRepository.cs     # Base repository
│   │   ├── UnitOfWork.cs            # Unit of Work pattern
│   │   └── Specific repositories    # MemberRepository, TrainerRepository, etc.
│   ├── Constants/                   # Roles and Permissions constants
│   └── GlobalUsings.cs              # Global namespace imports
│
├── Core/                             # Business logic layer
│   ├── Modules/                     # Feature-based modules
│   │   ├── Accounts/                # Authentication services
│   │   ├── Analyticals/             # Dashboard analytics
│   │   ├── Attendances/             # Attendance services
│   │   ├── Bookings/                # Booking services
│   │   ├── Members/                 # Member services and validators
│   │   ├── Memberships/             # Membership services
│   │   ├── Payments/                # Payment services
│   │   ├── Plans/                   # Plan services and validators
│   │   ├── Sessions/                # Session services and validators
│   │   ├── Trainers/                # Trainer services and validators
│   │   └── UserManagement/          # User and role management services
│   ├── Mappers/                     # Entity mapping extensions
│   │   └── EntityMappers.cs        # Extension methods for entity-to-viewmodel mapping
│   ├── ThirdParty/                  # Third-party service integrations
│   │   ├── AttachmentService/       # File upload service
│   │   └── Email/                   # Email service
│   └── GlobalUsings.cs              # Global namespace imports
│
└── Web/                              # Presentation layer
    ├── Controllers/                  # MVC controllers
    ├── Views/                        # Razor views organized by feature
    ├── Models/                       # Error handling models
    ├── Attributes/                   # Custom attributes (RequirePermissionAttribute)
    ├── Authorization/                # Custom authorization handlers
    ├── Configurations/               # Service registration extensions
    ├── Helpers/                      # Helper classes (PermissionHelper)
    ├── Middleware/                   # Global exception handling middleware
    ├── ViewComponents/               # Reusable view components
    ├── wwwroot/                     # Static files (CSS, JS, images)
    ├── Program.cs                    # Application startup
    └── GlobalUsings.cs               # Global namespace imports
```

---

## 📝 Notes

- **Clean Code:** The codebase follows SOLID principles and clean code practices
- **Module-Based Architecture:** Core layer is organized into feature modules for better maintainability
- **Global Usings:** Each layer uses `GlobalUsings.cs` to centralize common namespace imports
- **Repository Pattern:** Ensures clean data access abstraction and testability
- **Unit of Work:** Transaction management ensures data consistency
- **Dependency Injection:** All dependencies are injected via constructor injection
- **Migrations:** Database schema is managed through Entity Framework migrations
- **Validation:** FluentValidation is used for comprehensive input validation
- **Error Handling:** Global exception handling middleware provides consistent error responses
- **Permission System:** Fine-grained permission-based authorization for secure access control
- **Audit Fields:** Automatic tracking of CreatedAt and UpdatedAt timestamps via BaseEntity
- **Toast Notifications:** User-friendly feedback using NToastNotify
- **Entity Mappers:** Custom extension methods for clean entity-to-viewmodel transformations

---

## 📄 License

This project is part of a gym management system implementation.

---

**Built with ❤️ using ASP.NET Core MVC and Clean Architecture principles**
