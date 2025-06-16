using CookingLovers.Models;
using CookingLovers.BLL;
using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace CookingLovers.UI
{
    public partial class RecipeForm : Form
    {
        private RecipeManager recipeManager = new RecipeManager();
        private User currentUser;
        private Recipe editingRecipe;
        private string selectedImagePath;

        public RecipeForm(User user, Recipe recipeToEdit = null)
        {
            InitializeComponent();
            currentUser = user;
            editingRecipe = recipeToEdit;

            if (editingRecipe != null)
            {
                txtTitle.Text = editingRecipe.Title;
                txtInstructions.Text = editingRecipe.Instructions;
                txtIngredients.Text = editingRecipe.Ingredients;
                selectedImagePath = editingRecipe.ImagePath;

                string fullPath = Path.Combine(Application.StartupPath, "Images", selectedImagePath);
                if (File.Exists(fullPath))
                {
                    picRecipe.Image = Image.FromFile(fullPath);
                }
            }
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (.jpg, *.png)|.jpg;*.png";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                selectedImagePath = Path.GetFileName(dialog.FileName);

                string destinationPath = Path.Combine(Application.StartupPath, "Images", selectedImagePath);
                if (!File.Exists(destinationPath))
                {
                    File.Copy(dialog.FileName, destinationPath);
                }

                picRecipe.Image = Image.FromFile(destinationPath);
            }
        }

        

        private void btnSave_Click_2(object sender, EventArgs e)
        {
            {
                if (string.IsNullOrWhiteSpace(txtTitle.Text) ||
                    string.IsNullOrWhiteSpace(txtInstructions.Text) ||
                    string.IsNullOrWhiteSpace(txtIngredients.Text))
                {
                    MessageBox.Show("Please fill in all fields.");
                    return;
                }

                if (editingRecipe == null)
                {
                    Recipe newRecipe = new Recipe
                    {
                        Title = txtTitle.Text.Trim(),
                        Instructions = txtInstructions.Text.Trim(),
                        Ingredients = txtIngredients.Text.Trim(),
                        ImagePath = selectedImagePath ?? "",
                        CreatedBy = currentUser.Id
                    };

                    recipeManager.AddRecipe(newRecipe);
                }
                else
                {
                    editingRecipe.Title = txtTitle.Text.Trim();
                    editingRecipe.Instructions = txtInstructions.Text.Trim();
                    editingRecipe.Ingredients = txtIngredients.Text.Trim();
                    editingRecipe.ImagePath = selectedImagePath ?? "";

                    recipeManager.UpdateRecipe(editingRecipe);
                }

                this.Close();
            }
        }
    }
}