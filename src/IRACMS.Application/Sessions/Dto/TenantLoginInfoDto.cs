using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using IRACMS.MultiTenancy;

namespace IRACMS.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}