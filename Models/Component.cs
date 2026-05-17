using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_9.Models
{
    public class Component
    {
        [Key]
        [MaxLength(10)]
        [Column(TypeName = "char(10)")]
        public string Code { get; set; } = null!;

        [Required]
        [MaxLength(300)]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int ComponentManufacturersId { get; set; }

        [ForeignKey(nameof(ComponentManufacturersId))]
        public ComponentManufacturer ComponentManufacturer { get; set; } = null!;

        public int ComponentTypesId { get; set; }

        [ForeignKey(nameof(ComponentTypesId))]
        public ComponentType ComponentType { get; set; } = null!;
        public ICollection<PCComponent> PCComponents { get; set; } = new List<PCComponent>();
    }
}
