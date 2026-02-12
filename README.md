📇 MVC-ADMIN-CONTACT-SYSTEM
A powerful, clean, and responsive web application built with ASP.NET Core 8.0 MVC for managing personal and professional contacts. This project demonstrates modern software architecture using the Repository Pattern and PostgreSQL integration.

✨ Features

🔐 User Authentication: Secure registration and login system with session-based management.

📇 Contact Management: Complete CRUD (Create, Read, Update, Delete) functionality for your contact list.

📸 Image Processing: Upload and manage profile pictures for users and unique images for each contact.

📱 Responsive Design: A sleek, mobile-friendly UI crafted with Bootstrap.

🏗️ Clean Architecture: Organized using the Repository Pattern to ensure the code is maintainable and testable.

🐘 Database Persistence: Reliable data storage using PostgreSQL.

🛠️ Technologies Used 
Layer,Technology
Backend,ASP.NET Core 8.0 (MVC)
Language,C#
Database,PostgreSQL
Data Provider,Npgsql
Frontend,"Bootstrap 5, jQuery, HTML5, CSS3"
Architecture,Repository Pattern



    
🚀 Getting Started
Prerequisites
.NET 8.0 SDK

PostgreSQL 12+

Visual Studio 2022 or VS Code


🛠️ Installation
Clone the Repository:
git clone <repository-url>
cd Ketan_MVC

Restore NuGet Packages:
dotnet restore


🗄️ Database Setup
1. Create a PostgreSQL database named Demo.

2. Open MVC/appsettings.json and update your credentials:
"ConnectionStrings": {
  "pgconn": "Host=localhost;Port=5432;Database=Demo;Username=YOUR_USERNAME;Password=YOUR_PASSWORD"
}

3. Run migrations or execute the provided SQL scripts to set up the tables.


🏃 Running the App
1. Navigate to the MVC project folder:
   cd MVC

2. Start the application:
   dotnet run

3. Open your browser to: https://localhost:5001


📖 How to Use
Register: Create a new admin account.

Login: Access your personalized dashboard.

Add Contacts: Click "Add New" to save a contact with their name, details, and a photo.

Manage: View your list, update existing details, or remove contacts you no longer need.

   
   

