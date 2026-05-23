using Cwiczenia5.Dtos;

namespace Cwiczenia5.Services;

public interface IPcService
{
    Task<IReadOnlyList<PcResponseDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<PcWithComponentsResponseDto?> GetByIdWithComponentsAsync(int id, CancellationToken cancellationToken);
    Task<PcResponseDto> CreateAsync(PcCreateRequestDto request, CancellationToken cancellationToken);
    Task<PcResponseDto?> UpdateAsync(int id, PcUpdateRequestDto request, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}
