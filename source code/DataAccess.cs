using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DeliveryRouteOptimizer
{
    public static class DataAccess
    {
        private static string MasterConn =
    @"Server=ELITEX840\MSSQLSERVER01;Database=master;Trusted_Connection=True;TrustServerCertificate=True;";


        private static string DbName = "DeliveryRouteDB";

        private static string DbConn =
            @"Server=ELITEX840\MSSQLSERVER01;Database=DeliveryRouteDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public static void EnsureDatabase()
        {
            using (var conn = new SqlConnection(MasterConn))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"
IF DB_ID(N'{DbName}') IS NULL
BEGIN
    CREATE DATABASE [{DbName}];
END";
                    cmd.ExecuteNonQuery();
                }
            }

            using (var conn = new SqlConnection(DbConn))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"

-- MAPS TABLE
IF OBJECT_ID('Maps') IS NULL
BEGIN
CREATE TABLE Maps(
    MapID INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL
);
END

-- LOCATIONS TABLE
IF OBJECT_ID('Locations') IS NULL
BEGIN
CREATE TABLE Locations(
    LocationID INT IDENTITY PRIMARY KEY,
    MapID INT NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    FOREIGN KEY(MapID) REFERENCES Maps(MapID) ON DELETE CASCADE
);
END

-- ROADS TABLE
IF OBJECT_ID('Roads') IS NULL
BEGIN
CREATE TABLE Roads(
    RoadID INT IDENTITY PRIMARY KEY,
    MapID INT NOT NULL,
    LocationA INT NOT NULL,
    LocationB INT NOT NULL,
    Distance INT NOT NULL,
    FOREIGN KEY(MapID) REFERENCES Maps(MapID) ON DELETE CASCADE,
    FOREIGN KEY(LocationA) REFERENCES Locations(LocationID),
    FOREIGN KEY(LocationB) REFERENCES Locations(LocationID)
);
END

-- ROUTES TABLE
IF OBJECT_ID('Routes') IS NULL
BEGIN
CREATE TABLE Routes(
    RouteID INT IDENTITY PRIMARY KEY,
    MapID INT NOT NULL,
    StartName NVARCHAR(200),
    EndName NVARCHAR(200),
    Path NVARCHAR(MAX),
    Distance INT,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY(MapID) REFERENCES Maps(MapID) ON DELETE CASCADE
);
END

-- DELIVERY POINTS TABLE (MINIMAL OPTION A)
IF OBJECT_ID('DeliveryPoints') IS NULL
BEGIN
CREATE TABLE DeliveryPoints(
    PointID INT IDENTITY PRIMARY KEY,
    MapID INT NOT NULL,
    LocationID INT NOT NULL,
    Name NVARCHAR(200),
    FOREIGN KEY(MapID) REFERENCES Maps(MapID) ON DELETE CASCADE,
    FOREIGN KEY(LocationID) REFERENCES Locations(LocationID)
);
END
";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static int CreateMap(string name)
        {
            using (var conn = new SqlConnection(DbConn))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        "INSERT INTO Maps(Name) OUTPUT INSERTED.MapID VALUES(@name)";
                    cmd.Parameters.AddWithValue("@name", name);
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        public static List<Map> GetMaps()
        {
            var list = new List<Map>();
            using (var conn = new SqlConnection(DbConn))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT MapID, Name FROM Maps";
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new Map
                            {
                                MapID = r.GetInt32(0),
                                Name = r.GetString(1)
                            });
                        }
                    }
                }
            }
            return list;
        }

        public static int CreateLocation(int mapId, string name)
        {
            using (var conn = new SqlConnection(DbConn))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        "INSERT INTO Locations(MapID, Name) OUTPUT INSERTED.LocationID VALUES(@map,@name)";
                    cmd.Parameters.AddWithValue("@map", mapId);
                    cmd.Parameters.AddWithValue("@name", name);
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        public static List<Location> GetLocations(int mapId)
        {
            var list = new List<Location>();
            using (var conn = new SqlConnection(DbConn))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        "SELECT LocationID, MapID, Name FROM Locations WHERE MapID=@map";
                    cmd.Parameters.AddWithValue("@map", mapId);
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new Location
                            {
                                LocationID = r.GetInt32(0),
                                MapID = r.GetInt32(1),
                                Name = r.GetString(2)
                            });
                        }
                    }
                }
            }
            return list;
        }

        public static int CreateRoad(int mapId, int aId, int bId, int distance)
        {
            using (var conn = new SqlConnection(DbConn))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        "INSERT INTO Roads(MapID, LocationA, LocationB, Distance) OUTPUT INSERTED.RoadID VALUES(@map,@a,@b,@d)";
                    cmd.Parameters.AddWithValue("@map", mapId);
                    cmd.Parameters.AddWithValue("@a", aId);
                    cmd.Parameters.AddWithValue("@b", bId);
                    cmd.Parameters.AddWithValue("@d", distance);
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        public static List<Road> GetRoads(int mapId)
        {
            var list = new List<Road>();
            using (var conn = new SqlConnection(DbConn))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        "SELECT RoadID, MapID, LocationA, LocationB, Distance FROM Roads WHERE MapID=@map";
                    cmd.Parameters.AddWithValue("@map", mapId);
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new Road
                            {
                                RoadID = r.GetInt32(0),
                                MapID = r.GetInt32(1),
                                LocationA = r.GetInt32(2),
                                LocationB = r.GetInt32(3),
                                Distance = r.GetInt32(4)
                            });
                        }
                    }
                }
            }
            return list;
        }

        public static int SaveRoute(int mapId, string start, string end, string path, int distance)
        {
            using (var conn = new SqlConnection(DbConn))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        "INSERT INTO Routes(MapID, StartName, EndName, Path, Distance) OUTPUT INSERTED.RouteID VALUES(@map,@s,@e,@p,@d)";
                    cmd.Parameters.AddWithValue("@map", mapId);
                    cmd.Parameters.AddWithValue("@s", (object)start ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@e", (object)end ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@p", (object)path ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@d", distance);
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        public static List<RouteRecord> GetRoutes(int mapId)
        {
            var list = new List<RouteRecord>();
            using (var conn = new SqlConnection(DbConn))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        "SELECT RouteID, MapID, StartName, EndName, Path, Distance, CreatedAt FROM Routes WHERE MapID=@map ORDER BY CreatedAt DESC";
                    cmd.Parameters.AddWithValue("@map", mapId);
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new RouteRecord
                            {
                                RouteID = r.GetInt32(0),
                                MapID = r.GetInt32(1),
                                Start = r.IsDBNull(2) ? null : r.GetString(2),
                                End = r.IsDBNull(3) ? null : r.GetString(3),
                                Path = r.IsDBNull(4) ? null : r.GetString(4),
                                Distance = r.IsDBNull(5) ? 0 : r.GetInt32(5),
                                CreatedAt = r.GetDateTime(6)
                            });
                        }
                    }
                }
            }
            return list;
        }

        public static int CreateDeliveryPoint(int mapId, int locationId, string name)
        {
            using (var conn = new SqlConnection(DbConn))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        "INSERT INTO DeliveryPoints(MapID, LocationID, Name) OUTPUT INSERTED.PointID VALUES(@map,@loc,@name)";
                    cmd.Parameters.AddWithValue("@map", mapId);
                    cmd.Parameters.AddWithValue("@loc", locationId);
                    cmd.Parameters.AddWithValue("@name", name);
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        public static List<DeliveryPoint> GetDeliveryPoints(int mapId)
        {
            var list = new List<DeliveryPoint>();
            using (var conn = new SqlConnection(DbConn))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        "SELECT PointID, MapID, LocationID, Name FROM DeliveryPoints WHERE MapID=@map";
                    cmd.Parameters.AddWithValue("@map", mapId);
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new DeliveryPoint
                            {
                                PointID = r.GetInt32(0),
                                MapID = r.GetInt32(1),
                                LocationID = r.GetInt32(2),
                                Name = r.IsDBNull(3) ? null : r.GetString(3)
                            });
                        }
                    }
                }
            }
            return list;
        }
    }
}
