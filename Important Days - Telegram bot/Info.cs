﻿using Telegram.Bot.Types;
using Telegram.Bot;

namespace Important_Days___Telegram_bot
{
    public class Info
    {
        public async void ShowInfo(Message mes, ITelegramBotClient bot)
        {
            if (mes.Text != null)
            {
                if (mes.Text.ToLower().Contains("/info") || mes.Text.ToLower().Contains("/start"))
                {
                    await bot.SendTextMessageAsync(mes.Chat.Id, "Список команд:\n" +
                        "1. /info - показать список команд;");
                    return;
                }
            }
        }
    }
}