using Telegram.Bot;
using Telegram.Bot.Types;

namespace Important_Days___Telegram_bot
{
    internal class Program
    {
        static void Main()
        {
            var client = new TelegramBotClient("6195754339:AAHYpY4OlXU-peB0q8EHwJm-d5un732nUEk");

            client.StartReceiving(Update, Error);
            Console.ReadLine();
        }

        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            Info info = new Info();

            var message = update.Message;

            if (message.Text != null)
            {
                info.ShowInfo(message, botClient);
            }
        }

        private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }
    }
}