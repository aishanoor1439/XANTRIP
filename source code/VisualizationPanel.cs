using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DeliveryRouteOptimizer
{
    public class VisualizationPanel : Panel
    {
        private const int Margin = 40;
        private readonly Random rnd = new Random();
        private float zoom = 1.0f;
        private const float ZoomStep = 0.1f;
        private const float MinZoom = 0.3f;
        private const float MaxZoom = 3.0f;

        private PointF panOffset = new PointF(0, 0);


        private readonly System.Windows.Forms.Timer layoutTimer;

        public List<Location> Locations { get; set; } = new();
        public List<Road> Roads { get; set; } = new();

        public List<int> HighlightedPath { get; set; } = new();
        public List<int> HighlightedTspPath { get; set; } = new();

        private readonly Dictionary<int, PointF> nodePositions = new();
        private readonly Dictionary<int, PointF> velocities = new();

        public VisualizationPanel()
        {
            DoubleBuffered = true;

            layoutTimer = new System.Windows.Forms.Timer();
            layoutTimer.Interval = 30;
            layoutTimer.Tick += (s, e) =>
            {
                RunLayoutStep();
                Invalidate();
            };
        }

        // ===================== PUBLIC =====================
        public void StartLayout()
        {
            InitializeLayout();
            layoutTimer.Start();
        }

        // ===================== INIT =====================
        private void InitializeLayout()
        {
            nodePositions.Clear();
            velocities.Clear();

            foreach (var loc in Locations)
            {
                nodePositions[loc.LocationID] = new PointF(
                    rnd.Next(Margin, Math.Max(Margin + 1, Width - Margin)),
                    rnd.Next(Margin, Math.Max(Margin + 1, Height - Margin))
                );

                velocities[loc.LocationID] = PointF.Empty;
            }
        }

        // ===================== PHYSICS =====================
        private void RunLayoutStep()
        {
            if (Locations.Count == 0 || Roads.Count == 0)
                return;

            // ---- PARAMETERS ----
            const float repulsion = 1800f;
            const float spring = 0.08f;
            const float damping = 0.85f;
            const float gravity = 0.0025f;
            const float stopVelocity = 0.05f;

            // ---- DISTANCE SCALING ----
            float avgDist = (float)Roads.Average(r => r.Distance);
            float scale = Math.Min(Width, Height) / (avgDist * 2.5f);
            scale = Math.Clamp(scale, 0.5f, 6f);

            // ---- CENTER GRAVITY ----
            float cx = Width / 2f;
            float cy = Height / 2f;

            foreach (var loc in Locations)
            {
                var p = nodePositions[loc.LocationID];
                velocities[loc.LocationID] = new PointF(
                    velocities[loc.LocationID].X + (cx - p.X) * gravity,
                    velocities[loc.LocationID].Y + (cy - p.Y) * gravity
                );
            }

            // ---- REPULSION ----
            foreach (var a in Locations)
            {
                foreach (var b in Locations)
                {
                    if (a.LocationID == b.LocationID) continue;

                    var pa = nodePositions[a.LocationID];
                    var pb = nodePositions[b.LocationID];

                    float dx = pa.X - pb.X;
                    float dy = pa.Y - pb.Y;
                    float dist = MathF.Sqrt(dx * dx + dy * dy) + 0.1f;

                    float force = repulsion / (dist * dist);

                    velocities[a.LocationID] = new PointF(
                        velocities[a.LocationID].X + dx / dist * force,
                        velocities[a.LocationID].Y + dy / dist * force
                    );
                }
            }

            // ---- SPRINGS (ROADS) ----
            foreach (var road in Roads)
            {
                var a = nodePositions[road.LocationA];
                var b = nodePositions[road.LocationB];

                float dx = b.X - a.X;
                float dy = b.Y - a.Y;
                float dist = MathF.Sqrt(dx * dx + dy * dy) + 0.1f;

                float desired = road.Distance * scale;
                float force = (dist - desired) * spring;

                float fx = dx / dist * force;
                float fy = dy / dist * force;

                velocities[road.LocationA] = new PointF(
                    velocities[road.LocationA].X + fx,
                    velocities[road.LocationA].Y + fy
                );

                velocities[road.LocationB] = new PointF(
                    velocities[road.LocationB].X - fx,
                    velocities[road.LocationB].Y - fy
                );
            }

            // ---- APPLY ----
            bool moving = false;

            foreach (var loc in Locations)
            {
                var v = velocities[loc.LocationID];
                var p = nodePositions[loc.LocationID];

                p.X += v.X * damping;
                p.Y += v.Y * damping;

                p.X = Math.Clamp(p.X, Margin, Width - Margin);
                p.Y = Math.Clamp(p.Y, Margin, Height - Margin);

                velocities[loc.LocationID] = new PointF(v.X * damping, v.Y * damping);
                nodePositions[loc.LocationID] = p;

                if (Math.Abs(v.X) > stopVelocity || Math.Abs(v.Y) > stopVelocity)
                    moving = true;
            }

            if (!moving)
                layoutTimer.Stop();
        }

        // ===================== DRAW =====================
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            DrawRoads(g);
            DrawHighlightedPath(g);
            DrawTspPath(g);
            DrawNodes(g);
        }

        private void DrawRoads(Graphics g)
        {
            using var pen = new Pen(Color.Gray, 1);
            using var font = new Font("Segoe UI", 8);

            foreach (var road in Roads)
            {
                var a = nodePositions[road.LocationA];
                var b = nodePositions[road.LocationB];

                g.DrawLine(pen, a.X, a.Y, b.X, b.Y);

                float mx = (a.X + b.X) / 2;
                float my = (a.Y + b.Y) / 2;

                string text = road.Distance.ToString();
                var size = g.MeasureString(text, font);

                g.FillRectangle(Brushes.White,
                    mx - size.Width / 2,
                    my - size.Height / 2,
                    size.Width,
                    size.Height);

                g.DrawString(text, font, Brushes.DarkBlue,
                    mx - size.Width / 2,
                    my - size.Height / 2);
            }
        }

        private void DrawHighlightedPath(Graphics g)
        {
            if (HighlightedPath.Count < 2) return;

            using var pen = new Pen(Color.Red, 3);
            for (int i = 0; i < HighlightedPath.Count - 1; i++)
                g.DrawLine(pen,
                    nodePositions[HighlightedPath[i]].X,
                    nodePositions[HighlightedPath[i]].Y,
                    nodePositions[HighlightedPath[i + 1]].X,
                    nodePositions[HighlightedPath[i + 1]].Y);
        }

        private void DrawTspPath(Graphics g)
        {
            if (HighlightedTspPath.Count < 2) return;

            using var pen = new Pen(Color.Green, 3);
            for (int i = 0; i < HighlightedTspPath.Count - 1; i++)
                g.DrawLine(pen,
                    nodePositions[HighlightedTspPath[i]].X,
                    nodePositions[HighlightedTspPath[i]].Y,
                    nodePositions[HighlightedTspPath[i + 1]].X,
                    nodePositions[HighlightedTspPath[i + 1]].Y);
        }

        private void DrawNodes(Graphics g)
        {
            using var font = new Font("Segoe UI", 9, FontStyle.Bold);

            foreach (var loc in Locations)
            {
                var p = nodePositions[loc.LocationID];

                g.FillEllipse(Brushes.LightBlue, p.X - 12, p.Y - 12, 24, 24);
                g.DrawEllipse(Pens.Black, p.X - 12, p.Y - 12, 24, 24);

                var size = g.MeasureString(loc.Name, font);
                g.DrawString(loc.Name, font, Brushes.Black,
                    p.X - size.Width / 2,
                    p.Y + (p.Y < Height / 2 ? 14 : -20));
            }
        }
    }
}
