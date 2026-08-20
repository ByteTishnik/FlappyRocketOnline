# Flappy Rocket Online

A Flappy Bird-inspired online game built with **C#, Raylib-cs, ASP.NET Core, Entity Framework Core, and PostgreSQL**.

The project combines a desktop game with an authenticated ASP.NET Core Web API, allowing players to register, log in, submit their scores, and compete on an online leaderboard.

---

## 📸 Screenshots

### Main Menu


<img width="1277" height="716" alt="Main Menu" src="https://github.com/user-attachments/assets/6ac97698-08f8-41a6-b2e1-b9a799cbc473" />

### Gameplay


<img width="1274" height="716" alt="Gameplay" src="https://github.com/user-attachments/assets/96134933-24b1-4141-9d96-33f66b668819" />

### Swagger


<img width="1450" height="611" alt="Swagger API" src="https://github.com/user-attachments/assets/6b85b3f5-7f35-4f39-b49f-e8fcc9899d2d" />


### Leaderboard 


---

## ✨ Features


### Gameplay

- Smooth arcade gameplay
- Four difficulty modes:
  - Easy
  - Medium
  - Hard
  - Dynamic — difficulty scales with score
- Procedurally generated pipes
- Pause system
- Score tracking
- Game restart
- Pixel-art style UI
- Background music

### Online Features

- User registration
- User login
- JWT authentication
- Online leaderboard
- Separate leaderboard for each difficulty
- Best score storage
- REST API communication

### Backend

- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL database
- EF Core migrations
- BCrypt password hashing
- JWT authentication
- DTO-based API architecture

---

## 🏗️ Architecture

The project consists of two main parts:

### Game Client

A desktop game built with **C# and Raylib-cs**.

The client handles:

- Game loop
- Rendering
- Player movement
- Pipe generation
- Collision detection
- Difficulty
- Score system
- Communication with the backend API

### Backend

An **ASP.NET Core Web API** responsible for:

- User registration
- Authentication
- JWT token generation
- Score submission
- Leaderboard queries
- Persistent data storage

Entity Framework Core is used as the ORM, with **PostgreSQL** as the database.

---

## 🛠️ Tech Stack

### Game

- C#
- Raylib-cs

### Backend

- C#
- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- Npgsql
- JWT Authentication
- BCrypt

### Development

- Git
- GitHub
- EF Core Migrations

---

## 🗄️ Database

The project originally used SQLite during early development.

The backend has since been migrated to **PostgreSQL** using Entity Framework Core and Npgsql.

The database currently contains:

- `Users`
- `Scores`

Scores are associated with users and include:

- Score
- Date
- Difficulty

EF Core migrations are used to manage the database schema.

---

## 🔐 Authentication

The API uses **JWT-based authentication**.

Passwords are never stored directly. They are hashed using **BCrypt** before being stored in the database.

The authentication flow is:

```text
Register
   ↓
Password hashing
   ↓
PostgreSQL
   ↓
Login
   ↓
JWT token
   ↓
Authenticated API requests