using Important_Days___Telegram_bot.Models;
using Telegram.Bot;

namespace Important_Days___Telegram_bot
{
    static class Alerts
    {
        internal static void ShowAlerts()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<UserEventModel> userEvents = db.userEvents.OrderBy(d => d.eventDate).ToList();

                foreach (var item in userEvents)
                {
                    DateTime weekBefore = item.eventDate.Value.AddDays(-7);
                    DateTime threeDaysBefore = item.eventDate.Value.AddDays(-3);

                    if (Program.botClient != null)
                    {
                        if (weekBefore <= DateTime.Now && item.numberRemainingAlerts == 3)
                        {
                            Program.botClient.SendTextMessageAsync(item.userId, "Через 7 дней: " + item.eventName);

                            item.numberRemainingAlerts--;
                            db.userEvents.Update(item);
                            db.SaveChanges();
                        }
                        else if (threeDaysBefore <= DateTime.Now && item.numberRemainingAlerts == 2)
                        {
                            Program.botClient.SendTextMessageAsync(item.userId, "Через 3 дня: " + item.eventName);

                            item.numberRemainingAlerts--;
                            db.userEvents.Update(item);
                            db.SaveChanges();
                        }
                        else if (item.eventDate <= DateTime.Now && item.numberRemainingAlerts == 1)
                        {
                            Program.botClient.SendTextMessageAsync(item.userId, "Сегодня: " + item.eventName);

                            db.userEvents.Remove(item);
                            db.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}
