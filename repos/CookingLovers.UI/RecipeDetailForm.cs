using CookingLovers.Models;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CookingLovers.UI
{
    public partial class RecipeDetailForm : Form
    {
        private Recipe recipe;

        public RecipeDetailForm(Recipe recipe)
        {
            InitializeComponent();
            this.recipe = recipe;
            this.Load += RecipeDetailForm_Load;
        }

        private void RecipeDetailForm_Load(object sender, EventArgs e)
        {
            if (recipe == null)
            {
                MessageBox.Show("No recipe data found.");
                this.Close();
                return;
            }

            lblTitle.Text = recipe.Title.ToUpper();

            txtSteps.Text = string.IsNullOrWhiteSpace(recipe.Instructions)
                ? "No instructions available for this recipe."
                : recipe.Instructions;

            lstIngredients.Items.Clear();
            if (!string.IsNullOrWhiteSpace(recipe.Ingredients))
            {
                string[] items = recipe.Ingredients.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                lstIngredients.Items.AddRange(items);
            }

            string path = Path.Combine(Application.StartupPath, "Images", recipe.ImagePath);
            if (File.Exists(path))
            {
                using (var img = Image.FromFile(path))
                {
                    picRecipe.Image = new Bitmap(img);
                }
                picRecipe.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                picRecipe.Image = null;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}