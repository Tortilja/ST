using System.ComponentModel.DataAnnotations;

namespace ST.Models
{
    public class About
    {
        [Key]
        public int About_ID { get; set; }

        public string About_Description { get; set; }

    }
}
