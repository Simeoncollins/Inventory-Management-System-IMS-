# 📦 Inventory Management System (IMS)

This is a demo project built with **.NET Core Blazor Server** using a **Clean Architecture, plugin-based approach**.

The Inventory Management System (IMS) simulates how inventories, products, and activities interact in a production setting.

---

## 🚀 Features

- **Inventory Management**  
  - Add and manage inventories representing product components.

- **Product Management**  
  - Add products, define which inventories are required to produce them.

- **Activities**  
  - **Purchase**: Simulate purchasing inventory stock.  
  - **Produce**: Simulate product creation using available inventories.  
  - **Sell**: Simulate selling finished products.

- **Reports**  
  - Provides detailed activity reports.

- **Authentication & Authorization**  
  - Signup and login pages for employees and admins.  
  - Admin functionality for managing users and assigning departments (requires EF Core).

---

## 🗄️ Data Stores

This project supports **two data store options**:

1. **In-Memory Data Store** (default)  
   - Used here for simplicity and easy configuration.  
   - No setup required.

2. **EF Core Data Store**  
   - For real database persistence with SQL Server.  
   - Requires setup (see instructions below).

---

## ⚙️ Setup Instructions

### Prerequisites
- .NET 8.0 SDK (or compatible runtime)
- Visual Studio 2022 (recommended)
- SQL Server (only required if using EF Core data store)

### Running with In-Memory Store (default)
Simply run the project — no extra configuration required.

### Switching to EF Core + SQL Server
1. Install and configure SQL Server.  
2. Open `IMS.WebApp/appSettings.Development.json`.  
3. Replace the `InventoryManagement` connection string value with your SQL Server connection string.  
4. Open **Package Manager Console** in Visual Studio.  
5. Run the following commands(IMSContext for app data, ApplicationDbContext for identity):  
   ```powershell
   Update-Database -Context IMSContext
   Update-Database -Context ApplicationDbContext
   ```
6. Navigate to the `Properties` folder and open `launchSettings.json`.  
   Replace all values of `ASPNETCORE_ENVIRONMENT` with:  
   ```json
   "Development"
   ```

---

## 🔑 Authentication

- The project uses **Microsoft Identity** for authentication.  
- By default, the `[Authorize]` attributes are commented out so the project runs with the in-memory store.  
- To enable authorization:  
  - Make sure EF Core with SQL Server is configured.  
  - Go under the `Pages` folder and uncomment the `[Authorize]` attributes.  
    Example:  
    ```csharp
    @attribute [Authorize(Policy = "Inventory")]
    ```

---

## 🔐 Admin functionality

The project includes an **Admin** area for managing users and assigning departments. **Admin features require EF Core to be configured** (i.e., the real database, not the in-memory store).

### How to enable and use Admin features

1. **Configure EF Core**  
   - Make sure you have a working SQL Server and the project is configured to use the EF Core data store (see the *EF Core + SQL Server* section above). Run migrations and update the database.

2. **Create a user account**  
   - Register a new user via the signup page.

3. **Assign Admin role to the user (manual step)**  
   - After creating the user, open your browser and navigate to:  
     ```
     https://<your-host>/Account/ManageUsers
     ```
   - Find the created user, click **Manage**, and assign the **Admin** role to that account.

4. **Enable page authorization in code**  
   - Open the project folder:  
     ```
     IMSWebApp/Components/Account/Pages
     ```
   - Find the files `ManageUsers.razor` and `ManageUser.razor`.  
   - Uncomment the authorize attribute at the top of each file so it reads:  
     ```csharp
     @attribute [Authorize(Policy = "Admin")]
     ```
   - Save and rebuild the project. With the attribute enabled, only users with the **Admin** policy/role can access these pages.

5. **What Admin pages do**  
   - The Admin pages are used to assign departments to users. Departments control which pages/features a user may access in the app. Once an account is assigned the Admin role, it can manage user-department assignments via the `ManageUsers` UI.

### Notes & tips
- If you use the in-memory data store, role/permission persistence will not survive application restarts. Use EF Core for persistent roles and users.  
- After enabling authorization attributes and assigning roles, test by logging in as the admin account and verifying you can access `/Account/ManageUsers`.  
- If you see authorization errors, ensure your app is using the correct `ASPNETCORE_ENVIRONMENT` (e.g., `Development`) and the correct connection string for your database.

---

## 🛠️ Tech Stack

- **.NET Core Blazor Server**
- **Clean Architecture (plugin-based)**
- **Entity Framework Core** (for SQL Server option)
- **In-Memory provider** (for test/demo)
- **Microsoft Identity** (for authentication/authorization)

---

## 📄 License

This is a demo project for educational purposes. You may use or modify it freely.
