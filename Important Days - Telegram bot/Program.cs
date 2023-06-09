using Telegram.Bot;
using Telegram.Bot.Types;
using Timer = System.Timers.Timer;

namespace Important_Days___Telegram_bot
{
    internal class Program
    {
        internal static ITelegramBotClient? botClient;
        private static Timer timer = new Timer(1000);

        static void Main()
        {
            TelegramBotClient client = new TelegramBotClient(Config.token);

            client.StartReceiving(Update, Error);
            botClient = client;

            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;
            timer.AutoReset = true;
            timer.Start();

            Console.WriteLine("Important Days - Telegram bot 0.7.4 start  " + DateTime.Now);
            Console.ReadLine();
        }

        private static void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            Alerts.ShowAlerts();
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