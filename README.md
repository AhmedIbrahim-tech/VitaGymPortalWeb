# 🏋️ Gym Management System

A comprehensive web application built with **ASP.NET Core MVC** to efficiently manage gym operations, including member subscriptions, trainer scheduling, session booking, and financial reporting.

---

## ✨ Key Features

This system provides a full suite of features necessary for modern gym management:

* **Member Management:** Full CRUD (Create, Read, Update, Delete) functionality, including detailed tracking of **Health Records** for each member.
* **Trainer Management:** Full CRUD for trainers, plus management of their **Specialties** and **Scheduling**.
* **Session Management:** Full CRUD for sessions, along with **Scheduling** and the ability for members to process **Bookings**.
* **Plan Management:** Management of membership plans, including the ability to **Activate/Deactivate** plans and define their **Duration** and **Price**.
* **Analytics & Reporting:** A dedicated **Dashboard** for viewing key analytics and generated reports.

### Additional Implemented Features

* **Authentication & Authorization:** Integrated **Microsoft Identity Package** for secure user login and role-based access control.
* **Member Search:** Added a new fast feature to search for members by their **Phone ID**.

---

## 🛠️ Technology Stack

The project is built on a clean, layered architecture using industry-standard Microsoft and open-source technologies:

| Category | Technology / Pattern | Description |
| :--- | :--- | :--- |
| **Backend Framework** | **ASP.NET Core MVC** | Handles the web application structure and request pipeline. |
| **Database** | **SQL Server** | The primary relational data store. |
| **ORM** | **Entity Framework Core** | Used for database interaction via Code First approach. |
| **Design Pattern** | **Repository Pattern + Unit of Work** | Provides an abstraction layer for data access and ensures transaction integrity. |
| **Inversion of Control** | **Dependency Injection** | Core pattern for managing service lifecycles and decoupling components. |
| **Mapping** | **AutoMapper** | Used to simplify data transfer and mapping between **ViewModels** and **Entities**. |
| **Frontend** | **Bootstrap + Razor Views** | Used for the responsive and dynamic User Interface (UI). |

---

## 🏗️ Architecture Overview

The system adheres to a layered architecture for separation of concerns, making the codebase scalable and maintainable:

* **Presentation Layer (MVC):** Handled by **MVC Controllers** and **Razor Views**. This layer is responsible for routing user requests, validating input, and rendering the UI.
* **Business Logic Layer (Services):** Handled by the **Services** project. This is where the core logic resides, orchestrating data flow and applying business rules.
* **Data Access Layer:** Managed by the **Repository** pattern and **Entity Framework Core**. This layer abstracts database operations, ensuring clean access and manipulation of data entities.

---

## 🚀 Getting Started

Follow these steps to set up and run the project locally.

### Prerequisites

* **.NET 9.0 SDK** (or newer)
* **SQL Server LocalDB** or a configured SQL Server instance
* **Visual Studio** (recommended IDE)

### Installation & Setup

1.  **Clone the Repository:**
    ```bash
    git clone [https://github.com/YounisSaid/GymMangementMVC](https://github.com/YounisSaid/GymMangementMVC)
    ```

2.  **Configure Database Connection:**
    * Open **`appsettings.json`** and **`appsettings.Development.json`** in the `GymManagmentMVC` project.
    * Update the `ConnectionStrings` section with your local SQL Server details.

3.  **Run Migrations:**
    Open the terminal in the solution directory and apply the database schema changes and initial data (which includes the user roles):
    ```bash
    dotnet ef database update --project GymManagmentDAL
    ```

4.  **Run the Application:**
    Run the main MVC project:
    ```bash
    dotnet run --project GymManagmentMVC
    ```
    The application should launch in your web browser.

---

## 👤 Initial Login Credentials

Use the following credentials to access the application and explore the different user roles:

| Role | Email | Password |
| :--- | :--- | :--- |
| **Super Admin** | `YounisTest@gmail.com` | `P@ssw0rd` |
| **Admin** | `MoazMostafa@gmail.com` | `P@ssw0rd` |

---

