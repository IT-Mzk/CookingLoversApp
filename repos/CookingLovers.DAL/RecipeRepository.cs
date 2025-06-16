using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CookingLovers.Models;

namespace CookingLovers.DAL
{
    public class RecipeRepository
    {
        private readonly string connectionString = @"Server=localhost;Database=CookingLoversDB;Trusted_Connection=True;";

        public List<Recipe> GetAllRecipes()
        {
            var list = new List<Recipe>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Recipes";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Recipe
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Title = reader["Title"].ToString(),
                        ImagePath = reader["ImagePath"].ToString(),
                        Instructions = reader["Instructions"].ToString(),
                        Ingredients = reader["Ingredients"].ToString(),
                        CreatedBy = Convert.ToInt32(reader["CreatedBy"])
                    });
                }
            }

            return list;
        }

        public void AddRecipe(Recipe recipe)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Recipes (Title, ImagePath, Instructions, Ingredients, CreatedBy) " +
                               "VALUES (@Title, @ImagePath, @Instructions, @Ingredients, @CreatedBy)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Title", recipe.Title ?? "");
                cmd.Parameters.AddWithValue("@ImagePath", recipe.ImagePath ?? "");
                cmd.Parameters.AddWithValue("@Instructions", recipe.Instructions ?? "");
                cmd.Parameters.AddWithValue("@Ingredients", recipe.Ingredients ?? "");
                cmd.Parameters.AddWithValue("@CreatedBy", recipe.CreatedBy);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateRecipe(Recipe recipe)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Recipes SET " +
                               "Title = @Title, " +
                               "ImagePath = @ImagePath, " +
                               "Instructions = @Instructions, " +
                               "Ingredients = @Ingredients " +
                               "WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Title", recipe.Title);
                cmd.Parameters.AddWithValue("@ImagePath", recipe.ImagePath);
                cmd.Parameters.AddWithValue("@Instructions", recipe.Instructions);
                cmd.Parameters.AddWithValue("@Ingredients", recipe.Ingredients);
                cmd.Parameters.AddWithValue("@Id", recipe.Id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteRecipe(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Recipes WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}