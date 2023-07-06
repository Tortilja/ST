using System.ComponentModel.DataAnnotations;

namespace ST.Models
{
    public class CompetitionRegistration
    {
        [Key]
        public int Competition_Registration_ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Phone_Number { get; set; }
        public string Email { get; set; }
    }
}
