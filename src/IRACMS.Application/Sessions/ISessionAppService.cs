using System.Threading.Tasks;
using Abp.Application.Services;
using IRACMS.Sessions.Dto;

namespace IRACMS.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
