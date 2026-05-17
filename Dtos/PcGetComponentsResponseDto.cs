namespace APBD_9.Dtos
{
    public class PcGetComponentsResponseDto
    {
        public int PCId { get; set; }
        public string PCName { get; set; } = null!;
        public List<ComponentItemDto> Components { get; set; } = new();
    }
}
