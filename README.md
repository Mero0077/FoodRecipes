# 🍲 Food Recipe Management System

## 📌 Overview
*Food Recipe Management System* is a simple yet structured backend project designed to practice **Onion Architecture**, **CQRS**, and clean, scalable code patterns.

It enables users to:
- Browse and manage recipes.
- Add, update, or delete recipes.
- Categorize recipes by cuisine, ingredients, or difficulty.
- Maintain clean separation of concerns for easier maintainability and testing.

> ⚙️ *This project is a **work in progress** to deepen practical experience with modern architectural best practices.*

---

## 🧩 Key Features

- **Onion Architecture** for clear separation of concerns.
- **CQRS (Command Query Responsibility Segregation)** for handling reads & writes efficiently.
- Clean domain and application layers with clear interfaces.
- Flexible repository pattern for plugging in any data source.
- Solid foundation to add future features like ratings, user profiles, or advanced search.

---

## 🚀 Tech Stack

- **Backend:** .NET Core (.NET 7+)
- **Database:** SQL Server *(can switch easily thanks to clean architecture)*
- **Patterns:** CQRS, Mediator, Repository, Dependency Injection

---

## 📁 Project Structure Highlights

- **Domain Layer:** Core business models & interfaces.
- **Application Layer:** Commands, Queries, Handlers.
- **Infrastructure Layer:** Data access (EF Core / raw SQL).
- **Presentation Layer:** API Controllers, DTOs.

---

## ✏️ Status

✅ Core CRUD operations  
⚙️ Basic CQRS commands & queries  
🔄 *More features & improvements coming soon…*
