namespace CookingLovers.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public string Instructions { get; set; }     
        public string Ingredients { get; set; }      
        public int CreatedBy { get; set; }
    }
}