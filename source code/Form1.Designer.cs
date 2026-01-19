using System.Windows.Forms;

namespace DeliveryRouteOptimizer
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            txtMapName = new TextBox();
            btnCreateMap = new Button();
            cmbMaps = new ComboBox();
            btnLoadMap = new Button();
            txtLocationName = new TextBox();
            btnAddLocation = new Button();
            lstLocations = new ListBox();
            cmbLocA = new ComboBox();
            cmbLocB = new ComboBox();
            txtDistance = new TextBox();
            btnAddRoad = new Button();
            lstRoads = new ListBox();
            txtSourceName = new TextBox();
            txtTargetName = new TextBox();
            btnDijkstra = new Button();
            btnTSP = new Button();
            lstRoutes = new ListBox();
            txtDPName = new TextBox();
            txtDPAddress = new TextBox();
            cmbDPLocation = new ComboBox();
            btnAddDeliveryPoint = new Button();
            lstDeliveryPoints = new ListBox();
            visualPanel = new VisualizationPanel();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // txtMapName
            // 
            txtMapName.Location = new Point(10, 74);
            txtMapName.Name = "txtMapName";
            txtMapName.PlaceholderText = "New map name";
            txtMapName.Size = new Size(611, 31);
            txtMapName.TabIndex = 0;
            // 
            // btnCreateMap
            // 
            btnCreateMap.BackColor = SystemColors.MenuHighlight;
            btnCreateMap.ForeColor = Color.White;
            btnCreateMap.Location = new Point(627, 74);
            btnCreateMap.Name = "btnCreateMap";
            btnCreateMap.Size = new Size(125, 35);
            btnCreateMap.TabIndex = 1;
            btnCreateMap.Text = "Create";
            btnCreateMap.UseVisualStyleBackColor = false;
            btnCreateMap.Click += btnCreateMap_Click;
            // 
            // cmbMaps
            // 
            cmbMaps.Location = new Point(10, 381);
            cmbMaps.Name = "cmbMaps";
            cmbMaps.Size = new Size(611, 33);
            cmbMaps.TabIndex = 2;
            // 
            // btnLoadMap
            // 
            btnLoadMap.BackColor = SystemColors.MenuHighlight;
            btnLoadMap.ForeColor = Color.White;
            btnLoadMap.Location = new Point(625, 379);
            btnLoadMap.Name = "btnLoadMap";
            btnLoadMap.Size = new Size(125, 35);
            btnLoadMap.TabIndex = 3;
            btnLoadMap.Text = "Load";
            btnLoadMap.UseVisualStyleBackColor = false;
            btnLoadMap.Click += btnLoadMap_Click;
            // 
            // txtLocationName
            // 
            txtLocationName.Location = new Point(10, 111);
            txtLocationName.Name = "txtLocationName";
            txtLocationName.PlaceholderText = "Location name";
            txtLocationName.Size = new Size(611, 31);
            txtLocationName.TabIndex = 4;
            // 
            // btnAddLocation
            // 
            btnAddLocation.BackColor = SystemColors.MenuHighlight;
            btnAddLocation.ForeColor = Color.White;
            btnAddLocation.Location = new Point(627, 111);
            btnAddLocation.Name = "btnAddLocation";
            btnAddLocation.Size = new Size(125, 35);
            btnAddLocation.TabIndex = 5;
            btnAddLocation.Text = "Add Location";
            btnAddLocation.UseVisualStyleBackColor = false;
            btnAddLocation.Click += btnAddLocation_Click;
            // 
            // lstLocations
            // 
            lstLocations.ItemHeight = 25;
            lstLocations.Location = new Point(10, 420);
            lstLocations.Name = "lstLocations";
            lstLocations.Size = new Size(740, 104);
            lstLocations.TabIndex = 6;
            // 
            // cmbLocA
            // 
            cmbLocA.Location = new Point(10, 148);
            cmbLocA.Name = "cmbLocA";
            cmbLocA.Size = new Size(150, 33);
            cmbLocA.TabIndex = 7;
            cmbLocA.Text = "Location 01";
            // 
            // cmbLocB
            // 
            cmbLocB.Location = new Point(166, 148);
            cmbLocB.Name = "cmbLocB";
            cmbLocB.Size = new Size(150, 33);
            cmbLocB.TabIndex = 8;
            cmbLocB.Text = "Location 02";
            // 
            // txtDistance
            // 
            txtDistance.Location = new Point(322, 148);
            txtDistance.Name = "txtDistance";
            txtDistance.PlaceholderText = "Distance";
            txtDistance.Size = new Size(299, 31);
            txtDistance.TabIndex = 9;
            // 
            // btnAddRoad
            // 
            btnAddRoad.BackColor = SystemColors.MenuHighlight;
            btnAddRoad.ForeColor = Color.White;
            btnAddRoad.Location = new Point(627, 148);
            btnAddRoad.Name = "btnAddRoad";
            btnAddRoad.Size = new Size(125, 35);
            btnAddRoad.TabIndex = 10;
            btnAddRoad.Text = "Add Road";
            btnAddRoad.UseVisualStyleBackColor = false;
            btnAddRoad.Click += btnAddRoad_Click;
            // 
            // lstRoads
            // 
            lstRoads.ItemHeight = 25;
            lstRoads.Location = new Point(10, 187);
            lstRoads.Name = "lstRoads";
            lstRoads.Size = new Size(740, 129);
            lstRoads.TabIndex = 11;
            // 
            // txtSourceName
            // 
            txtSourceName.Location = new Point(12, 591);
            txtSourceName.Name = "txtSourceName";
            txtSourceName.PlaceholderText = "Source name";
            txtSourceName.Size = new Size(338, 31);
            txtSourceName.TabIndex = 12;
            // 
            // txtTargetName
            // 
            txtTargetName.Location = new Point(356, 591);
            txtTargetName.Name = "txtTargetName";
            txtTargetName.PlaceholderText = "Target name";
            txtTargetName.Size = new Size(394, 31);
            txtTargetName.TabIndex = 13;
            // 
            // btnDijkstra
            // 
            btnDijkstra.BackColor = SystemColors.MenuHighlight;
            btnDijkstra.ForeColor = Color.White;
            btnDijkstra.Location = new Point(10, 628);
            btnDijkstra.Name = "btnDijkstra";
            btnDijkstra.Size = new Size(740, 45);
            btnDijkstra.TabIndex = 14;
            btnDijkstra.Text = "Find Shortest path";
            btnDijkstra.UseVisualStyleBackColor = false;
            btnDijkstra.Click += btnDijkstra_Click;
            // 
            // btnTSP
            // 
            btnTSP.BackColor = SystemColors.MenuHighlight;
            btnTSP.ForeColor = Color.White;
            btnTSP.Location = new Point(10, 679);
            btnTSP.Name = "btnTSP";
            btnTSP.Size = new Size(740, 45);
            btnTSP.TabIndex = 15;
            btnTSP.Text = "Compute TSP";
            btnTSP.UseVisualStyleBackColor = false;
            btnTSP.Click += btnTSP_Click;
            // 
            // lstRoutes
            // 
            lstRoutes.ItemHeight = 25;
            lstRoutes.Location = new Point(10, 731);
            lstRoutes.Name = "lstRoutes";
            lstRoutes.Size = new Size(740, 104);
            lstRoutes.TabIndex = 16;
            // 
            // txtDPName
            // 
            txtDPName.Location = new Point(10, 896);
            txtDPName.Name = "txtDPName";
            txtDPName.PlaceholderText = "Delivery point name";
            txtDPName.Size = new Size(191, 31);
            txtDPName.TabIndex = 17;
            // 
            // txtDPAddress
            // 
            txtDPAddress.Location = new Point(207, 896);
            txtDPAddress.Name = "txtDPAddress";
            txtDPAddress.PlaceholderText = "Address";
            txtDPAddress.Size = new Size(200, 31);
            txtDPAddress.TabIndex = 18;
            // 
            // cmbDPLocation
            // 
            cmbDPLocation.Location = new Point(413, 894);
            cmbDPLocation.Name = "cmbDPLocation";
            cmbDPLocation.Size = new Size(206, 33);
            cmbDPLocation.TabIndex = 19;
            // 
            // btnAddDeliveryPoint
            // 
            btnAddDeliveryPoint.BackColor = SystemColors.MenuHighlight;
            btnAddDeliveryPoint.ForeColor = Color.White;
            btnAddDeliveryPoint.Location = new Point(625, 892);
            btnAddDeliveryPoint.Name = "btnAddDeliveryPoint";
            btnAddDeliveryPoint.Size = new Size(125, 35);
            btnAddDeliveryPoint.TabIndex = 20;
            btnAddDeliveryPoint.Text = "Add";
            btnAddDeliveryPoint.UseVisualStyleBackColor = false;
            btnAddDeliveryPoint.Click += btnAddDeliveryPoint_Click;
            // 
            // lstDeliveryPoints
            // 
            lstDeliveryPoints.ItemHeight = 25;
            lstDeliveryPoints.Location = new Point(10, 937);
            lstDeliveryPoints.Name = "lstDeliveryPoints";
            lstDeliveryPoints.Size = new Size(740, 104);
            lstDeliveryPoints.TabIndex = 21;
            // 
            // visualPanel
            // 
            visualPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            visualPanel.BorderStyle = BorderStyle.FixedSingle;
            visualPanel.HighlightedPath = (List<int>)resources.GetObject("visualPanel.HighlightedPath");
            visualPanel.HighlightedTspPath = (List<int>)resources.GetObject("visualPanel.HighlightedTspPath");
            visualPanel.Location = new Point(756, 34);
            visualPanel.Name = "visualPanel";
            visualPanel.Size = new Size(1138, 996);
            visualPanel.TabIndex = 22;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Bernard MT Condensed", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.MenuHighlight;
            label1.Location = new Point(10, 34);
            label1.Name = "label1";
            label1.Size = new Size(133, 33);
            label1.TabIndex = 23;
            label1.Text = "Create Map";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Bernard MT Condensed", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.MenuHighlight;
            label2.Location = new Point(10, 345);
            label2.Name = "label2";
            label2.Size = new Size(116, 33);
            label2.TabIndex = 24;
            label2.Text = "Load Map";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Bernard MT Condensed", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.MenuHighlight;
            label3.Location = new Point(10, 555);
            label3.Name = "label3";
            label3.Size = new Size(191, 33);
            label3.TabIndex = 25;
            label3.Text = "Route Optimizer";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Bernard MT Condensed", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = SystemColors.MenuHighlight;
            label4.Location = new Point(10, 860);
            label4.Name = "label4";
            label4.Size = new Size(180, 33);
            label4.TabIndex = 26;
            label4.Text = "Delivery Points";
            // 
            // Form1
            // 
            AutoScroll = true;
            AutoSize = true;
            BackColor = Color.White;
            ClientSize = new Size(1924, 1050);
            Controls.Add(lstDeliveryPoints);
            Controls.Add(btnAddDeliveryPoint);
            Controls.Add(label4);
            Controls.Add(cmbDPLocation);
            Controls.Add(txtDPAddress);
            Controls.Add(label3);
            Controls.Add(txtDPName);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtMapName);
            Controls.Add(btnCreateMap);
            Controls.Add(cmbMaps);
            Controls.Add(btnLoadMap);
            Controls.Add(txtLocationName);
            Controls.Add(btnAddLocation);
            Controls.Add(lstLocations);
            Controls.Add(cmbLocA);
            Controls.Add(cmbLocB);
            Controls.Add(txtDistance);
            Controls.Add(btnAddRoad);
            Controls.Add(lstRoads);
            Controls.Add(txtSourceName);
            Controls.Add(txtTargetName);
            Controls.Add(btnDijkstra);
            Controls.Add(btnTSP);
            Controls.Add(lstRoutes);
            Controls.Add(visualPanel);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "XANTRIP";
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.TextBox txtMapName;
        private System.Windows.Forms.Button btnCreateMap;
        private System.Windows.Forms.ComboBox cmbMaps;
        private System.Windows.Forms.Button btnLoadMap;

        private System.Windows.Forms.TextBox txtLocationName;
        private System.Windows.Forms.Button btnAddLocation;
        private System.Windows.Forms.ListBox lstLocations;

        private System.Windows.Forms.ComboBox cmbLocA;
        private System.Windows.Forms.ComboBox cmbLocB;
        private System.Windows.Forms.TextBox txtDistance;
        private System.Windows.Forms.Button btnAddRoad;
        private System.Windows.Forms.ListBox lstRoads;

        private System.Windows.Forms.TextBox txtSourceName;
        private System.Windows.Forms.TextBox txtTargetName;
        private System.Windows.Forms.Button btnDijkstra;
        private System.Windows.Forms.Button btnTSP;
        private System.Windows.Forms.ListBox lstRoutes;

        private System.Windows.Forms.TextBox txtDPName;
        private System.Windows.Forms.TextBox txtDPAddress;
        private System.Windows.Forms.ComboBox cmbDPLocation;
        private System.Windows.Forms.Button btnAddDeliveryPoint;
        private System.Windows.Forms.ListBox lstDeliveryPoints;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}
