using System.ComponentModel.DataAnnotations;

namespace ST.Models
{
    public class Event
    {
        [Key]
        public int Event_ID { get; set; }
        public string Event_Title { get; set; }
        public string Event_ShortDescription { get; set; }
        public string Event_Description { get; set; }

    }
}
