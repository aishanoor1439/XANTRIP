DeliveryRouteOptimizer (SQL Server) - Project

Features implemented:
- Save and load Maps (multiple maps)
- Save Locations and Roads (graph) to SQL Server (LocalDB)
- Load map data into graph
- Compute shortest path (Dijkstra) and save route history
- Compute TSP (brute force) and save route history
- Save Delivery Points (customers) per map
- Database creation script runs automatically using LocalDB (MSSQLLocalDB)

Requirements:
- Windows with .NET 6 SDK
- Visual Studio 2022+ recommended
- LocalDB instance (MSSQLLocalDB). Visual Studio typically installs this.
- The project uses System.Data.SqlClient package.

How to run:
1. Open the .csproj in Visual Studio.
2. Build and Run. The app will create database DeliveryRouteDB in LocalDB and create tables.
3. Create a map, add locations, add roads, load map, and compute routes.

Notes:
- No user authentication implemented (per request).
- If you want SQL Server full instance, change connection strings in DataAccess.cs (MasterConn and DbConn).
