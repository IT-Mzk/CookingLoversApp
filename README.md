# Cooking Lovers

A Windows Forms (WinForms) application for food lovers to manage, share, and explore recipes.  
It supports user registration, recipe creation with images, admin controls, and integration with the [TheMealDB](https://www.themealdb.com/api.php) API for discovering meals from around the world.

---

## 📑 Table of Contents

- [Features](#features)  
- [Installation and Setup](#installation-and-setup)  
- [Screenshots](#screenshots)  
- [Technologies Used](#technologies-used)  
- [API Integration](#api-integration)  
- [Author](#author)

---

## 🌟 Features

- 👤 User registration & login system (Admin & User roles)
- 🍽️ Create, edit, delete personal recipes with:
  - Title
  - Ingredients
  - Step-by-step instructions
  - Image upload
- 📸 Display recipe list with image previews
- 🛠️ Admin panel:
  - View and delete users
  - Grant or revoke admin permissions
- 🔍 Real-time recipe search by title
- 🌐 Integrated with [TheMealDB API](https://www.themealdb.com/api.php):
  - Search meals
  - View instructions, ingredients, image
  - Add searched meals to personal recipe list
- 💾 SQL Server database backend
- 🖼️ All recipe data (including sample dishes like *Pho*, *Bun Bo*, *Cha Nem*) loaded dynamically from DB

---

## 🛠 Installation and Setup

1. **Requirements**
   - Visual Studio 2022+
   - .NET 6 or .NET 8
   - SQL Server Management Studio
   - Newtonsoft.Json NuGet package

2. **Clone the Repository**
   ```bash
   git clone https://github.com/yourusername/CookingLovers.git
   cd CookingLovers

   Thiết lập cơ sở dữ liệu

Mở SQL Server và chạy tập lệnh CookingLoversDB.sql được cung cấp để tạo và khởi tạo cơ sở dữ liệu.
Đảm bảo chuỗi kết nối trong RecipeRepository.cs khớp với cài đặt SQL Server của bạn.
Cài đặt phụ thuộc

Mở dự án trong Visual Studio.
Nhấp chuột phải vào giải pháp > Quản lý các gói NuGet > Cài đặt:
Newtonsoft.Json
Xây dựng dự án.
Chạy ứng dụng

Đặt CookingLovers.UI làm dự án khởi động.
Nhấn F5 hoặc nhấp vào Start để khởi chạy.
Tài khoản quản trị mặc định

Tên người dùng: Mindthief
Mật khẩu: 06042004


🧱 Công nghệ được sử dụng
C# (.NET WinForms)
Máy chủ SQL
Newtonsoft.Json
API TheMealDB
Phiên bản Visual Studio 2022
🌐 Tích hợp API
Dự án này sử dụng API TheMealDB để tìm kiếm bữa ăn theo thời gian thực:

Tìm kiếm theo từ khóa
Hiển thị hướng dẫn và thành phần
Thêm bữa ăn vào cơ sở dữ liệu công thức nấu ăn địa phương của bạn
👤 Tác giả
DUC MANH PHAM, DUY MAC
Sinh viên CNTT tại Đại học Công nghệ thông tin và Quản lý, Rzeszów



