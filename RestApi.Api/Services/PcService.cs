using Microsoft.EntityFrameworkCore;
using RestApi.Api.Data;
using RestApi.Api.DTOs;
using RestApi.Api.Models;

namespace RestApi.Api.Services;

public class PcService : IPcService
{
    private readonly AppDbContext _context;

    public PcService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<PcListItemDto>> GetAllAsync()
    {
        return await _context.PCs
            .Select(p => new PcListItemDto
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

    public async Task<PcDetailsDto?> GetWithComponentsAsync(int id)
    {
        var pc = await _context.PCs
            .Include(p => p.PCComponents)
                .ThenInclude(pc => pc.Component)
                    .ThenInclude(c => c.ComponentManufacturer)
            .Include(p => p.PCComponents)
                .ThenInclude(pc => pc.Component)
                    .ThenInclude(c => c.ComponentType)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pc is null)
        {
            return null;
        }

        return new PcDetailsDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock,
            Components = pc.PCComponents.Select(pc => new PcComponentDto
            {
                Amount = pc.Amount,
                Component = new ComponentDto
                {
                    Code = pc.Component.Code,
                    Name = pc.Component.Name,
                    Description = pc.Component.Description,
                    Manufacturer = new ManufacturerDto
                    {
                        Id = pc.Component.ComponentManufacturer.Id,
                        Abbreviation = pc.Component.ComponentManufacturer.Abbreviation,
                        FullName = pc.Component.ComponentManufacturer.FullName,
                        FoundationDate = pc.Component.ComponentManufacturer.FoundationDate
                    },
                    Type = new ComponentTypeDto
                    {
                        Id = pc.Component.ComponentType.Id,
                        Abbreviation = pc.Component.ComponentType.Abbreviation,
                        Name = pc.Component.ComponentType.Name
                    }
                }
            }).ToList()
        };
    }

    public async Task<PcListItemDto> CreateAsync(CreatePcDto dto)
    {
        var pc = new PC
        {
            Name = dto.Name,
            Weight = dto.Weight,
            Warranty = dto.Warranty,
            CreatedAt = dto.CreatedAt,
            Stock = dto.Stock
        };

        _context.PCs.Add(pc);
        await _context.SaveChangesAsync();

        return new PcListItemDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock
        };
    }

    public async Task<PcListItemDto?> UpdateAsync(int id, UpdatePcDto dto)
    {
        var pc = await _context.PCs.FindAsync(id);

        if (pc is null)
        {
            return null;
        }

        pc.Name = dto.Name;
        pc.Weight = dto.Weight;
        pc.Warranty = dto.Warranty;
        pc.CreatedAt = dto.CreatedAt;
        pc.Stock = dto.Stock;

        await _context.SaveChangesAsync();

        return new PcListItemDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var pc = await _context.PCs.FindAsync(id);

        if (pc is null)
        {
            return false;
        }

        _context.PCs.Remove(pc);
        await _context.SaveChangesAsync();

        return true;
    }
}
