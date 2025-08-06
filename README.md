# 🍽️ Food Recipe Management System

A scalable and secure backend system for managing food recipes — built with **Clean Architecture**, **CQRS**, and modern best practices.

> 💡 Designed to showcase architectural clarity, real-world features, and extensibility.

---

## ✨ Key Features

- 🔐 **Authentication & Authorization**
  - Secure registration & login with **bcrypt** password hashing.
  - **Dynamic roles** (e.g., Admin, User, Chef) with fine-grained access control.

- 🍱 **Recipe Management**
  - Create, update, delete, and categorize recipes.
  - Associate recipes with cuisines, difficulty levels, ingredients.

- 🔥 **Trending & Hot Recipes**
  - Calculated via background jobs using:
    - ✅ Views count
    - ✅ Likes
    - ✅ Add-to-cart/wishlist interactions

- 🧾 **Wishlist & Favourites**
  - Users can add recipes to personal wishlists.
  - Used as part of the trending algorithm.

- 🧠 **Clean Architecture**
  - Clear separation of concerns between:
    - `Domain` → Business logic
    - `Application` → Use cases (CQRS, Services)
    - `Infrastructure` → EF Core / database / hashing
    - `API` → REST endpoints, DTOs

- ⚙️ **Background Jobs**
  - Hot recipe rankings calculated asynchronously.
  - Modular setup for future jobs (email, reports, etc.).

- 🛠️ **Scalable Codebase**
  - Built with **CQRS**, **MediatR**, and **Repository Pattern**.
  - Easy to extend or swap components (e.g., switch DB or add caching).

---

## 🧪 Tech Stack

| Layer          | Tech                                    |
|----------------|-----------------------------------------|
| Language       | C# (.NET 8+)                            |
| Framework      | ASP.NET Core Web API                    |
| Database       | SQL Server (via EF Core)                |
| Auth & Hashing | JWT + Bcrypt                            |
| Patterns       | Clean Architecture, CQRS, Mediator      |
| Jobs           | Hangfire (or any background job runner) |

---

## 🧱 Project Structure

```
/src
├── Domain
│   ├── Entities
│   ├── Enums
│   └── Interfaces
├── Application
│   ├── Commands
│   ├── Queries
│   ├── DTOs
│   ├── Interfaces
│   └── Services
├── Infrastructure
│   ├── EF DbContext
│   ├── Repositories
│   ├── Auth
│   └── Hashing
└── Presentation (API)
    ├── Controllers
    ├── Middleware
    └── Validators
```


---

## 🧑‍🍳 Example Use Cases

- A chef logs in and adds a new Moroccan Chicken recipe.
- Users add it to wishlist and view it frequently.
- A background job increases its **"hotness" score**, making it trend on the homepage.
- Admin assigns a new "Chef" role dynamically to an active user.

---

## 🚧 Status

✅ Auth, Role System, Recipe CRUD  
✅ CQRS with MediatR  
✅ Trending Recipe Logic (in progress)  
✅ Wishlist & Like Features  
✅ Background Job Integration  

---

