# 📦 Inventory Management System

This is a **demo Inventory Management System** built with **.NET Core Blazor Server** using a **Clean Architecture, plugin-based approach**.  

The system simulates inventory and product management in a production environment, including purchasing, producing, and selling activities.  

---

## 🚀 Features

- **Inventory Management**  
  - Add and manage product components in the **Inventory** page.  

- **Product Management**  
  - Add products and specify which inventories are required to produce them.  

- **Activities**  
  - **Purchase**: Simulate buying inventory stock.  
  - **Produce**: Simulate producing products using defined inventories.  
  - **Sell**: Simulate selling products.  

- **Reports**  
  - View reports of all activities (purchases, production, and sales).  

- **Authentication**  
  - Signup & login pages for employees and admins.  
  - Microsoft Identity integrated (disabled by default when using the in-memory data store).  

---

## 🛠️ Data Stores

The project supports two data store options:  

1. **In-Memory Data Store** *(default)*  
   - Used for easy configuration and testability.  
   - No setup required.  

2. **EF Core + SQL Server** *(optional)*  
   - For actual data persistence.  
   - To enable:  
     1. Install and configure **SQL Server**.  
     2. Update the connection string in:  
        ```json
        // IMS.WebApp/appSettings.Development.json
        "ConnectionStrings": {
          "InventoryManagement": "Your-SQL-Server-Connection-String"
        }
        ```  
     3. Open **Package Manager Console** in Visual Studio and run:  
        ```powershell
        Add-Migration InitialCreate
        Update-Database
        ```  
     4. Open `Properties/launchSettings.json` and ensure all `ASPNETCORE_ENVIRONMENT` values are set to:  
        ```json
        "Development"
        ```  

---

## 🔑 Authentication Setup (Optional)

By default, Microsoft Identity authentication is disabled in the in-memory setup.  
To enable authentication:  

1. Ensure a working SQL Server database is configured.  
2. Go to the **Pages** folder.  
3. Uncomment the `[Authorize]` attributes, for example:  
   ```csharp
   @attribute [Authorize(Policy = "Inventory")]
   ```  

---

## ⚡ Getting Started

1. Clone the repository.  
2. Open the solution in **Visual Studio 2022**.  
3. Run the project (defaults to **in-memory mode**).  
4. If needed, configure EF Core with SQL Server as described above.  

---

## 📝 Notes

- This project is for **demo/educational purposes**.  
- Uses **Blazor Server** with **Clean Architecture** principles.  
- In-memory mode is recommended for quick testing and demos.  
