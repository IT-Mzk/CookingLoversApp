using System.Collections.Generic;
using CookingLovers.Models;
using CookingLovers.DAL;

namespace CookingLovers.BLL
{
    public class RecipeManager
    {
        private RecipeRepository recipeRepository = new RecipeRepository();

        public List<Recipe> GetAllRecipes()
        {
            return recipeRepository.GetAllRecipes();
        }

        public void AddRecipe(Recipe recipe)
        {
            recipeRepository.AddRecipe(recipe);
        }

        public void UpdateRecipe(Recipe recipe)
        {
            recipeRepository.UpdateRecipe(recipe);
        }

        public void DeleteRecipe(int id)
        {
            recipeRepository.DeleteRecipe(id);
        }
    }
}