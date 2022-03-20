using Microsoft.EntityFrameworkCore;

namespace Medium.Users.Database
{
    public static class DatabaseInitializator
    {
        public static void Initializat(DbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
