using CookingLovers.BLL;
using CookingLovers.Models;
using System;
using System.Windows.Forms;

namespace CookingLovers.UI
{
    public partial class LoginForm : Form
    {
        private UserManager userManager = new UserManager();

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            UserManager userManager = new UserManager();
            var user = userManager.GetUserByUsername(username);

            if (user == null)
            {
                MessageBox.Show("Account does not exist. Please register first.");
                return;
            }

            if (user.Password != password)
            {
                MessageBox.Show("Incorrect password.");
                return;
            }

            MessageBox.Show($"Logged in as: {user.Role}", "Success");
            this.Hide();
            new MainForm(user).ShowDialog();
            this.Show();
        }

       

        private void btnRegister_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            new RegisterForm().Show();
        }
    }
}