using System.Text.Json;
using Asklepios.Core.DTO.Clinics;
using Asklepios.Core.DTO.Users;
using Asklepios.Core.Repositories.Users;
using Microsoft.Extensions.Caching.Distributed;

namespace Asklepios.Infrastructure.Redis.Repositories.Users;

public class MedicalStaffCacheRepository : IMedicalStaffCacheRepository
{
    private readonly IDistributedCache _cache;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromDays(1);

    public MedicalStaffCacheRepository(IDistributedCache cache)
    {
        _cache = cache;
    }
    
    private string GetCacheKey() => "ClinicDoctors";
    private string GetMedicalStaffCacheKey(int pageIndex, int pageSize) 
        => $"Asklepios_MedicalStaff_{pageIndex}_{pageSize}";

    public async Task<IReadOnlyList<ClinicDoctorListDto>?> GetClinicDoctorsAsync()
    {
        var cacheKey = GetCacheKey();
        var cachedData = await _cache.GetStringAsync(cacheKey);
        return cachedData != null ? JsonSerializer.Deserialize<IReadOnlyList<ClinicDoctorListDto>>(cachedData) : null;
    }

    public async Task SetClinicDoctorsAsync(IReadOnlyList<ClinicDoctorListDto> clinicDoctors)
    {
        var cacheKey = GetCacheKey();
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = _cacheDuration
        };
        
        await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(clinicDoctors), options);
    }

    public async Task<IReadOnlyList<MedicalStaffListDto>?> GetMedicalStaffAsync(int pageIndex, int pageSize)
    {
        var cacheKey = GetMedicalStaffCacheKey(pageIndex, pageSize);
        var cachedData = await _cache.GetStringAsync(cacheKey);
        return cachedData != null ? JsonSerializer.Deserialize<IReadOnlyList<MedicalStaffListDto>>(cachedData) : null;
    }

    public async Task SetMedicalStaffAsync(int pageIndex, int pageSize, IReadOnlyList<MedicalStaffListDto> medicalStaffList)
    {
        var cacheKey = GetMedicalStaffCacheKey(pageIndex, pageSize);
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = _cacheDuration
        };

        await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(medicalStaffList), options);
    }
}