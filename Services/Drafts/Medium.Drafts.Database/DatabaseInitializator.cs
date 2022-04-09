using Microsoft.EntityFrameworkCore;

namespace Medium.Drafts.Database
{
    public static class DatabaseInitializator
    {
        public static void Initializat(DbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
