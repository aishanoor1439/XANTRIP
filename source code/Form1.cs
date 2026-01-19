using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DeliveryRouteOptimizer
{
    public partial class Form1 : Form
    {
        Graph graph = new Graph();
        int currentMapId = -1;
        List<Location> currentLocations = new List<Location>();
        List<Road> currentRoads = new List<Road>();
        private VisualizationPanel visualPanel;


        public Form1()
        {
            InitializeComponent();
            try
            {
                DataAccess.EnsureDatabase();
                LoadMaps();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database initialization error: " + ex.Message);
            }
        }

        private void LoadMaps()
        {
            cmbMaps.Items.Clear();
            var maps = DataAccess.GetMaps();
            foreach (var m in maps)
                cmbMaps.Items.Add(new ComboBoxItem(m.Name, m.MapID));

            if (cmbMaps.Items.Count > 0)
                cmbMaps.SelectedIndex = 0;
        }

        private void btnCreateMap_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMapName.Text))
            {
                MessageBox.Show("Enter map name");
                return;
            }

            int id = DataAccess.CreateMap(txtMapName.Text.Trim());
            LoadMaps();
            MessageBox.Show("Map created with ID: " + id);
        }

        private void btnLoadMap_Click(object sender, EventArgs e)
        {
            if (cmbMaps.SelectedItem == null)
            {
                MessageBox.Show("Select a map");
                return;
            }

            currentMapId = ((ComboBoxItem)cmbMaps.SelectedItem).Value;

            currentLocations = DataAccess.GetLocations(currentMapId);
            currentRoads = DataAccess.GetRoads(currentMapId);

            RefreshLists();

            graph.BuildFromDb(currentMapId, currentLocations, currentRoads);

            // Update visualization
            visualPanel.Locations = currentLocations;
            visualPanel.Roads = currentRoads;
            visualPanel.StartLayout();
            visualPanel.HighlightedPath.Clear();
            visualPanel.Invalidate(); // force redraw


            MessageBox.Show($"Map loaded. Locations: {currentLocations.Count}, Roads: {currentRoads.Count}");
        }

        private void btnAddLocation_Click(object sender, EventArgs e)
        {
            if (currentMapId < 0)
            {
                MessageBox.Show("Load or create a map first");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtLocationName.Text))
            {
                MessageBox.Show("Enter name");
                return;
            }

            int id = DataAccess.CreateLocation(currentMapId, txtLocationName.Text.Trim());

            currentLocations = DataAccess.GetLocations(currentMapId);
            RefreshLists();

            MessageBox.Show("Location added (ID: " + id + ")");
        }

        private void btnAddRoad_Click(object sender, EventArgs e)
        {
            if (currentMapId < 0)
            {
                MessageBox.Show("Load or create a map first");
                return;
            }

            if (cmbLocA.SelectedItem == null || cmbLocB.SelectedItem == null)
            {
                MessageBox.Show("Select both endpoints");
                return;
            }

            if (!int.TryParse(txtDistance.Text, out int d))
            {
                MessageBox.Show("Enter numeric distance");
                return;
            }

            var aId = ((ComboBoxItem)cmbLocA.SelectedItem).Value;
            var bId = ((ComboBoxItem)cmbLocB.SelectedItem).Value;

            DataAccess.CreateRoad(currentMapId, aId, bId, d);

            currentRoads = DataAccess.GetRoads(currentMapId);
            graph.BuildFromDb(currentMapId, currentLocations, currentRoads);
            RefreshLists();

            MessageBox.Show("Road added");
        }

        private void btnDijkstra_Click(object sender, EventArgs e)
        {
            if (graph == null) return;

            var path = graph.Dijkstra(
                txtSourceName.Text.Trim(),
                txtTargetName.Text.Trim()
            );

            if (path == null || path.Count == 0)
            {
                MessageBox.Show("No path found");
                return;
            }

            int dist = 0;
            for (int i = 0; i < path.Count - 1; i++)
                dist += graph.GetWeight(path[i], path[i + 1]);

            MessageBox.Show(
                "Shortest path:\n" +
                string.Join(" → ", path) +
                "\nDistance: " + dist
            );

            // 🔴 VISUALIZE SHORTEST PATH
            var pathIds = new List<int>();

            foreach (var name in path)
            {
                var loc = currentLocations.FirstOrDefault(l => l.Name == name);
                if (loc != null)
                    pathIds.Add(loc.LocationID);
            }

            // 🔴 VISUALIZE SHORTEST PATH USING IDs
            visualPanel.HighlightedPath = pathIds;
            visualPanel.Invalidate();

            visualPanel.Invalidate();

            if (currentMapId > 0)
                DataAccess.SaveRoute(
                    currentMapId,
                    txtSourceName.Text.Trim(),
                    txtTargetName.Text.Trim(),
                    string.Join("->", path),
                    dist
                );
        }


        private void btnTSP_Click(object sender, EventArgs e)
        {
            var nodes = graph.GetNodes();
            if (nodes.Count < 2)
            {
                MessageBox.Show("Need at least 2 locations for TSP");
                return;
            }

            var (path, cost) = TSP.Solve(nodes, graph);

            if (path == null || path.Count == 0)
            {
                MessageBox.Show("No valid TSP route found");
                return;
            }

            // Convert names → IDs
            var pathIds = new List<int>();
            foreach (var name in path)
            {
                var loc = currentLocations.FirstOrDefault(l => l.Name == name);
                if (loc != null)
                    pathIds.Add(loc.LocationID);
            }

            // Clear Dijkstra, show TSP
            visualPanel.HighlightedPath.Clear();
            visualPanel.HighlightedTspPath = pathIds;
            visualPanel.Invalidate();

            MessageBox.Show(
                "Optimal Delivery Route (TSP):\n" +
                string.Join(" → ", path) +
                "\nTotal Cost: " + cost
            );

            if (currentMapId > 0)
            {
                DataAccess.SaveRoute(
                    currentMapId,
                    path.First(),
                    path.Last(),
                    string.Join("->", path),
                    cost
                );
            }
        }


        private void RefreshLists()
        {
            // Locations
            lstLocations.Items.Clear();
            cmbLocA.Items.Clear();
            cmbLocB.Items.Clear();
            cmbDPLocation.Items.Clear();

            foreach (var l in currentLocations)
            {
                lstLocations.Items.Add(l.Name + " (ID:" + l.LocationID + ")");
                cmbLocA.Items.Add(new ComboBoxItem(l.Name, l.LocationID));
                cmbLocB.Items.Add(new ComboBoxItem(l.Name, l.LocationID));
                cmbDPLocation.Items.Add(new ComboBoxItem(l.Name, l.LocationID));
            }

            // Roads
            lstRoads.Items.Clear();
            foreach (var r in currentRoads)
            {
                var a = currentLocations.FirstOrDefault(x => x.LocationID == r.LocationA)?.Name ?? r.LocationA.ToString();
                var b = currentLocations.FirstOrDefault(x => x.LocationID == r.LocationB)?.Name ?? r.LocationB.ToString();
                lstRoads.Items.Add(a + " - " + b + " : " + r.Distance);
            }

            // Routes
            lstRoutes.Items.Clear();
            if (currentMapId > 0)
            {
                var routes = DataAccess.GetRoutes(currentMapId);
                foreach (var rt in routes)
                    lstRoutes.Items.Add(rt.CreatedAt.ToString("g") + " | " + rt.Start + "->" + rt.End + " | Dist: " + rt.Distance);
            }

            // Delivery Points
            lstDeliveryPoints.Items.Clear();
            if (currentMapId > 0)
            {
                var pts = DataAccess.GetDeliveryPoints(currentMapId);
                foreach (var p in pts)
                {
                    var locName = currentLocations.FirstOrDefault(l => l.LocationID == p.LocationID)?.Name ?? "Unknown";
                    lstDeliveryPoints.Items.Add($"{p.Name} | {p.Address} | Location: {locName}");
                }
            }
        }

        private void btnAddDeliveryPoint_Click(object sender, EventArgs e)
        {
            if (currentMapId < 0)
            {
                MessageBox.Show("Load or create a map first");
                return;
            }

            if (cmbLocA.SelectedItem == null)
            {
                MessageBox.Show("Select a location for the delivery point");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDPName.Text))
            {
                MessageBox.Show("Enter delivery point name");
                return;
            }

            int locId = ((ComboBoxItem)cmbLocA.SelectedItem).Value;

            DataAccess.CreateDeliveryPoint(currentMapId, locId, txtDPName.Text.Trim());

            RefreshLists();
            MessageBox.Show("Delivery point added");
        }
    }

    public class ComboBoxItem
    {
        public string Text { get; set; }
        public int Value { get; set; }

        public ComboBoxItem(string t, int v)
        {
            Text = t;
            Value = v;
        }

        public override string ToString() => Text;
    }
}
