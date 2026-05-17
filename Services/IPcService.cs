using APBD_9.Dtos;

namespace APBD_9.Services
{
    public interface IPcService
    {
        Task<IEnumerable<PcGetAllResponseDto>> GetAllAsync();
        Task<PcGetComponentsResponseDto?> GetComponentsByIdAsync(int id);
        Task<PcCreateResponseDto> CreateAsync(PcCreateRequestDto dto);
        Task<bool> UpdateAsync(int id, PcUpdateRequestDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
