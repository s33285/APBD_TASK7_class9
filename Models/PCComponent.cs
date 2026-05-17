using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_9.Models
{
    public class PCComponent
    {
        public int PCId { get; set; }

        [MaxLength(10)]
        [Column(TypeName = "char(10)")]
        public string ComponentCode { get; set; } = null!;

        public int Amount { get; set; }

        [ForeignKey(nameof(PCId))]
        public PC PC { get; set; } = null!;

        [ForeignKey(nameof(ComponentCode))]
        public Component Component { get; set; } = null!;
    }
}
