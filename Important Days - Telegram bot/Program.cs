using Telegram.Bot;
using Telegram.Bot.Types;
using Timer = System.Timers.Timer;

namespace Important_Days___Telegram_bot
{
    internal class Program
    {
        internal static ITelegramBotClient? botClient;
        private static Timer timer = new Timer(3600000); //3600000

        static void Main()
        {
            TelegramBotClient client = new TelegramBotClient(Config.token);

            client.StartReceiving(Update, Error);
            botClient = client;

            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;
            timer.AutoReset = true;
            timer.Start();

            Log.PrintLogStartInfo();
            Log.PrintLogWorkingHours();
            Log.PrintLogDowntime();
            Console.ReadLine();
        }

        private static void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            Alerts.ShowAlerts();
            //Log.SuperUserMessage(botClient);
            Log.PrintLogWorkingHours();
            Log.PrintLogDowntime();
        }

        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            try
            {
                BotState action = new BotState();

                var message = update.Message;

                if (message != null && message.Text != null)
                {
                    Log.DebugResponse(message);
                    await action.Action(message, botClient);
                    //Log.ResetDowntime();
                }
            }
            catch (Exception ex)
            {
                Log.DebugUpdate(ex);
                //throw; // Переводит в метод Error. Но после того как отрабатывает метод Error, бот не крашится, но и больше не отвечат!
                // Если ловить исключение в try catch и не давать переходить в метод Error, то бот работает исправно.
            }
        }

        private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            Log.DebugError(arg2);

            throw new NotImplementedException(); // Без этой строки не все пути к коду возращают значения.
        }
    }
}