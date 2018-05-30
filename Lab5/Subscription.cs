using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace Lab5
{
    public class Subscription
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string username { get; set; }
    }
}