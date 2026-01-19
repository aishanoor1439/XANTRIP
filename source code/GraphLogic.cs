using System;
using System.Collections.Generic;
using System.Linq;

namespace DeliveryRouteOptimizer
{
    public class Graph
    {
        Dictionary<string, Dictionary<string, int>> adj = new Dictionary<string, Dictionary<string,int>>();

        public void Clear() => adj.Clear();

        public void BuildFromDb(int mapId, List<Location> locations, List<Road> roads)
        {
            adj.Clear();
            var idToName = locations.ToDictionary(l => l.LocationID, l => l.Name);
            foreach (var loc in locations)
            {
                if (!adj.ContainsKey(loc.Name)) adj[loc.Name] = new Dictionary<string,int>();
            }
            foreach (var r in roads)
            {
                if (!idToName.ContainsKey(r.LocationA) || !idToName.ContainsKey(r.LocationB)) continue;
                var a = idToName[r.LocationA];
                var b = idToName[r.LocationB];
                adj[a][b] = r.Distance;
                adj[b][a] = r.Distance;
            }
        }

        public void AddEdge(string a, string b, int w)
        {
            if (!adj.ContainsKey(a)) adj[a] = new Dictionary<string,int>();
            if (!adj.ContainsKey(b)) adj[b] = new Dictionary<string,int>();
            adj[a][b] = w;
            adj[b][a] = w;
        }

        public List<string> Dijkstra(string start, string end)
        {
            if (!adj.ContainsKey(start) || !adj.ContainsKey(end)) return new List<string>();
            var dist = adj.ToDictionary(x => x.Key, x => int.MaxValue);
            var prev = new Dictionary<string, string>();
            var q = new HashSet<string>(adj.Keys);

            dist[start] = 0;

            while (q.Count > 0)
            {
                var u = q.OrderBy(x => dist[x]).First();
                q.Remove(u);

                if (u == end) break;

                foreach (var v in adj[u])
                {
                    int alt = dist[u] + v.Value;
                    if (alt < dist[v.Key])
                    {
                        dist[v.Key] = alt;
                        prev[v.Key] = u;
                    }
                }
            }

            var path = new List<string>();
            if (!prev.ContainsKey(end) && start != end) 
            {
                if (start == end && adj.ContainsKey(start)) { path.Add(start); return path; }
                return new List<string>(); 
            }
            var curr = end;
            path.Add(curr);
            while (curr != start)
            {
                curr = prev[curr];
                path.Add(curr);
            }
            path.Reverse();
            return path;
        }

        public List<string> GetNodes() => adj.Keys.ToList();
        public int GetWeight(string a, string b) => adj[a][b];
    }

    public class TSP
    {
        public static (List<string> path, int cost) Solve(List<string> nodes, Graph g)
        {
            var bestPath = new List<string>();
            int bestCost = int.MaxValue;

            foreach (var perm in Permutations(nodes))
            {
                int cost = 0;
                bool valid = true;

                for (int i = 0; i < perm.Count - 1; i++)
                {
                    try { cost += g.GetWeight(perm[i], perm[i + 1]); }
                    catch { valid = false; break; }
                }

                if (valid && cost < bestCost)
                {
                    bestCost = cost;
                    bestPath = perm.ToList();
                }
            }
            return (bestPath, bestCost);
        }

        static IEnumerable<List<string>> Permutations(List<string> list)
        {
            if (list.Count == 1) yield return list;
            else
            {
                foreach (var item in list)
                {
                    var remaining = list.Where(x => x != item).ToList();
                    foreach (var perm in Permutations(remaining))
                    {
                        var result = new List<string> { item };
                        result.AddRange(perm);
                        yield return result;
                    }
                }
            }
        }
    }
}
