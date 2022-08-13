using System.ComponentModel.DataAnnotations;

namespace XSSDemo.Models
{
    public class PersonInfo
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Bio { get; set; }
    }
}
