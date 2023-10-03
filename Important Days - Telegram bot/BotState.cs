using Telegram.Bot.Types;
using Telegram.Bot;

namespace Important_Days___Telegram_bot
{
    internal class BotState
    {
        internal async Task Action(Message mes, ITelegramBotClient bot)
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
                else if (initializationEvent.initializationAddEvent(mes) == true)
                {
                    userEvent.AddEvent(mes, bot);
                }
                else if (mes.Text.ToLower().Contains("/show"))
                {
                    userEvent.ShowEvents(mes, bot);
                }
                else if (mes.Text.ToLower().Contains("/delete"))
                {
                    userEvent.ShowDeleteEventRef(mes, bot);
                }
                else if (initializationEvent.initializationDeleteEvent(mes) == true)
                {
                    userEvent.DeleteEvent(mes, bot);
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
