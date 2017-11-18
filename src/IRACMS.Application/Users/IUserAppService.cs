using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using IRACMS.Roles.Dto;
using IRACMS.Users.Dto;

namespace IRACMS.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();
    }
}