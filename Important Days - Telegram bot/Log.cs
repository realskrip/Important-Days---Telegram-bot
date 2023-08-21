using Important_Days___Telegram_bot.Models;

namespace Important_Days___Telegram_bot
{
    internal class Log
    {
        private static int workingHours = 0;
        private static int downTime = 0;
        internal static void PrintLogSpy(UserEventModel eventModel)
        {
            using (StreamWriter log = new StreamWriter("log.txt", true))
            {
                log.WriteLine(DateTime.Now + "\t" + "SPY\t" +  eventModel.eventName);
                Console.WriteLine(DateTime.Now + "\t" + "SPY\t" + eventModel.eventName);
            }
        }

        internal static void PrintLogStartInfo()
        {
            using (StreamWriter log = new StreamWriter("log.txt", true))
            {
                log.WriteLine(DateTime.Now + "\t" + "INFO\t" + "Important Days - Telegram bot 0.8.1 start");
                Console.WriteLine(DateTime.Now + "\t" + "INFO\t" + "Important Days - Telegram bot 0.8.1 start");
            }
        }

        internal static void PrintLogWorkingHours()
        {
            using (StreamWriter log = new StreamWriter("log.txt", true))
            {
                log.WriteLine(DateTime.Now + "\t" + "WORKINGTIME\t" + "Working hours:\t" + workingHours);
                Console.WriteLine(DateTime.Now + "\t" + "WORKINGTIME\t" + "Working hours:\t" + workingHours);
            }
            workingHours++;
        }

        internal static void PrintLogDowntime()
        {
            using (StreamWriter log = new StreamWriter("log.txt", true))
            {
                log.WriteLine(DateTime.Now + "\t" + "DOWNTIME\t" + "Downtime:\t" + downTime);
                Console.WriteLine(DateTime.Now + "\t" + "DOWNTIME\t" + "Downtime:\t" + downTime);
            }
            downTime++;
        }

        internal static void ResetDowntime()
        {
            using (StreamWriter log = new StreamWriter("log.txt", true))
            {
                log.WriteLine(DateTime.Now + "\t" + "DEBUG\t" + "Reset downtime, message delivered");
                Console.WriteLine(DateTime.Now + "\t" + "DEBUG\t" + "Reset downtime, message delivered");
            }
            downTime = 0;
        }
    }
}
