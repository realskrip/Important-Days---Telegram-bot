using Important_Days___Telegram_bot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Timer = System.Timers.Timer;

namespace Important_Days___Telegram_bot
{
    class Program
    {
        public static ITelegramBotClient? botClient;
        public static Timer timer = new Timer(3600000);

        static void Main()
        {
            TelegramBotClient client = new TelegramBotClient(Config.token);

            client.StartReceiving(Update, Error);
            botClient = client;

            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;
            timer.AutoReset = true;
            timer.Start();

            Console.ReadLine();
        }

        private static void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            Alerts();
        }

        private static void Alerts()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<UserEventModel> userEvents = db.userEvents.OrderBy(d => d.eventDate).ToList();
                
                foreach (var item in userEvents)
                {
                    DateTime weekBefore = item.eventDate.Value.AddDays(-7);
                    DateTime threeDaysBefore = item.eventDate.Value.AddDays(-3);

                    if (botClient != null)
                    {
                        if (weekBefore <= DateTime.Now && item.numberRemainingAlerts == 3)
                        {
                            botClient.SendTextMessageAsync(item.userId, "Через 7 дней " + item.eventName);

                            item.numberRemainingAlerts--;
                            db.userEvents.Update(item);
                            db.SaveChanges();
                        }
                        else if (threeDaysBefore <= DateTime.Now && item.numberRemainingAlerts == 2)
                        {
                            botClient.SendTextMessageAsync(item.userId, "Через 3 дней " + item.eventName);

                            item.numberRemainingAlerts--;
                            db.userEvents.Update(item);
                            db.SaveChanges();
                        }
                        else if (item.eventDate <= DateTime.Now && item.numberRemainingAlerts == 1)
                        {
                            botClient.SendTextMessageAsync(item.userId, "Сегодня " + item.eventName);

                            db.userEvents.Remove(item);
                            db.SaveChanges();
                        }
                    }
                }
            }
        }

        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            BotState action = new BotState();

            var message = update.Message;

            if (message != null && message.Text != null)
            {
                action.Action(message, botClient);
            }
        }

        private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }
    }
}