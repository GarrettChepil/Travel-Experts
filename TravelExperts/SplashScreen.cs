using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TravelExperts
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
        }
        Thread dbt = new Thread(TravelExpertsDB.LoadDB);
        System.Windows.Forms.Timer t;
        private void SplashScreen_Shown(object sender, EventArgs e)
        {
            t = new System.Windows.Forms.Timer();
            t.Interval = 32;
            t.Start();
            t.Tick += new EventHandler(t_Tick);
        }

        private void t_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(1);
            if (progressBar1.Value == 100)
            {
                t.Stop();
                Login login = new Login();
                login.Show();
                this.Hide();
            }
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            dbt.Start();
        }
    }
}
