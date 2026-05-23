using Cwiczenia5.Data;
using Cwiczenia5.Dtos;
using Cwiczenia5.Models;
using Microsoft.EntityFrameworkCore;

namespace Cwiczenia5.Services;

public class PcService : IPcService
{
    private readonly AppDbContext _context;

    public PcService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<PcResponseDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.PCs
            .AsNoTracking()
            .OrderBy(pc => pc.Id)
            .Select(pc => new PcResponseDto
            {
                Id = pc.Id,
                Name = pc.Name,
                Weight = pc.Weight,
                Warranty = pc.Warranty,
                CreatedAt = pc.CreatedAt,
                Stock = pc.Stock
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<PcWithComponentsResponseDto?> GetByIdWithComponentsAsync(int id, CancellationToken cancellationToken)
    {
        var pc = await _context.PCs
            .AsNoTracking()
            .Include(pc => pc.PCComponents)
                .ThenInclude(pcComponent => pcComponent.Component)
                .ThenInclude(component => component.Manufacturer)
            .Include(pc => pc.PCComponents)
                .ThenInclude(pcComponent => pcComponent.Component)
                .ThenInclude(component => component.Type)
            .FirstOrDefaultAsync(pc => pc.Id == id, cancellationToken);

        if (pc is null)
        {
            return null;
        }

        return new PcWithComponentsResponseDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock,
            Components = pc.PCComponents
                .OrderBy(pcComponent => pcComponent.ComponentCode)
                .Select(pcComponent => new PcComponentResponseDto
                {
                    Amount = pcComponent.Amount,
                    Component = new ComponentResponseDto
                    {
                        Code = pcComponent.Component.Code,
                        Name = pcComponent.Component.Name,
                        Description = pcComponent.Component.Description,
                        Manufacturer = new ManufacturerResponseDto
                        {
                            Id = pcComponent.Component.Manufacturer.Id,
                            Abbreviation = pcComponent.Component.Manufacturer.Abbreviation,
                            FullName = pcComponent.Component.Manufacturer.FullName,
                            FoundationDate = pcComponent.Component.Manufacturer.FoundationDate
                        },
                        Type = new ComponentTypeResponseDto
                        {
                            Id = pcComponent.Component.Type.Id,
                            Abbreviation = pcComponent.Component.Type.Abbreviation,
                            Name = pcComponent.Component.Type.Name
                        }
                    }
                })
                .ToList()
        };
    }

    public async Task<PcResponseDto> CreateAsync(PcCreateRequestDto request, CancellationToken cancellationToken)
    {
        var pc = new PC
        {
            Name = request.Name!.Trim(),
            Weight = request.Weight!.Value,
            Warranty = request.Warranty!.Value,
            CreatedAt = request.CreatedAt!.Value,
            Stock = request.Stock!.Value
        };

        _context.PCs.Add(pc);
        await _context.SaveChangesAsync(cancellationToken);

        return MapToResponse(pc);
    }

    public async Task<PcResponseDto?> UpdateAsync(int id, PcUpdateRequestDto request, CancellationToken cancellationToken)
    {
        var pc = await _context.PCs.FindAsync(new object[] { id }, cancellationToken);

        if (pc is null)
        {
            return null;
        }

        pc.Name = request.Name!.Trim();
        pc.Weight = request.Weight!.Value;
        pc.Warranty = request.Warranty!.Value;
        pc.CreatedAt = request.CreatedAt!.Value;
        pc.Stock = request.Stock!.Value;

        await _context.SaveChangesAsync(cancellationToken);

        return MapToResponse(pc);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var pc = await _context.PCs.FindAsync(new object[] { id }, cancellationToken);

        if (pc is null)
        {
            return false;
        }

        _context.PCs.Remove(pc);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    private static PcResponseDto MapToResponse(PC pc)
    {
        return new PcResponseDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock
        };
    }
}
