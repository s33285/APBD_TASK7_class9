using APBD_9.Dtos;

namespace APBD_9.Repositories
{
    public interface IPcRepository
    {
        Task<IEnumerable<PcGetAllResponseDto>> GetAllAsync();
        Task<PcGetComponentsResponseDto?> GetComponentsByIdAsync(int id);
        Task<PcCreateResponseDto> CreateAsync(PcCreateRequestDto dto);
        Task<bool> UpdateAsync(int id, PcUpdateRequestDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
