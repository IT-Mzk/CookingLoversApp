using System;

namespace CookingLovers.Models
{
    public class ShoppingListItem
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public string ItemName { get; set; }
        public bool IsChecked { get; set; }
    }
}