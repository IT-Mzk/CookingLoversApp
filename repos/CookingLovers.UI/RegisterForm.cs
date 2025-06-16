using CookingLovers.BLL;
using CookingLovers.Models;
using System;
using System.Windows.Forms;

namespace CookingLovers.UI
{
    public partial class RegisterForm : Form
    {
        private UserManager userManager = new UserManager();

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string confirmPwd = txtConfirmPwd.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            if (password.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters.");
                return;
            }

            if (password != confirmPwd)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            UserManager userManager = new UserManager();

            if (userManager.UsernameExists(username))
            {
                MessageBox.Show("Username already exists.");
                return;
            }

            User newUser = new User
            {
                Username = username,
                Password = password,
                Role = "user"
            };

            userManager.AddUser(newUser);
            MessageBox.Show("Registration successful. Please log in.");
            this.Hide();
            new LoginForm().Show();
        }
    }
}