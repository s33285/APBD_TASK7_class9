using System.ComponentModel.DataAnnotations;

namespace APBD_9.Dtos
{
    public class PcCreateRequestDto
    {

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    public float Weight { get; set; }
    public int Warranty { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Stock { get; set; }

    }
}
