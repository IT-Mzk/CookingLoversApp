using System;
using System.Net.Http;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Collections.Generic;
using CookingLovers.Models;
using System.IO;
using CookingLovers.BLL;
using System.Threading.Tasks;

namespace CookingLovers.UI
{
    public partial class MealApiForm : Form
    {   
        public Action OnRecipeAdded;
        private RecipeManager recipeManager = new RecipeManager();
        private User currentUser;
        private MealApiResponse latestSearchResult;

        public MealApiForm(User user)
        {
            InitializeComponent();
            currentUser = user;

            lstMeals.SelectedIndexChanged += lstMeals_SelectedIndexChanged;
            btnSearch.Click += btnSearch_Click;
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                MessageBox.Show("Please enter a keyword.");
                return;
            }

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = $"https://www.themealdb.com/api/json/v1/1/search.php?s={keyword}";
                    string response = await client.GetStringAsync(apiUrl);

                    latestSearchResult = JsonConvert.DeserializeObject<MealApiResponse>(response);
                    lstMeals.Items.Clear();

                    if (latestSearchResult.meals != null)
                    {
                        foreach (var meal in latestSearchResult.meals)
                        {
                            lstMeals.Items.Add(meal);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No recipes found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void lstMeals_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstMeals.SelectedItem is Meal selectedMeal)
            {
                lblMealName.Text = selectedMeal.strMeal;
                txtInstructions.Text = selectedMeal.strInstructions;

                lstIngredients.Items.Clear();
                foreach (var ing in selectedMeal.GetIngredients())
                {
                    lstIngredients.Items.Add(ing);
                }

                if (!string.IsNullOrEmpty(selectedMeal.strMealThumb))
                {
                    try
                    {
                        picMeal.Load(selectedMeal.strMealThumb);
                        picMeal.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    catch
                    {
                        picMeal.Image = null;
                    }
                }
                else
                {
                    picMeal.Image = null;
                }
            }
        }

        private void btnAddRecipe_Click(object sender, EventArgs e)
        {
            btnAddRecipe.Enabled = false;
            if (lstMeals.SelectedItem is Meal selectedMeal)
            {
                var recipe = new Recipe
                {
                    Title = selectedMeal.strMeal,
                    Instructions = selectedMeal.strInstructions,
                    Ingredients = string.Join("\n", selectedMeal.GetIngredients()),
                    ImagePath = SaveImageToLocal(selectedMeal.strMealThumb, selectedMeal.strMeal),
                    CreatedBy = currentUser.Id
                };

                recipeManager.AddRecipe(recipe);
                MessageBox.Show("Recipe added successfully.");
               
            }
            btnAddRecipe.Enabled = true;
        }

        private string SaveImageToLocal(string imageUrl, string name)
        {
            try
            {
                string imagesFolder = Path.Combine(Application.StartupPath, "Images");
                if (!Directory.Exists(imagesFolder))
                    Directory.CreateDirectory(imagesFolder);

                string fileName = name.Replace(" ", "_") + ".jpg";
                string localPath = Path.Combine(imagesFolder, fileName);

                using (HttpClient client = new HttpClient())
                {
                    var data = client.GetByteArrayAsync(imageUrl).Result;
                    File.WriteAllBytes(localPath, data);
                }

                return fileName;
            }
            catch
            {
                return "";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public class MealApiResponse
    {
        public List<Meal> meals { get; set; }
    }

    public class Meal
    {
        public string strMeal { get; set; }
        public string strInstructions { get; set; }
        public string strMealThumb { get; set; }

        public string strIngredient1 { get; set; }
        public string strIngredient2 { get; set; }
        public string strIngredient3 { get; set; }
        public string strIngredient4 { get; set; }
        public string strIngredient5 { get; set; }
        public string strIngredient6 { get; set; }

        public override string ToString() => strMeal;

        public List<string> GetIngredients()
        {
            var list = new List<string>();
            if (!string.IsNullOrWhiteSpace(strIngredient1)) list.Add(strIngredient1);
            if (!string.IsNullOrWhiteSpace(strIngredient2)) list.Add(strIngredient2);
            if (!string.IsNullOrWhiteSpace(strIngredient3)) list.Add(strIngredient3);
            if (!string.IsNullOrWhiteSpace(strIngredient4)) list.Add(strIngredient4);
            if (!string.IsNullOrWhiteSpace(strIngredient5)) list.Add(strIngredient5);
            if (!string.IsNullOrWhiteSpace(strIngredient6)) list.Add(strIngredient6);
            return list;
        }
    }
}