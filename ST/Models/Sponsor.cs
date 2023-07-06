using System.ComponentModel.DataAnnotations;

namespace ST.Models
{
    public class Sponsor
    {
        [Key]
        public int Sponsor_ID { get; set; }
        public string Sponsor_Name { get; set; }
        public string? Sponsor_Image { get; set; }
    }
}
