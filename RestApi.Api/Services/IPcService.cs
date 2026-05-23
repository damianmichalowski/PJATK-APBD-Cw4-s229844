using RestApi.Api.DTOs;

namespace RestApi.Api.Services;

public interface IPcService
{
    Task<List<PcListItemDto>> GetAllAsync();
    Task<PcDetailsDto?> GetWithComponentsAsync(int id);
    Task<PcListItemDto> CreateAsync(CreatePcDto dto);
    Task<PcListItemDto?> UpdateAsync(int id, UpdatePcDto dto);
    Task<bool> DeleteAsync(int id);
}
