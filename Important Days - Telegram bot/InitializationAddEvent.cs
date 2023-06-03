using Telegram.Bot.Types;
using Telegram.Bot;

namespace Important_Days___Telegram_bot
{
    class InitializationAddEvent
    {
        public bool validationAddEvent(Message mes, ITelegramBotClient bot)
        {
            if (mes.Text != null)
            {
                char[] validationEvent = new char[4];
                string? validationEventString;
                for (int i = 0; i <= 3; i++)
                {
                    validationEvent[i] = mes.Text[i];
                }
                validationEventString = new string(validationEvent);

                if (validationEventString.ToLower().Contains("add:"))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
