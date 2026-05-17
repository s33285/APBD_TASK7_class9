namespace APBD_9.Dtos
{
    public class ComponentItemDto
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int Amount { get; set; }
        public string ComponentType { get; set; } = null!;
        public string Manufacturer { get; set; } = null!;
    }
}
