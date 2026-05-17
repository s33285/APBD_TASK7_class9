using APBD_9.Dtos;
using APBD_9.Repositories;

namespace APBD_9.Services
{
    public class PcService : IPcService
    {
        private readonly IPcRepository _repository;

        public PcService(IPcRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<PcGetAllResponseDto>> GetAllAsync() => _repository.GetAllAsync();
        public Task<PcGetComponentsResponseDto?> GetComponentsByIdAsync(int id) => _repository.GetComponentsByIdAsync(id);
        public Task<PcCreateResponseDto> CreateAsync(PcCreateRequestDto dto) => _repository.CreateAsync(dto);
        public Task<bool> UpdateAsync(int id, PcUpdateRequestDto dto) => _repository.UpdateAsync(id, dto);
        public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}
