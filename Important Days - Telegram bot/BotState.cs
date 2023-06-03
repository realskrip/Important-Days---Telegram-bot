using Telegram.Bot.Types;
using Telegram.Bot;

namespace Important_Days___Telegram_bot
{
    public class BotState
    {
        public async void Action(Message mes, ITelegramBotClient bot)
        {
            Info info = new Info();
            UserEvent userEvent = new UserEvent();
            InitializationEvent initializationEvent = new InitializationEvent();

            if (mes.Text != null)
            {
                if (mes.Text.ToLower().Contains("/info") || mes.Text.ToLower().Contains("/start"))
                {
                    info.ShowInfo(mes, bot);
                }
                else if (mes.Text.ToLower().Contains("/add"))
                {
                    userEvent.ShowAddEventRef(mes, bot);
                }
                else if (initializationEvent.initializationAddEvent(mes, bot) == true)
                {
                    userEvent.AddEvent(mes, bot);
                }
                else
                {
                    await bot.SendTextMessageAsync(mes.Chat.Id, "Я не знаю такой команды! Для того, чтобы просмотреть список доступных команд, введите /info");
                    return;
                }
            }
        }
    }
}
