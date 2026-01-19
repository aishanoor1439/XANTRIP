# 🚀 XANTRIP – Delivery Route Optimization System

![C#](https://img.shields.io/badge/C%23-.NET-blue)
![SQL Server](https://img.shields.io/badge/SQL_Server-Database-red)
![WindowsForms](https://img.shields.io/badge/Windows_Forms-GUI-green)
![Status](https://img.shields.io/badge/Status-Active-success)
![License](https://img.shields.io/badge/License-Academic-lightgrey)

> **XANTRIP** is a delivery route optimization system designed for logistics and courier services. The project leverages advanced graph algorithms, such as Dijkstra's and Travelling Salesman Problem (TSP), along with force-directed graph visualization, to efficiently plan delivery routes.

---

## ✨ Key Highlights

✔ Optimized delivery routes for multiple locations
✔ Interactive visualization of delivery networks
✔ Secure user login and dashboard
✔ Efficient shortest-path and multi-location route planning
✔ Database integration for maps, locations, roads, and routes

---

## 📌 Introduction & Problem

Efficient route planning is critical in logistics, affecting operational costs, fuel consumption, delivery time, and customer satisfaction. While shortest-path problems are addressed by Dijkstra's algorithm, multi-location trips present the Travelling Salesman Problem (TSP). **XANTRIP** automates and optimizes these calculations, providing both computational and visual solutions.

### Problem Statement

Given a set of delivery locations with roads of different distances, the system must:

1. Create and load maps
2. Find the shortest route between two locations
3. Find optimal route covering all locations
4. Visually represent routes and distances
5. Add delivery points
6. Store and retrieve history from a database

---

## 🛠️ Technology & Paradigms

| Area                    | Technology / Paradigm                                 |
| ----------------------- | ----------------------------------------------------- |
| Programming Language    | C# (.NET Framework / .NET Core)                       |
| GUI Framework           | Windows Forms                                         |
| Database                | Microsoft SQL Server                                  |
| Database Connectivity   | ADO.NET                                               |
| Development Environment | Visual Studio                                         |
| Visualization           | System.Drawing (GDI+)                                 |
| Software Design         | Object-Oriented Programming                           |
| User Interaction        | Event-Driven Programming                              |
| Optimization Strategy   | Greedy Paradigm                                       |
| Development Methodology | Modular Development + Integration Testing             |
| Architecture            | Layered (Presentation → Business Logic → Data Access) |

---

## 📦 Algorithms Used

### Graph Representation

* Locations as vertices, roads as edges, distances as weights
* Stored using adjacency lists

### Dijkstra's Algorithm (Shortest Path)

* Finds shortest distance between two locations
* Expands to nearest unvisited nodes, updating distances greedily

### Travelling Salesman Problem (TSP)

* Finds optimal route visiting all locations once with minimum cost
* Implemented using Dynamic Programming with Bitmasking

### Force-Directed Graph Layout

* Visualizes nodes and edges with readable spacing
* Repulsive forces between nodes, spring forces along roads
* Stabilized with damping and gravity towards center

---

## 🖼️ Interfaces & Screens

* **Login Interface** – Secure user authentication
* **Main Dashboard** – Map selection, location/road management, algorithm execution buttons, visualization panel
* **Visualization Panel** – Nodes, edges, shortest path (red), optimal route (green)
* **Database Interface** – SQL Server backend, CRUD operations, persistent storage

---

## ✅ Conclusion

XANTRIP demonstrates practical application of graph algorithms in logistics. Key highlights:

* Strong understanding of algorithms
* Object-oriented design principles
* Database integration
* Interactive, user-friendly UI

**Future Extensions:**

* Real-world maps integration (Google Maps API)
* Heuristic TSP algorithms
* Dynamic delivery constraints

---

## 👩‍💻 Author

**Aisha Noor**
Bachelor in Software Engineering – Bahria University Karachi Campus
Diploma in Software Engineering – Aptech

🔗 LinkedIn: https://www.linkedin.com/in/aisha-noor-3520062a6/

---

## 📄 License

This project is developed for academic purposes.
© 2026 – All Rights Reserved.
