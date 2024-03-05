using System.ComponentModel.DataAnnotations;

namespace Cafe.Models
{
    public class About
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
