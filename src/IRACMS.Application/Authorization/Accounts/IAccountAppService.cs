using System.Threading.Tasks;
using Abp.Application.Services;
using IRACMS.Authorization.Accounts.Dto;

namespace IRACMS.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
