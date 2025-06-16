using CookingLovers.BLL;
using CookingLovers.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CookingLovers.UI
{
    public partial class MainForm : Form
    {
        private User currentUser;
        private RecipeManager recipeManager = new RecipeManager();
        private List<Recipe> allRecipes;

        public MainForm(User user)
        {
            InitializeComponent();
            currentUser = user;
            lblWelcome.Text = $"Welcome, {currentUser.Username} ({currentUser.Role})";

            btnAdminPanel.Visible = currentUser.Role == "admin";

            LoadRecipes();
            lstRecipes.SelectedIndexChanged += LstRecipes_SelectedIndexChanged;
        }

        private void LoadRecipes()
        {
            lstRecipes.Items.Clear();
            allRecipes = recipeManager.GetAllRecipes();

            // Thêm 3 món mẫu nếu danh sách rỗng
            if (allRecipes.Count == 0)
            {
                recipeManager.AddRecipe(new Recipe { Title = "Pho", ImagePath = "pho.jpg", CreatedBy = currentUser.Id });
                recipeManager.AddRecipe(new Recipe { Title = "Bun Bo", ImagePath = "bunbo.jpg", CreatedBy = currentUser.Id });
                recipeManager.AddRecipe(new Recipe { Title = "Cha Nem", ImagePath = "chanem.jpg", CreatedBy = currentUser.Id });

                allRecipes = recipeManager.GetAllRecipes();
            }

            foreach (var recipe in allRecipes)
            {
                lstRecipes.Items.Add(recipe);
            }

            lstRecipes.DisplayMember = "Title";
        }

        private void LstRecipes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstRecipes.SelectedIndex >= 0)
            {
                var selected = allRecipes[lstRecipes.SelectedIndex];
                string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", selected.ImagePath);

                if (File.Exists(fullPath))
                    picRecipe.Image = Image.FromFile(fullPath);
                else
                    picRecipe.Image = null;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstRecipes.SelectedIndex < 0) return;

            var selected = allRecipes[lstRecipes.SelectedIndex];
            bool canDelete = currentUser.Role == "admin" || selected.CreatedBy == currentUser.Id;

            if (!canDelete)
            {
                MessageBox.Show("You can only delete recipes you created.", "Permission Denied");
                return;
            }

            DialogResult confirm = MessageBox.Show($"Delete '{selected.Title}'?", "Confirm", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                recipeManager.DeleteRecipe(selected.Id);
                LoadRecipes();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new RecipeForm(currentUser);
            form.ShowDialog();
            LoadRecipes();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lstRecipes.SelectedIndex < 0) return;

            var selected = allRecipes[lstRecipes.SelectedIndex];
            var form = new RecipeForm(currentUser, selected);
            form.ShowDialog();
            LoadRecipes();
        }

        private void btnAdminPanel_Click(object sender, EventArgs e)
        {
            AdminPanelForm panel = new AdminPanelForm();
            panel.ShowDialog();
        }

        private void lstRecipes_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (lstRecipes.SelectedIndex >= 0)
            {
                var selected = (Recipe)lstRecipes.SelectedItem;
                string path = Path.Combine(Application.StartupPath, "Images", selected.ImagePath);

                if (File.Exists(path))
                {
                    picRecipe.Image = Image.FromFile(path);
                }
                else
                {
                    picRecipe.Image = null;
                }
            }
        }

        

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Có thể dùng LoadRecipes() ở đây nếu muốn tự động
        }

        private void btnConfirm_Click_1(object sender, EventArgs e)
        {
            if (lstRecipes.SelectedIndex >= 0)
            {
                var selectedRecipe = (Recipe)lstRecipes.SelectedItem;
                new RecipeDetailForm(selectedRecipe).ShowDialog();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            if (lstRecipes.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a recipe to delete.");
                return;
            }

            // Lấy món ăn đang chọn
            var selectedRecipe = (Recipe)lstRecipes.SelectedItem;

            // Kiểm tra quyền xóa
            bool isAdmin = currentUser.Role == "admin";
            bool isOwner = selectedRecipe.CreatedBy == currentUser.Id;

            if (!isAdmin && !isOwner)
            {
                MessageBox.Show("You can only delete recipes you created.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Xác nhận xóa
            DialogResult confirm = MessageBox.Show($"Are you sure you want to delete '{selectedRecipe.Title}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                recipeManager.DeleteRecipe(selectedRecipe.Id);
                LoadRecipes(); // Tải lại danh sách sau khi xóa
                MessageBox.Show("Recipe deleted successfully.");
            }
        }

        private void btnAdminPanel_Click_1(object sender, EventArgs e)
        {
            
                var panel = new AdminPanelForm();
                panel.ShowDialog(); // Hiển thị dạng modal
            
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();

            var filtered = allRecipes
                .FindAll(r => r.Title.ToLower().Contains(keyword));

            lstRecipes.Items.Clear();
            foreach (var recipe in filtered)
            {
                lstRecipes.Items.Add(recipe);
            }

            // Reset ảnh khi tìm
            picRecipe.Image = null;
        }

        private void btnMealApi_Click(object sender, EventArgs e)
        {
            var form = new MealApiForm(currentUser);
            form.ShowDialog();
            LoadRecipes(); // load lại nếu người dùng vừa thêm món từ API
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}