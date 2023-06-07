using Telegram.Bot.Types;
using Telegram.Bot;

namespace Important_Days___Telegram_bot
{
    internal class InitializationEvent
    {
        internal bool initializationAddEvent(Message mes)
        {
            if (mes.Text != null)
            {
                char[] initializationEvent = new char[4];
                string? initializationEventString;
                for (int i = 0; i <= 3; i++)
                {
                    initializationEvent[i] = mes.Text[i];
                }
                initializationEventString = new string(initializationEvent);

                if (initializationEventString.ToLower().Contains("add:"))
                {
                    return true;
                }
            }
            return false;
        }

        internal bool initializationDeleteEvent(Message mes)
        {
            if (mes.Text != null)
            {
                char[] initializationEvent = new char[7];
                string? initializationEventString;

                for (int i = 0; i <= 6; i++)
                {
                    initializationEvent[i] = mes.Text[i];
                }
                initializationEventString = new string(initializationEvent);

                if (initializationEventString.ToLower().Contains("delete:"))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
