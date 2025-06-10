# <div align="center">Welcome to Tixora 🎟️</div>

<!-- Typing effect for a cool introduction -->
<p align="center">
  <a href="https://git.io/typing-svg">
    <img src="https://readme-typing-svg.herokuapp.com?font=Fira+Code&pause=1000&color=F75C7E&center=true&vCenter=true&width=435&lines=Tixora+Ticketing+Platform;Built+with+ASP.NET+MVC+%2B+Stripe;Book+Events+Online+Easily!" alt="Typing SVG" />
  </a>
</p>

---
### 👋 About the Project
**Tixora** is an online ticket booking platform built using **ASP.NET MVC**. It allows users to browse, book, and manage event tickets easily. Admins can create events, Venue, TicketCategory and manage bookings.

---
### 🛠️ Tech Stack
#### Backend
<p align="left">
  <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white" />
  <img src="https://img.shields.io/badge/ASP.NET_MVC-5C2D91?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/Entity_Framework-68217A?style=for-the-badge&logo=entity-framework&logoColor=white" />
</p>

#### Frontend
<p align="left">
  <img src="https://img.shields.io/badge/HTML5-E34F26?style=for-the-badge&logo=html5&logoColor=white" />
  <img src="https://img.shields.io/badge/CSS3-1572B6?style=for-the-badge&logo=css3&logoColor=white" />
  <img src="https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white" />
</p>

#### Payment & Tools
<p align="left">
  <img src="https://img.shields.io/badge/Stripe-6772E5?style=for-the-badge&logo=stripe&logoColor=white" />
  <img src="https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white" />
  <img src="https://img.shields.io/badge/Visual_Studio-5C2D91?style=for-the-badge&logo=visual-studio&logoColor=white" />
</p>

---

### 🚀 Features

#### For Users
- 🔍 Advanced search & filter for events by category, date, venue
- 🎫 Seamless ticket booking experience with selection of seat categories
- 💳 Secure payment processing via Stripe integration
- 📱 Responsive design for mobile and desktop
- 🎁 User dashboard to view purchased tickets and order history
- 👨‍💼 Edit Profile 

#### For Admins
- 🏟️ Create and manage venues with custom seating arrangements
- 🎭 Event management with detailed customization options
- 🎟️ Configure multiple ticket types with pricing tiers
- 👥 User management and role-based access control

---
### 🔧 Setup & Installation

#### Prerequisites
- Visual Studio 2022 or later
- .NET 9.0 SDK or higher
- SQL Server (Express or higher)
- Stripe account (for payment processing)

#### Steps
1. **Clone the repository**
   ```
   git clone https://github.com/MostafaMohsen58/Ticketing-Platform
   cd tixora
   ```

2. **Database Setup**
   ```
   Update-Database
   ```
   This will create and seed the database using Entity Framework migrations.

3. **Configure Stripe API Keys**
   - Create a `.env` file in the root directory
   - Add your Stripe API keys:
     ```
     STRIPE_SECRET_KEY=sk_test_your_secret_key
     STRIPE_PUBLISHABLE_KEY=pk_test_your_publishable_key
     ```

4. **Run the application**
   ```
   dotnet run
   ```
   Or press F5 in Visual Studio.

5. **Access the application**
   - Local development: https://localhost:5001
   - Default admin credentials:
     - Email: admin@example.com
     - Password: Admin@123

---

### 🔐 Authentication & Authorization

Tixora implements Identity for authentication with the following roles:
- **Admin**: Full access to manage all aspects of the system
- **User**: Browse events and make purchases

Role-based permissions are enforced throughout the application to ensure proper access control.

---
### 📞 Contact

Team Members:


- Ahmed Mohamed Ramadan: [LinkedIn](https://www.linkedin.com/in/ahmed-ramadan-28333423a/)
- Dua Khalid Helal: [LinkedIn](https://www.linkedin.com/in/helaldua74/)
- Fatma Abdelhamid Helmy: [LinkedIn](https://www.linkedin.com/in/fatma-abdelhameed-9738821bb/)
- Mostafa Mohsen Elnahas: [LinkedIn](https://www.linkedin.com/in/mostafa-elnahas/)
- Reem Atef Heikal: [LinkedIn](http://www.linkedin.com/in/reem-heikal)




---
<p align="center">
  Made with by ©️Mostafa Elnahas
</p>
