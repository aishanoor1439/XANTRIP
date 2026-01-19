using System;
using System.Data.SqlClient;
using System.Windows.Forms;
 

namespace DeliveryRouteOptimizer
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        { 
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Splash());
        }
    }
}
