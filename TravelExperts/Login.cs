using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TravelExperts
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(txtLogin.IsPresent() && txtPass.IsPresent())
            {
                if(Agent.AuthorizeAgent(txtLogin.Text, txtPass.Text))
                {
                    Form1 mainmenu = new Form1();
                    this.Hide();
                    mainmenu.Show();
                }
                else
                {
                    MessageBox.Show("The User Name or Password is incorrect", "Authorization Failed");
                    txtPass.Clear();
                    txtLogin.Focus();
                }
            }
            
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtPass.Clear();
            txtLogin.Clear();
            txtLogin.Focus();
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
