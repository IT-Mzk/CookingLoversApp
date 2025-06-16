using CookingLovers.BLL;
using CookingLovers.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CookingLovers.UI
{
    public partial class AdminPanelForm : Form
    {
        private UserManager userManager = new UserManager();

        public AdminPanelForm()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            var users = userManager.GetAllUsers();
            dgvUsers.DataSource = users;
            dgvUsers.Columns["Password"].Visible = false; // Ẩn mật khẩu
        }

        private User GetSelectedUser()
        {
            if (dgvUsers.CurrentRow?.DataBoundItem is User selectedUser)
                return selectedUser;

            MessageBox.Show("Please select a user.");
            return null;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var user = GetSelectedUser();
            if (user == null) return;

            DialogResult confirm = MessageBox.Show($"Delete user '{user.Username}'?", "Confirm", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                userManager.DeleteUser(user.Id);
                LoadUsers();
                MessageBox.Show("User deleted.");
            }
        }

        private void btnPromote_Click_1(object sender, EventArgs e)
        {
            var user = GetSelectedUser();
            if (user == null || user.Role == "admin")
            {
                MessageBox.Show("User is already an admin.");
                return;
            }

            userManager.UpdateUserRole(user.Id, "admin");
            LoadUsers();
            MessageBox.Show($"'{user.Username}' is now an admin.");
        }

        private void btnRevoke_Click_1(object sender, EventArgs e)
        {
            var user = GetSelectedUser();
            if (user == null || user.Role == "user")
            {
                MessageBox.Show("User is not an admin.");
                return;
            }

            userManager.UpdateUserRole(user.Id, "user");
            LoadUsers();
            MessageBox.Show($"'{user.Username}' has been demoted to user.");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}