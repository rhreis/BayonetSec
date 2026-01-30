using BayonetSec.Application.DTOs;
using BayonetSec.Application.Interfaces;
using BayonetSec.Domain.Entities;
using BayonetSec.Domain.Exceptions;

namespace BayonetSec.Application.Services;

public class AssetService : IAssetService
{
    private readonly IAssetRepository _assetRepository;

    public AssetService(IAssetRepository assetRepository)
    {
        _assetRepository = assetRepository;
    }

    public async Task<AssetDto?> GetByIdAsync(Guid id, Guid tenantId)
    {
        var asset = await _assetRepository.GetByIdAndTenantAsync(id, tenantId);
        return asset == null ? null : MapToDto(asset);
    }

    public async Task<IEnumerable<AssetDto>> GetByProjectIdAsync(Guid projectId, Guid tenantId)
    {
        var assets = await _assetRepository.GetByProjectIdAsync(projectId);
        // Note: In a real implementation, filter by tenant through repository
        return assets.Select(MapToDto);
    }

    public async Task CreateAsync(CreateAssetDto dto, Guid tenantId)
    {
        var asset = new Asset(dto.ProjectId, dto.Name, dto.Type, dto.Description, dto.IpAddress, dto.Url);
        await _assetRepository.AddAsync(asset);
    }

    public async Task UpdateAsync(Guid id, UpdateAssetDto dto, Guid tenantId)
    {
        var asset = await _assetRepository.GetByIdAndTenantAsync(id, tenantId);
        if (asset == null)
            throw new DomainException("Asset not found");

        asset.Update(dto.Name, dto.Type, dto.Description, dto.IpAddress, dto.Url);
        await _assetRepository.UpdateAsync(asset);
    }

    public async Task DeleteAsync(Guid id, Guid tenantId)
    {
        var asset = await _assetRepository.GetByIdAndTenantAsync(id, tenantId);
        if (asset == null)
            throw new DomainException("Asset not found");

        await _assetRepository.DeleteAsync(asset);
    }

    private static AssetDto MapToDto(Asset asset)
    {
        return new AssetDto
        {
            Id = asset.Id,
            ProjectId = asset.ProjectId,
            Name = asset.Name,
            Type = asset.Type,
            Description = asset.Description,
            IpAddress = asset.IpAddress,
            Url = asset.Url,
            CreatedAt = asset.CreatedAt
        };
    }
}