using Microsoft.EntityFrameworkCore;

namespace Important_Days___Telegram_bot
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserEventModel> userEvents { get; set; } = null!;
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=ImportantDaysDB;user=root;password=010312");
        }
    }
}
