# Aquila.Data — Lightweight In-Memory RDBMS with MVC Demo

## Overview

**Aquila.Data** is a small, self-contained, in-memory relational database system implemented in C#.  
It was built as a practical engineering challenge to demonstrate:

- Data modeling and constraint enforcement
- Core database concepts (PK, unique keys, indexes, joins)
- Clear separation of concerns
- Clean MVC usage over a custom data engine

The solution intentionally avoids SQL and external databases to focus on **core reasoning and system design**.

---

## High-Level Architecture

The solution is split into three clear projects:

```
Aquila.Data
│
├── Aquila.Data.Core
│   ├── Engine
│   │   └── DatabaseEngine.cs
│   ├── Storage
│   │   └── Table.cs
│   ├── Query
│   │   └── JoinEngine.cs
│   └── Exceptions
│       └── ConstraintViolationException.cs
│
├── Aquila.Data.REPL
│   └── Program.cs
│
├── Aquila.Data.Presentation (ASP.NET MVC)
│   ├── Controllers
│   │   ├── CustomersController.cs
│   │   └── OrdersController.cs
│   ├── Repositories
│   │   ├── CustomerRepository.cs
│   │   └── OrderRepository.cs
│   ├── Infrastructure
│   │   └── AquilaDatabase.cs
│   ├── Models (ViewModels)
│   │   ├── CustomerViewModel.cs
│   │   └── OrderViewModel.cs
│   └── Views
│       ├── Customers
│       └── Orders
│
└── Aquila.Data.Core.Tests
    └── TableTests.cs
```

---

## Aquila.Data.Core — RDBMS Engine

### Features Implemented

- Table creation with **Primary Key**
- **Unique constraints** (non-PK)
- In-memory **indexing** for PK and unique keys
- CRUD operations
- INNER JOIN support
- Domain-specific exception handling
- Minimal unit tests for constraint enforcement

### Design Notes

- Primary keys are defined **at table creation time**
- PKs are immutable and enforced via indexed lookup
- Unique constraints are enforced separately from PKs
- No SQL parsing — operations are API-driven
- Storage engine is framework-agnostic

---

## Aquila.Data.REPL

A simple console application that demonstrates:

- Creating tables
- Inserting data
- Running INNER JOIN queries
- Verifying constraint behavior interactively

The REPL uses the same `Aquila.Data.Core` engine as the MVC application.

---

## Aquila.Data.Presentation — ASP.NET MVC Demo

### Purpose

To demonstrate real-world usage of the RDBMS through a simple web UI.

### Key Characteristics

- Uses **Repository Pattern** (no CQRS, no MediatR)
- Controllers are thin and orchestration-only
- Repositories encapsulate data access and relationship checks
- In-memory database shared via a single application-wide instance
- Full CRUD for Customers and Orders
- INNER JOIN displayed via Orders listing
- User-friendly validation and error messages

### Referential Validation

When creating an Order:
- If the provided `CustomerId` does not exist, the user receives a clear message:
  > "Customer with the given ID does not exist."

This validation is intentionally handled at the repository level.

---

## Exception Handling Strategy

- Core throws **domain exceptions** (`ConstraintViolationException`)
- MVC translates exceptions into user-facing validation messages
- No database or framework exceptions leak into the UI

---

## Unit Testing

Included minimal tests validating:

- Primary key enforcement
- Unique constraint enforcement

The tests focus on **correctness of rules**, not framework behavior.

---

## About Commits

This repository was submitted with **a single consolidated commit**.

I am fully aware that:
- In professional environments, commits should be **small and frequent**
- Each logical change should ideally be isolated and documented

The single commit approach was chosen here purely to simplify review and submission, not as a reflection of normal workflow practices.

---

## Why This Approach

This implementation prioritizes:

- Clear thinking over feature bloat
- Explicit design decisions
- Ease of reasoning and review
- Practical trade-offs explained in code and structure

---

## How to Run

1. Open the solution in Visual Studio
2. Run `Aquila.Data.REPL` to explore the engine
3. Run `Aquila.Data.Presentation` to use the MVC application

---

## Final Notes

This project is intentionally small but complete.  
It demonstrates how core database concepts can be expressed clearly without relying on external systems or heavy abstractions.

