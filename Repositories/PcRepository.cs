using APBD_9.Data;
using APBD_9.Dtos;
using APBD_9.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_9.Repositories
{
    public class PcRepository : IPcRepository
    {
        private readonly AppDbContext _context;

        public PcRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PcGetAllResponseDto>> GetAllAsync()
        {
            return await _context.PCs
                .AsNoTracking()
                .Select(p => new PcGetAllResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Weight = p.Weight,
                    Warranty = p.Warranty,
                    CreatedAt = p.CreatedAt,
                    Stock = p.Stock
                })
                .ToListAsync();
        }

        public async Task<PcGetComponentsResponseDto?> GetComponentsByIdAsync(int id)
        {
            var pc = await _context.PCs
                .AsNoTracking()
                .Include(p => p.PCComponents).ThenInclude(pcc => pcc.Component).ThenInclude(c => c.ComponentType)
                .Include(p => p.PCComponents).ThenInclude(pcc => pcc.Component).ThenInclude(c => c.ComponentManufacturer)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pc is null)
                return null;

            return new PcGetComponentsResponseDto
            {
                PCId = pc.Id,
                PCName = pc.Name,
                Components = pc.PCComponents.Select(pcc => new ComponentItemDto
                {
                    Code = pcc.ComponentCode,
                    Name = pcc.Component.Name,
                    Description = pcc.Component.Description,
                    Amount = pcc.Amount,
                    ComponentType = pcc.Component.ComponentType.Name,
                    Manufacturer = pcc.Component.ComponentManufacturer.FullName
                }).ToList()
            };
        }

        public async Task<PcCreateResponseDto> CreateAsync(PcCreateRequestDto dto)
        {
            var pc = new PC
            {
                Name = dto.Name,
                Weight = dto.Weight,
                Warranty = dto.Warranty,
                CreatedAt = dto.CreatedAt,
                Stock = dto.Stock
            };

            await _context.PCs.AddAsync(pc);
            await _context.SaveChangesAsync();

            return new PcCreateResponseDto
            {
                Id = pc.Id,
                Name = pc.Name,
                Weight = pc.Weight,
                Warranty = pc.Warranty,
                CreatedAt = pc.CreatedAt,
                Stock = pc.Stock
            };
        }

        public async Task<bool> UpdateAsync(int id, PcUpdateRequestDto dto)
        {
            var pc = await _context.PCs.FindAsync(id);

            if (pc is null)
                return false;

            pc.Name = dto.Name;
            pc.Weight = dto.Weight;
            pc.Warranty = dto.Warranty;
            pc.CreatedAt = dto.CreatedAt;
            pc.Stock = dto.Stock;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var pc = await _context.PCs
                .Include(p => p.PCComponents)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pc is null)
                return false;

            _context.PCs.Remove(pc);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
