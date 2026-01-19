using System;
using System.Collections.Generic;

namespace DeliveryRouteOptimizer
{
    public class Map
    {
        public int MapID { get; set; }
        public string Name { get; set; }
    }

    public class Location
    {
        public int LocationID { get; set; }
        public int MapID { get; set; }
        public string Name { get; set; }
    }

    public class Road
    {
        public int RoadID { get; set; }
        public int MapID { get; set; }
        public int LocationA { get; set; }
        public int LocationB { get; set; }
        public int Distance { get; set; }
    }

    public class RouteRecord
    {
        public int RouteID { get; set; }
        public int MapID { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Path { get; set; }
        public int Distance { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class DeliveryPoint
    {
        public int PointID { get; set; }
        public int MapID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double? Lat { get; set; }
        public double? Lon { get; set; }
        public int LocationID { get; internal set; }
    }
}
