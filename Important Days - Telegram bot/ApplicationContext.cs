using Important_Days___Telegram_bot.Models;
using Microsoft.EntityFrameworkCore;

namespace Important_Days___Telegram_bot
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserEventModel> userEvents { get; set; } = null!;
        public ApplicationContext()
        {
            //Database.EnsureCreated();  // If the database is already created, this line will throw an exception when building.
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=ubuntu-VirtualBox;database=ImportantDaysDB;user=skrip;password=010312"); //server=localhost server=ubuntu-VirtualBox
        }
    }
}

// server=ubuntu-VirtualBox;database=ImportantDaysDB;user=skrip;password=010312
// server=localhost;database=ImportantDaysDB;user=root;password=010312