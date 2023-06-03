using Telegram.Bot;
using Telegram.Bot.Types;

namespace Important_Days___Telegram_bot
{
    class Program
    {
        static void Main()
        {
            var client = new TelegramBotClient("6195754339:AAHYpY4OlXU-peB0q8EHwJm-d5un732nUEk");

            client.StartReceiving(Update, Error);
            Console.ReadLine();
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