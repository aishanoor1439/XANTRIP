using System;
using System.Windows.Forms;

namespace DeliveryRouteOptimizer
{
    public partial class Splash : Form
    {
        private System.Windows.Forms.Timer timer;

        public Splash()
        {
            InitializeComponent();

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 2000; // 2 seconds
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            this.Hide();

            LoginForm loginForm = new LoginForm(); 
            loginForm.Show();
        }
    }
}
