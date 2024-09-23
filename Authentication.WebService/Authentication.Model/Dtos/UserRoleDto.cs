using Infrastructure.Model.Dtos.Implementations;
using Infrastructure.Model.Dtos.Interfaces;

namespace Authentication.Model.Dtos
{
    public class UserRoleDto
    {
        public class UserRoleGetDto : BaseDto<long>
        {
            public long UserId { get; set; }
            public string UserFullName { get; set; } = string.Empty;
            public int RoleId { get; set; }
            public string RoleName { get; set; } = string.Empty;
        }

        public class UserRolePostAndPutDto : IDto
        {
            public long UserId { get; set; }
            public List<int> RoleIdList { get; set; } = new List<int>();
        }

        public class UserRoleFilterDto : BasePaginateFilterDto
        {
            public long? UserId { get; set; }
            public int? RoleId { get; set; }
        }
    }
}
