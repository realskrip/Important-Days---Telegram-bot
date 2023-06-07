using Telegram.Bot.Types;
using Telegram.Bot;
using Important_Days___Telegram_bot.Models;

namespace Important_Days___Telegram_bot
{
    public class UserEvent
    {
        DateTime eventDate;
        DateTime today;
        string? eventName;
        string? eventDateString;
        string? userEvent;
        char[]? date;
        char[]? name;

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

        public async void AddEvent(Message mes, ITelegramBotClient bot)
        {
            today = DateTime.Today;
            userEvent = mes.Text;

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
                    return;
                }

                // Writing event to the database
                eventModel.userId = mes.Chat.Id;
                eventModel.eventName = eventName;
                eventModel.eventDate = eventDate;
                eventModel.numberRemainingAlerts = 3;

                using (ApplicationContext db = new ApplicationContext())
                {
                    db.userEvents.Add(eventModel);
                    db.SaveChanges();
                }

                await bot.SendTextMessageAsync(mes.Chat.Id, "Событие добавлено!");
            }

            return;
        }

        public async void ShowEvents(Message mes, ITelegramBotClient bot)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<UserEventModel> userEvents = db.userEvents.Where(e => e.userId ==  mes.Chat.Id).ToList();
                string outputEvents = "Ваши события\n";

                if (userEvents != null)
                {
                    if (userEvents.Count <= 0)
                    {
                        await bot.SendTextMessageAsync(mes.Chat.Id, "Вы еще не добавляли событий!");
                        return;
                    }
                    else
                    {
                        for (int i = 0; i < userEvents.Count; i++)
                        {
                            outputEvents += i + 1 + ". " + userEvents[i].eventName + "  " + $"{userEvents[i].eventDate:d}" + "\n";
                        }

                        await bot.SendTextMessageAsync(mes.Chat.Id, outputEvents);
                        return;
                    }
                }
            }
        }

        public async void ShowDeleteEventRef(Message mes, ITelegramBotClient bot)
        {
            if (mes.Text != null)
            {
                if (mes.Text.ToLower().Contains("/delete"))
                {
                    await bot.SendTextMessageAsync(mes.Chat.Id, "Для того, чтобы удалить событие, введите:\n" +
                        "delete:номер события из вашего списка /show\n" +
                        "\n" +
                        "Например:\n" +
                        "delete:12\n");
                    return;
                }
            }
        }

        public async void DeleteEvent(Message mes, ITelegramBotClient bot)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                string eventNumberString;
                int eventNumber;

                char[] trim = new char[] { 'd', 'e', 'l', 'e', 't', 'e', ':' };
                List<UserEventModel> userEvents = db.userEvents.Where(e => e.userId == mes.Chat.Id).ToList();

                if (userEvents != null)
                {
                    if (userEvents.Count <= 0)
                    {
                        await bot.SendTextMessageAsync(mes.Chat.Id, "Вы еще не добавляли событий!");
                        return;
                    }
                    else
                    {
                        if (mes.Text != null)
                        {
                            eventNumberString = mes.Text;
                            eventNumberString = eventNumberString.TrimStart(trim);

                            if (int.TryParse(eventNumberString, out eventNumber) == false)
                            {
                                await bot.SendTextMessageAsync(mes.Chat.Id, "Номер события введен некорректно! Попробуйте еще раз.");
                                return;
                            }
                            else
                            {
                                if (userEvents.Count < eventNumber)
                                {
                                    await bot.SendTextMessageAsync(mes.Chat.Id, "Номер события введен некорректно! Попробуйте еще раз.");
                                    return;
                                }

                                db.userEvents.Remove(userEvents[eventNumber - 1]);
                                db.SaveChanges();

                                await bot.SendTextMessageAsync(mes.Chat.Id, "Событие удалено.");
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}