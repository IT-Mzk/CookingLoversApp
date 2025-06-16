using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CookingLovers.Models;

namespace CookingLovers.BLL
{
    public class MealApiService
    {
        private readonly string baseUrl = "https://www.themealdb.com/api/json/v1/1/search.php?s=";

        public async Task<List<Recipe>> SearchRecipesAsync(string keyword)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(baseUrl + keyword);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<MealApiResponse>(json);

                    var recipes = new List<Recipe>();

                    if (apiResponse?.Meals != null)
                    {
                        foreach (var meal in apiResponse.Meals)
                        {
                            recipes.Add(new Recipe
                            {
                                Title = meal.strMeal,
                                Instructions = meal.strInstructions,
                                Ingredients = meal.GetIngredientsString(),
                                ImagePath = meal.strMealThumb, // This is a URL, not a file path
                                CreatedBy = 1 // Hoặc để null nếu không dùng userId
                            });
                        }
                    }

                    return recipes;
                }
                return null;
            }
        }
    }

    public class MealApiResponse
    {
        public List<Meal> Meals { get; set; }
    }

    public class Meal
    {
        public string strMeal { get; set; }
        public string strInstructions { get; set; }
        public string strMealThumb { get; set; }

        // Ingredients
        public string strIngredient1 { get; set; }
        public string strIngredient2 { get; set; }
        public string strIngredient3 { get; set; }
        public string strIngredient4 { get; set; }
        public string strIngredient5 { get; set; }

        public string GetIngredientsString()
        {
            var list = new List<string>();
            if (!string.IsNullOrWhiteSpace(strIngredient1)) list.Add(strIngredient1);
            if (!string.IsNullOrWhiteSpace(strIngredient2)) list.Add(strIngredient2);
            if (!string.IsNullOrWhiteSpace(strIngredient3)) list.Add(strIngredient3);
            if (!string.IsNullOrWhiteSpace(strIngredient4)) list.Add(strIngredient4);
            if (!string.IsNullOrWhiteSpace(strIngredient5)) list.Add(strIngredient5);
            return string.Join("\n", list);
        }
    }
}