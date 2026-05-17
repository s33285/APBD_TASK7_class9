using System.ComponentModel.DataAnnotations;

namespace APBD_9.Models
{
    public class ComponentManufacturer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Abbreviation { get; set; } = null!;

        [Required]
        [MaxLength(300)]
        public string FullName { get; set; } = null!;
        public DateOnly FoundationDate { get; set; }

        public ICollection<Component> Components { get; set; } = new List<Component>();
    }
}
