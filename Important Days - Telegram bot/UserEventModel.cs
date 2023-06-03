using System.ComponentModel.DataAnnotations;

namespace Important_Days___Telegram_bot
{
    public class UserEventModel
    {
        [Key]
        public Guid eventId { get; set; }
        public long userId { get; set; }
        public string? eventName { get; set; }
        public DateTime? eventDate { get; set; }
    }
}
