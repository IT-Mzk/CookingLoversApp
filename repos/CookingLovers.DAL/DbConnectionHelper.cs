using System.Data.SqlClient;

namespace CookingLovers.DAL
{
    public static class DbConnectionHelper
    {
        public static string ConnectionString =>
            "Server=localhost;Database=CookingLoversDB;Trusted_Connection=True;";
        // Nếu dùng SQL Server Auth:
        // "Server=localhost;Database=CookingLoversDB;User Id=sa;Password=yourpassword;"
    }
}