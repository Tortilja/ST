using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ST.Models
{
    public class ImageGallery
    {
        [Key]
        public int Image_ID { get; set; }
        public string? Image_Name { get; set; }
        [Display(Name = "Event")]
        public int Event_FK_ID { get; set; }
        [ForeignKey("Event_FK_ID")]
        public virtual Event? Event { get; set; }
    }
}
