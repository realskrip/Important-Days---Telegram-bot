using Telegram.Bot.Types;
using Telegram.Bot;

namespace Important_Days___Telegram_bot
{
    public class UserEvent
    {
        public async void ShowAddEventRef(Message mes, ITelegramBotClient bot)
        {
            if (mes.Text != null)
            {
                if (mes.Text.ToLower().Contains("/add"))
                {
                    await bot.SendTextMessageAsync(mes.Chat.Id, "Введите имя и дату события в формате\n" +
                        "Собыие: add:Название события\n" +
                        "Дата события: дд.мм.гггг\n" +
                        "\n" +
                        "Например:\n" +
                        "add:Поздравить кошку с её днем рождения!\n" +
                        "29.12.2024");
                    return;
                }
            }
        }

        public async void ParseEvent(Message mes, ITelegramBotClient bot)
        {
            DateTime eventDate;
            DateTime today = DateTime.Today;

            string eventName;
            string eventDateString;
            string? userEvent;
            userEvent = mes.Text;

            char[] date;
            char[] name;

            UserEventModel eventModel = new UserEventModel();

            if (userEvent != null)
            {
                date = new char[10];
                name = new char[userEvent.Length - 15]; // 15 = date size (10) + \n (1) + add: (4)

                int stringIndex = userEvent.Length - 1;
                int dateArrIndex = date.Length - 1;
                int nameArrIndex = name.Length - 1;

                // parse date
                try
                {
                    while (userEvent[stringIndex] != '\n')
                    {
                        date[dateArrIndex] = userEvent[stringIndex];
                        dateArrIndex--;
                        stringIndex--;
                    }
                    stringIndex--;
                    eventDateString = new string(date);

                    // Event date validation
                    try
                    {
                        if (DateTime.TryParse(eventDateString, out eventDate) == false)
                        {
                            await bot.SendTextMessageAsync(mes.Chat.Id, "Дата введена некорректно! Попробуйте еще раз.");
                            return;
                        }

                        if (eventDate < today)
                        {
                            await bot.SendTextMessageAsync(mes.Chat.Id, "Дата введена некорректно! Попробуйте еще раз.");
                            return;
                        }
                    }
                    catch
                    {
                        await bot.SendTextMessageAsync(mes.Chat.Id, "Дата введена некорректно! Попробуйте еще раз.");
                        return;
                    }
                }
                catch
                {
                    await bot.SendTextMessageAsync(mes.Chat.Id, "Дата введена некорректно! Попробуйте еще раз.");
                    return;
                }

                // parse name
                while (stringIndex > 3)
                {
                    name[nameArrIndex] = userEvent[stringIndex];
                    nameArrIndex--;
                    stringIndex--;
                }
                eventName = new string(name);

                // Event name validation
                if (eventName.Length <= 0)
                {
                    await bot.SendTextMessageAsync(mes.Chat.Id, "Вы не ввели имя события! Попробуйте еще раз.");
                }

                // Writing event to the database
                eventModel.userId = mes.Chat.Id;
                eventModel.eventName = eventName;
                eventModel.eventDate = eventDate;

                using (ApplicationContext db = new ApplicationContext())
                {
                    db.userEvents.Add(eventModel);
                    db.SaveChanges();
                }

                await bot.SendTextMessageAsync(mes.Chat.Id, "Событие добавлено!");
            }

            return;
        }
    }
}
