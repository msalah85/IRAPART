using Abp.Application.Services;
using Abp.Application.Services.Dto;
using IRACMS.MultiTenancy.Dto;

namespace IRACMS.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
